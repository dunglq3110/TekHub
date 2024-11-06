using LaserTag.Host.Frame;
using LaserTag.Host.Logic;
using LaserTag.Host.SocketHandler.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.SocketHandler
{
    public class MessageHandlerFactory
    {
        private readonly Dictionary<HostActionCode, object> _handlers;
        private readonly GameManager _gameManager;

        public MessageHandlerFactory(GameManager gameManager)
        {
            _gameManager = gameManager;
            _handlers = new Dictionary<HostActionCode, object>
            {
                { HostActionCode.InitialConnection, new InitialConnectionHandler(gameManager) },
                { HostActionCode.UpdatePlayerData, new UpdatePlayerDataHandler(gameManager) },
                { HostActionCode.PlayerBuyUpgrade, new PlayerBuyUpgradeHandler(gameManager) },
                { HostActionCode.HealthArmorReport, new HealthArmorReportHandler(gameManager) },
                { HostActionCode.BulletReport, new BulletReportHandler(gameManager) },
                { HostActionCode.DamageReport, new DamageReportHandler(gameManager) },
                { HostActionCode.HealingReport, new HealingReportHandler(gameManager) },
                { HostActionCode.SSketchReport, new SSketchReportHandler(gameManager) },
            };
        }

        public IMessageHandler<T> GetHandler<T>(HostActionCode actionCode)
        {
            if (_handlers.TryGetValue(actionCode, out var handler))
            {
                if (handler is IMessageHandler<T> typedHandler)
                {
                    return typedHandler;
                }
                throw new InvalidOperationException($"Handler for action code {actionCode} exists but is not of type IMessageHandler<{typeof(T).Name}>");
            }
            throw new KeyNotFoundException($"No handler registered for action code {actionCode}");
        }
    }
}
