using CommunityToolkit.Mvvm.ComponentModel;
using LaserTag.Host.Dtos;
using LaserTag.Host.Frame;
using LaserTag.Host.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using WebSocketSharp.Server;

namespace LaserTag.Host.Logic
{
    public partial class GameManager : ObservableObject
    {
        #region Singleton
        private static GameManager _instance;

        private GameManager()
        {
            //InitData();
            InitDataAttribute();
            InitDataUpgrade();
            InitDataGameConfig();

            //InitSamplePlayer();
        }

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        #endregion

        #region Websocket

        private WebSocketServer _wssv;

        [ObservableProperty]
        private string ipAddress;
        public void StartWebSocketServer()
        {
            try
            {
                // Check if WebSocketServer is already initialized
                if (_wssv == null)
                {
                    IpAddress = GetLocalIPv4();
                    _wssv = new WebSocketServer($"ws://{IpAddress}:8080");
                    _wssv.AddWebSocketService<PlayerClientSession>("/LaserTag");
                    _wssv.Start();
                }
                else
                {
                    // WebSocketServer is already running, do nothing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
                // Handle exception if necessary
            }
        }

        private string GetLocalIPv4()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address.ToString();
            }
        }
        #endregion

        #region Game logic

        #region Player Operations
        [ObservableProperty]
        private ObservableCollection<Player> team1Players = [];

        [ObservableProperty]
        private ObservableCollection<Player> team2Players = [];

        [ObservableProperty]
        private ObservableCollection<Player> team3Players = [];

        [ObservableProperty]
        private ObservableCollection<Player> team4Players = [];

        [ObservableProperty]
        private ObservableCollection<Player> allPlayers = [];

        public Dictionary<string, PlayerClientSession> PlayerClients = [];


        public void AddPlayer(PlayerClientSession playerClientSession)
        {
            PlayerClients[playerClientSession.ID] = playerClientSession;

            Application.Current.Dispatcher.Invoke(() =>
            {
                Team1Players.Add(playerClientSession.Player);
                AllPlayers.Add(playerClientSession.Player);
            });

            NotifyAllPlayerInfo("Player " + playerClientSession.Player.Name + " Joined the room!!");

        }

        public void RemovePlayer(Player player)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AllPlayers.Remove(player);
                PlayerClients.Remove(player.ConnectionId);
                // Remove from corresponding team
                Team1Players.Remove(player);
                Team2Players.Remove(player);
                Team3Players.Remove(player);
                Team4Players.Remove(player);
            });
            
        }
        public int GetPlayerTeam(Player player)
        {
            if (Team1Players.Contains(player)) return 1;
            if (Team2Players.Contains(player)) return 2;
            if (Team3Players.Contains(player)) return 3;
            if (Team4Players.Contains(player)) return 4;
            return -1; // Player not found in any team
        }
        public void NotifyAllPlayer(string data)
        {
            foreach (var playerClient in PlayerClients)
            {
                playerClient.Value.SendData(data);
            }
        }

        public void NotifyAllPlayerExcept(string data, params string[] excludedKeys)
        {
            // Convert the array of excluded keys to a HashSet for fast lookup
            var excludedKeySet = new HashSet<string>(excludedKeys);

            foreach (var playerClient in PlayerClients)
            {
                // Check if the player's key (ID) is not in the excluded list
                if (!excludedKeySet.Contains(playerClient.Key))
                {
                    playerClient.Value.SendData(data);
                }
            }
        }

        public void NotifyAllPlayerInfo(string message)
        {
            var dataFrame = new HostFrameDataBuilder<Object>()
                       .SetActionCode(HostActionCode.GameMessage)
                       .SetMessageType(MessageType.Info)
                       .SetMessage(message)
                       .Build();
            var data = JsonConvert.SerializeObject(dataFrame, Formatting.Indented);
            NotifyAllPlayer(data);
        }
        #endregion

        #region Upgrade & player attribute logic

        [ObservableProperty]
        private ObservableCollection<Config> defaultPlayerAttribute = [];

        [ObservableProperty]
        private ObservableCollection<GameAttribute> gameAttributes = [];

        [ObservableProperty]
        private ObservableCollection<Upgrade> upgrades = [];

        [ObservableProperty]
        private ObservableCollection<UpgradeAttribute> upgradeAttributes = [];

        [ObservableProperty]
        private ObservableCollection<PlayerUpgrades> playerUpgrades = [];

        private Dictionary<int, Dictionary<string, int>> playerUpgradeCache = new Dictionary<int, Dictionary<string, int>>();

        public void PlayerBuyUpgrade(int playerId, int upgradeId)
        {
            var player = AllPlayers.FirstOrDefault(p => p.Id == playerId);
            var upgrade = Upgrades.FirstOrDefault(u => u.Id == upgradeId);

            if (player == null || upgrade == null)
            {
                throw new ArgumentException("Player or Upgrade not found.");
            }

            if (player.Credit < upgrade.Cost)
            {
                throw new InvalidOperationException("Insufficient funds to buy the upgrade.");
            }

            var playerUpgrade = new PlayerUpgrades
            {
                Player = player,
                Upgrade = upgrade
            };

            PlayerUpgrades.Add(playerUpgrade);
            player.Credit -= upgrade.Cost;

            // Update the upgrade cache
            if (!playerUpgradeCache.ContainsKey(playerId))
            {
                playerUpgradeCache[playerId] = new Dictionary<string, int>();
            }

            foreach (var attribute in upgrade.Attributes)
            {
                if (!playerUpgradeCache[playerId].ContainsKey(attribute.GameAttribute.CodeName))
                {
                    playerUpgradeCache[playerId][attribute.GameAttribute.CodeName] = 0;
                }
                playerUpgradeCache[playerId][attribute.GameAttribute.CodeName] += attribute.Value;
            };
            player.Upgrades.Add(upgrade);
        }

        public void AssignPlayerAttributeAfterUpgrades()
        {
            foreach (var player in AllPlayers)
            {
                Dictionary<string, int> playerUpgrades = new Dictionary<string, int>();
                if (playerUpgradeCache.ContainsKey(player.Id))
                {
                    playerUpgrades = playerUpgradeCache[player.Id];
                }

                foreach (var attribute in GameAttributes)
                {
                    // Get the default value from Config
                    var defaultConfig = DefaultPlayerAttribute.FirstOrDefault(c => c.CodeName == attribute.CodeName);
                    int baseValue = defaultConfig != null ? int.Parse(defaultConfig.Value1) : 0;

                    // Get the upgrade value from the cache
                    int upgradeValue = playerUpgrades.ContainsKey(attribute.CodeName) ? playerUpgrades[attribute.CodeName] : 0;

                    // Calculate total value
                    int totalValue = baseValue + upgradeValue;

                    // Set or update the player's attribute
                    var existingAttribute = player.PlayerAttributes.FirstOrDefault(pa => pa.GameAttribute.Id == attribute.Id);
                    if (existingAttribute != null)
                    {
                        existingAttribute.Value = totalValue;
                    }
                    else
                    {
                        player.AddAttribute(attribute, totalValue);
                    }

                    // Update CurrentHealth and CurrentBullet based on max values
                    if (attribute.CodeName == "health_max")
                    {
                        player.CurrentHealth = totalValue;
                    }
                    else if (attribute.CodeName == "bullet_max")
                    {
                        player.CurrentBullet = totalValue;
                    }
                }
            }
        }

        public void PrepareForNewRound()
        {
            // Reset current health and bullets to max values for each player
            foreach (var player in AllPlayers)
            {
                var maxHealth = player.GetAttributeValue("health_max") ?? 0; 
                var maxBullet = player.GetAttributeValue("bullet_max") ?? 0;  

                player.CurrentHealth = maxHealth;
                player.CurrentBullet = maxBullet;
            }
        }

        public void SendPlayerAttributes()
        {
            foreach (var player in AllPlayers)
            {
                var gunAttributes = new Dictionary<string, int>();
                var vestAttributes = new Dictionary<string, int>();

                // Populate the dictionaries based on IsGun property
                foreach (var attribute in player.PlayerAttributes)
                {
                    if (attribute.GameAttribute.IsGun)
                    {
                        gunAttributes[attribute.GameAttribute.CodeName] = attribute.Value;
                    }
                    else
                    {
                        vestAttributes[attribute.GameAttribute.CodeName] = attribute.Value;
                    }
                }

                // Create the final structure
                var playerAttributesData = new PlayerAttributesResponse
                {
                    ForGun = gunAttributes,
                    ForVest = vestAttributes
                };

                // Create the HostFrameData using the builder
                var attributesFrame = new HostFrameDataBuilder<PlayerAttributesResponse>()
                    .SetActionCode(HostActionCode.SendPlayerAttributes)
                    .SetMessageType(MessageType.Info)
                    .SetMessage("Player attributes!!")
                    .SetData(playerAttributesData)
                    .Build();

                // Serialize and send the data to the player
                string serializedFrame = JsonConvert.SerializeObject(attributesFrame, Formatting.Indented);
                PlayerClients[player.ConnectionId].SendData(serializedFrame);
            }
        }



        #endregion

        #region Game play
        //debug
        [ObservableProperty]
        private Match match = new();

        [ObservableProperty]
        private Round currentRound = new();

        [ObservableProperty]
        private ObservableCollection<Round> rounds = [];

        [ObservableProperty]
        private ObservableCollection<Config> gameConfigs = [];

        [ObservableProperty]
        private ObservableCollection<int> teamWins = [0, 0, 0, 0];

        public Visibility StartMatchVisibility => Match.Stage == MatchStage.Lobbying ? Visibility.Visible : Visibility.Collapsed;
        public Visibility EndMatchVisibility => Match.Stage == MatchStage.Started ? Visibility.Visible : Visibility.Collapsed;
        public Visibility StartRoundVisibility => (Match.Stage == MatchStage.Started && CurrentRound.Stage == RoundStage.Lobbying) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility BattlePhaseVisibility => (Match.Stage == MatchStage.Started && CurrentRound.Stage == RoundStage.BuyPhase) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility EndRoundVisibility => (Match.Stage == MatchStage.Started && (CurrentRound.Stage == RoundStage.BattlePhase || CurrentRound.Stage == RoundStage.Paused)) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility PauseRoundVisibility => (Match.Stage == MatchStage.Started && CurrentRound.Stage == RoundStage.BattlePhase) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ResumeRoundVisibility => (Match.Stage == MatchStage.Started && CurrentRound.Stage == RoundStage.Paused) ? Visibility.Visible : Visibility.Collapsed;


        public void UpdateButtonVisibility()
        {
            OnPropertyChanged(nameof(StartMatchVisibility));
            OnPropertyChanged(nameof(EndMatchVisibility));
            OnPropertyChanged(nameof(StartRoundVisibility));
            OnPropertyChanged(nameof(BattlePhaseVisibility));
            OnPropertyChanged(nameof(EndRoundVisibility));
            OnPropertyChanged(nameof(PauseRoundVisibility));
            OnPropertyChanged(nameof(ResumeRoundVisibility));
        }
        public void StartMatch()
        {
            if (AllPlayers.Count < 2)
            {
                MessageBox.Show("Need at least 2 players to start match!!");
                return;
            }
            if (Match.Stage != MatchStage.Lobbying)
            {
                MessageBox.Show("Create new match before Starting!!");
                return;
            }

            Match.Stage = MatchStage.Started;
            Match.StartTime = DateTime.Now;

            SendTeamCredential();
            SendGameAttributeDetail();
            NotifyAllPlayerInfo("Game start!!");
            UpdateButtonVisibility();
        }
        public void EndMatch()
        {
            if (Match.Stage != MatchStage.Started)
            {
                MessageBox.Show("Start match before Ending!!");
                return;
            }
            int winnerTeam = FindWinnerTeamOfMatch();
            if (winnerTeam == 0)
            {
                NotifyAllPlayerInfo("Match ended in a draw!!");
            }
            else
            {
                NotifyAllPlayerInfo("Team " + winnerTeam + " wins the Match!!");
            }
            Match = new();
            NotifyAllPlayerInfo("Match End!!");
            UpdateButtonVisibility();

        }
        public void StartRoundBuyPhase()
        {
            //BattlePhase
            Rounds.Add(CurrentRound);
            if (CurrentRound.Stage != RoundStage.Lobbying)
            {
                MessageBox.Show("Create new round before Starting!!");
                return;
            }
            CurrentRound.StartTime = DateTime.Now;
            CurrentRound.Stage = RoundStage.BuyPhase;
            NotifyAllPlayerInfo("Round started!!");

            foreach (var player in AllPlayers)
            {                
                var upgradesFrame = new HostFrameDataBuilder<ListUpgradesDTO>()
                       .SetActionCode(HostActionCode.SendUpgradesData)
                       .SetMessageType(MessageType.Info)
                       .SetMessage("Equipment upgrades!!")
                       .SetData(new ListUpgradesDTO(player, Upgrades.ToList()))
                       .Build();
                PlayerClients[player.ConnectionId].SendData(JsonConvert.SerializeObject(upgradesFrame, Formatting.Indented));
            }
            PrepareForNewRound();
            UpdateButtonVisibility();
        }

        public void BattlePhase()
        {
            if (CurrentRound.Stage != RoundStage.BuyPhase)
            {
                MessageBox.Show("Start Buy Phase before Battle!!");
                return;
            }
            CurrentRound.Stage = RoundStage.BattlePhase;
            AssignPlayerAttributeAfterUpgrades();
            SendPlayerAttributes();
            SyncAllPlayers();
            NotifyAllPlayerInfo("Battle Phase!!");
            UpdateButtonVisibility();
        }
        public void EndRound()
        {
            if (CurrentRound.Stage != RoundStage.BattlePhase)
            {
                MessageBox.Show("Start Battle Phase before Ending!!");
                return;
            }
            CurrentRound.EndTime = DateTime.Now;
            int winnerTeam = FindWinnerTeamOfRound();
            if (winnerTeam == 0)
            {
                NotifyAllPlayerInfo("Round ended in a draw!!");
            }
            else
            {
                NotifyAllPlayerInfo("Team " + winnerTeam + " wins the round!!");
                TeamWins[winnerTeam - 1]++;
            }
            NotifyAllPlayerInfo("Round ended!!");
            CurrentRound = new Round();
            UpdateButtonVisibility();
        }
        public void PauseRound()
        {
            CurrentRound.Stage = RoundStage.Paused;
            NotifyAllPlayerInfo("Round Paused!!");
            UpdateButtonVisibility();
        }

        public void ResumeRound()
        {
            if (CurrentRound.Stage != RoundStage.Paused)
            {
                MessageBox.Show("Pause Round before Resuming!!");
                return;
            }
            CurrentRound.Stage = RoundStage.BattlePhase;
            NotifyAllPlayerInfo("Round Resume!!");
            UpdateButtonVisibility();
        }



        public void SendTeamCredential()
        {
            // Initialize the list of PlayerCredentialResponse
            List<PlayerCredentialResponse> credentials = [];

            // Loop through allPlayers and assign team IDs based on which team the player is in
            foreach (var player in AllPlayers)
            {
                string teamId = string.Empty;

                if (Team1Players.Contains(player))
                {
                    teamId = "01";  // Team 1
                }
                else if (Team2Players.Contains(player))
                {
                    teamId = "02";  // Team 2
                }
                else if (Team3Players.Contains(player))
                {
                    teamId = "03";  // Team 3
                }
                else if (Team4Players.Contains(player))
                {
                    teamId = "04";  // Team 4
                }

                // Create PlayerCredentialResponse for the player
                var credential = new PlayerCredentialResponse
                {
                    PlayerId = player.Id.ToString(),
                    MacGun = player.MacGun,
                    MacVest = player.MacVest,
                    TeamId = teamId
                };

                // Add to the credentials list
                credentials.Add(credential);
            }
            var hostFrameData = new HostFrameDataBuilder<List<PlayerCredentialResponse>>()
                        .SetActionCode(HostActionCode.SendPlayerCredentials)
                        .SetMessageType(MessageType.Response)
                        .SetMessage("Player Credentials")
                        .SetData(credentials)
                        .Build();
            string data = JsonConvert.SerializeObject(hostFrameData, Formatting.Indented);
            NotifyAllPlayer(data);
            // Loop through the PlayerClients and send the credentials to each player

        }
        public void SyncAllPlayers()
        {
            foreach (var player in AllPlayers)
            {
                PlayerClients[player.ConnectionId].SendSyncData();
            }
        }
        public void SendGameAttributeDetail()
        {
            List<GameAttributeDetailDTO> gameAttributeDetailDTOs = new List<GameAttributeDetailDTO>();
            foreach (var attribute in GameAttributes)
            {
                gameAttributeDetailDTOs.Add(new GameAttributeDetailDTO(attribute));
            }

            var hostFrameData = new HostFrameDataBuilder<List<GameAttributeDetailDTO>>()
                        .SetActionCode(HostActionCode.SendGameAttributeDetail)
                        .SetMessageType(MessageType.Response)
                        .SetMessage("Game Attribute Details")
                        .SetData(gameAttributeDetailDTOs)
                        .Build();
            string data = JsonConvert.SerializeObject(hostFrameData, Formatting.Indented);
            NotifyAllPlayer(data);
        }
        public int FindWinnerTeamOfRound()
        {
            // Step 1: Calculate the number of surviving players and total health for each team.
            var teamSurvivors = new (int survivingCount, int totalHealth)[]
            {
                (Team1Players.Count(p => p.CurrentHealth > 0), Team1Players.Where(p => p.CurrentHealth > 0).Sum(p => p.CurrentHealth)),
                (Team2Players.Count(p => p.CurrentHealth > 0), Team2Players.Where(p => p.CurrentHealth > 0).Sum(p => p.CurrentHealth)),
                (Team3Players.Count(p => p.CurrentHealth > 0), Team3Players.Where(p => p.CurrentHealth > 0).Sum(p => p.CurrentHealth)),
                (Team4Players.Count(p => p.CurrentHealth > 0), Team4Players.Where(p => p.CurrentHealth > 0).Sum(p => p.CurrentHealth))
            };

            // Step 2: Find the maximum number of survivors.
            int maxSurvivingPlayers = teamSurvivors.Max(t => t.survivingCount);

            // Step 3: Get all teams that have the maximum number of survivors.
            var teamsWithMaxSurvivors = teamSurvivors
                .Select((team, index) => (index + 1, team.survivingCount, team.totalHealth)) // Add 1 to index for 1-based team index.
                .Where(t => t.survivingCount == maxSurvivingPlayers)
                .ToList();

            // Step 4: If there's only one team with the highest number of survivors, return that team.
            if (teamsWithMaxSurvivors.Count == 1)
            {
                return teamsWithMaxSurvivors[0].Item1; // Return the team index (1-based).
            }

            // Step 5: If multiple teams have the same number of survivors, compare total health.
            int maxTotalHealth = teamsWithMaxSurvivors.Max(t => t.totalHealth);
            var teamsWithMaxHealth = teamsWithMaxSurvivors
                .Where(t => t.totalHealth == maxTotalHealth)
                .ToList();

            // Step 6: If only one team has the highest total health, return that team.
            if (teamsWithMaxHealth.Count == 1)
            {
                return teamsWithMaxHealth[0].Item1; // Return the team index (1-based).
            }

            // Step 7: If it's still a tie, return 0 to represent a draw.
            return 0;
        }

        public int FindWinnerTeamOfMatch()
        {
            // Step 1: Find the maximum number of wins among all teams.
            int maxWins = TeamWins.Max();

            // Step 2: Get all teams that have the maximum number of wins.
            var teamsWithMaxWins = TeamWins
                .Select((wins, index) => (index + 1, wins)) // Add 1 to index for 1-based team index.
                .Where(t => t.wins == maxWins)
                .ToList();

            // Step 3: If only one team has the most wins, return that team.
            if (teamsWithMaxWins.Count == 1)
            {
                return teamsWithMaxWins[0].Item1; // Return the team index (1-based).
            }

            // Step 4: If multiple teams have the same number of wins, return 0 for a draw.
            return 0;
        }



        #endregion

        #region Game Logs

        [ObservableProperty]
        private ObservableCollection<ShootLog> shootLogs = [];

        [ObservableProperty]
        private ObservableCollection<HitLog> hitLogs = [];

        [ObservableProperty]
        private ObservableCollection<LogViewModel> filteredLogs = [];
        #endregion

        #region Game Summary
        [ObservableProperty]
        private Player selectedPlayer;

        [ObservableProperty]
        private List<string> filterCategories = ["Shoot", "Hit other player", "Hitted by other player"];

        [ObservableProperty]
        private string selectedFilterCategory;
        partial void OnSelectedPlayerChanged(Player value)
        {
            UpdateFilteredLogs();
        }

        partial void OnSelectedFilterCategoryChanged(string value)
        {
            UpdateFilteredLogs();
        }

        partial void OnShootLogsChanged(ObservableCollection<ShootLog> value)
        {
            UpdateFilteredLogs();
        }

        partial void OnHitLogsChanged(ObservableCollection<HitLog> value)
        {
            UpdateFilteredLogs();
        }

        private void UpdateFilteredLogs()
        {
            if (SelectedPlayer == null || string.IsNullOrEmpty(SelectedFilterCategory))
            {
                FilteredLogs.Clear();
                return;
            }

            FilteredLogs.Clear();

            switch (SelectedFilterCategory)
            {
                case "Shoot":
                    var shootResults = ShootLogs
                        .Where(log => log.Shooter.Name == SelectedPlayer.Name)
                        .Select(log => new LogViewModel
                        {
                            Time = log.Time,
                            Round = log.Round,
                            Description = $"Shot fired",
                            Type = "Shoot"
                        });
                    foreach (var result in shootResults)
                        FilteredLogs.Add(result);
                    break;

                case "Hit other player":
                    var hitResults = HitLogs
                        .Where(log => log.Shooter.Name == SelectedPlayer.Name)
                        .Select(log => new LogViewModel
                        {
                            Time = log.Time,
                            Round = log.Round,
                            Description = $"Hit {log.Target.Name} for {log.Damage} damage",
                            Type = log.HitType.ToString(),
                            Damage = log.Damage
                        });
                    foreach (var result in hitResults)
                        FilteredLogs.Add(result);
                    break;

                case "Hitted by other player":
                    var hitByResults = HitLogs
                        .Where(log => log.Target.Name == SelectedPlayer.Name)
                        .Select(log => new LogViewModel
                        {
                            Time = log.Time,
                            Round = log.Round,
                            Description = $"Hit by {log.Shooter.Name} for {log.Damage} damage",
                            Type = log.HitType.ToString(),
                            Damage = log.Damage
                        });
                    foreach (var result in hitByResults)
                        FilteredLogs.Add(result);
                    break;
            }
        }
        #endregion

        #endregion

        #region Utils
        public void Test()
        {

            NotifyAllPlayerInfo("Test notification!!!");

            var a = 1;
        }


        #endregion

        #region InitData
        public void InitDataAttribute()
        {
            // Step 1: Load the Config data (simulated from the seeded database)
            var configData = new List<Config>
            {
                //Default player attributes
                new Config { Id = 1, Name = "Default Player Damage", CodeName = "damage_value", Value1 = "100" },
                new Config { Id = 2, Name = "Default Player Max Bullet", CodeName = "bullet_max", Value1 = "30" },
                new Config { Id = 3, Name = "Player Fire Level", CodeName = "fire_level", Value1 = "0" },
                new Config { Id = 4, Name = "Max Health", CodeName = "health_max", Value1 = "10000" },
                new Config { Id = 5, Name = "Armor", CodeName = "armor_value", Value1 = "50" },
                new Config { Id = 6, Name = "Initial money", CodeName = "money_init", Value1 = "100000" },

            };

            // Add to the existing ObservableCollection
            foreach (var config in configData)
            {
                DefaultPlayerAttribute.Add(config);
            }

            // Step 2: Create a list of GameAttributes (simulated attribute definitions from the attribute table)
            var newGameAttributes = new List<GameAttribute>
            {

                //Gun attributes
                new GameAttribute { Id = 1, Name = "Damage Value", CodeName = "damage_value", IsGun = true },
                new GameAttribute { Id = 2, Name = "Heal Value", CodeName = "healing_value", IsGun = true },
                new GameAttribute { Id = 3, Name = "Max Bullet", CodeName = "bullet_max", IsGun = true },
                new GameAttribute { Id = 4, Name = "Max SSketch Bullet", CodeName = "ssketch_bullet_max", IsGun = true },
                new GameAttribute { Id = 5, Name = "Bullet Reload Time", CodeName = "bullet_reload_time", IsGun = true },
                new GameAttribute { Id = 6, Name = "SSketch Bullet Reload Time", CodeName = "ssketch_bullet_reload_time", IsGun = true },
                new GameAttribute { Id = 7, Name = "Life Steal Value", CodeName = "life_steal_value", IsGun = true },
                new GameAttribute { Id = 8, Name = "Has True Damage", CodeName = "has_true_damage", IsGun = true },

                new GameAttribute { Id = 9,  Name = "Fire Rate", CodeName = "fire_rate", IsGun = true },
                new GameAttribute { Id = 10, Name = "Fire Level", CodeName = "fire_level", IsGun = true },
                new GameAttribute { Id = 11, Name = "Fire Duration", CodeName = "fire_duration", IsGun = true},
                new GameAttribute { Id = 12, Name = "Fire Value", CodeName = "fire_value", IsGun = true },
                new GameAttribute { Id = 13, Name = "Fire Has True Damage", CodeName = "fire_has_true_damage", IsGun = true },

                new GameAttribute { Id = 14, Name = "Poison Rate", CodeName = "poison_rate", IsGun = true },
                new GameAttribute { Id = 15, Name = "Poison Level", CodeName = "poison_level", IsGun = true },
                new GameAttribute { Id = 16, Name = "Poison Duration", CodeName = "poison_duration", IsGun = true },
                new GameAttribute { Id = 17, Name = "Poison Value", CodeName = "poison_value", IsGun = true },

                new GameAttribute { Id = 18, Name = "Deheal Level", CodeName = "deheal_level", IsGun = true },
                new GameAttribute { Id = 19, Name = "Deheal Duration", CodeName = "deheal_duration", IsGun = true },
                new GameAttribute { Id = 20, Name = "Deheal Heal Reduction", CodeName = "deheal_heal_reduction", IsGun = true },
                new GameAttribute { Id = 21, Name = "Deheal Block Regen", CodeName = "deheal_block_regen", IsGun = true },

                new GameAttribute { Id = 20, Name = "Silence Rate", CodeName = "silence_rate", IsGun = true },
                new GameAttribute { Id = 21, Name = "Silence Level", CodeName = "silence_level", IsGun = true },
                new GameAttribute { Id = 22, Name = "Silence Duration", CodeName = "silence_duration", IsGun = true },
                new GameAttribute { Id = 23, Name = "Silence Armor Decrease", CodeName = "silence_armor_decrease", IsGun = true },

                //Vest attributes
                new GameAttribute { Id = 50, Name = "Max Health", CodeName = "health_max", IsGun = false },
                new GameAttribute { Id = 51, Name = "Max Armor", CodeName = "armor_max", IsGun = false },
                new GameAttribute { Id = 52, Name = "Armor Plus", CodeName = "armor_plus", IsGun = false },
                new GameAttribute { Id = 53, Name = "Armor Minus", CodeName = "armor_minus", IsGun = false },
                new GameAttribute { Id = 54, Name = "Extra Damage Receive", CodeName = "extra_damage_receive", IsGun = false },
                new GameAttribute { Id = 55, Name = "Base Damage Vulnerability", CodeName = "base_damage_vul", IsGun = false },
                new GameAttribute { Id = 56, Name = "Base Damage Resistance", CodeName = "base_damage_res", IsGun = false },
                new GameAttribute { Id = 57, Name = "Bonus Damage Vulnerability", CodeName = "bonus_damage_vul", IsGun = false },
                new GameAttribute { Id = 58, Name = "Bonus Damage Resistance", CodeName = "bonus_damage_res", IsGun = false },
            };

            // Add to the existing ObservableCollection
            foreach (var gameAttribute in newGameAttributes)
            {
                GameAttributes.Add(gameAttribute);
            }


        }
        public void InitDataUpgrade()
        {
            var newUpgrades = new List<Upgrade>
            {
                new Upgrade
                {
                    Id = 1,
                    Name = "Attack plus",
                    Cost = 500,
                    Description = "Increases attack attributes.",
                    Attributes = new List<UpgradeAttribute>()
                },
                new Upgrade
                {
                    Id = 2,
                    Name = "Defense plus",
                    Cost = 400,
                    Description = "Increases defense attributes.",
                    Attributes = new List<UpgradeAttribute>()
                },
                new Upgrade
                {
                    Id = 3,
                    Name = "Fire plus",
                    Cost = 400,
                    Description = "Increases Fire attributes.",
                    Attributes = new List<UpgradeAttribute>()
                },
            };


            var upgradeAttributes = new List<UpgradeAttribute>
            {
                new UpgradeAttribute (1, newUpgrades[0], GameAttributes[0], 10),        // Attack plus - Damage Value
                new UpgradeAttribute (2, newUpgrades[0], GameAttributes[1], 5),         // Attack plus - Max Bullet

                new UpgradeAttribute (3, newUpgrades[1], GameAttributes[23], 50),        // Defense plus - Max Health            
                new UpgradeAttribute (4, newUpgrades[1], GameAttributes[24], 50),        // Defense plus - Armor

                new UpgradeAttribute (5, newUpgrades[2], GameAttributes[8], 100),       // Fire plus - Fire Rate
                new UpgradeAttribute (6, newUpgrades[2], GameAttributes[9], 1),        // Fire plus - Fire Level
                new UpgradeAttribute (7, newUpgrades[2], GameAttributes[10], 10),       // Fire plus - Fire Value
                new UpgradeAttribute (8, newUpgrades[2], GameAttributes[11], 5),        // Fire plus - Fire Duration

            };

            // Associate UpgradeAttributes with their corresponding Upgrades
            newUpgrades[0].Attributes.Add(upgradeAttributes[0]); // Attack plus - Damage Value
            newUpgrades[0].Attributes.Add(upgradeAttributes[1]); // Attack plus - Max Bullet

            newUpgrades[1].Attributes.Add(upgradeAttributes[2]); // Defense plus - Max Health
            newUpgrades[1].Attributes.Add(upgradeAttributes[3]); // Defense plus - Armor

            newUpgrades[2].Attributes.Add(upgradeAttributes[4]); // Fire plus - Fire Rate
            newUpgrades[2].Attributes.Add(upgradeAttributes[5]); // Fire plus - Fire Level
            newUpgrades[2].Attributes.Add(upgradeAttributes[6]); // Fire plus - Fire Value
            newUpgrades[2].Attributes.Add(upgradeAttributes[7]); // Fire plus - Fire Duration


            // Set the Upgrade reference for each UpgradeAttribute
            foreach (var upgrade in newUpgrades)
            {
                foreach (var attribute in upgrade.Attributes)
                {
                    attribute.Upgrade = upgrade;
                }
            }

            // Update the ObservableCollections
            Upgrades = new ObservableCollection<Upgrade>(newUpgrades);
            UpgradeAttributes = new ObservableCollection<UpgradeAttribute>(upgradeAttributes);
        }
        public void InitDataGameConfig()
        {
            var configs = new List<Config>
            {
                new Config { Id = 7, Name = "Rounds per match", CodeName = "round_per_match", Value1 = "3" },
                new Config { Id = 8, Name = "Time per round", CodeName = "time_round", Value1 = "10" },
            };

            // Add to the existing ObservableCollection
            foreach (var config in configs)
            {
                GameConfigs.Add(config);
            }
        }

        public void InitSamplePlayer()
        {
            Player player = new Player();
            player.Id = 1;
            player.Name = "Player 1";
            player.MacGun = "00:00:00:00:00:01";
            player.MacVest = "00:00:00:00:00:02";
            player.Credit = 1000;
            Team1Players.Add(player);
            AllPlayers.Add(player);

        }


        #endregion
    }
}
