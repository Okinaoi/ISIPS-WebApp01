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

        public DateTime EndDate
        {
            get { return Intervention.EndDate; }
            set { Intervention.EndDate = value; }
        }

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

        public string TechnicianName { get => Intervention.Technician.Lastname + " " + Intervention.Technician.Firstname; }

        public string CustomerName { get { return Intervention.Client.Lastname + " " + Intervention.Client.Firstname; } }

        public Address FacturationAddress { get => Intervention.Client.PrivateAddress; }

        public Address TechnicianAddress { get => Intervention.Technician.PrivateAddress; }

        public Address InterventionAddress { get { return Intervention.InterventionAddress; } }

    }
}
