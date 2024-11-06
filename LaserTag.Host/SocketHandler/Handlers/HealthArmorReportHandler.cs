using TekHub.Host.Dtos;
using TekHub.Host.Extensions;
using TekHub.Host.Frame;
using TekHub.Host.Helper;
using TekHub.Host.Logic;
using TekHub.Host.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TekHub.Host.SocketHandler.Handlers
{
    public class HealthArmorReportHandler : MessageHandlerBase<string>
    {
        public HealthArmorReportHandler(GameManager gameManager) : base(gameManager) { }

        public override void Handle(PlayerClientSession session, string data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var healthArmorReport = GameHelper.DecodeGunReport<HealthArmorReport>(GameHelper.StringToByteArray(data));
                Player player = _gameManager.AllPlayers.FirstOrDefault(p => p.Id == healthArmorReport.id);
                if (player == null) return;
                player.CurrentHealth = healthArmorReport.health;
                player.CurrentArmor = healthArmorReport.armor;
                session.SendSyncData();
            });         
        }
    }
}
