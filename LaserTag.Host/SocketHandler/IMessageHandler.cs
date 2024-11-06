using TekHub.Host.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.SocketHandler
{
    public interface IMessageHandler<T>
    {
        void Handle(PlayerClientSession session, T data);
    }
}
