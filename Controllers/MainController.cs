using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Users;
using WebApi.Services;
using WebApi.Models.Article;


public class MainController : Controller
{

    private IArticleService _articleService;
    private IUserService _userService;

    public MainController(
        IArticleService articleService,
        IUserService userService)
    {
        _articleService = articleService;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    { 
        var articles = _articleService.GetAll();
        return View(articles);
    }

    [HttpGet("/show/{id}")]
    public IActionResult Show(int id)
    {
        // show one article
        var article = _articleService.GetByArticleId(id);
        return View(article);
    }


}