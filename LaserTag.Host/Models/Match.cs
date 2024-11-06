using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Models
{
    public partial class Match : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private DateTime startTime;

        [ObservableProperty]
        private DateTime endTime;

        [ObservableProperty]
        private MatchStage stage;

        [ObservableProperty]
        private ObservableCollection<Round> rounds;

        public Match()
        {
            Stage = MatchStage.Lobbying;
            Rounds = new ObservableCollection<Round>();
        }

        public override string ToString()
        {
            return $"Start: {StartTime}, End: {EndTime}";
        }
    }

    public enum MatchStage
    {
        Null = 0,
        Lobbying = 1,
        Started = 2,
        Finished = 3,
    }
}
