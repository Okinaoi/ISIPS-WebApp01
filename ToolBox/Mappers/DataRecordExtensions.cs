using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ToolBox.Mappers
{
    public static class DataRecordExtensions
    {
        public static SessionInfo ToSessionInfo(this IDataRecord dr)
        {
            SessionInfo si = null;
            if (((DbDataReader)dr).HasRows)
            {
                si = new SessionInfo();
                si.Firstname = dr["Firstname"].ToString();
                si.UserId = (int)dr["UserId"];
                si.Status = (int)dr["CompanyStatus"];
            }
            return si;
        }

        public static User ToUser(this IDataRecord dr, bool withAddress = false)
        {
            User u = null;
            if (((DbDataReader)dr).HasRows)
            {
                u = new User();
                u.UserId = (int)dr["UserId"];
                u.Lastname = dr["Lastname"].ToString();
                u.Firstname = dr["Firstname"].ToString();
                u.BirthDate = (DateTime)dr["BirthDate"];
                u.CompanyStatus = (int)dr["CompanyStatus"];
                u.Email = dr["Email"].ToString();
                u.Phonenumber = dr["PhoneNumber"].ToString();
                u.Sex = dr["Sex"].ToString();
                u.PrivateAddress.AddressId = (int)dr["PrivateAddressId"];
                u.NationalNumber = dr["NationalNumber"].ToString();
                if (withAddress)
                {
                    u.PrivateAddress.HouseNumber = (int)dr["HouseNumber"];
                    u.PrivateAddress.StreetName = dr["StreetName"].ToString();
                    u.PrivateAddress.City = dr["City"].ToString();
                    u.PrivateAddress.PostalCode = dr["PostalCode"].ToString();
                }

            }
            return u;
        }

        public static Address ToAddress(this IDataRecord dr)
        {
            Address add = null;
            if (((DbDataReader)dr).HasRows)
            {
                add = new Address();
                add.AddressId = (int)dr["AddressId"];
                add.HouseNumber = (int)dr["HouseNumber"];
                add.StreetName = dr["StreetName"].ToString();
                add.City = dr["City"].ToString();
                add.PostalCode = dr["PostalCode"].ToString();
            }
            return add;
        }


        public static Contract ToContract(this IDataRecord dr, bool withUser = false)
        {
            Contract con = null;
            
            if (((DbDataReader)dr).HasRows)
            {

                con = new Contract();
                con.ContractId = (int)dr["ContractId"];
                con.ContractType = (int)dr["ContractType"];
                con.Address.AddressId = (int)dr["AddressId"];
                con.Client.UserId = (int)dr["ClientId"];
                con.Description = dr["WorkDescription"].ToString();
                con.IsOnGoing = ((int)dr["IsOnGoing"]).ToBoolean();
                con.Duration = (int)dr["Duration"];
                con.InverventionCount = (int)dr["InterventionCount"];
                con.Interventions = Services.GetInterventionsFromContractId(con.ContractId).ToList();
                if (withUser)
                {
                    con.Client.UserId = (int)dr["UserId"];
                    con.Client.Firstname = dr["Firstname"].ToString();
                    con.Client.Lastname = dr["Lastname"].ToString();
                }
            }
            return con;
        }

        public static Intervention ToIntervention(this IDataRecord dr)
        {
            Intervention inter = null;
            if (((DbDataReader)dr).HasRows)
            {
                inter = new Intervention();
                inter.InterventionId = (int)dr["InterventionId"];
                inter.Price = (double)dr["Price"];
                inter.StartDate = (DateTime)dr["StartDate"];
                inter.EndDate = (DateTime)dr["EndDate"];
                inter.Duration = (int)dr["Duration"];
                inter.IsOnGoing = ((int)dr["IsOnGoing"]).ToBoolean();
                inter.Description = dr["WorkDescription"].ToString();
                inter.ContractId = (int)dr["ContractId"];
                inter.Technician.UserId = (int)dr["tech_UserId"];
                inter.Technician.Firstname = dr["tech_Firstname"].ToString();
                inter.Technician.Lastname = dr["tech_Lastname"].ToString();
                inter.Technician.NationalNumber = dr["tech_NationalNumber"].ToString();
                inter.Technician.Phonenumber = dr["tech_PhoneNumber"].ToString();
                inter.Technician.Sex = dr["tech_Sex"].ToString();
                inter.Technician.CompanyStatus = (int)dr["tech_CompanyStatus"];
                inter.Technician.BirthDate = (DateTime)dr["tech_BirthDate"];
                inter.Technician.Email = dr["tech_Email"].ToString();
                inter.Technician.PrivateAddress.AddressId = (int)dr["tech_AddressId"];
                inter.Technician.PrivateAddress.HouseNumber = (int)dr["tech_HouseNumber"];
                inter.Technician.PrivateAddress.StreetName = dr["tech_StreetName"].ToString();
                inter.Technician.PrivateAddress.City = dr["tech_City"].ToString();
                inter.Technician.PrivateAddress.PostalCode = dr["inter_PostalCode"].ToString();
                inter.Client.UserId = (int)dr["client_UserId"];
                inter.Client.Firstname = dr["client_Firstname"].ToString();
                inter.Client.Lastname = dr["client_Lastname"].ToString();
                inter.Client.NationalNumber = dr["client_NationalNumber"].ToString();
                inter.Client.Phonenumber = dr["client_PhoneNumber"].ToString();
                inter.Client.Sex = dr["client_Sex"].ToString();
                inter.Client.CompanyStatus = (int)dr["client_CompanyStatus"];
                inter.Client.BirthDate = (DateTime)dr["client_BirthDate"];
                inter.Client.Email = dr["client_Email"].ToString();
                inter.Client.PrivateAddress.AddressId = (int)dr["client_AddressId"];
                inter.Client.PrivateAddress.HouseNumber = (int)dr["client_HouseNumber"];
                inter.Client.PrivateAddress.StreetName = dr["client_StreetName"].ToString();
                inter.Client.PrivateAddress.City = dr["client_City"].ToString();
                inter.Client.PrivateAddress.PostalCode = dr["client_PostalCode"].ToString();
                inter.InterventionAddress.AddressId = (int)dr["inter_AddressId"];
                inter.InterventionAddress.City = dr["inter_City"].ToString();
                inter.InterventionAddress.StreetName = dr["inter_StreetName"].ToString();
                inter.InterventionAddress.HouseNumber = (int)dr["inter_HouseNumber"];
                inter.InterventionAddress.PostalCode = dr["inter_PostalCode"].ToString();
            }
            return inter;
        }

        public static Intervention ToInterventionReduced(this IDataRecord dr)
        {
            Intervention inter = null;
            if (((DbDataReader)dr).HasRows)
            {
                inter = new Intervention();
                inter.InterventionId = (int)dr["InterventionId"];
                inter.Price = (double)dr["Price"];
                inter.StartDate = (DateTime)dr["StartDate"];
                inter.EndDate = (DateTime)dr["EndDate"];
                inter.Duration = (int)dr["Duration"];
                inter.IsOnGoing = ((int)dr["IsOnGoing"]).ToBoolean();
                inter.Description = dr["WorkDescription"].ToString();
                inter.ContractId = (int)dr["ContractId"];
            }
            return inter;
        }
    }
}
