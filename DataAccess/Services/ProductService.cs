using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System.Diagnostics;

namespace DataAccess.Services;

public class ProductService
{
    private readonly CategoryRepository _categoryRepository;
    private readonly ProductRepository _productRepository;

    public ProductService(CategoryRepository categoryRepository, ProductRepository productRepository)
    {

        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public bool CreateProduct(ProductRegistration form)
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
            }
            var productEntity = new ProductEntity
            {
                Title = form.Title,
                Price = form.Price,
                CategoryId = categoryEntity.Id
            };

            var result = _productRepository.Create(productEntity);
            if (result != null)
            {
                Console.Clear();
                Console.WriteLine($"Created a new product");
                Console.WriteLine($"Product Id =  {productEntity.Id}");
                Console.WriteLine($"Title =  {productEntity.Title}");
                Console.WriteLine($"Price =  {productEntity.Price}");

                Console.WriteLine($"Category Id =   {categoryEntity.Id}");
                Console.WriteLine($"CategoryName =  {categoryEntity.CategoryName}");

                Console.ReadKey();
                return true;
            }
            
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
    public void ReadOneProduct(string title)
    {
        try
        {
            title.Trim();
            var exists = _productRepository.Exists(x => x.Title == title);

            if (exists)
            {
                var returnedProduct = _productRepository.ReadOneEntity(x => x.Title == title);
                if (returnedProduct != null)
                {
                    var returnedCategory = _categoryRepository.ReadOneEntity(x => x.Id == returnedProduct.CategoryId);

                    Console.Clear();
                    Console.WriteLine($"id: {returnedProduct.Id}");
                    Console.WriteLine($"Price: {returnedProduct.Price}");
                    Console.WriteLine($"CategoryId:{returnedProduct.CategoryId}");
                    Console.WriteLine($"CategoryName:{returnedCategory.CategoryName}");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine($"The input:'{title}' was not found in the database.");
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
    public void ReadAllProducts()
    {
        try
        {
            List<ProductEntity> returnedProducts = _productRepository.ReadAllEntities().ToList();
            List<CategoryEntity> returnedCategories = _categoryRepository.ReadAllEntities().ToList();

            if (returnedProducts != null)
            {
                Console.Clear();
                foreach (var entity in returnedProducts)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    //var category = _categoryRepository.ReadOneEntity(x => x.Id == entity.CategoryId);
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine($"id: {entity.Id}");
                    Console.WriteLine($"Title:{entity.Title}");
                    Console.WriteLine($"Price: {entity.Price}");
                    Console.WriteLine($"CategoryId:{entity.CategoryId}");
                    var categoryMatchingtheCurrentAdress = returnedCategories.FindAll(x => x.Id == entity.CategoryId);
                    if (categoryMatchingtheCurrentAdress.Count != 0)
                    {
                        foreach (var x in categoryMatchingtheCurrentAdress)
                        {
                            Console.WriteLine($"CategoryName:{x.CategoryName}");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Category was not assigned for this product.");
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
    }
    public void UpdateProduct(string title)
    {
        try
        {
            Console.Clear();
            title.Trim();
            var returnedProduct = _productRepository.ReadOneEntity(x => x.Title == title);

            var exist = _categoryRepository.Exists(x => x.Id == returnedProduct.CategoryId);
            if (!exist)
            {
                var returnedCategory = _categoryRepository.ReadOneEntity(x => x.Id == returnedProduct.CategoryId);
                Console.WriteLine("Enter a new Category:");
                var newCategory = Console.ReadLine();
            }

            Console.Clear();

            Console.WriteLine($"Current product Title: {returnedProduct.Title}");
            Console.WriteLine();
            Console.WriteLine("Enter a new title:");
            var newTitle = Console.ReadLine();
            Console.WriteLine("Enter a new price:");
            var newPrice = Console.ReadLine();


            returnedProduct.Title = string.IsNullOrEmpty(newTitle) ? returnedProduct.Title : newTitle;
            //returnedCategory.Id = string.IsNullOrEmpty(newTitle) ? returnedProduct.Title : newTitle;

            if (string.IsNullOrEmpty(newPrice))
            {
                returnedProduct.Price = returnedProduct.Price;
            }
            else
            {
                returnedProduct.Price = decimal.Parse(newPrice);
            }


            if (!_productRepository.Exists(x => x.Title == newTitle))
            {
                returnedProduct.Title = newTitle!;

                // predicate to find entity to update      //entity to update with
                var success = _productRepository.Update(z => z.Id == returnedProduct.Id, returnedProduct);

                if (success != null)
                {
                    Console.Clear();
                    Console.WriteLine($"New Title:{newTitle}");
                    Console.WriteLine($"New Title:{newPrice}");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Title didnt get updated");
                }
            }
            else
            {
                Console.WriteLine("The title name must be unique");
                Console.WriteLine("press any key to return. . . ");
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public void DeleteProduct(string title)
    {
        try
        {
            title.Trim();

            var exists = _productRepository.Exists(x => x.Title == title);

            if (exists)
            {
                var returnedEntity = _productRepository.ReadOneEntity(x => x.Title == title);
                var deletedEntity = _productRepository.Delete(x => x.Title == title);
                if (deletedEntity)
                {

                    Console.Clear();
                    Console.WriteLine($"id: {returnedEntity.Id}");
                    Console.WriteLine($"Title: {returnedEntity.Title}");
                    Console.WriteLine($"Price:{returnedEntity.Price}");
                    Console.WriteLine($"CategoryId:{returnedEntity.CategoryId}");

                    Console.WriteLine($"The Product {returnedEntity.Title} has been deleted.");
                    Console.WriteLine("press any key to return. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine($"The input:'{title}' was not found in the database.");
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
