using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Frame
{
    public interface IGunReport
    {
        string ToString();
    }

    public struct DamageReport : IGunReport
    {
        public ReportType type;
        public byte taggerId;
        public byte victimId;
        public ushort damage;

        public override string ToString()
        {
            return $"DamageReport: Tagger ID: {taggerId}, Victim ID: {victimId}, Damage: {damage}";
        }
    }
    public struct HealingReport : IGunReport
    {
        public ReportType type;
        public byte healerId;
        public byte healedId;
        public ushort healAmount;

        public override string ToString()
        {
            return $"HealingReport: Healer ID: {healerId}, Healed ID: {healedId}, Heal Amount: {healAmount}";
        }
    }
    public struct SSketchReport : IGunReport
    {
        public ReportType type;
        public byte taggerId;
        public byte victimId;
        public ushort damage;

        public override string ToString()
        {
            return $"SSketchReport: Tagger ID: {taggerId}, Victim ID: {victimId}, Damage: {damage}";
        }
    }
    public struct HealthArmorReport : IGunReport
    {
        public ReportType type;
        public byte id;
        public ushort health;
        public ushort armor;

        public override string ToString()
        {
            return $"HealthArmorReport: ID: {id}, Health: {health}, Armor: {armor}";
        }
    }

    public struct BulletReport : IGunReport
    {
        public ReportType type;
        public byte id;
        public byte normalBullets;
        public byte ssketchBullets;

        public override string ToString()
        {
            return $"BulletReport: ID: {id}, Normal Bullets: {normalBullets}, SSketch Bullets: {ssketchBullets}";
        }
    }
    public enum ReportType : byte
    {
        /// For Ping Pong, keeping gun and vest alive, most frequently used
        Ping = 0,
        Pong = 1,

        // For debuffs, health and armor report to the websocket, second most frequently used
        Debuff = 2,
        HealthArmorReport = 3,

        // For damage, healing, sSketch, confirm packet, third frequently used
        ConfirmPacket = 4,
        DamagePacket = 5,
        HealingPacket = 6,
        SSketchPacket = 7,

        // For damage, healing, sSketch, report, fourth frequently used
        DamageReport = 8,
        HealingReport = 9,
        SSketchReport = 10,
        BulletReport = 11,

        // Additional message types, for pairing, match init, least frequently used
        Pairing = 12,
        MappingPlayers = 13,
        VestStatPacket = 14
    };
}

