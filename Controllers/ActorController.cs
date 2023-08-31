using Microsoft.AspNetCore.Mvc;
using MovieApp;
using MovieApp.Models;

namespace MovieApp.Controllers;

public class ActorController : Controller
{
    private readonly ILogger<ActorController> _logger;
    private readonly AppDbContext _context;

    public ActorController(ILogger<ActorController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index() 
    {
        return View(model: _context.Actors.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Store(Actor actor)
    {
        if(!ModelState.IsValid){
            return View("Create", actor);
        }

        _context.Actors.Add(actor);
        _context.SaveChanges();
        

        TempData["SuccessMessage"] = $"{actor.Name} was added successfully!";
        
        return RedirectToAction("Index", "Actor");
    }
    public IActionResult Edit(int id)
    {
        var actor = _context.Actors.Find(id);

        if(actor == null){
            return NotFound();
        }

        return View(actor);
    }

    [HttpPost]
    public IActionResult Update(Actor actor)
    {
        if(!ModelState.IsValid){
            return View("Edit", actor);
        }

        _context.Actors.Update(actor);
        _context.SaveChanges();
        
        TempData["SuccessMessage"] = $"{actor.Name} was updated successfully!";
        
        return RedirectToAction("Index", "Actor");
    }

    [HttpPost]
    public IActionResult Destroy(int id)
    {
        var actor = _context.Actors.Find(id);

        if(actor == null){
            return NotFound();
        }
        
        _context.Actors.Remove(actor);
        _context.SaveChanges();

        TempData["SuccessMessage"] = $"{actor.Name} was deleted successfully!";

        return RedirectToAction("Index", "Actor");
    }
}