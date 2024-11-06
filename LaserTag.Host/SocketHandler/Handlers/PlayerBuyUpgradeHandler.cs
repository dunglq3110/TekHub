﻿using LaserTag.Host.Dtos;
using LaserTag.Host.Frame;
using LaserTag.Host.Logic;
using LaserTag.Host.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.SocketHandler.Handlers
{
    public class PlayerBuyUpgradeHandler : MessageHandlerBase<List<int>>
    {
        public PlayerBuyUpgradeHandler(GameManager gameManager) : base(gameManager) { }

        public override void Handle(PlayerClientSession session, List<int> data)
        {
            try
            {
                foreach (var item in data)
                {
                    _gameManager.PlayerBuyUpgrade(session.Player.Id, item);
                }
                SendResponse(session, HostActionCode.GameMessage, MessageType.Info, "Your upgrades applied!!");
            }
            catch (Exception ex)
            {
                SendResponse(session, HostActionCode.GameMessage, MessageType.Error, "Buy Upgrade error:!!" + ex.Message);

            }
        }
    }
}