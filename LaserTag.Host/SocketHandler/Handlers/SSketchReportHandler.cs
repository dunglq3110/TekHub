﻿using LaserTag.Host.Dtos;
using LaserTag.Host.Extensions;
using LaserTag.Host.Frame;
using LaserTag.Host.Helper;
using LaserTag.Host.Logic;
using LaserTag.Host.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LaserTag.Host.SocketHandler.Handlers
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
                if (shooter != null && target != null)
                {
                    HitLog hitLog = new HitLog
                    {
                        Id = GameManager.Instance.HitLogs.Count(),
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