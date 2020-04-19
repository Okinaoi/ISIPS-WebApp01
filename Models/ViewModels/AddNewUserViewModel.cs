using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class AddNewUserViewModel : RegisterViewModel
    {
        public override string Status 
        { 
            get => ((Role)User.CompanyStatus).ToString();
            set 
            {
                User.CompanyStatus = (int)((Role)Enum.Parse(typeof(Role), value, true)); 
            }
        }
    }
}
