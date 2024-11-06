using LaserTag.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Dtos
{
    public class ListUpgradesDTO
    {
        public int Credit { get; set; }
        public List<PlayerUpgradeDTO> Upgrades { get; set; } = new List<PlayerUpgradeDTO>();

        public ListUpgradesDTO(int credit, List<PlayerUpgradeDTO> upgrades)
        {
            Credit = credit;
            Upgrades = upgrades;
        }

        public ListUpgradesDTO(Player player, List<Upgrade> allUpgrades)
        {
            // Set the player's current credit
            Credit = player.Credit;

            // Loop through all available upgrades
            foreach (var upgrade in allUpgrades)
            {
                // Check if the player already owns this upgrade
                var isBought = player.Upgrades.Any(u => u.Id == upgrade.Id);

                // Create the PlayerUpgradeDTO for this upgrade
                var playerUpgradeDTO = new PlayerUpgradeDTO
                {
                    Upgrade = upgrade.Clone(),  // Clone the upgrade to avoid modifying original data
                    Available = !isBought  // Mark available if the player hasn't bought it
                };

                // Add to the list of upgrades
                Upgrades.Add(playerUpgradeDTO);
            }
        }
    }

    // This class represents the relationship between a player and the upgrade
    public class PlayerUpgradeDTO
    {
        public Upgrade Upgrade { get; set; } = new Upgrade();  // Original upgrade details
        public bool Available { get; set; }  // Availability for the specific player
    }
}
