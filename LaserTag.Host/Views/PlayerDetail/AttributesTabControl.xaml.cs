using LaserTag.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaserTag.Host.Views.PlayerDetail
{
    /// <summary>
    /// Interaction logic for AttributesTabControl.xaml
    /// </summary>
    public partial class AttributesTabControl : UserControl
    {
        private readonly Player _player;

        public AttributesTabControl(Player player)
        {
            InitializeComponent();
            _player = player;
            LoadAttributes();

            // Expand both sections by default
            GunAttributesExpander.IsExpanded = true;
            VestAttributesExpander.IsExpanded = true;
        }

        private void LoadAttributes()
        {
            if (_player?.PlayerAttributes == null) return;

            // Split attributes into gun and vest attributes
            var gunAttributes = _player.PlayerAttributes
                .Where(attr => attr.GameAttribute.IsGun)
                .OrderBy(attr => attr.GameAttribute.Name)
                .ToList();

            var vestAttributes = _player.PlayerAttributes
                .Where(attr => !attr.GameAttribute.IsGun)
                .OrderBy(attr => attr.GameAttribute.Name)
                .ToList();

            // Set ItemsSource for both lists
            GunAttributesList.ItemsSource = gunAttributes;
            VestAttributesList.ItemsSource = vestAttributes;
        }
    }
}
