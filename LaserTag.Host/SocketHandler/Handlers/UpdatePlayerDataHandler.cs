using LaserTag.Host.Dtos;
using LaserTag.Host.Frame;
using LaserTag.Host.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.SocketHandler.Handlers
{
    public class UpdatePlayerDataHandler : MessageHandlerBase<UpdatePlayerRequest>
    {
        public UpdatePlayerDataHandler(GameManager gameManager) : base(gameManager) { }

        public override void Handle(PlayerClientSession session, UpdatePlayerRequest data)
        {
            try
            {
                session.Player.Name = data.Name;
                session.Player.MacVest = data.MacVest;
                session.Player.MacGun = data.MacGun;

                SendResponse(session, HostActionCode.GameMessage, MessageType.Success, "Your profile updated! , Enjoy!");
            }
            catch (Exception ex)
            {
                SendResponse(session, HostActionCode.GameMessage, MessageType.Error, ex.Message);
            }
        }
    }
}
