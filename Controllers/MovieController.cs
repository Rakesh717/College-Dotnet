using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;

namespace MovieApp.Controllers;

public class MovieController : Controller
{
    private readonly ILogger<MovieController> _logger;
    private readonly AppDbContext _context;

    public MovieController(ILogger<MovieController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index() 
    {
        return View(model: _context.Movies.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Store(Movie movie)
    {
        if(!ModelState.IsValid){
            return View("Create", movie);
        }

        _context.Movies.Add(movie);
        _context.SaveChanges();
        

        TempData["SuccessMessage"] = $"{movie.Title} was added successfully!";
        
        return RedirectToAction("Index", "Movie");
    }
    public IActionResult Edit(int id)
    {
        var movie = _context.Movies.Find(id);

        if(movie == null){
            return NotFound();
        }

        return View(movie);
    }

    [HttpPost]
    public IActionResult Update(Movie movie)
    {
        if(!ModelState.IsValid){
            return View("Edit", movie);
        }

        _context.Movies.Update(movie);
        _context.SaveChanges();
        
        TempData["SuccessMessage"] = $"{movie.Title} was updated successfully!";
        
        return RedirectToAction("Index", "Movie");
    }

    [HttpPost]
    public IActionResult Destroy(int id)
    {
        var movie = _context.Movies.Find(id);

        if(movie == null){
            return NotFound();
        }
        
        _context.Movies.Remove(movie);
        _context.SaveChanges();

        TempData["SuccessMessage"] = $"{movie.Title} was deleted successfully!";

        return RedirectToAction("Index", "Movie");
    }
}