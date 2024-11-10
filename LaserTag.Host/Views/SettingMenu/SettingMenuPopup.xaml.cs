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
using TekHub.Host.Logic;
using TekHub.Host.Models;
using TekHub.Host.Views.PlayerDetail;

namespace TekHub.Host.Views.SettingMenu
{
    /// <summary>
    /// Interaction logic for SettingMenuPopup.xaml
    /// </summary>

    public partial class SettingMenuPopup : UserControl
    {
        private readonly Player _player;
        private readonly GameConfigTabControl gameConfigTabControl;
        private readonly PlayerDefaultTabControl playerDefaultTabControl;
        private readonly UpgradeSettingTabControl upgradeSettingTabControl;
        private UserControl _currentTab;

        public SettingMenuPopup()
        {
            InitializeComponent();
            DataContext = GameManager.Instance;

            gameConfigTabControl = new GameConfigTabControl();
            playerDefaultTabControl = new PlayerDefaultTabControl();
            upgradeSettingTabControl = new UpgradeSettingTabControl();

            // Start with Detail tab
            ShowGameConfigTab();
        }

        private void GameConfigTab_Click(object sender, RoutedEventArgs e)
        {
            ShowGameConfigTab();
        }

        private void PlayerDefaultTab_Click(object sender, RoutedEventArgs e)
        {
            ShowPlayerDefaultTab();
        }

        private void UpgradeSettingTab_Click(object sender, RoutedEventArgs e)
        {
            ShowUpgradeSettingTab();
        }

        private void ShowGameConfigTab()
        {
            UpdateTabColors(GameConfigTab);
            SwitchTabContent(gameConfigTabControl);
        }

        private void ShowPlayerDefaultTab()
        {
            UpdateTabColors(PlayerDefaultTab);
            SwitchTabContent(playerDefaultTabControl);
        }

        private void ShowUpgradeSettingTab()
        {
            UpdateTabColors(UpgradeSettingTab);
            SwitchTabContent(upgradeSettingTabControl);
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

            GameConfigTab.Background = inactiveColor;
            PlayerDefaultTab.Background = inactiveColor;
            UpgradeSettingTab.Background = inactiveColor;

            selectedTab.Background = activeColor;
        }
    }
}
