using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using WebSocketSharp.Server;
using WebSocketSharp;
using Newtonsoft.Json;
using Microsoft.VisualBasic;

namespace LaserTag.TestGun
{
    public partial class GameManager : ObservableObject
    {
        #region Singleton
        private static GameManager _instance;

        private GameManager()
        {
            StartWebSocketServer();
        }

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        #endregion


        private WebSocketServer _wssv;

        [ObservableProperty]
        private string ipAddress = "1.1.1.1";

        [ObservableProperty]
        private string macGun = "00:00:00:00:00:00";

        [ObservableProperty]
        private string macVest = "00:00:00:00:00:00";

        [ObservableProperty]
        private bool hasVest = false;

        [ObservableProperty]
        private string resultText = "";

        public PlayerClientSession PlayerClient;

        [ObservableProperty]
        private string gunLog = "";
        public void StartWebSocketServer()
        {
            try
            {
                // Check if WebSocketServer is already initialized
                if (_wssv == null)
                {
                    IpAddress = GetLocalIPv4();
                    _wssv = new WebSocketServer($"ws://{IpAddress}:81");
                    _wssv.AddWebSocketService<PlayerClientSession>("/ws");
                    _wssv.Start();
                }
                else
                {
                    // WebSocketServer is already running, do nothing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
                // Handle exception if necessary
            }
        }

        private string GetLocalIPv4()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address.ToString();
            }
        }

        public void SendSubmitMac()
        {
            // Create an object to hold the values for the JSON
            var macData = new
            {
                key = "submit_mac",
                gun_mac = MacGun,
                vest_mac = MacVest,
                is_vest_connected = HasVest
            };

            // Convert the object to a JSON string using Newtonsoft.Json
            string jsonString = JsonConvert.SerializeObject(macData);

            // Send the JSON string using PlayerClient.SendData
            PlayerClient.SendData(jsonString);
        }
        public void SendGunLog()
        {
            PlayerClient.SendData(GunLog);
        }

    }

    public class PlayerClientSession : WebSocketBehavior
    {

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
            GameManager.Instance.ResultText += e.Data + Environment.NewLine;
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            GameManager.Instance.PlayerClient = this;
            
        }
        public void SendData(string data)
        {
            Send(data);
        }
        
    }
}
