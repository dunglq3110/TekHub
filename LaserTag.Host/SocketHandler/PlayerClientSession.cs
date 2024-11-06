using TekHub.Host.Dtos;
using TekHub.Host.Extensions;
using TekHub.Host.Frame;
using TekHub.Host.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Windows;
using TekHub.Host.Helper;
using TekHub.Host.SocketHandler;
namespace TekHub.Host.Logic
{
    public class PlayerClientSession : WebSocketBehavior
    {
        private readonly MessageHandlerFactory _handlerFactory;
        public Player Player { get; set; } = new Player();

        public PlayerClientSession()
        {
            _handlerFactory = new MessageHandlerFactory(GameManager.Instance);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var frame = JsonConvert.DeserializeObject<HostFrameData<object>>(e.Data);
            if (frame == null) return;

            try
            {
                switch (frame.ActionCode)
                {
                    case HostActionCode.InitialConnection:
                        var initData = JsonConvert.DeserializeObject<HostFrameData<AddPlayerRequest>>(e.Data);
                        _handlerFactory.GetHandler<AddPlayerRequest>(frame.ActionCode).Handle(this, initData.Data);
                        break;

                    case HostActionCode.UpdatePlayerData:
                        var updateData = JsonConvert.DeserializeObject<HostFrameData<UpdatePlayerRequest>>(e.Data);
                        _handlerFactory.GetHandler<UpdatePlayerRequest>(frame.ActionCode).Handle(this, updateData.Data);
                        break;

                    case HostActionCode.PlayerBuyUpgrade:
                        var buyUpgradeData = JsonConvert.DeserializeObject<HostFrameData<List<int>>>(e.Data);
                        _handlerFactory.GetHandler<List<int>>(frame.ActionCode).Handle(this, buyUpgradeData.Data);
                        break;

                    case HostActionCode.HealthArmorReport:
                        var healthArmorData = JsonConvert.DeserializeObject<HostFrameData<string>>(e.Data);
                        _handlerFactory.GetHandler<string>(frame.ActionCode).Handle(this, healthArmorData.Data);
                        break;

                    case HostActionCode.BulletReport:
                        var bulletReportData = JsonConvert.DeserializeObject<HostFrameData<string>>(e.Data);
                        _handlerFactory.GetHandler<string>(frame.ActionCode).Handle(this, bulletReportData.Data);
                        break;

                    case HostActionCode.DamageReport:
                        var damageReportData = JsonConvert.DeserializeObject<HostFrameData<string>>(e.Data);
                        _handlerFactory.GetHandler<string>(frame.ActionCode).Handle(this, damageReportData.Data);
                        break;

                    case HostActionCode.HealingReport:
                        var healingReportData = JsonConvert.DeserializeObject<HostFrameData<string>>(e.Data);
                        _handlerFactory.GetHandler<string>(frame.ActionCode).Handle(this, healingReportData.Data);
                        break;

                    case HostActionCode.SSketchReport:
                        var ssketchReportdata = JsonConvert.DeserializeObject<HostFrameData<string>>(e.Data);
                        _handlerFactory.GetHandler<string>(frame.ActionCode).Handle(this, ssketchReportdata.Data);
                        break;

                    default: break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling message: {ex.Message}");
            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            GameManager.Instance.NotifyAllPlayerInfo($"Player: {Player.Name} Disconected");
            GameManager.Instance.RemovePlayer(Player);
            base.OnClose(e);
        }

        public void SendData(string data)
        {
            Send(data);
        }
        public void SendData(HostActionCode actionCode, MessageType messageType, string message = "", object data = null)
        {
            var response = new HostFrameDataBuilder<object>()
                .SetActionCode(actionCode)
                .SetMessageType(messageType)
                .SetMessage(message)
                .SetData(data)
                .Build();

            string jsonData = JsonConvert.SerializeObject(response, Formatting.Indented);
            SendData(jsonData);
        }

        public void SendSyncData()
        {
            try
            {
                var response = new HostFrameDataBuilder<object>()
                    .SetActionCode(HostActionCode.SendSyncPlayerData)
                    .SetMessageType(MessageType.Success)
                    .SetData(new SyncPlayerDTO(Player))
                    .Build();
                if (State == WebSocketState.Open)
                {
                    string data = JsonConvert.SerializeObject(response, Formatting.Indented);

                    Send(data);

                }
                else
                {
                    Console.WriteLine($"Cannot send sync data - connection state: {State}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending sync data: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

        }
    }
}
