// See https://aka.ms/new-console-template for more information
using Autofac;
using Business.Abstract.Products;
using Business.DependencyResolvers.Autofac;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

// Ensure database is created
using (var context = new DataBaseContext())
{
    context.Database.EnsureCreated();
}

// Setup Autofac
var builder = new ContainerBuilder();
builder.RegisterModule(new AutofacBusinessModule());
var container = builder.Build();

// Resolve services
var productService = container.Resolve<IProductService>();
var categoryService = container.Resolve<ICategoryService>();
var brandService = container.Resolve<IBrandService>();

Console.WriteLine("Products in the database:");
var result = productService.GetAll();
if (result.Success)
{
    foreach (var product in result.Data)
    {
        Console.WriteLine($"Product: {product.ProductName}");
    }
}
else
{
    Console.WriteLine($"Error: {result.Message}");
}