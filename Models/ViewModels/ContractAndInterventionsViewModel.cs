using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class ContractAndInterventionsViewModel
    {
        public Contract Contract { get; set; }
        public List<Intervention> Interventions { get; set; }
    }
}
