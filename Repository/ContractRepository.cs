using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using ToolBox.DataBaseConnectionTools;
using ToolBox.Mappers;

namespace Repository
{
    public class ContractRepository : IRepository<Contract>
    {
        Connection conn = new Connection(StaticValues.IsipsDbConnectionString);
        public Contract Insert(Contract entity)
        {
            Command command = new Command("INSERT INTO ClientContract OUTPUT inserted.* VALUES (@contractType, @sd, @ed, @dur, @interCount, @isOngoing, @description, @clientId, @addId)");
            command.AddParameter("contractType", entity.ContractType);
            command.AddParameter("sd", entity.StartDate);
            command.AddParameter("ed", entity.EndDate);
            command.AddParameter("dur", entity.Duration);
            command.AddParameter("interCount", entity.InverventionCount);
            command.AddParameter("isOnGoing", entity.IsOnGoing);
            command.AddParameter("description", entity.Description);
            command.AddParameter("clientId", entity.Client.UserId);
            command.AddParameter("addId", entity.Address.AddressId);
            Contract c = conn.ExecuteReaderSingle(command, dr => dr.ToContract());
            Address a = new AddressRepository().Select(c.Address.AddressId);
            User client = new UserRepository().Select(c.Client.UserId);
            c.Address = a;
            c.Client = client;
            return c;

        }

        public IEnumerable<Contract> Select()
        {
            Command command = new Command("SELECT * FROM ClientContract");
            return conn.ExecuteReader(command, dr => dr.ToContract());
        }
                                          

        public Contract Select(int id)
        {
            Command command = new Command("SELECT * FROM ClientContract WHERE ContractId  = @id");
            command.AddParameter("id", id);
            return conn.ExecuteReaderSingle(command, dr => dr.ToContract());
        }

        public Contract Update(Contract enitity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
