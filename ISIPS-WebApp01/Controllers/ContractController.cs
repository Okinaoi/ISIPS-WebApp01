using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;
using Models.ViewModels;
using Repository;

namespace ISIPS_WebApp01.Controllers
{
    public class ContractController : Controller
    {
        IRepository<User> users = new UserRepository();
        IRepository<Contract> contracts = new ContractRepository();
        IRepository<Address> addresses = new AddressRepository();

        public IActionResult AllContracts()
        {
            return View();
        }

        public IActionResult CreateContract()
        {
            if (HttpContext.Session.GetInt32("sessionId") != null)
            {
                int st = (int)HttpContext.Session.GetInt32("sessionCompanyStatus");
                if (st == 1 || st == 2)
                {
                    User u = users.Select((int)HttpContext.Session.GetInt32("sessionId"));
                    Contract contract = new Contract();
                    contract.Client = u;
                    return View(new ClientContractViewModel(contract));
                }
                return Unauthorized();
            }
            return View("Index", "Home");
            
        }

        [HttpPost]
        public IActionResult CreateContract(ClientContractViewModel cvm)
        {
            Address add = null;
            Contract c;
            c = cvm.GetContract;
            c.Client = users.Select((int)HttpContext.Session.GetInt32("sessionId"));
            if (cvm.AddressId == 0)
            {
                add = new Address();
                
                add.HouseNumber = cvm.HouseNumber;
                add.StreetName = cvm.StreetName;
                add.PostalCode = cvm.PostalCode;
                add.City = cvm.City;
                add = addresses.Insert(add);
                c = cvm.GetContract;
                c.Address = add;
            }

            else                
                c.Address = c.Client.PrivateAddress;
            
            contracts.Insert(c);
            return RedirectToAction("Index", "Home");

        }
    }
}