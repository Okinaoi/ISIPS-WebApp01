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
        public static SessionInfo CheckLoginCredentials(LoginDto loginCredentials)
        {
            Connection conn = new Connection(StaticValues.IsipsDbConnectionString);
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
    }
}
