using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Frame
{
    public enum HostActionCode
    {

        //Game Log
        HealthArmorReport = 3,
        DamageReport = 8,
        HealingReport = 9,
        SSketchReport = 10,
        BulletReport = 11,
        //Mobile send something to Host (code 100)
        InitialConnection = 101,
        PlayerBuyUpgrade = 102,
        PlayerGameLog = 103,
        UpdatePlayerData = 1,
        PlayerDisconnected = 2,
        //Host send something to mobile (code 200)
        GameMessage = 200,
        SendUpgradesData = 221,
        SendPlayerCredentials = 222,
        SendPlayerAttributes = 223,
        SendGameAttributeDetail = 224,
        SendSyncPlayerData = 225,

        

        HeartBeat = 999,


    }
}
