using Models;
using Models.DataModels;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolBox.DataBaseConnectionTools;
using ToolBox.Mappers;


namespace ToolBox
{
    public static class Services
    {
        // utilisé lors du login pour verifier qu'un username et password sont correctes, si il sont correcte la fonction renvois l'id , le prénom et le company
        // status de l'utilisateur , si non elle renvoie null
        static Connection conn = new Connection(StaticValues.IsipsDbConnectionString);
        public static SessionInfo CheckLoginCredentials(LoginDto loginCredentials)
        {
            //vérifie l'email et le password (utilisé au moment du login)   
            Command command = new Command("SELECT UserId, CompanyStatus, Firstname FROM Users WHERE Email like @email AND UserPassword like @password");
            command.AddParameter("email", loginCredentials.Email);
            command.AddParameter("password", loginCredentials.Password);
            return conn.ExecuteReaderSingle(command, dr => dr.ToSessionInfo());
        }

        
        public static Role GetCurrentSessionRole(int UserCompanyStatus)
        {
            // traduit le companyStatus d'un utilisateur en Enum de type Role
            return (Role)UserCompanyStatus;
        }

        public static Address GetAdressFromContract(int contractId)
        {
            // Nous renvoie l'adresse d'un contrat
            Command command = new Command("SELECT * FROM Addresses A " +
                                          "JOIN ClientContract CC ON CC.AddressId = A.AddressId " +
                                          "WHERE CC.ContractId = @id");
            command.AddParameter("id", contractId);
            return conn.ExecuteReaderSingle(command, dr => dr.ToAddress());
        }

        public static IEnumerable<User> GetTechnicians()
        {
            //fct qui return une list de techniciens(sans les admin et customers)
            Command command = new Command("SELECT * FROM Users WHERE CompanyStatus = @cs");
            command.AddParameter("cs", 2);
            return conn.ExecuteReader(command, dr => dr.ToUser());
        }

        public static IEnumerable<User> GetCustomers()
        {
            //fct qui return une liste de customers (les utilisateurs sans les admin donc)
            Command command = new Command("SELECT * FROM Users WHERE CompanyStatus = @cs OR CompanyStatus = @cs2");
            command.AddParameter("cs", 1);
            command.AddParameter("cs2", 2);
            return conn.ExecuteReader(command, dr => dr.ToUser());
        }

        public static IEnumerable<Intervention> GetInterventionsFromContractId(int id)
        {
            Command command = new Command("SELECT I.* , W.UserId as tech_UserId, W.Firstname as tech_Firstname," +
                                          "W.Lastname as tech_Lastname, W.Email as tech_Email, W.BirthDate as tech_BirthDate," +
                                          "W.CompanyStatus as tech_CompanyStatus, W.NationalNumber as tech_NationalNumber," +
                                          "W.Phonenumber as tech_PhoneNumber, W.Sex as tech_Sex," +
                                          "WA.AddressId as tech_AddressId, WA.HouseNumber as tech_HouseNumber," +
                                          "WA.StreetName as tech_StreetName, WA.City as tech_City, WA.PostalCode as tech_PostalCode," +
                                          "C.UserId as client_UserId, C.Firstname as client_Firstname, C.Lastname as client_Lastname, C.BirthDate as client_BirthDate," +
                                          "C.Email as client_Email, C.CompanyStatus as client_CompanyStatus, C.NationalNumber as client_NationalNumber," +
                                          "C.Phonenumber as client_PhoneNumber, C.Sex as client_Sex, CA.AddressId as client_AddressId," +
                                          "CA.HouseNumber as client_HouseNumber, CA.StreetName as client_StreetName," +
                                          "CA.City as client_City, CA.PostalCode as client_PostalCode," +
                                          "A.AddressId as inter_AddressId, A.HouseNumber as inter_HouseNumber," +
                                          "A.StreetName as inter_StreetName, A.City as inter_City, A.PostalCode as inter_PostalCode " +
                                          "FROM Interventions I " +
                                          "JOIN ClientContract CC ON CC.ContractId = I.ContractId " +
                                          "JOIN Users W ON W.UserId = I.WorkerId " +
                                          "JOIN Users C ON C.UserId = CC.ClientId " +
                                          "JOIN Addresses CA ON CA.AddressId = C.PrivateAddressId " +
                                          "JOIN Addresses WA ON WA.AddressId = W.PrivateAddressId " +
                                          "JOIN Addresses A ON A.AddressId = CC.AddressId " +
                                          "WHERE I.ContractId = @id");
            command.AddParameter("id", id);
            return conn.ExecuteReader(command, dr => dr.ToIntervention());
        }

        public static IEnumerable<Contract> GetPendingContracts()
        {
            //fct qui return la liste des demandes de contrats de la part des clients
            Command command = new Command("SELECT CC.*, U.UserId, U.LastName, U.FirstName FROM ClientContract CC JOIN Users U ON U.UserId = CC.ClientId WHERE CC.ContractId NOT IN (SELECT ContractId FROM Interventions)");
            return conn.ExecuteReader(command, dr => dr.ToContract(withUser:true));
        }

        public static IEnumerable<Contract> GetOnGoingContracts()
        {
            //fct qui nous donne la liste des contrats en cours (pas les demande de contrat)
            Command command = new Command("SELECT CC.*, U.UserId, U.LastName, U.FirstName FROM ClientContract CC " +
                                          "RIGHT JOIN Interventions I ON I.ContractId = CC.ContractId " +
                                          "JOIN Users U ON U.UserId = CC.ClientId");
            return conn.ExecuteReader(command, dr => dr.ToContract(withUser:true));
        }

        public static IEnumerable<Contract> GetContractsFromUserId(int id)
        {
            Command command = new Command("SELECT * FROM ClientContract WHERE ClientId = @id");
            command.AddParameter("id", id);
            var contracts = conn.ExecuteReader(command, dr => dr.ToContract());
            return contracts.Where(c => c.Interventions.Count() > 0);
                                         
        }

        public static bool UserExist(int id)
        {
            //fct qui return true si un certain utilisateur existe
            Command command = new Command($"SELECT COUNT(*) FROM Users WHERE UserId = @id");
            command.AddParameter("id", id);
            int rowCount = (int)conn.ExecuteScalar(command);
            return rowCount > 0 ? true : false;
        }
    }
}
