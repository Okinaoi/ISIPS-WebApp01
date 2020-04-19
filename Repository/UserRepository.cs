using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using ToolBox.DataBaseConnectionTools;
using ToolBox.Mappers;

namespace Repository
{
    public class UserRepository : IRepository<User>
    {
        Connection conn = new Connection(StaticValues.IsipsDbConnectionString);

        public User Insert(User entity)
        {
            AddressRepository addRepo = new AddressRepository();

            Address add = addRepo.Insert(entity.PrivateAddress);

            Command Usercommand = new Command("INSERT INTO Users OUTPUT inserted.* VALUES (@ln, @fn, @bd, " +
                                                                         " @nn, @pn, @mail," +
                                                                         " @pwd, @sex, @cpnstatus, @addId)");
            Usercommand.AddParameter("ln", entity.Lastname);
            Usercommand.AddParameter("fn", entity.Firstname);
            Usercommand.AddParameter("bd", entity.BirthDate);
            Usercommand.AddParameter("nn", entity.NationalNumber);
            Usercommand.AddParameter("pn", entity.Phonenumber);
            Usercommand.AddParameter("mail", entity.Email);
            Usercommand.AddParameter("pwd", entity.Password);
            Usercommand.AddParameter("sex", entity.Sex);
            Usercommand.AddParameter("cpnstatus", entity.CompanyStatus);
            Usercommand.AddParameter("addId", add.AddressId);

            User u = conn.ExecuteReaderSingle(Usercommand, dr => dr.ToUser());
            u.PrivateAddress = add;
            return u;
        }                                                                                           

        public IEnumerable<User> Select()
        {
            Command command = new Command("SELECT U.*, A.* FROM Users U JOIN Addresses A ON A.AddressId = U.PrivateAddressId");
            return conn.ExecuteReader(command, dr => dr.ToUser(withAddress: true));
        }

        public User Select(int id)
        {
            Command command = new Command("SELECT U.*, A.* " +
                                          "FROM Users U " +
                                          "JOIN Addresses A ON A.AddressId = U.PrivateAddressId " +
                                          "WHERE U.UserId = @id");
            command.AddParameter("id", id);
            return conn.ExecuteReaderSingle(command, dr => dr.ToUser(withAddress: true));
        }

        public User Update(User entity)
        {
            AddressRepository ar = new AddressRepository();
            Command command = new Command("UPDATE Users " +
                                          "SET Lastname = @ln," +
                                              "Firstname = @fn," +
                                              "BirthDate = @bd," +
                                              "NationalNumber = @nn," +
                                              "PhoneNumber = @pn," +
                                              "Email = @mail," +
                                              "Sex = @sex," +
                                              $"CompanyStatus = {entity.CompanyStatus}" +
                                              "OUTPUT inserted.* " + 
                                              "WHERE UserId = @uid");
            command.AddParameter("ln", entity.Lastname);
            command.AddParameter("fn", entity.Firstname);
            command.AddParameter("bd", entity.BirthDate);
            command.AddParameter("nn", entity.NationalNumber);
            command.AddParameter("pn", entity.Phonenumber);
            command.AddParameter("mail", entity.Email);
            command.AddParameter("pwd", entity.Password);
            command.AddParameter("sex", entity.Sex);
            command.AddParameter("uid", entity.UserId);
            // TODO : update the address

            Address add = ar.Update(entity.PrivateAddress);

            User u = conn.ExecuteReaderSingle(command, dr => dr.ToUser());
            u.PrivateAddress = add;
            return u;

        }

        public void Delete(int id)
        {
            Command commandCheck = new Command("SELECT COUNT(*) FROM Users WHERE UserId = @id");
            commandCheck.AddParameter("id", id);
            int rowCount = (int)conn.ExecuteScalar(commandCheck);
            Command command = new Command("DELETE FROM Users WHERE UserId = @id");
            command.AddParameter("id", id);
            conn.ExecuteNonQuery(command);
        }
    }
}
