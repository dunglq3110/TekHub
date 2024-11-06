using LaserTag.Host.Dtos;
using LaserTag.Host.Helper;
using LaserTag.Host.Logic;
using LaserTag.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Extensions
{
    public static class GameManagerExtension
    {
        public static bool CheckAddNewPlayer(this GameManager gameManager, AddPlayerRequest request)
        {
            if (gameManager.PlayerClients == null) throw new Exception("PlayerClients dictionary is null");
            if (!request.MacGun.IsMacAddress()) throw new Exception("Your gun's mac address is not format corrected");
            if (!request.MacVest.IsMacAddress()) throw new Exception("Your vest's mac address is not format corrected");

            foreach (var playerClient in gameManager.PlayerClients)
            {
                var player = playerClient.Value.Player;
                if (player.Name == request.Name) throw new Exception("Your name is exist in the match!!");

                if (player.MacGun == request.MacGun) throw new Exception("Your gun is exist in the match!!");

                if (player.MacVest == request.MacVest) throw new Exception("Your vest is exist in the match!!");

            }
            return true;
        }

        

    }
}
