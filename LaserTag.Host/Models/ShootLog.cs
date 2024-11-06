using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{

    public partial class ShootLog : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private Player shooter;

        [ObservableProperty]
        private Round round;

        [ObservableProperty]
        private DateTime time;

        // Constructor


        public override string ToString()
        {
            return $"Shooter: {Shooter.Name}, Round: {Round.Id}, Time: {Time}";
        }
    }
}
