using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class ClientContractViewModel
    {
        private Contract Contract = new Contract();
        public ClientContractViewModel(Contract contract)
        {
            Contract = contract;
        }

        public ClientContractViewModel()
        {

        }

        public int ClientId
        {
            get { return Contract.Client.UserId; }
        }

        public Address ClientPrivateAddress
        {
            get { return Contract.Client.PrivateAddress; }
            set { Contract.Client.PrivateAddress = value; }
        }
        

        public string ClientDescription
        {
            get { return Contract.Description; }
            set { Contract.Description = value; }
        }

        public bool InterventionAddressIsClientPrivateAddress { get; set; } = false;

        public int AddressId
        {
            get { return Contract.Address.AddressId; }
            set { Contract.Address.AddressId = value; }
        }


        public int HouseNumber
        {
            get { return Contract.Address.HouseNumber; }
            set { Contract.Address.HouseNumber = value; }
        }

        public string StreetName
        {
            get { return Contract.Address.StreetName; }
            set { Contract.Address.StreetName = value; }
        }

        public string PostalCode
        {
            get { return Contract.Address.PostalCode; }
            set { Contract.Address.PostalCode = value; }
        }      

        public string City
        {
            get { return Contract.Address.City; }
            set { Contract.Address.City = value; }
        }

        public Contract GetContract { get => Contract; }
    }
}
