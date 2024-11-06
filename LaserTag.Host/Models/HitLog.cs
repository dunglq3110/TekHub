using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{
    public partial class HitLog : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private Player shooter;

        [ObservableProperty]
        private Player target;

        [ObservableProperty]
        private Round round;

        [ObservableProperty]
        private HitType hitType;

        [ObservableProperty]
        private int damage;

        [ObservableProperty]
        private DateTime time;

        public override string ToString()
        {
            return $"Shooter: {Shooter.Name}, Target: {Target.Name}, Damage: {Damage}, Time: {Time}";
        }
    }

    public enum HitType
    {
        Normal = 0,
        Healing = 1,
        SSketch = 2,
    }


}
