using TekHub.Host.Dtos;
using TekHub.Host.Extensions;
using TekHub.Host.Frame;
using TekHub.Host.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.SocketHandler.Handlers
{
    public class InitialConnectionHandler : MessageHandlerBase<AddPlayerRequest>
    {
        public InitialConnectionHandler(GameManager gameManager) : base(gameManager) { }

        public override void Handle(PlayerClientSession session, AddPlayerRequest data)
        {
            try
            {
                if (_gameManager.CheckAddNewPlayer(data))
                {
                    session.Player.Id = _gameManager.PlayerClients.Count + 1;
                    session.Player.ConnectionId = session.ID;
                    session.Player.Name = data.Name;
                    session.Player.MacVest = data.MacVest;
                    session.Player.MacGun = data.MacGun;

                    _gameManager.AddPlayer(session);
                    SendResponse(session, HostActionCode.GameMessage, MessageType.Success, "You are added, Enjoy!");
                }
            }
            catch (Exception ex)
            {
                SendResponse(session, HostActionCode.GameMessage, MessageType.Error, ex.Message);
            }
        }
    }
}
