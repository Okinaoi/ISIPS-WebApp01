﻿using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using ToolBox.DataBaseConnectionTools;
using ToolBox.Mappers;

namespace Repository
{
    public class InterventionRepository : IRepository<Intervention>, ISpecificRepository<Intervention>
    {
        Connection conn = new Connection(StaticValues.IsipsDbConnectionString);

        public Intervention Insert(Intervention entity)
        {
            Command command = new Command("INSERT INTO Interventions (StartDate, EndDate, Price, WorkDescription, WorkerId, ContractId)  OUTPUT inserted.* VALUES (@sd, @ed, @p, @wd, @wi, @ci)");
            command.AddParameter("sd", entity.StartDate);
            command.AddParameter("ed", entity.EndDate);
            command.AddParameter("p", entity.Price);
            command.AddParameter("wd", entity.Description);
            command.AddParameter("wi", entity.Technician.UserId);
            command.AddParameter("ci", entity.ContractId);
            int intId = conn.ExecuteReaderSingle(command, dr => dr.ToInterventionReduced()).InterventionId;
            return Select(intId);
        }

        public IEnumerable<Intervention> SelectForAdmin()
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
                                          "JOIN Addresses A ON A.AddressId = CC.AddressId ");

            return conn.ExecuteReader(command, dr => dr.ToIntervention());
        }

        public IEnumerable<Intervention> SelectByTechnician(int technicianId)
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
                                          "WHERE WorkerId = @techId");

            command.AddParameter("techId", technicianId);
            return conn.ExecuteReader(command, dr => dr.ToIntervention());
        }

        public IEnumerable<Intervention> Select()
        {
            throw new NotImplementedException();
        }

        public Intervention Select(int id)
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
                                          "WHERE InterventionId = @interventionId");
            command.AddParameter("interventionId", id);
            return conn.ExecuteReaderSingle(command, dr => dr.ToIntervention());
        }

        public Intervention Update(Intervention entity)
        {
            Command command = new Command("UPDATE Interventions " +
                                          "SET StartDate = @sd," +
                                          "EndDate = @ed," +
                                          "Price = @prc," +
                                          "Duration = @dur," +
                                          "IsOnGoing = @iog," +
                                          "WorkDescription = @wd," +
                                          "WorkerId = @wi," +
                                          "ContractId = @ci " +
                                          "WHERE InterventionId = @id");
            command.AddParameter("sd", entity.StartDate);
            command.AddParameter("ed", entity.EndDate);
            command.AddParameter("prc", entity.Price);
            command.AddParameter("dur", entity.Duration);
            command.AddParameter("iog", entity.IsOnGoing);
            command.AddParameter("wd", entity.Description);
            command.AddParameter("wi", entity.Technician.UserId);
            command.AddParameter("ci", entity.ContractId);
            command.AddParameter("id", entity.InterventionId);
            conn.ExecuteNonQuery(command);
            return Select(entity.InterventionId);

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
