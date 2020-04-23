using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.DataModels
{
    public class Contract
    {
        private string _Description;
        public int ContractId { get; set; }
        public int ContractType { get; set; }
        public DateTime StartDate { get; set; } = new DateTime(2000,01,01);
        public DateTime EndDate { get; set; } = new DateTime(2000, 01, 01);
        public string Description
        {
            get
            {
                return string.IsNullOrEmpty(_Description) ? "Le client n'a pas fournis de description lors de sa demande" : _Description;
            }
            set
            {
                _Description = value;
            }
        }
        public int Duration { get; set; }
        public int InverventionCount { get; set; }
        public bool IsOnGoing { get; set; }
        public User Client { get; set; } = new User();
        public Address Address { get; set; } = new Address();
        public List<Intervention> Interventions { get; set; } = new List<Intervention>();
    }
}
