using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using ToolBox.DataBaseConnectionTools;
using ToolBox.Mappers;

namespace Repository
{
    public class AddressRepository : IRepository<Address>
    {
        Connection conn = new Connection(StaticValues.IsipsDbConnectionString);

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Address Insert(Address entity)
        {
            Command AddressCommand = new Command("INSERT INTO Addresses OUTPUT inserted.* VALUES(@hn, @sn, @city, @pc)");
            AddressCommand.AddParameter("hn", entity.HouseNumber);
            AddressCommand.AddParameter("sn", entity.StreetName);
            AddressCommand.AddParameter("city", entity.City);
            AddressCommand.AddParameter("pc", entity.PostalCode);
            return conn.ExecuteReaderSingle(AddressCommand, dr => dr.ToAddress());
        }

        public IEnumerable<Address> Select()
        {
            Command command = new Command("SELECT * FROM Addresses");
            return conn.ExecuteReader(command, dr => dr.ToAddress());
        }

        public Address Select(int id)
        {
            Command command = new Command("SELECT * FROM Addresses WHERE AddressId = @id");
            command.AddParameter("id", id);
            return conn.ExecuteReaderSingle(command, dr => dr.ToAddress());
        }

        public Address Update(Address enitity)
        {
            Command command = new Command("UPDATE Addresses" +
                                          "SET HouseNumber = @hn" +
                                              "StreetName = @sn" +
                                              "City = @city" +
                                              "PostalCode = @pc" +
                                              "OUTPUT inserted.*" +
                                              "WHERE AddressId = @addId");
            command.AddParameter("hn", enitity.HouseNumber);
            command.AddParameter("sn", enitity.StreetName);
            command.AddParameter("city", enitity.City);
            command.AddParameter("pc", enitity.PostalCode);
            command.AddParameter("addId", enitity.AddressId);
            return conn.ExecuteReaderSingle(command, dr => dr.ToAddress());

        }
    }
}
