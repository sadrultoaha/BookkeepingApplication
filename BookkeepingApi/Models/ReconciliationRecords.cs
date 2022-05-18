using System.Collections.Generic;

namespace BookkeepingApi.Models
{
    public class ReconciliationRecords
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int Year { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }


        // DTO Fields
        public string ActionName { get; set; }
        public string TypeName { get; set; }
        public virtual List<ReconciliationRecords> ReconciliationRecordList { get; set; }
    }
}
