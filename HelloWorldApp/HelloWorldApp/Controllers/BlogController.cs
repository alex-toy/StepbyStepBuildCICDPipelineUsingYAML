using BookLibrary.Contexts;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly BloggingContext _db;

    public BlogController(BloggingContext db)
    {
        _db = db;
    }

    [HttpGet("blogs")]
    public IEnumerable<Blog> GetAll()
    {
        List<Blog> blogs = _db.Blogs.Include(b => b.Posts).ToList();

        return blogs;
    }

    [HttpPost("blogs")]
    public void Add(int blogId, string title, string content)
    {
        try
        {
            Post newPost = new () { BlogId = blogId, Title = title, Content = content };
            _db.Posts.Add(newPost);
            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while adding a post: {ex.Message}");
            throw;
        }
    }

    [HttpPut("blogs")]
    public void Update(int postId, string title, string content)
    {
        try
        {
            Post? post = _db.Posts.FirstOrDefault(p => p.Id == postId);

            if (post is null) return;

            post.Title = title;
            post.Content = content;

            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while updating a post: {ex.Message}");
            throw;
        }
    }

    [HttpDelete("blogs")]
    public void Delete(Post post)
    {
        try
        {
            _db.Posts.Remove(post);
            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while deleting a post: {ex.Message}");
            throw;
        }
    }
}