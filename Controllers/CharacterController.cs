using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Controllers;

public class CharacterController : Controller
{
    private readonly ILogger<CharacterController> _logger;
    private readonly AppDbContext _context;

    public CharacterController(ILogger<CharacterController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var characters = _context.Characters
                    .Include(c => c.Actor)
                    .Include(c => c.Movie)
                    .ToList();

        return View(model: _context.Characters.ToList());
    }

    public IActionResult Create()
    {
        ViewBag.Actors = _context.Actors.ToList();
        ViewBag.Movies = _context.Movies.ToList();

        return View();
    }

    [HttpPost]
    public IActionResult Store(Character character)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Actors = _context.Actors.ToList();
            ViewBag.Movies = _context.Movies.ToList();
            return View("Create", character);
        }

        _context.Characters.Add(character);
        _context.SaveChanges();


        TempData["SuccessMessage"] = $"{character.CharacterName} was added successfully!";

        return RedirectToAction("Index", "Character");
    }
    public IActionResult Edit(int id)
    {
        var character = _context.Characters.Find(id);

        if (character == null)
        {
            return NotFound();
        }

        ViewBag.Actors = _context.Actors.ToList();
        ViewBag.Movies = _context.Movies.ToList();

        return View(character);
    }

    [HttpPost]
    public IActionResult Update(Character character)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Actors = _context.Actors.ToList();
            ViewBag.Movies = _context.Movies.ToList();

            return View("Edit", character);
        }

        _context.Characters.Update(character);
        _context.SaveChanges();

        TempData["SuccessMessage"] = $"{character.CharacterName} was updated successfully!";

        return RedirectToAction("Index", "Character");
    }

    [HttpPost]
    public IActionResult Destroy(int id)
    {
        var character = _context.Characters.Find(id);

        if (character == null)
        {
            return NotFound();
        }

        _context.Characters.Remove(character);
        _context.SaveChanges();

        TempData["SuccessMessage"] = $"{character.CharacterName} was deleted successfully!";

        return RedirectToAction("Index", "Character");
    }
}