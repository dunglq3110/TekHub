using TekHub.Host.Frame;
using TekHub.Host.Helper;
using TekHub.Host.Logic;
using TekHub.Host.Models;
using System.Windows;

namespace TekHub.Host.SocketHandler.Handlers
{

    public class HealingReportHandler : MessageHandlerBase<string>
    {
        public HealingReportHandler(GameManager gameManager) : base(gameManager) { }

        public override void Handle(PlayerClientSession session, string data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var healingReport = GameHelper.DecodeGunReport<HealingReport>(GameHelper.StringToByteArray(data));
                var shooter = _gameManager.AllPlayers.FirstOrDefault(p => p.Id == healingReport.healerId);
                var target = _gameManager.AllPlayers.FirstOrDefault(p => p.Id == healingReport.healedId);
                if (shooter != null && target != null)
                {
                    HitLog hitLog = new HitLog
                    {
                        Id = GameManager.Instance.HitLogs.Count(),
                        Shooter = shooter,
                        Target = target,
                        Round = _gameManager.CurrentRound,
                        HitType = HitType.Healing,
                        Damage = healingReport.healAmount,
                        Time = DateTime.Now
                    };
                    _gameManager.HitLogs.Add(hitLog);
                    shooter.Credit += healingReport.healAmount;
                }
            });
        }
    }
}
