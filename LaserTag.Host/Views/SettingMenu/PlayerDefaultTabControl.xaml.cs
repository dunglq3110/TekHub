using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TekHub.Host.ViewModel;

namespace TekHub.Host.Views.SettingMenu
{
    /// <summary>
    /// Interaction logic for PlayerDefaultTabControl.xaml
    /// </summary>
    public partial class PlayerDefaultTabControl : UserControl
    {
        public PlayerDefaultTabViewModel ViewModel { get; }

        public PlayerDefaultTabControl()
        {
            InitializeComponent();
            ViewModel = new PlayerDefaultTabViewModel();
            DataContext = ViewModel;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel == null) return;
            var selectedFilter = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            ViewModel.LoadEditableConfigs(selectedFilter);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveChanges();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DiscardChanges();
        }
    }
}
