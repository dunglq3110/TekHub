using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Models
{
    public partial class Round : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private DateTime startTime;

        [ObservableProperty]
        private DateTime endTime;

        [ObservableProperty]
        private RoundStage stage;

        public Round()
        {
            Stage = RoundStage.Lobbying;
        }
    }

    public enum RoundStage
    {
        Null = 0,
        Lobbying = 1,
        BuyPhase = 2,
        BattlePhase = 3,
        Finished = 4,
        Paused = 5
    }
}
