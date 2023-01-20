namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Article;
using WebApi.Services;

[ApiController]
[Route("[controller]")]

public class ArticleController : Controller
{

    private IArticleService _articleService;
    private IMapper _mapper;

    public ArticleController(
        IArticleService articleService,
        IMapper mapper)
    {
        _articleService = articleService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult New()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateArticleRequest model)
    {

        _articleService.Create(model);
        return Ok(new { message = "Article created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateArticleRequest model)
    {
        _articleService.Update(id, model);
        return Ok(new { message = "Article updated" });
    }
}