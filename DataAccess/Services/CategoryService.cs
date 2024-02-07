
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using System.Diagnostics;

namespace DataAccess.Services;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository;
    private readonly ProductRepository _productRepository;
    private readonly AdressRepository _adressRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly RoleRepository _roleRepository;

    public CategoryService(ProductRepository productRepository,AdressRepository adressRepository, CustomerRepository customerRepository, RoleRepository roleRepository, CategoryRepository categoryRepository)
    {
        _adressRepository = adressRepository;
        _customerRepository = customerRepository;
        _roleRepository = roleRepository;
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    // public void CreateNewCategory(CategoryEntity form)
    //{
    //    try
    //    {
    //        if (!_roleRepository.Exists(x => x.RoleName == form.RoleName))
    //        {
    //            var roleEntity = _roleRepository.Create(new RoleEntity
    //            {
    //                RoleName = form.RoleName
    //            });

    //            var result = _roleRepository.Create(roleEntity);
    //            if (result != null)
    //            {
    //                Console.WriteLine($"Created a new role name");
    //                Console.WriteLine($".Role Name =  {form.RoleName}");
    //                Console.WriteLine($"productEntity.Lastname =  {roleEntity.Id}");
    //                Console.ReadKey();
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine($"The role name {form.RoleName} already exist ");
    //            Console.ReadKey();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message);
    //    }
    //}

    //public void ReadOneCategory(string categoryName)
    //{
    //    try
    //    {
    //        rolename.Trim();
    //        var exists = _roleRepository.Exists(x => x.RoleName == rolename);

    //        if (exists)
    //        {
    //            var entity = _roleRepository.ReadOneEntity(x => x.RoleName == rolename);
    //            if (entity != null)
    //            {
    //                var customer = _customerRepository.ReadOneEntity(x => x.RoleId == entity.Id);

    //                Console.WriteLine($"RoleId: {entity.Id}");
    //                Console.WriteLine($"Role: {entity.RoleName}");

    //                if (customer != null)
    //                {
    //                    Console.WriteLine($"id: {customer.Id}");
    //                    Console.WriteLine($"Firstname: {customer.FirstName}");
    //                    Console.WriteLine($"Lastname:{customer.Lastname}");
    //                    Console.WriteLine($"Email:{customer.Email}");
    //                    Console.WriteLine($"PhoneNumber:{customer.PhoneNumber}"); ;
    //                }
    //                else
    //                {
    //                    Console.WriteLine("Customer was null for role.");
    //                }

    //                Console.WriteLine("press any key to return. . .");
    //                Console.ReadKey();
    //                Console.Clear();
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine($"The input:'{rolename}' was not found in the database.");
    //            Console.WriteLine("press any key to return. . .");
    //            Console.ReadKey();
    //            Console.Clear();
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }

    //}

    //public void ReadAllCategories()
    //{
    //    List<CategoryEntity> returnedCategories = _categoryRepository.ReadAllEntities().ToList();

    //    if (returnedCategories != null)
    //    {
    //        Console.Clear();
    //        foreach (var entity in returnedCategories)
    //        {
    //            var product = _customerRepository.ReadOneEntity(x => x.RoleId == entity.Id);
    //            var adress = _adressRepository.ReadOneEntity(x => x.Id == customer.Id);

    //            var role = _roleRepository.ReadOneEntity(x => x.Id == customer.Id);

    //            Console.WriteLine($"RoleId: {entity.Id}");
    //            Console.WriteLine($"Role: {entity.RoleName}");

    //            if (role != null)
    //            {
    //                Console.WriteLine($"AdressId: {adress.Id}");
    //                Console.WriteLine($"StreetName: {adress.StreetName}");
    //                Console.WriteLine($"City: {adress.City}");
    //                Console.WriteLine($"Postalcode: {adress.PostalCode}");
    //            }
    //            else
    //            {
    //                Console.WriteLine("Adress was null for Customer.");
    //            }
    //            if (customer != null)
    //            {
    //                Console.WriteLine($"id: {customer.Id}");
    //                Console.WriteLine($"Firstname: {customer.FirstName}");
    //                Console.WriteLine($"Lastname:{customer.Lastname}");
    //                Console.WriteLine($"Email:{customer.Email}");
    //                Console.WriteLine($"PhoneNumber:{customer.PhoneNumber}");
    //            }
    //            else
    //            {
    //                Console.WriteLine("Customer was null for this adress");
    //            }

    //        }
    //        Console.WriteLine("press any key to return. . .");
    //        Console.ReadLine();
    //    }
    //    else
    //    {
    //        Console.WriteLine("Nothing found, try creating a customer prior to running this.");
    //        Console.WriteLine("press any key to return. . .");
    //        Console.ReadKey();
    //        Console.Clear();
    //    }
    //}

  
    //public bool CreateProduct(ProductRegistration form)
    //{
    //    try
    //    {
    //        if (!_productRepository.Exists(x => x.Id == form.Id))
    //        {
    //            var categoryEntity = _categoryRepository.ReadOneEntity(x => x.CategoryName == form.CategoryName);
    //            if (categoryEntity == null)
    //            {
    //                categoryEntity = _categoryRepository.Create(new CategoryEntity
    //                {
    //                    CategoryName = form.CategoryName
    //                });
    //            }
    //            var productEnity = new ProductEntity
    //            {
    //                Title = form.Title,
    //                Price = form.Price,
    //                CategoryId = categoryEntity.Id
    //            };
    //            var result = _productRepository.Create(productEnity);

    public bool CreateCategory(ProductRegistration form)
    {
        try
        {
            var categoryEntity = _categoryRepository.ReadOneEntity(x => x.CategoryName == form.CategoryName);
            if (categoryEntity == null)
            {
                categoryEntity = _categoryRepository.Create(new CategoryEntity
                {
                    CategoryName = form.CategoryName
                });
                
                if (categoryEntity != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Created a new Category");
                    Console.WriteLine($"Product Id =  {categoryEntity.Id}");
                    Console.WriteLine($"Title =  {categoryEntity.CategoryName}");

                    Console.ReadKey();
                    return true;
                }
            }
            else
            {
                Console.WriteLine($"Category name {form.CategoryName} already exist, it must be unique.");
            }
       
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public void ReadOneCategory(string categoryname)
    {
        try
        {
            categoryname.Trim();
         
            var returnedCategory = _categoryRepository.ReadOneEntity(x => x.CategoryName == categoryname);

            if (returnedCategory != null)
            {
                var returnedProducts = _productRepository.ReadAllEntities(x => x.CategoryId == returnedCategory.Id).ToList();
                Console.Clear();
                if (returnedProducts.Count != 0)
                {
                    Console.WriteLine($"CategoryId:{returnedCategory.Id}");
                    Console.WriteLine($"CategoryName:{returnedCategory.CategoryName}");
                    Console.WriteLine("products associated with the category");
                    foreach (var product in returnedProducts)
                    {
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine($"id: {product.Title}");
                        Console.WriteLine($"id: {product.Id}");
                        Console.WriteLine($"Price: {product.Price}");
                        Console.WriteLine($"CategoryId:{product.CategoryId}");

                    }
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();

                }
                else
                {
                    Console.WriteLine($"No products associated with Categoryname:{categoryname}");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine($"The input:'{categoryname}' was not found in the database.");
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
    public void ReadAllCategories()
    {
        try
        {
            List<ProductEntity> returnedProducts = _productRepository.ReadAllEntities().ToList();
            List<CategoryEntity> returnedCategories = _categoryRepository.ReadAllEntities().ToList();

            if (returnedCategories != null)
            {
                Console.Clear();
                foreach (var entity in returnedCategories)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    //var category = _categoryRepository.ReadOneEntity(x => x.Id == entity.CategoryId);
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine($"id: {entity.Id}");
                    Console.WriteLine($"Category Name:{entity.CategoryName}");
                    
                    var productMatchingtheCurrentCategory = returnedProducts.FindAll(x => x.CategoryId == entity.Id);
                    if (productMatchingtheCurrentCategory.Count != 0)
                    {
                        foreach (var x in productMatchingtheCurrentCategory)
                        {
                            Console.WriteLine($"Product Id:{x.Id}");
                            Console.WriteLine($"Product Title:{x.Title}");
                            Console.WriteLine($"Price:{x.Price}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This category doesnt contain any products.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nothing found, try creating a product prior to running this.");
                Console.WriteLine("press any key to return. . .");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine();
            Console.WriteLine("press any key to return. . .");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    } //
    //public bool UpdateProductOrCategory(ProductRegistration form)
    //{
    //    try
    //    {
    //        var returnedEntity = _productRepository.ReadOneEntity(x => x.Title == form.Title);
    //        var returnedCategory = _categoryRepository.ReadOneEntity(x => x.Id == returnedEntity.CategoryId);

    //        if (_productRepository.Exists(x => x.Title == form.Title))
    //        {
    //            int specify = 0;
    //            if (specify != 1 || specify != 2)
    //            {
    //                Console.Clear();
    //                Console.WriteLine("What do you want to update?");
    //                Console.WriteLine("1. Update product details");
    //                Console.WriteLine("2. Update category details ");

    //                Console.WriteLine();
    //                Console.WriteLine($"id: {returnedEntity.Id}");
    //                Console.WriteLine($"Title: {returnedEntity.Title}");
    //                Console.WriteLine($"Price:{returnedEntity.Price}");

    //                Console.WriteLine($"CategoryId:{returnedEntity.Id}");
    //                Console.WriteLine($"Category:{returnedCategory.CategoryName}");

    //                specify = Convert.ToInt32(Console.ReadLine());
    //            }
    //            if (specify == 1)
    //            {
    //                try
    //                {
    //                    Console.Clear();

    //                    Console.WriteLine($"Current Id:{returnedEntity.Id} ,Title: {returnedEntity.Title}, Price: {returnedEntity.Price}");
    //                    Console.WriteLine($"Current Id:{returnedCategory.Id} ,Title: {returnedCategory.CategoryName} ");

    //                    Console.Write("Enter a new Title:");
    //                    var newTitle = Console.ReadLine();
    //                    Console.Write("Enter a new Price:");
    //                    var newPrice = Convert.ToDecimal(Console.ReadLine());
    //                    Console.Write("Enter a new Category:");
    //                    var newCategoryForSpecificProductName = Console.ReadLine();

    //                    //if variables null or empty 
    //                    returnedEntity.Title = string.IsNullOrEmpty(newTitle) ? returnedEntity.Title : newTitle;
    //                    returnedEntity.Price = decimal.IsPositive(newPrice) ? newPrice : returnedEntity.Price;
    //                    returnedCategory.CategoryName = string.IsNullOrEmpty(newCategoryForSpecificProductName) ? returnedCategory.CategoryName : newCategoryForSpecificProductName;

    //                    // if product title exist
    //                    if (_productRepository.Exists(x => x.Title == newTitle))
    //                    {
    //                        var productEntity = _productRepository.Update(new ProductEntity
    //                        {
    //                            Title = newTitle,
    //                            Price = newPrice,
    //                        });                           
    //                    }

    //                    // if product title doesnt exist
    //                    if (!_productRepository.Exists(x => x.Title == newTitle))
    //                    {
    //                        Console.WriteLine("Create new product menu");
    //                        var product = new ProductRegistration();                          
    //                        product.Title = newTitle;
    //                        product.Price = newPrice;
    //                        product.CategoryName = newCategoryForSpecificProductName;
    //                        var productEntity = CreateProduct(product);

    //                    }

    //                    //if categoryname is set by user swap ID of the product
    //                    if (returnedCategory.CategoryName == newCategoryForSpecificProductName)
    //                    {
    //                        var newcategoryEntity = _categoryRepository.Create(new CategoryEntity
    //                        {
    //                            CategoryName = newCategoryForSpecificProductName,
    //                        });

    //                        //edit Product CategoryId to the existing productEntity.ID
    //                        returnedEntity.Id = newcategoryEntity.Id;
    //                    }


    //                    var success = _productRepository.Update(z => z.Id == returnedEntity.Id, returnedEntity);

    //                    if (success != null)
    //                    {
    //                        Console.Clear();
    //                        Console.WriteLine($"New title:{newTitle}");
    //                        Console.WriteLine($"New price:{newPrice}");
    //                        Console.WriteLine($"New Category:{newCategoryForSpecificProductName}");
    //                        Console.WriteLine("press any key to return. . .");
    //                        Console.ReadKey();
    //                    }
    //                    else
    //                    {
    //                        Console.WriteLine("The product didnt get updated");
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    Debug.WriteLine(ex.Message);
    //                }
    //            }
    //            if (specify == 2)
    //            {
    //                try
    //                {
    //                    var categoryEntity = _categoryRepository.ReadOneEntity(x => x.CategoryName == returnedCategory.CategoryName);
    //                    Console.Clear();

    //                    //var categories = _categoryRepository.ReadAllEntities();
    //                    //foreach (var item in categories)
    //                    //{
    //                    //    Console.WriteLine($"{item.CategoryName}");
    //                    //}

    //                    Console.WriteLine($"Current category name: {returnedCategory.CategoryName}");
    //                    Console.Write("Enter a new category name:");

    //                    // if Category exists and a name change happens
    //                    var newCategory = Console.ReadLine();
    //                    //if (_categoryRepository.Exists(x => x.CategoryName == returnedCategory.CategoryName))
    //                    //{
    //                    //    var existingCategoryEntity = 
    //                    //    _categoryRepository.Update(x => x.CategoryName == newCategory, returnedCategory.CategoryName);

    //                    //    ////edit product categoryId to the existing category.ID
    //                    //    //returnedEntity.RoleId = existingCategoryEntity.Id;
    //                    //}

    //                    if (!_categoryRepository.Exists(x => x.CategoryName == newCategory))
    //                    {
    //                        categoryEntity = _categoryRepository.Create(new CategoryEntity
    //                        {
    //                            CategoryName = newCategory!,
    //                        });

    //                        //edit Customer RoleId to the newly created roleEntity.Id.
    //                        returnedEntity.CategoryId = categoryEntity.Id;
    //                    }

    //                    // predicate to find entity to update      //entity to update with
    //                    var success = _categoryRepository.Update(z => z.Id == returnedEntity.Id, categoryEntity);

    //                    if (success != null)
    //                    {
    //                        Console.Clear();
    //                        Console.WriteLine($"For Product: {returnedEntity.Title}");
    //                        Console.WriteLine($"New Category:{newCategory}");
    //                        Console.WriteLine("press any key to return. . .");
    //                        Console.ReadKey();
    //                    }
    //                    else
    //                    {
    //                        Console.WriteLine("New role didnt get updated");
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    Debug.WriteLine(ex.Message);
    //                }
    //            }

    //            return true;
    //        }
    //        else
    //        {
    //            Console.WriteLine($"The input:'{form.Title}' was not found in the database.");
    //            Console.WriteLine("press any key to return. . .");
    //            Console.ReadKey();
    //            Console.Clear();
    //            return false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine(ex.Message);
    //        return false;
    //    }
    //}

    public void UpdateCategory(string categoryname)
    {
        try
        {
            var returnedCategory = _categoryRepository.ReadOneEntity(x => x.CategoryName == categoryname);
            Console.Clear();
            if (returnedCategory != null)
            {
                categoryname.Trim();
                Console.Clear();
                Console.WriteLine($"Current Category Name: {returnedCategory.CategoryName}");
                Console.WriteLine();
                Console.WriteLine("Enter a new title:");
                var newTitle = Console.ReadLine();

                returnedCategory.CategoryName = string.IsNullOrEmpty(newTitle) ? returnedCategory.CategoryName : newTitle;

                if (!_categoryRepository.Exists(x => x.CategoryName == newTitle))
                {
                    returnedCategory.CategoryName = newTitle!;

                    // predicate to find entity to update      //entity to update with
                    var success = _categoryRepository.Update(z => z.Id == returnedCategory.Id, returnedCategory);

                    if (success != null)
                    {
                        Console.Clear();
                        Console.WriteLine($"New categoryname:{newTitle}");
                        Console.WriteLine("press any key to return. . .");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("The category name must be unique");
                    Console.WriteLine("press any key to return. . . ");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine($"Categoryname {categoryname} not found in the database.");
            }
           
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    } //
   
    public void DeleteCategory(string categoryName)
    {
        try
        {
            categoryName.Trim();

            var exists = _categoryRepository.Exists(x => x.CategoryName == categoryName);

            if (exists)
            {
                var returnedEntity = _categoryRepository.ReadOneEntity(x => x.CategoryName == categoryName);
                var deletedEntity = _categoryRepository.Delete(x => x.CategoryName == categoryName);

                if (deletedEntity)
                {
                    var adress = _adressRepository.ReadOneEntity(x => x.Id == returnedEntity.Id);

                    Console.Clear();
                    Console.WriteLine($"id: {returnedEntity.Id}");
                    Console.WriteLine($"categoryName: {returnedEntity.CategoryName}");

                    Console.WriteLine($"The Category has been deleted.");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine($"The input:'{categoryName}' was not found in the database.");
                Console.WriteLine("press any key to return. . .");
                Console.ReadKey();
                Console.Clear();
            }
        }
        catch (Exception)
        {
            throw;
        }
    } //


}
