using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class AdminInterventionViewModel
    {
        private Intervention Intervention;

        public AdminInterventionViewModel(Intervention inter)
        {
            Intervention = inter;
        }

        public int InterventionId { get => Intervention.InterventionId; }

        public int CustomerId { get => Intervention.Client.UserId; }

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

        public string Description
        {
            get { return Intervention.Description; }
            set { Intervention.Description = value; }
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

        public int TechnicianId { get => Intervention.Technician.UserId; }

        public string TechnicianName { get => Intervention.Technician.Lastname + " " + Intervention.Technician.Firstname; }

        public string TechnicianEmail { get => Intervention.Technician.Email; }

        public string TechnicianPhone { get => Intervention.Technician.Phonenumber; }

        public string CustomerName { get { return Intervention.Client.Lastname + " " + Intervention.Client.Firstname; } }

        public string CustomerEmail { get => Intervention.Client.Email; }

        public string CustomerPhone { get => Intervention.Client.Phonenumber; }

        public Address FacturationAddress { get => Intervention.Client.PrivateAddress; }

        public Address TechnicianAddress { get => Intervention.Technician.PrivateAddress; }

        public Address InterventionAddress { get { return Intervention.InterventionAddress; } }

    }
}
