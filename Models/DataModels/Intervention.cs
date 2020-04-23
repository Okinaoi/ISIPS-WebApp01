using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DataModels
{
    public class Intervention
    {
        public Intervention()
        {

        }
        public Intervention(int contractId)
        {
            ContractId = contractId;
        }
        public int InterventionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public bool IsOnGoing { get; set; }
        public string Description { get; set; }
        public Address InterventionAddress { get; set; } = new Address();
        public User Technician { get; set; } = new User();
        public User Client { get; set; } = new User();
        public int ContractId { get; set; }
    }
}
