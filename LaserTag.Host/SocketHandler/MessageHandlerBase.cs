using TekHub.Host.Frame;
using TekHub.Host.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.SocketHandler
{
    public abstract class MessageHandlerBase<T> : IMessageHandler<T>
    {
        protected readonly GameManager _gameManager;

        protected MessageHandlerBase(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public abstract void Handle(PlayerClientSession session, T data);

        protected void SendResponse(PlayerClientSession session, HostActionCode actionCode, MessageType messageType, string message = "", object data = null)
        {
            var response = new HostFrameDataBuilder<object>()
                .SetActionCode(actionCode)
                .SetMessageType(messageType)
                .SetMessage(message)
                .SetData(data)
                .Build();

            string jsonData = JsonConvert.SerializeObject(response, Formatting.Indented);
            session.SendData(jsonData);
        }
    }
}
