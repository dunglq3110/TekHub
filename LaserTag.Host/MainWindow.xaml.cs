using TekHub.Host.Logic;
using TekHub.Host.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TekHub.Host
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MatchPage matchPage;
        private GameProgressPage gameProgressPage;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = GameManager.Instance;

            matchPage = new MatchPage();
            gameProgressPage = new GameProgressPage();
            GameManager.Instance.StartWebSocketServer();
            MainFrame.Navigate(matchPage);
        }
        private void WifiConnect_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.StartWebSocketServer();
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.StartWebSocketServer();
        }

        private void MatchButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(matchPage);
            GameProgressButton.BorderBrush = Brushes.Transparent;
        }

        private void GameProgressButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(gameProgressPage);
            MatchButton.BorderBrush = Brushes.Transparent;
        }
        private void StartMatch_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.StartMatch();
        }
        private void EndMatch_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.EndMatch();
        }

        private void StartRound_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.StartRoundBuyPhase();
        }
        private void BattlePhase_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.BattlePhase();
        }
        private void EndRound_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.EndRound();
        }
        private void PauseRound_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.PauseRound();
        }
        private void ResumeRound_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance.ResumeRound();
        }


        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is "Z" for debugger
            if (e.Key == Key.Z)
            {
                GameManager.Instance.Test();
            }
        }
    }
}