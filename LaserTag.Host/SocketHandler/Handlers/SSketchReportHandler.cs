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
    public class SSketchReportHandler : MessageHandlerBase<string>
    {
        public SSketchReportHandler(GameManager gameManager) : base(gameManager) { }

        public override void Handle(PlayerClientSession session, string data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var ssketchReport = GameHelper.DecodeGunReport<SSketchReport>(GameHelper.StringToByteArray(data));
                var shooter = _gameManager.AllPlayers.FirstOrDefault(p => p.Id == ssketchReport.taggerId);
                var target = _gameManager.AllPlayers.FirstOrDefault(p => p.Id == ssketchReport.victimId);
                var random = new Random();

                if (shooter != null && target != null)
                {
                    HitLog hitLog = new HitLog
                    {
                        Id = random.Next(1, int.MaxValue),
                        Shooter = shooter,
                        Target = target,
                        Round = _gameManager.CurrentRound,
                        HitType = HitType.SSketch,
                        Damage = ssketchReport.damage,
                        Time = DateTime.Now
                    };
                    _gameManager.HitLogs.Add(hitLog);
                    shooter.Credit += ssketchReport.damage;
                }
            });
        }
    }
}
