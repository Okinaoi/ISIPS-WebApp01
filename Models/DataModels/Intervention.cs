using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DataModels
{
    public class Intervention
    {
        public int InterventionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public bool IsOnGoing { get; set; }
        public Address InterventionAddress { get; set; } = new Address();
        public User Technician { get; set; } = new User();
        public User Client { get; set; } = new User();
    }
}
