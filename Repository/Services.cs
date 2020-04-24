using Models;
using Models.DataModels;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using ToolBox.DataBaseConnectionTools;
using ToolBox.Mappers;


namespace Repository
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
