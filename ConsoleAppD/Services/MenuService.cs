using ConsoleAppD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using DataAccess.Repositories;
using System.Data;
using static Dapper.SqlMapper;

namespace ConsoleAppD.Services
{
    public class MenuService
    {
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private readonly AdressService _adressService;
        private readonly RoleService _roleService;
        private readonly CategoryService _categoryService;

        public MenuService(CustomerService customerService, ProductService productservice, AdressService adressService, RoleService roleService, CategoryService categoryService)
        {
            _customerService = customerService;
            _productService = productservice;
            _adressService = adressService;
            _roleService = roleService;
            _categoryService = categoryService;
            _categoryService = categoryService;
        }

        public void ShowMenu ()
        {
            bool runmenu = true;
            int input = 0;

            while (runmenu)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("MenuService");
                try
                {
                    if (input >= 0 && input <= 9)
                    {
                        Console.Clear();
                        Console.WriteLine("Options are 1-7");
                        Console.WriteLine("1. Customer");
                        Console.WriteLine("2. Roles"); 
                        Console.WriteLine("3. Adresses");
                        Console.WriteLine("4. Products");
                        Console.WriteLine("5. Categories");
                        Console.WriteLine("6. Delete ");
                        Console.WriteLine("7. Exit application");

                        Console.Write("Enter option: ");
                        input = Convert.ToInt32(Console.ReadLine());
                    }          
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Valid options are 1-7");
                    Console.ReadKey();
                    Console.Clear();
                    ShowMenu();
                }
                
                switch (input)
                {
                    case 0:
                        {
                            Console.Clear();
                            ShowMenu();
                            break;
                        }

                    case 1:
                        {
                            Console.Clear();
                            Console.WriteLine("Customer Menu");
                            CustomerMenu();
                            input = 0;
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("4. Roles");
                            RoleMenu();
                            input = 0;
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            Console.WriteLine("5. Adresses");
                            AdressMenu();
                            input = 0;
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            Console.WriteLine("6. Products");
                            ProductMenu();
                            input = 0;
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            Console.WriteLine("7. Categories");
                            CategoryMenu();
                            input = 0;
                            break;
                        }
                    case 6:
                        {
                            Console.Clear();
                            Console.WriteLine("8. Delete");
                            DeleteMenu();
                            input = 0;
                            break;
                        }
                    case 7:
                        {
                            Console.Clear();
                            Console.WriteLine("10. Exit Application");
                            runmenu = false;
                            break;
                        }

                    default:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Valid options are 1-9");
                            Console.ReadKey();
                            Console.Clear();
                            ShowMenu();
                            break;
                        }
                }
            }      
       }
  

        public void CustomerMenu()
        {
            try
            {
                Console.WriteLine("1. Create a New Customer");
                Console.WriteLine("2. Read One Customer");
                Console.WriteLine("3. Read All Customer");
                Console.WriteLine("4. Update Customer");

                int specify = 0;
                specify = Convert.ToInt32(Console.ReadLine());

                if (specify == 1)
                {
                    try
                    {

                        var form = new CustomerRegistration();
                        Console.WriteLine("Firstname:");
                        form.FirstName = Console.ReadLine()!;

                        Console.WriteLine("Lastname:");
                        form.Lastname = Console.ReadLine()!;

                        Console.WriteLine("Email:");
                        form.Email = Console.ReadLine()!;

                        Console.WriteLine("PhoneNumber:");
                        form.PhoneNumber = Console.ReadLine()!;

                        Console.WriteLine("StreetName:");
                        form.StreetName = Console.ReadLine()!;

                        Console.WriteLine("City:");
                        form.City = Console.ReadLine()!;

                        Console.WriteLine("Postalcode:");
                        form.PostalCode = Console.ReadLine()!;

                        Console.WriteLine("RoleName:");
                        form.RoleName = Console.ReadLine()!;
                        Console.Clear();

                        var result = _customerService.CreateCustomer(form);


                        if (result)
                        {
                            Console.WriteLine($"The customer {form.FirstName} {form.Lastname} has been created.");
                        }
                        else
                        {
                            Console.WriteLine($"The customer {form.FirstName} with email {form.Email} already exist in the database.");
                        }
                        Console.ReadKey();
                        ShowMenu();
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
                        Console.Write("Enter an email adress to search for:");
                        string email = Console.ReadLine()!;
                        _customerService.ReadOneCustomer(email);
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (specify == 3)
                {
                    try
                    {
                        _customerService.ReadAllCustomers();
                        ShowMenu();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
                if (specify == 4)
                {
                    try
                    {
                        var form = new CustomerRegistration();

                        Console.WriteLine("search for a customer to updatevia email:");
                        form.Email = Console.ReadLine();
                        _customerService.UpdateCustomer(form);

                        ShowMenu();
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("valid options: 1-4");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadKey();
                    ShowMenu();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void ProductMenu()
        {
            try
            {
                Console.WriteLine("1. Create a New Product");
                Console.WriteLine("2. Read One Product");
                Console.WriteLine("3. Read All Products");
                Console.WriteLine("4. Update Product");

                int specify = 0;
                specify = Convert.ToInt32(Console.ReadLine());

                if (specify == 1)
                {
                    try
                    {

                        Console.WriteLine("Create new product menu");
                        var product = new ProductRegistration();

                        Console.WriteLine("enter title");
                        product.Title = Console.ReadLine()!;

                        Console.WriteLine("enter Price");
                        product.Price = Convert.ToDecimal(Console.ReadLine());

                        Console.WriteLine("enter CategoryName");
                        product.CategoryName = Console.ReadLine()!;

                        _productService.CreateProduct(product);
                        ShowMenu();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (specify == 2)
                {

                    try
                    {
                        Console.WriteLine("Enter product title:");
                        var product = Console.ReadLine()!;
                        _productService.ReadOneProduct(product);
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (specify == 3)
                {
                    try
                    {
                        _productService.ReadAllProducts();
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (specify == 4)
                {
                    try
                    {
                        Console.WriteLine("Enter product title to update:");
                        var product = Console.ReadLine()!;
                        _productService.UpdateProduct(product); 
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("valid options: 1-4");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadKey();
                    ShowMenu();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public void AdressMenu()
        {
            try
            {
                Console.WriteLine("1. Create a new adress");
                Console.WriteLine("2. Read one adress");
                Console.WriteLine("3. Read all adresses");
                Console.WriteLine("4. Update adress");

                int specify = 0;
                specify = Convert.ToInt32(Console.ReadLine());

                

                if (specify == 1)
                {
                    try
                    {
                        AdressEntity entity = new AdressEntity();

                        Console.WriteLine("Enter StreetName:");
                        entity.StreetName = Console.ReadLine();
                       

                        Console.WriteLine("Enter City:");
                        entity.City = Console.ReadLine();

                        Console.WriteLine("Enter PostalCode:");
                        entity.PostalCode = Console.ReadLine();

                        _adressService.CreateNewAdress(entity);
                        ShowMenu();


                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
                if (specify == 2)
                {
                    try
                    {
                        var entity = new AdressEntity();

                        Console.WriteLine("Enter streetname:");
                        entity.StreetName = Console.ReadLine();

                        Console.WriteLine("Enter City:");
                        entity.City = Console.ReadLine();

                        Console.WriteLine("Enter PostalCode:");
                        entity.PostalCode = Console.ReadLine()!;
                        _adressService.ReadOneAdress(entity);
                        ShowMenu();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
                if (specify == 3)
                {
                    try
                    {
                        _adressService.ReadAllAdresses();
                        ShowMenu();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
                if (specify == 4)
                {
                    try
                    {
                        AdressEntity entity = new AdressEntity();

                        Console.WriteLine("Enter StreetName:");
                        entity.StreetName = Console.ReadLine();


                        Console.WriteLine("Enter City:");
                        entity.City = Console.ReadLine();

                        Console.WriteLine("Enter PostalCode:");
                        entity.PostalCode = Console.ReadLine();

                        _adressService.UpdateAdress(entity);
                        ShowMenu();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("valid options: 1-4");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadKey();
                    AdressMenu();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public void DeleteMenu()
        {
            try
            {
                Console.WriteLine("1. Customer");
                Console.WriteLine("2. Adress");
                Console.WriteLine("3. Role");
                Console.WriteLine("4. Category");
                Console.WriteLine("5. Product");
                int specify = 0;
                specify = Convert.ToInt32(Console.ReadLine());
                
                //ProductRegistration form = new ProductRegistration();

                if (specify == 1)
                {
                    try
                    {
                        Console.WriteLine("Enter email:");
                        string email = Console.ReadLine()!;
                        _customerService.DeleteCustomer(email);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (specify == 2)
                {
                    
                    try
                    {
                        AdressEntity entity = new AdressEntity();
                        Console.WriteLine("Enter streetName:");
                        entity.StreetName = Console.ReadLine()!;
                        Console.WriteLine("Enter City:");
                        entity.City = Console.ReadLine()!;
                        Console.WriteLine("Enter Postalcode:");
                        entity.PostalCode = Console.ReadLine()!;

                        //_adressService.DeleteAdress(entity);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (specify == 3)
                {
                    try
                    {
                        Console.WriteLine("Enter roleName title:");
                        string roleName = Console.ReadLine()!;
                        _roleService.DeleteRole(roleName);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (specify == 4)
                {
                    try
                    {
                        Console.WriteLine("Enter categoryName:");
                        string categoryName = Console.ReadLine()!;
                        _categoryService.DeleteCategory(categoryName);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (specify == 5)
                {
                    try
                    {
                        Console.WriteLine("Enter product title:");
                        string product = Console.ReadLine()!;
                        _productService.DeleteProduct(product);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("valid options: 1-4");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadKey();
                    //CategoryMenu();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }
        public void RoleMenu()
        {
            try
            {
                Console.WriteLine("1. Create new role");
                Console.WriteLine("2. Read one role ");
                Console.WriteLine("3. Read all roles");
                Console.WriteLine("4. Update role");

                int specify = 0;
                specify = Convert.ToInt32(Console.ReadLine());

                if (specify == 1)
                {
                    try
                    {
                        Console.WriteLine("Enter Role name:");
                        RoleEntity role = new RoleEntity();
                        role.RoleName = Console.ReadLine()!;
                        _roleService.CreateNewRole(role);
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (specify == 2)
                {
                    try
                    {
                        Console.WriteLine("Enter role name:");
                        string role = Console.ReadLine()!;
                        _roleService.ReadOneRole(role);
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (specify == 3)
                {
                    try
                    {
                        _roleService.ReadAllRoles();
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (specify == 4)
                {
                    try
                    {
                        Console.WriteLine("Enter role to update:");
                        string role = Console.ReadLine()!;
                        _roleService.UpdateRoles(role);
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("valid options: 1-4");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadKey();
                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                
            }
        }
        public void CategoryMenu()
        {
            try
            {
                Console.WriteLine("1. Create a New Category");
                Console.WriteLine("2. Read One Category");
                Console.WriteLine("3. Read All Categories");
                Console.WriteLine("4. Update Category");

                int specify = 0;
                specify = Convert.ToInt32(Console.ReadLine());

                if (specify == 1)
                {
                    try
                    {

                        Console.WriteLine("Create a new Category");
                        var product = new ProductRegistration();

                        Console.WriteLine("enter CategoryName");
                        product.CategoryName = Console.ReadLine()!;

                        _categoryService.CreateCategory(product);
                        ShowMenu();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                if (specify == 2)
                {

                    try
                    {
                        Console.WriteLine("Enter category title:");
                        var category = Console.ReadLine()!;
                        _categoryService.ReadOneCategory(category);
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (specify == 3)
                {
                    try
                    {
                        _categoryService.ReadAllCategories();
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (specify == 4)
                {
                    try
                    {
                        Console.WriteLine("Enter Category name to update:");
                        var category = Console.ReadLine()!;
                        _categoryService.UpdateCategory(category);
                        ShowMenu();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("valid options: 1-4");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadKey();
                    ShowMenu();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
