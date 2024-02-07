
using DataAccess.Repositories;
using DataAccess.Models;
using DataAccess.Entities;
using System.Diagnostics;
using static Dapper.SqlMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace DataAccess.Services;

//tar hand om den "stora logiken" både att skapa själva adressen och customer och sätta role
public class CustomerService
{
    private readonly AdressRepository _adressRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly RoleRepository _roleRepository;
 
    public CustomerService(AdressRepository adressRepository, CustomerRepository customerRepository, RoleRepository roleRepository)
    {
        _adressRepository = adressRepository;
        _customerRepository = customerRepository;
        _roleRepository = roleRepository;
       
    }

    public bool CreateCustomer(CustomerRegistration form)
    {
        try
        {
            
            if (!_customerRepository.Exists(x => x.Email == form.Email))
            {
                          
                var roleEntity = _roleRepository.ReadOneEntity(x => x.RoleName == form.RoleName);
                if (roleEntity == null)
                {
                    roleEntity = _roleRepository.Create(new RoleEntity
                    {
                        RoleName = form.RoleName,
                    });
                    
                }
                var adressEntity = _adressRepository.ReadOneEntity(x => x.StreetName == form.StreetName);
                if (adressEntity == null)
                {
                    adressEntity = _adressRepository.Create(new AdressEntity
                    {
                        StreetName = form.StreetName,
                        City = form.City,
                        PostalCode = form.PostalCode,
                    });
                }
                var customerEntity = new CustomerEntity
                {
                    FirstName = form.FirstName,
                    Lastname = form.Lastname,
                    Email = form.Email,
                    PhoneNumber = form.PhoneNumber,
                    RoleId = roleEntity.Id,
                    AdressId = adressEntity.Id,
                };


                var result = _customerRepository.Create(customerEntity);
                if (result != null)
                {
                    Console.Clear();
                    Console.WriteLine($"CreateProduct true");
                    Console.WriteLine($"customerEntity.FirstName =  {customerEntity.FirstName}");
                    Console.WriteLine($"customerEntity.Lastname =  {customerEntity.Lastname}");
                    Console.WriteLine($"customerEntity.Email =   {customerEntity.Email}");
                    Console.WriteLine($"customerEntity.PhoneNumber =   {customerEntity.PhoneNumber}");

                    Console.WriteLine($"customerEntity.Id =   {adressEntity.Id}");
                    Console.WriteLine($"customerEntity.StreetName =  {adressEntity.StreetName}");
                    Console.WriteLine($"customerEntity.City =  {adressEntity.City}");
                    Console.WriteLine($"customerEntity.PostalCode =  {adressEntity.PostalCode}");

                    Console.WriteLine($"roleEntity.RoleId =  {roleEntity.Id}");
                    Console.WriteLine($"roleEntity.RoleName =  {roleEntity.RoleName}");
                    Console.ReadKey();
                    return true;
                }
            }
            else { Console.WriteLine("The email adress already exist, it has to be unique."); }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
    
    public void ReadOneCustomer(string email)
    {
        try
        {
            email.Trim();
                        
            var entity = _customerRepository.ReadOneEntity(x => x.Email == email);
            if (entity != null)
            {
                var adress = _adressRepository.ReadOneEntity(x => x.Id == entity.AdressId);
                var role = _roleRepository.ReadOneEntity(x => x.Id == entity.RoleId);
                Console.Clear();
                Console.WriteLine($"id: {entity.Id}");
                Console.WriteLine($"Firstname: {entity.FirstName}");
                Console.WriteLine($"Lastname:{entity.Lastname}");
                Console.WriteLine($"Email:{entity.Email}");
                Console.WriteLine($"PhoneNumber:{entity.PhoneNumber}");

                if (adress != null)
                {
                    Console.WriteLine($"AdressId: {adress.Id}");
                    Console.WriteLine($"StreetName: {adress.StreetName}");
                    Console.WriteLine($"City: {adress.City}");
                    Console.WriteLine($"Postalcode: {adress.PostalCode}");
                }
                else
                {
                    Console.WriteLine("Adress was null for Customer.");
                }
                if (role != null)
                {
                    Console.WriteLine($"RoleId: {role.Id}");
                    Console.WriteLine($"Role: {role.RoleName}");
                }
                else
                {
                    Console.WriteLine("Adress was null for Customer.");
                }

                Console.WriteLine("press any key to return. . .");
                Console.ReadKey();
                Console.Clear();
            }
            else 
            {
                Console.Clear();
                Console.WriteLine($"The input:'{email}' was not found in the database.");
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
    public void ReadAllCustomers()
    {   
        List<CustomerEntity> returnedCustomers = _customerRepository.ReadAllEntities().ToList();
        List<AdressEntity> returnedAdresses = _adressRepository.ReadAllEntities().ToList();
        List<RoleEntity> returnedRoles = _roleRepository.ReadAllEntities().ToList();

        if (returnedCustomers != null && returnedAdresses != null && returnedRoles != null)
        {
            Console.Clear();
            foreach (var entity in returnedCustomers)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var matchingAdress = returnedAdresses.FindAll(x => x.Id == entity.AdressId);
                var matchingRole = returnedRoles.FindAll(x => x.Id == entity.RoleId);
                Console.WriteLine("-------------------------------------");
                Console.WriteLine($"id: {entity.Id}");
                Console.WriteLine($"Firstname: {entity.FirstName}");
                Console.WriteLine($"Lastname:{entity.Lastname}");
                Console.WriteLine($"Email:{entity.Email}");
                Console.WriteLine($"PhoneNumber:{entity.PhoneNumber}");

               if(matchingAdress.Count != 0)
                {
                    foreach (var adress in matchingAdress)
                    {
                        Console.WriteLine($"Adress Id: {adress.Id}");
                        Console.WriteLine($"StreetName: {adress.StreetName}");
                        Console.WriteLine($"City: {adress.City}");
                        Console.WriteLine($"Postalcode: {adress.PostalCode}");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No matching adress for the customer.");
                }
                if (matchingRole.Count != 0)
                {
                    foreach (var role in matchingRole)
                    {
                        Console.WriteLine($"Role Id: {role.Id}");
                        Console.WriteLine($"Role: {role.RoleName}");
                    }
                }
                 else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No matching adress for the customer.");
                }
            }
            Console.WriteLine("press any key to return. . .");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Nothing found, try creating a customer prior to running this.");
            Console.WriteLine("press any key to return. . .");
            Console.ReadKey();
            Console.Clear();
        }
    }
    public bool UpdateCustomer(CustomerRegistration form)
    {   
        try
        {
            var returnedEntity = _customerRepository.ReadOneEntity(x => x.Email == form.Email);
            var returnedAdress = _adressRepository.ReadOneEntity(x => x.Id == returnedEntity.AdressId);
            var returnedRole = _roleRepository.ReadOneEntity(x => x.Id == returnedEntity.RoleId);
            Console.Clear();

            if (_customerRepository.Exists(x => x.Email == returnedEntity.Email))
            {
                int specify = 0;
                
                if (specify != 1 || specify != 2 || specify != 3)
                {
                    Console.Clear();
                    Console.WriteLine("What do you want to update?");
                    Console.WriteLine("1. Update customer adress");
                    Console.WriteLine("2. Update customer role");
                    Console.WriteLine("3. Update customer details");
                    Console.WriteLine();

                    Console.WriteLine($"id: {returnedEntity.Id}");
                    Console.WriteLine($"Firstname: {returnedEntity.FirstName}");
                    Console.WriteLine($"Lastname:{returnedEntity.Lastname}");
                    Console.WriteLine($"Email:{returnedEntity.Email}");
                    Console.WriteLine($"PhoneNumber:{returnedEntity.PhoneNumber}");

                    Console.WriteLine($"AdressId: {returnedAdress.Id}");
                    Console.WriteLine($"StreetName: {returnedAdress.StreetName}");
                    Console.WriteLine($"City: {returnedAdress.City}");
                    Console.WriteLine($"PostalCode: {returnedAdress.PostalCode}");

                    Console.WriteLine($"RoleId: {returnedRole.Id}");
                    Console.WriteLine($"Role: {returnedRole.RoleName}");
                    specify = Convert.ToInt32(Console.ReadLine());
                }
                if (specify == 1)
                {
                    try
                    {
                        var adressEntity = _adressRepository.ReadOneEntity(x => x.StreetName == returnedAdress.StreetName);
                        Console.Clear();

                        Console.WriteLine($"Current StreetName:{returnedAdress.StreetName} ,City: {returnedAdress.City}, PostalCode: {returnedAdress.PostalCode}");   
                        
                        Console.Write("Enter a new StreetName:");
                        var newAdress = Console.ReadLine();
                        Console.Write("Enter a new City:");
                        var newCity = Console.ReadLine();
                        Console.Write("Enter a new Postalcode:");
                        var newPostalcode = Console.ReadLine();
                      
                        if (_adressRepository.Exists(x => x.StreetName == newAdress) && _adressRepository.Exists(x => x.PostalCode == newPostalcode) && _adressRepository.Exists(x => x.City == newCity))
                        {
                            var existingAdressEntity = _adressRepository.ReadOneEntity(x => x.StreetName == newAdress);

                            //edit Customer AdressId to the existing adressEntity.ID
                            returnedEntity.AdressId = existingAdressEntity.Id;
                        }

                        if (!_adressRepository.Exists(x => x.StreetName == newAdress) || _adressRepository.Exists(x => x.PostalCode == newPostalcode) || _adressRepository.Exists(x => x.City == newCity))
                        {
                            adressEntity = _adressRepository.Create(new AdressEntity
                            {
                                StreetName = newAdress!,
                                City = newCity!,
                                PostalCode = newPostalcode!,
                            });

                            //edit Customer AdressId to the newly created adressEntity.Id.
                            returnedEntity.AdressId = adressEntity.Id;
                        }
                        // predicate to find entity to update      //entity to update with
                        var success = _customerRepository.Update(z => z.Id == returnedEntity.Id, returnedEntity);

                        if (success != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"For customer: {returnedEntity.FirstName}");
                            Console.WriteLine($"New Streetname:{newAdress}");
                            Console.WriteLine($"New City:{newCity}");
                            Console.WriteLine($"New Streetname:{newPostalcode}");
                            Console.WriteLine("press any key to return. . .");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("New role didnt get updated");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                if (specify == 2)
                {
                    try
                    {
                        var roleEntity = _roleRepository.ReadOneEntity(x => x.RoleName == returnedRole.RoleName);
                        Console.Clear();

                        Console.WriteLine($"Current role: {returnedRole.RoleName}");
                        Console.Write("Enter a new role:");

                        var newRole = Console.ReadLine();
                        if (_roleRepository.Exists(x => x.RoleName == newRole))
                        {
                            var existingroleEntity = _roleRepository.ReadOneEntity(x => x.RoleName == newRole);

                            //edit Customer RoleId to the existing roleEntity.ID
                            returnedEntity.RoleId = existingroleEntity.Id;
                        }

                        if (!_roleRepository.Exists(x => x.RoleName == newRole))
                        {
                            roleEntity = _roleRepository.Create(new RoleEntity
                            {
                                RoleName = newRole!,
                            });

                            //edit Customer RoleId to the newly created roleEntity.Id.
                            returnedEntity.RoleId = roleEntity.Id;
                        }
                                                   // predicate to find entity to update      //entity to update with
                        var success = _customerRepository.Update(z => z.Id == returnedEntity.Id, returnedEntity);

                        if (success != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"For customer: {returnedEntity.FirstName}");
                            Console.WriteLine($"New role:{newRole}");
                            Console.WriteLine("press any key to return. . .");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("New role didnt get updated");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                if (specify == 3)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine($"Firstname: {returnedEntity.FirstName}");
                        Console.WriteLine($"Lastname:{returnedEntity.Lastname}");
                        Console.WriteLine($"Email:{returnedEntity.Email}");
                        Console.WriteLine($"PhoneNumber:{returnedEntity.PhoneNumber}");

                        Console.WriteLine("Leave field blank to keep previous value");
                        Console.Write("Enter a new Firstname:");
                        var newFirstName = Console.ReadLine();
                        Console.Write("Enter a new Lastname:");
                        var newLastName = Console.ReadLine();
                        Console.Write("Enter a new Email:");
                        var newEmail = Console.ReadLine();
                        Console.Write("Enter a new PhoneNumber:");
                        var newPhoneNumber = Console.ReadLine();
                        
                        //if (newFirstName == "" || newFirstName == null) 
                        //{
                        //    newFirstName = returnedEntity.FirstName;
                        //}
                        //if (newLastName == "" || newLastName == null)
                        //{
                        //    newLastName = returnedEntity.Lastname;
                        //}
                        //if (newEmail == "" || newEmail == null)
                        //{
                        //    newEmail = returnedEntity.Email;
                        //}
                        //if (newPhoneNumber == "" || newPhoneNumber == null)
                        //{
                        //    newPhoneNumber = returnedEntity.PhoneNumber;
                        //}
                       
                        returnedEntity.FirstName = string.IsNullOrEmpty(newFirstName) ? returnedEntity.FirstName : newFirstName;
                        returnedEntity.Lastname = string.IsNullOrEmpty(newLastName) ? returnedEntity.Lastname : newLastName;
                        returnedEntity.Email = string.IsNullOrEmpty(newEmail) ? returnedEntity.Email : newEmail;
                        returnedEntity.PhoneNumber = string.IsNullOrEmpty(newPhoneNumber) ? returnedEntity.PhoneNumber : newPhoneNumber;

                        //returnedEntity.FirstName = newFirstName;
                        //returnedEntity.Lastname = newLastName;
                        //returnedEntity.Email = newEmail;
                        //returnedEntity.PhoneNumber = newPhoneNumber;

                        // predicate to find entity to update      //entity to update with
                        var success = _customerRepository.Update(z => z.Id == returnedEntity.Id, returnedEntity);

                        if (success != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"Firstname: {returnedEntity.FirstName}");
                            Console.WriteLine($"Lastname:{returnedEntity.Lastname}");
                            Console.WriteLine($"Email:{returnedEntity.Email}");
                            Console.WriteLine($"PhoneNumber:{returnedEntity.PhoneNumber}");
                            Console.WriteLine("press any key to return. . .");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("New role didnt get updated");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine($"The input:'{form.Email}' was not found in the database.");
                Console.WriteLine("press any key to return. . .");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
    public void DeleteCustomer(string email)
    {
        try
        {
            email.Trim();

            var exists = _customerRepository.Exists(x => x.Email == email);

            if (exists)
            {
                var returnedEntity = _customerRepository.ReadOneEntity(x => x.Email == email);
                var deletedEntity = _customerRepository.Delete(x => x.Email == email);
                if (deletedEntity)
                {
                    var adress = _adressRepository.ReadOneEntity(x => x.Id == returnedEntity.AdressId);
                    var role = _roleRepository.ReadOneEntity(x => x.Id == returnedEntity.RoleId);
                    Console.Clear();
                    Console.WriteLine($"id: {returnedEntity.Id}");
                    Console.WriteLine($"Firstname: {returnedEntity.FirstName}");
                    Console.WriteLine($"Lastname:{returnedEntity.Lastname}");
                    Console.WriteLine($"Email:{returnedEntity.Email}");
                    Console.WriteLine($"PhoneNumber:{returnedEntity.PhoneNumber}");

                    Console.WriteLine($"AdressId: {adress.Id}");
                    Console.WriteLine($"StreetName: {adress.StreetName}");
                    Console.WriteLine($"PostalCode: {adress.PostalCode}");

                    Console.WriteLine($"RoleId: {role.Id}");
                    Console.WriteLine($"Role: {role.RoleName}");
                    Console.WriteLine($"The customer{returnedEntity.FirstName}{returnedEntity.Lastname} with email {returnedEntity.Email} has been deleted.");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine($"The input:'{email}' was not found in the database.");
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