using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccess.Services
{
    public class AdressService
    {
        private readonly AdressRepository _adressRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly RoleRepository _roleRepository;

        public AdressService(AdressRepository adressRepository, CustomerRepository customerRepository, RoleRepository roleRepository)
        {
            _adressRepository = adressRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
        }


        public void CreateNewAdress(AdressEntity entity)
        {
            try
            {
                //x => x.StreetName == entity.StreetName &&
                // x.City == entity.City &&
                // x.PostalCode == entity.PostalCode

                var result = _adressRepository.ReadOneEntity(x => x.StreetName == entity.StreetName &&
                 x.City == entity.City &&
                 x.PostalCode == entity.PostalCode);


                if (result == null)
                {
                    var adress = _adressRepository.Create(new AdressEntity
                    {
                        StreetName = entity.StreetName,
                        City = entity.City,
                        PostalCode = entity.PostalCode,
                    });

                    Console.WriteLine($"Created a new adress");
                    Console.WriteLine($"The StreetName  {entity.StreetName},in city {entity.City} {entity.PostalCode} was created.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"The StreetName  {entity.StreetName},in city {entity.City} {entity.PostalCode} already exist");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ReadAllAdresses()
        {
            try
            {
                List<CustomerEntity> returnedCustomers = _customerRepository.ReadAllEntities().ToList();
                List<AdressEntity> returnedAdresses = _adressRepository.ReadAllEntities().ToList();
                List<RoleEntity> returnedRoles = _roleRepository.ReadAllEntities().ToList();

                if (returnedAdresses != null)
                {
                    Console.Clear();
                    foreach (var entity in returnedAdresses)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine($"Id: {entity.Id}");
                        Console.WriteLine($"StreetName: {entity.StreetName}");
                        Console.WriteLine($"City: {entity.City}");
                        Console.WriteLine($"PostalCode: {entity.PostalCode}");

                        var customersMatchingtheCurrentAdress = returnedCustomers.FindAll(x => x.AdressId == entity.Id);

                        if (customersMatchingtheCurrentAdress.Count != 0)
                        {
                            foreach (var customer in customersMatchingtheCurrentAdress)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"id: {customer.Id}");
                                Console.WriteLine($"Firstname: {customer.FirstName}");
                                Console.WriteLine($"Lastname:{customer.Lastname}");
                                Console.WriteLine($"Email:{customer.Email}");
                                Console.WriteLine($"PhoneNumber:{customer.PhoneNumber}");

                                returnedRoles.FindAll(x => x.Id == customer.RoleId);

                                foreach (var x in returnedRoles)
                                {
                                    if (x.Id == customer.AdressId)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"Id: {x.Id}");
                                        Console.WriteLine($"Rolename: {x.RoleName}");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No customers associated with this Adress");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Nothing found, try creating a customer prior to running this.");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("press any key to return. . .");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void ReadOneAdress(AdressEntity entity)
        {
            var result = _adressRepository.ReadOneEntity(x => x.StreetName == entity.StreetName &&
            x.City == entity.City &&
            x.PostalCode == entity.PostalCode);
            if (result != null)
            {
                Console.Clear();

                var customer = _customerRepository.ReadOneEntity(x => x.AdressId == result.Id);
                var role = _roleRepository.ReadOneEntity(x => x.Id == customer.RoleId);

                Console.WriteLine($"id: {result.Id}");
                Console.WriteLine($"StreetName: {result.StreetName}");
                Console.WriteLine($"City:{result.City}");
                Console.WriteLine($"Email:{result.PostalCode}");
                if (customer != null)
                {
                    Console.WriteLine($"Firstname: {customer.Id}");
                    Console.WriteLine($"Firstname: {customer.FirstName}");
                    Console.WriteLine($"Lastname: {customer.Lastname}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine($"PhoneNumber: {customer.PhoneNumber}");
                }
                else
                {
                    Console.WriteLine("Customer was null for this adress.");
                }

                Console.WriteLine("press any key to return. . .");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nothing found, try creating an adress prior to running this.");
                Console.WriteLine("press any key to return. . .");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void UpdateAdress(AdressEntity entity)
        {
            try
            {
                var returnedAdress = _adressRepository.ReadOneEntity(x => x.StreetName == entity.StreetName &&
                x.City == entity.City &&
                x.PostalCode == entity.PostalCode);

                if (returnedAdress != null)
                {

                    Console.Clear();

                    Console.WriteLine($"Id: {returnedAdress.Id}");
                    Console.WriteLine($"Current streetname: {returnedAdress.StreetName}");
                    Console.WriteLine($"Current City: {returnedAdress.City}");
                    Console.WriteLine($"Current Postalcode: {returnedAdress.PostalCode}");

                    Console.Write("Enter a new streetname:");
                    var newStreetName = Console.ReadLine();
                    Console.Write("Enter a new City:");
                    var newCity = Console.ReadLine();
                    Console.Write("Enter a new Postalcode:");
                    var newPostal = Console.ReadLine();

                    returnedAdress.StreetName = string.IsNullOrEmpty(newStreetName) ? returnedAdress.StreetName : newStreetName;
                    returnedAdress.City = string.IsNullOrEmpty(newCity) ? returnedAdress.City : newCity;
                    returnedAdress.PostalCode = string.IsNullOrEmpty(newPostal) ? returnedAdress.PostalCode : newPostal;


                    // predicate to find entity to update      //entity to update with
                    var success = _adressRepository.Update(z => z.Id == returnedAdress.Id, returnedAdress);


                    if (success != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Updated adress");
                        Console.WriteLine($"Id: {returnedAdress.Id}");
                        Console.WriteLine($"Current streetname: {returnedAdress.StreetName}");
                        Console.WriteLine($"Current City: {returnedAdress.City}");
                        Console.WriteLine($"Current Postalcode: {returnedAdress.PostalCode}");
                        Console.WriteLine("press any key to return. . .");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Adress didnt get updated");
                        Console.WriteLine("press any key to return. . .");
                        Console.ReadKey();
                    }
                }

                else
                {
                    Console.WriteLine($"Adress with {entity.StreetName} {entity.City} {entity.PostalCode} was not found.");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void DeleteAdress(AdressEntity entity)
        {
            try
            {
                var result = _adressRepository.ReadOneEntity(x => x.StreetName == entity.StreetName &&
                x.City == entity.City &&
                x.PostalCode == entity.PostalCode);

                if (result != null)
                {
                    var deletedEntity = _adressRepository.Delete(x => x.StreetName == entity.StreetName);

                    if (deletedEntity)
                    {
                        var adress = _adressRepository.ReadOneEntity(x => x.Id == result.Id);
                        //var customerEntity = _customerRepository.ReadOneEntity(x => x.AdressId == returnedEntity.Id);
                        Console.Clear();
                        Console.WriteLine($"id: {result.Id}");
                        Console.WriteLine($"StreetName: {result.StreetName}");
                        Console.WriteLine($"StreetName: {result.City}");
                        Console.WriteLine($"PostalCode:{result.PostalCode}");

                        Console.WriteLine($"The Adress has been deleted.");
                        Console.WriteLine("press any key to return. . .");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"The input:'{entity.StreetName}' {entity.City} & {entity.PostalCode} was not found in the database.");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
