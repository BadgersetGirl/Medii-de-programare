namespace WebApi.Models.Article;

using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

public class CreateArticleRequest
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public string CategoryId { get; set; }

    [Required]    
    public string Status { get; set; }

}