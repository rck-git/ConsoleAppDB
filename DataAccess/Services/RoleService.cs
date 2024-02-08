using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class RoleService
    {
        private readonly AdressRepository _adressRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly RoleRepository _roleRepository;


        public RoleService(AdressRepository adressRepository, CustomerRepository customerRepository, RoleRepository roleRepository)
        {
            _adressRepository = adressRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;

        }
        public void CreateNewRole(RoleEntity form)
        {
            try
            {
                Console.Clear();
                if (!_roleRepository.Exists(x => x.RoleName == form.RoleName))
                {
                    var roleEntity = _roleRepository.Create(new RoleEntity
                    {
                        RoleName = form.RoleName
                    });


                    if (roleEntity != null)
                    {
                        Console.WriteLine($"Created a new role name");
                        Console.WriteLine($".Role Name =  {form.RoleName}");
                        Console.WriteLine($"productEntity.Lastname =  {roleEntity.Id}");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"The role name {form.RoleName} already exist");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ReadOneRole(string rolename)
        {
            try
            {
                Console.Clear();
                rolename.Trim();
                var count = 0;
                var role = _roleRepository.ReadOneEntity(x => x.RoleName == rolename);

                if (role != null)
                {
                    //x => x.RoleId == role.Id
                    var customers = _customerRepository.ReadAllEntities();
                    Console.Clear();
                    Console.WriteLine($"RoleId: {role.Id}");
                    Console.WriteLine($"Role: {role.RoleName}");

                    if (customers != null)
                    {
                        foreach (var custmr in customers)
                        {
                            if (custmr.Id == role.Id)
                            {
                                Console.WriteLine($"id: {custmr.Id}");
                                Console.WriteLine($"Firstname: {custmr.FirstName}");
                                Console.WriteLine($"Lastname:{custmr.Lastname}");
                                Console.WriteLine($"Email:{custmr.Email}");
                                Console.WriteLine($"PhoneNumber:{custmr.PhoneNumber}");
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No customers associated with this role");

                        }
                    }

                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($"The input:'{rolename}' was not found in the database.");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void ReadAllRoles()
        {
            try
            {
                List<RoleEntity> returnedRoles = _roleRepository.ReadAllEntities().ToList();
                List<AdressEntity> returnedAdresses = _adressRepository.ReadAllEntities().ToList();

                if (returnedRoles != null)
                {
                    List<CustomerEntity> returnedCustomers = _customerRepository.ReadAllEntities().ToList();
                    Console.Clear();

                    foreach (var entity in returnedRoles)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine($"RoleId: {entity.Id}");
                        Console.WriteLine($"Role: {entity.RoleName}");
                        //var customer = _customerRepository.ReadOneEntity(x => x.RoleId == entity.Id);
                        var customersMatchingtheCurrentRole = returnedCustomers.FindAll(x => x.RoleId == entity.Id);

                        if (customersMatchingtheCurrentRole.Count != 0)
                        { /*!= null*/
                            foreach (var customer in customersMatchingtheCurrentRole)
                            {

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"id: {customer.Id}");
                                Console.WriteLine($"Firstname: {customer.FirstName}");
                                Console.WriteLine($"Lastname:{customer.Lastname}");
                                Console.WriteLine($"Email:{customer.Email}");
                                Console.WriteLine($"PhoneNumber:{customer.PhoneNumber}");

                                var adress = returnedAdresses.FindAll(x => x.Id == customer.AdressId);

                                foreach (var x in returnedAdresses)
                                {
                                    if (x.Id == customer.AdressId)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"AdressId: {x.Id}");
                                        Console.WriteLine($"StreetName: {x.StreetName}");
                                        Console.WriteLine($"City: {x.City}");
                                        Console.WriteLine($"Postalcode: {x.PostalCode}");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No customers associated with this role");
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

        public void UpdateRoles(string rolename)
        {
            try
            {
                Console.Clear();
                rolename.Trim();
                var returnedRole = _roleRepository.ReadOneEntity(x => x.RoleName == rolename);
                Console.Clear();

                Console.WriteLine($"Current role: {returnedRole.RoleName}");
                Console.Write("Enter a new role:");

                var newRole = Console.ReadLine();

                if (!_roleRepository.Exists(x => x.RoleName == newRole))
                {
                    returnedRole.RoleName = newRole!;

                    // predicate to find entity to update      //entity to update with
                    var success = _roleRepository.Update(z => z.Id == returnedRole.Id, returnedRole);

                    if (success != null)
                    {
                        Console.Clear();
                        Console.WriteLine($"New role name:{newRole}");
                        Console.WriteLine("press any key to return. . .");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Role didnt get updated");
                    }
                }
                else
                {
                    Console.WriteLine("The role name must be unique");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void DeleteRole(string rolename)
        {
            try
            {
                Console.Clear();
                rolename.Trim();

                var exists = _roleRepository.Exists(x => x.RoleName == rolename);

                if (exists)
                {
                    var returnedEntity = _roleRepository.ReadOneEntity(x => x.RoleName == rolename);
                    var deletedEntity = _roleRepository.Delete(x => x.RoleName == rolename);
                    if (deletedEntity)
                    {
                        Console.Clear();
                        Console.WriteLine($"RoleId: {returnedEntity.Id}");
                        Console.WriteLine($"Role: {returnedEntity.RoleName}");
                        Console.WriteLine($"The role has been deleted.");
                        Console.WriteLine("press any key to return. . .");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine($"The input:'{rolename}' was not found in the database.");
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
