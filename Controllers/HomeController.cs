using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.TopFloppedMovies = _context.Movies
                .Where(m => m.Budget > m.Gross)
                .OrderByDescending(m => m.Budget - m.Gross)
                .ThenBy(m => m.Rating)
                .Take(5)
                .ToList();


        ViewBag.TopPaidCharacters = _context.Characters
                .GroupBy(c => c.ActorID)
                .Select(g => new
                {
                    ActorID = g.Key,
                    MovieID = g.Max(c => c.MovieID),
                    Pay = g.Max(c => c.Pay),
                    CharacterName = g.OrderByDescending(c => c.Pay).First().CharacterName
                })
                .OrderByDescending(a => a.Pay)
                .Take(5)
                .Join(_context.Actors,
                    character => character.ActorID,
                    actor => actor.ID,
                    (character, actor) => new
                    {
                        Actor = actor,
                        Character = character,
                    })
                .Join(_context.Movies,
                    character => character.Character.MovieID,
                    movie => movie.ID,
                    (character, movie) => new
                    {
                        Actor = character.Actor,
                        Character = character.Character,
                        Movie = movie
                    })
                .ToList();

        foreach (var item in ViewBag.TopPaidCharacters)
        {
            Console.WriteLine($"{item.Actor.Name} - {item.Character.CharacterName}");
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
