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
            
            Command command = new Command("SELECT UserId, CompanyStatus, Firstname FROM Users WHERE Email like @email AND UserPassword like @password");
            command.AddParameter("email", loginCredentials.Email);
            command.AddParameter("password", loginCredentials.Password);
            return conn.ExecuteReaderSingle(command, dr => dr.ToSessionInfo());
        }

        // traduit le companyStatus d'un utilisateur en Enum de type Role
        public static Role GetCurrentSessionRole(int UserCompanyStatus)
        {
            return (Role)UserCompanyStatus;
        }

        public static IEnumerable<User> GetTechnicians()
        {
            Command command = new Command("SELECT * FROM Users WHERE CompanyStatus = @cs");
            command.AddParameter("cs", 2);
            return conn.ExecuteReader(command, dr => dr.ToUser());
        }

        public static IEnumerable<User> GetCustomers()
        {
            Command command = new Command("SELECT * FROM Users WHERE CompanyStatus = @cs OR CompanyStatus = @cs2");
            command.AddParameter("cs", 1);
            command.AddParameter("cs2", 2);
            return conn.ExecuteReader(command, dr => dr.ToUser());
        }

        public static bool UserExist(int id)
        {
            Command command = new Command($"SELECT COUNT(*) FROM Users WHERE UserId = @id");
            command.AddParameter("id", id);
            int rowCount = (int)conn.ExecuteScalar(command);
            return rowCount > 0 ? true : false;
        }
    }
}
