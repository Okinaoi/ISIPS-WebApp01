using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DataModels
{
    public class Contract
    {
        public int ContractId { get; set; }
        public int ContractType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public int InverventionCount { get; set; }
        public bool IsOnGoing { get; set; }
        public Address Address { get; set; }
        public List<Intervention> Interventions { get; set; }
    }
}
