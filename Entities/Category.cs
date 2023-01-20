using System.Collections.Generic;

namespace WebApi.Entities
{
public class Category
{
public int CategoryId { get; set; }
public string Name { get; set; }
public ICollection<Article> Articles { get; set; }
}
}