using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Frame
{
    public class HostFrameData<T> where T : class
    {
        public HostActionCode ActionCode { get; set; }
        public MessageType MessageType { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }

    public class HostFrameDataBuilder<T> where T : class
    {
        private HostActionCode _actionCode;
        private MessageType _messageType;
        private string _message = string.Empty;
        private T _data;

        public HostFrameDataBuilder<T> SetActionCode(HostActionCode actionCode)
        {
            _actionCode = actionCode;
            return this;
        }

        public HostFrameDataBuilder<T> SetMessageType(MessageType messageType)
        {
            _messageType = messageType;
            return this;
        }

        public HostFrameDataBuilder<T> SetMessage(string message)
        {
            _message = message;
            return this;
        }

        public HostFrameDataBuilder<T> SetData(T data)
        {
            _data = data;
            return this;
        }

        public HostFrameData<T> Build()
        {
            return new HostFrameData<T>
            {
                ActionCode = _actionCode,
                MessageType = _messageType,
                Message = _message,
                Data = _data
            };
        }
    }

}
