﻿
namespace AccountingPolessUp.Models
{
    public class RankBonus
    {
        public int RankId { get; set; }
        public Rank Rank { get; set; }

        public int BonusId { get; set; }
        public Bonus Bonus { get; set; }
    }
}
