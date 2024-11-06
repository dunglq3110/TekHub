using LaserTag.Host.Logic;
using LaserTag.Host.Models;
using LaserTag.Host.Views.PlayerDetail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaserTag.Host.Views
{
    /// <summary>
    /// Interaction logic for MatchPage.xaml
    /// </summary>
    public partial class MatchPage : Page
    {
        public MatchPage()
        {
            InitializeComponent();
            DataContext = GameManager.Instance;
            
        }

        private void PlayerDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.DataContext is Player player)
            {
                // Create popup window
                var popup = new Window
                {
                    Title = $"Player Details - {player.Name}",
                    Width = 620,
                    Height = 600,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Owner = Window.GetWindow(this),
                    ResizeMode = ResizeMode.NoResize,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF333333"))
                };

                // Create and set the player detail control
                var playerDetailControl = new PlayerDetailPopup(player);
                popup.Content = playerDetailControl;

                // Show the popup
                popup.ShowDialog();
            }
        }

        private void MoveToTeam1_Click(object sender, RoutedEventArgs e)
        {
            MovePlayerToTeam(sender, GameManager.Instance.Team1Players, "Team 01");
        }

        private void MoveToTeam2_Click(object sender, RoutedEventArgs e)
        {
            MovePlayerToTeam(sender, GameManager.Instance.Team2Players, "Team 02");
        }

        private void MoveToTeam3_Click(object sender, RoutedEventArgs e)
        {
            MovePlayerToTeam(sender, GameManager.Instance.Team3Players, "Team 03");
        }

        private void MoveToTeam4_Click(object sender, RoutedEventArgs e)
        {
            MovePlayerToTeam(sender, GameManager.Instance.Team4Players, "Team 04");
        }

        private void MovePlayerToTeam(object sender, ObservableCollection<Player> targetTeam, string teamName)
        {
            if (sender is MenuItem menuItem &&
                menuItem.DataContext is Player player)
            {
                // Find the source team
                var sourceTeam = GetSourceTeam(player);

                if (sourceTeam != null && sourceTeam != targetTeam)
                {
                    // Move the player
                    sourceTeam.Remove(player);
                    targetTeam.Add(player);
                    GameManager.Instance.NotifyAllPlayerInfo("Player: " + player.Name + " moves to team: " + teamName);
                }
            }
        }

        private ObservableCollection<Player> GetSourceTeam(Player player)
        {
            var gameManager = GameManager.Instance;
            if (gameManager.Team1Players.Contains(player)) return gameManager.Team1Players;
            if (gameManager.Team2Players.Contains(player)) return gameManager.Team2Players;
            if (gameManager.Team3Players.Contains(player)) return gameManager.Team3Players;
            if (gameManager.Team4Players.Contains(player)) return gameManager.Team4Players;
            return null;
        }
    }
}
