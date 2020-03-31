using Models.DataModels;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Mappers
{
    public static class ListExtensions
    {
        public static IEnumerable<TechnicianInterventionViewModel> ToListTechInterventionVm(this List<Intervention> inters)
        {
            return inters.Select(i=>i.ToTechInterventionVm());
        }

        public static IEnumerable<AdminInterventionViewModel> ToListAdminInterventionVm(this List<Intervention> inters)
        {
            return inters.Select(i => i.ToAdminInterventionVm());
        }
    }
}
