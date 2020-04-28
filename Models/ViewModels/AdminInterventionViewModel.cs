
using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class AdminInterventionViewModel
    {
        private Intervention Intervention = new Intervention();

        public AdminInterventionViewModel()
        {
            
        }
        public AdminInterventionViewModel(Intervention inter)
        {
            Intervention = inter;
        }

        public int InterventionId { get => Intervention.InterventionId; set => Intervention.InterventionId = value; }

        public int CustomerId { get => Intervention.Client.UserId; }

        private string _ContractDescription;

        public string ContractDescription
        {
            get { return _ContractDescription; }
            set { _ContractDescription = value; }
        }


        public DateTime StartDate
        {
            get { return Intervention.StartDate; }
            set { Intervention.StartDate = value; }
        }

        public string StartDateString { get => StartDate.ToString("dd/MM/yyyy"); }


        public DateTime EndDate
        {
            get { return Intervention.EndDate.Date; }
            set { Intervention.EndDate = value; }
        }

        public string EndDateString { get => EndDate.ToString("dd/MM/yyyy"); }


        public int Duration
        {
            get { return Intervention.Duration; }
            set { Intervention.Duration = value; }
        }

        public double Price
        {
            get { return Intervention.Price; }
            set { Intervention.Price = value; }
        }

        public bool IsOnGoing
        {
            get { return Intervention.IsOnGoing; }
            set { Intervention.IsOnGoing = value; }
        }

        public int ContractId { get => Intervention.ContractId; set => Intervention.ContractId = value; }

        public int TechnicianId { get => Intervention.Technician.UserId; set => Intervention.Technician.UserId = value; }

        public string TechnicianName { get => Intervention.Technician.Lastname + " " + Intervention.Technician.Firstname; }

        public string TechnicianEmail { get => Intervention.Technician.Email; }

        public string TechnicianPhone { get => Intervention.Technician.Phonenumber; }


        public string CustomerName { get { return Intervention.Client.Lastname + " " + Intervention.Client.Firstname; } }


        public string CustomerEmail { get => Intervention.Client.Email; }

        public string CustomerPhone { get => Intervention.Client.Phonenumber; }

        public string FacturationAddress { get => $"{Intervention.Client.PrivateAddress.HouseNumber}, rue {Intervention.Client.PrivateAddress.StreetName} {Intervention.Client.PrivateAddress.PostalCode} {Intervention.Client.PrivateAddress.City}"; }

        public Address TechnicianAddress { get => Intervention.Technician.PrivateAddress; }

        public string InterventionAddress { get { return $"{Intervention.InterventionAddress.HouseNumber}, " +
                    $"rue {Intervention.InterventionAddress.StreetName} {Intervention.InterventionAddress.PostalCode} " +
                    $"{Intervention.InterventionAddress.City}"; } }

        public Intervention GetIntervention { get => Intervention; }

    }
}
