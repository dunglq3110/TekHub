using TekHub.Host.Frame;
using TekHub.Host.Helper;
using TekHub.Host.Logic;
using TekHub.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TekHub.Host.SocketHandler.Handlers
{

    public class BulletReportHandler : MessageHandlerBase<string>
    {
        public BulletReportHandler(GameManager gameManager) : base(gameManager) { }

        public override void Handle(PlayerClientSession session, string data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                GameManager gmInstance = GameManager.Instance;
                var bulletReport = GameHelper.DecodeGunReport<BulletReport>(GameHelper.StringToByteArray(data));
                Player player = _gameManager.AllPlayers.FirstOrDefault(p => p.Id == bulletReport.id);
                if (player == null) return;
                player.CurrentBullet = bulletReport.normalBullets;
                player.CurrentSSketchBullet = bulletReport.ssketchBullets;
                ShootLog shootLog = new ShootLog
                {
                    Id = gmInstance.ShootLogs.Count(),
                    Shooter = player,
                    Round = gmInstance.CurrentRound,
                    Time = DateTime.Now,
                };
                gmInstance.ShootLogs.Add(shootLog);
                session.SendSyncData();
            });
        }
    }
}
