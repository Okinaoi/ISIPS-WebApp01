using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    class TechnicianInterventionViewModel
    {
        public Intervention Intervention { get; set; }
        public User Client { get; set; }
    }
}
