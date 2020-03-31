using Models.DataModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolBox.Mappers
{
    public static class InterventionExtensions
    {
        public static TechnicianInterventionViewModel ToTechInterventionVm(this Intervention inter)
        {
            return new TechnicianInterventionViewModel(inter);
        }

        public static AdminInterventionViewModel ToAdminInterventionVm(this Intervention inter)
        {
            return new AdminInterventionViewModel(inter);
        }
    }
}
