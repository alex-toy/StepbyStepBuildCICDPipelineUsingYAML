using BookLibrary;
using BookLibrary.Models;
using HelloWorldApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly BooksContext _db;

    public ProductController(BooksContext db)
    {
        _db = db;
    }

    [HttpGet("GetAll")]
    public IEnumerable<Product> GetAll()
    {
        return new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 19.99m },
            new Product { Id = 2, Name = "Product 2", Price = 29.99m },
            new Product { Id = 3, Name = "Product 3", Price = 39.99m }
        };
    }

    [HttpGet("books")]
    public IEnumerable<Book> GetAllBooks()
    {
        List<Book> books = _db.Books.Include(b => b.Author).ToList();

        return books;
    }
}