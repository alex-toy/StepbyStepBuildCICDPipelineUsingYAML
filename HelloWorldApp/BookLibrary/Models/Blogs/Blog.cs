﻿namespace BookLibrary.Models.Blogs;

public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
