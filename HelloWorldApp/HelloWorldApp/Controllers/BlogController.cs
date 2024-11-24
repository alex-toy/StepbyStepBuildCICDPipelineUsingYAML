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

    [HttpPost("blog")]
    public void Add([FromBody] Blog blog)
    {
        try
        {
            _db.Blogs.Add(blog);
            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while adding a post: {ex.Message}");
            throw;
        }
    }

    [HttpPost("post")]
    public void Add([FromBody] Post post)
    {
        try
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while adding a post: {ex.Message}");
            throw;
        }
    }

    [HttpPut("blog")]
    public void Update([FromBody] Blog updatedBlog)
    {
        try
        {
            Blog? blog = _db.Blogs.FirstOrDefault(p => p.Id == updatedBlog.Id);

            if (blog is null) return;

            blog.Name = updatedBlog.Name;
            blog.Url = updatedBlog.Url;

            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while updating a post: {ex.Message}");
            throw;
        }
    }

    [HttpPut("post")]
    public void Update([FromBody] Post updatedPost)
    {
        try
        {
            Post? post = _db.Posts.FirstOrDefault(p => p.Id == updatedPost.Id);

            if (post is null) return;

            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;

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