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
        public static SessionInfo CheckLoginCredentials(LoginDto loginCredentials)
        {
            Connection conn = new Connection(StaticValues.IsipsDbConnectionString);
            Command command = new Command("SELECT UserId, CompanyStatus, Firstname FROM Users WHERE Email like @email AND UserPassword like @password");
            command.AddParameter("email", loginCredentials.Email);
            command.AddParameter("password", loginCredentials.Password);
            return conn.ExecuteReaderSingle(command, dr => dr.ToSessionInfo());
        }

        public static Role GetCurrentSessionRole(int UserCompanyStatus)
        {
            return (Role)UserCompanyStatus;
        }
    }
}
