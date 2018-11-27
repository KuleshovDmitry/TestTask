using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class GameStat
    {
        public enum EndReason
        {
            Escape,
            Dead
        }
        public string name { get; set; }
        public float duration { get; set; }
        public int coin { get; set; }
        public EndReason endReason { get; set; }
        public DateTime date { get; set; }
        public GameStat(){}
        public GameStat(string name, float duration, bool isEscape, int coin)
        {
            this.coin = coin;
            this.name = name;
            this.duration = duration;
            this.date = DateTime.Now;
            if (isEscape)
                this.endReason = EndReason.Escape;
            else
                this.endReason = EndReason.Dead;
        }
    }
}
