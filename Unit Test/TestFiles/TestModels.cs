using EcommerceAPI.Dto;
using EcommerceAPI.Models;

namespace Unit_Test.TestFiles
{
    public class TestModels
    {
        public static List<Product> GetTestProducts()
        {
            List<Product> products = new()
           {
                new Product
                {
                    Id = 1,
                    ProductName = "Product1",
                    ProductDescription = "Product1 Description",
                    ProductPrice = 100,
                    CustomerId = 1,
                    ProductImage = "Product1.jpg",
                    Categories = new List<Category>
                    {
                        new Category
                        {
                            Id = 1,
                            Name = "Category1",
                            Type = "type1"
                        }
                    }
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Product2",
                    ProductDescription = "Product2 Description",
                    ProductPrice = 200,
                    CustomerId = 2,
                    ProductImage = "Product2.jpg",
                    Categories = new List<Category>
                    {
                        new Category
                        {
                            Id = 2,
                            Name = "Category2",
                            Type = "type2"
                        }
                    }
                },
                new Product
                {
                    Id = 10,
                    ProductName = "Product10",
                    ProductDescription = "Product10 Description",
                    ProductPrice = 150,
                    ProductImage = "Product10.jpg",
                    CustomerId = 1,
                    Categories = new List<Category>
                    {
                        new Category
                        {
                            Id = 10,
                            Name = "Category10",
                            Type = "type10"
                        }
                    }
                }
            };
            return products;
        }

        public static ProductCategory GetProductTestCategory()
        {
            return new ProductCategory()
            {
                Id = 10,
                ProductName = "Product10",
                ProductDescription = "Product10 Description",
                ProductPrice = 150,
                ProductImage = "Product10.jpg",
                CustomerId = 1,
                Categories = new List<Category>
                {
                    new Category
                    {
                        Id = 10,
                        Name = "Category10",
                        Type = "type10"
                    }
                }
            };
        }

        public static List<CustomerDto> GetTestCustomers()
        {
            return new List<CustomerDto>
            {
                new CustomerDto
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@doe.com",
                    Admin = true,
                },
                new CustomerDto
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "Jane@Doe.com",
                    Admin = false,
                }
            };
        }

        public static Customer GetTestCustomer()
        {
            return new Customer()
            {
                Id = 5,
                FirstName = "John",
                LastName = "Doe",
                Email = "John@Doe.com",
                Admin = true,
                Password = "password",
            };
        }

        public static List<Category> GetTestCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Category1",
                    Type = "category"
                },
                new Category
                {
                    Id = 2,
                    Name = "Category2",
                    Type = "category"
                },
                new Category
                {
                    Id = 10,
                    Name = "On Sale",
                    Type = "tag"
                }
            };
        }
    }

}

