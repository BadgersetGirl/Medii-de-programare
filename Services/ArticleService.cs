namespace WebApi.Services;

using AutoMapper;
using BCrypt.Net;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Article;

public interface IArticleService
{
    IEnumerable<Article> GetAll();
    Article GetByArticleId(int id);
    void Create(CreateArticleRequest model);
    void Update(int id, UpdateArticleRequest model);
}

public class ArticleService : IArticleService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public ArticleService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Article> GetAll()
    {
        return _context.Articles;
    }

    public Article GetByArticleId(int id)
    {
        return getArticle(id);
    }

    public void Create(CreateArticleRequest model)
    {
        // map model to new user object
        var article = _mapper.Map<Article>(model);

        // save user
        _context.Articles.Add(article);
        _context.SaveChanges();
    }

    public void Update(int id, UpdateArticleRequest model)
    {
        var article = GetByArticleId(id);

        // article only update status
        article.Status = model.Status;

        // copy model to user and save
        _context.Articles.Update(article);
        _context.SaveChanges();
    }

    private Article getArticle(int id)
    {
        var article = _context.Articles.Find(id);
        if (article == null) throw new KeyNotFoundException("Article not found");

        // add Category to Article
        article.Category = _context.Categories.Find(article.CategoryId);
        return article;
    }
}