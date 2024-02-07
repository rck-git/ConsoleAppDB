
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
