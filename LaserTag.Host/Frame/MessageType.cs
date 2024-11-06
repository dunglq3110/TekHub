using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Frame
{
    public enum MessageType
    {
        Success = 1,
        Error = 2,
        Info = 3,
        Request = 4,
        Response = 5,
        Log = 6,
    }
}
