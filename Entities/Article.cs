
using System;
using System.Collections.Generic;
namespace WebApi.Entities;
public class Article
{
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string Status { get; set; }

    public ICollection<ArticleAccess> ArticleAccesses { get; set; }
    
}