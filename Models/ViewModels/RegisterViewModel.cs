using Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
    public class RegisterViewModel
    {
        public User User { get; set; }

        public string UserPassword { get => User.Password; set => User.Password = value; }

        [Compare("UserPassword", ErrorMessage ="Les mots de passe de corresponde pas")]
        public string ConfirmUserPassword { get; set; }

        public virtual string Status
        {
            get
            {
                return User.CompanyStatus == 1 ? "Client" : "Technician";
            }
            set
            {
                User.CompanyStatus = value == "Client" ? 1 : 2;
            }
        }
    }
}
