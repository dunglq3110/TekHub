using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{
    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class Player : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string connectionId = string.Empty;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string macGun = string.Empty;

        [ObservableProperty]
        private string macVest = string.Empty;

        [ObservableProperty]
        private int currentHealth;

        [ObservableProperty]
        private int currentBullet;

        [ObservableProperty]
        private int currentSSketchBullet;

        [ObservableProperty]
        private int currentArmor;

        [ObservableProperty]
        private int totalDamage;

        [ObservableProperty]
        private int totalHeal;

        [ObservableProperty]
        private int totalShots;

        [ObservableProperty]
        private int totalHits;

        [ObservableProperty]
        private int totalKills;

        [ObservableProperty]
        private int totalAssists;

        [ObservableProperty]
        private int totalDeaths;

        [ObservableProperty]
        private int credit = 10000;

        public List<PlayerAttribute> PlayerAttributes { get; set; } = new List<PlayerAttribute>();
        public List<Upgrade> Upgrades { get; set; } = new List<Upgrade>();

        // Add an attribute to the player

        public override string ToString()
        {
            return $"Name: {Name}";
        }
        public void AddAttribute(GameAttribute attribute, int value)
        {
            PlayerAttributes.Add(new PlayerAttribute
            {
                Player = this,
                GameAttribute = attribute,
                Value = value
            });
        }

        // Get the value of a specific attribute by its code name
        public int? GetAttributeValue(string codeName)
        {
            var attr = PlayerAttributes.FirstOrDefault(pa => pa.GameAttribute.CodeName == codeName);
            return attr?.Value;
        }

        // Update the value of a specific attribute
        public void SetAttributeValue(string codeName, int newValue)
        {
            var attr = PlayerAttributes.FirstOrDefault(pa => pa.GameAttribute.CodeName == codeName);
            if (attr != null)
            {
                attr.Value = newValue;
            }
        }

        // Get all attributes with their values
        public List<PlayerAttribute> GetAllAttributes()
        {
            return PlayerAttributes;
        }


    }

}
