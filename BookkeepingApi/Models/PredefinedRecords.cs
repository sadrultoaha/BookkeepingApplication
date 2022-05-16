using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookkeepingApi.Models
{
    public class PredefinedRecords
    {
      public int Id {get; set;}
      public int TypeId {get; set;}
      public DateTime Date {get; set;}
      public decimal Amount {get; set;}
    }
}
