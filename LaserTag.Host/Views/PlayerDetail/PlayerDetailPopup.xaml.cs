using TekHub.Host.Models;
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

namespace TekHub.Host.Views.PlayerDetail
{
    /// <summary>
    /// Interaction logic for PlayerDetailPopup.xaml
    /// </summary>
    // PlayerDetailPopup.xaml.cs
    public partial class PlayerDetailPopup : UserControl
    {
        private readonly Player _player;
        private readonly DetailTabControl _detailTab;
        private readonly AttributesTabControl _attributesTab;
        private readonly UpgradesTabControl _upgradesTab;
        private UserControl _currentTab;

        public PlayerDetailPopup(Player player)
        {
            InitializeComponent();
            _player = player;
            DataContext = _player;

            // Initialize tab controls
            _detailTab = new DetailTabControl(_player);
            _attributesTab = new AttributesTabControl(_player);
            _upgradesTab = new UpgradesTabControl(_player);

            // Start with Detail tab
            ShowDetailTab();
        }

        private void DetailTab_Click(object sender, RoutedEventArgs e)
        {
            ShowDetailTab();
        }

        private void AttributesTab_Click(object sender, RoutedEventArgs e)
        {
            ShowAttributesTab();
        }

        private void UpgradesTab_Click(object sender, RoutedEventArgs e)
        {
            ShowUpgradesTab();
        }

        private void ShowDetailTab()
        {
            UpdateTabColors(DetailTab);
            SwitchTabContent(_detailTab);
        }

        private void ShowAttributesTab()
        {
            UpdateTabColors(AttributesTab);
            SwitchTabContent(_attributesTab);
        }

        private void ShowUpgradesTab()
        {
            UpdateTabColors(UpgradesTab);
            SwitchTabContent(_upgradesTab);
        }

        private void SwitchTabContent(UserControl newTab)
        {
            if (_currentTab != null)
            {
                ContentArea.Children.Remove(_currentTab);
            }
            _currentTab = newTab;
            ContentArea.Children.Add(_currentTab);
        }

        private void UpdateTabColors(Button selectedTab)
        {
            var activeColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4A5D7E"));
            var inactiveColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));

            DetailTab.Background = inactiveColor;
            AttributesTab.Background = inactiveColor;
            UpgradesTab.Background = inactiveColor;

            selectedTab.Background = activeColor;
        }
    }
}
