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
    /// Interaction logic for UpgradesTabControl.xaml
    /// </summary>
    public partial class UpgradesTabControl : UserControl
    {
        private readonly Player _player;

        public UpgradesTabControl(Player player)
        {
            InitializeComponent();
            _player = player;
            DataContext = _player;
        }
    }
}
