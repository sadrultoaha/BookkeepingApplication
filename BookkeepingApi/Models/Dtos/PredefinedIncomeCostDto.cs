using BookkeepingApi.Models.Dtos;

namespace BookkeepingApi.Models.Dtos
{
    public class PredefinedIncomeCostDto
    {
        public virtual IncomeCostDto Income{ get; set; }
        public virtual IncomeCostDto CumulativeIncome { get; set; }
        public virtual IncomeCostDto Cost { get; set; }
        public virtual IncomeCostDto CumulativeCost { get; set; }
        public virtual IncomeCostDto Result { get; set; }

    }
}
