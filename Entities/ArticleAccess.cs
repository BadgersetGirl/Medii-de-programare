namespace WebApi.Entities
{
public class ArticleAccess
{

public int ArticleAccessId { get; set; }
public int UserId { get; set; }
public User User { get; set; }
public int ArticleId { get; set; }
public Article Article { get; set; }
}
}