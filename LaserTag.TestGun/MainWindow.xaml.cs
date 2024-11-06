using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaserTag.TestGun
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = GameManager.Instance; // Set DataContext to GameManager
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // You can add your connection logic here
            GameManager.Instance.SendSubmitMac();
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            // You can add your connection logic here
            GameManager.Instance.SendGunLog();
        }
        
    }
}
