using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using CRUDelicious.Models;
using CRUDelicious.ViewModels;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {

        private DishContext _context;

        public HomeController(DishContext context)
        {
            _context = context;
        }

        // GET: /Home/------------------------------------------------------------------------

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = _context.Dishes.ToList ();
            IEnumerable<Dish> SortedDishes = AllDishes.OrderByDescending(d => d.UpdatedAt);

            return View(SortedDishes);
        }

        //-----------NEW---------------------------------------------------------------
        
        [HttpGet]
        [Route("New")]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(DishViewModel dish)
        {
            if (!ModelState.IsValid)
            {
                return View("New");
            }

            Dish newDish = new Dish
            {
                DishName = dish.DishName,
                Chef = dish.Chef,
                Tastiness =dish.Tastiness,
                Calories = dish.Calories,
                Description = dish.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };
            _context.Add(newDish);
            _context.SaveChanges();
            Console.WriteLine("######################################################ADEDD##########################");
            return RedirectToAction("Index");
        }

        //--------SHOW------------------------------------------------------------------

        [HttpGet]
        [Route("Show")]
        public IActionResult Show()
        {
            return RedirectToAction("Show");
        }

        //--------Show Dish by {DISH ID}------------------------------------------------------------------

        [HttpGet]
        [Route("{dish_id}")]
        public IActionResult Show(int dish_id)
        {
            Dish RetrievedDish = _context.Dishes.SingleOrDefault(dish=> dish.Id == dish_id);

            return View("Show", RetrievedDish);
        }

        //------------EDIT DISH--------------------------------------------

        [HttpGet]
        [Route("Edit/{dish_id}")]
        public IActionResult Edit(int dish_id)
        {
            Dish RetrievedDish = _context.Dishes.SingleOrDefault(dish=> dish.Id == dish_id);
            DishViewModel ThisDish = new DishViewModel
            {
                DishName = RetrievedDish.DishName,
                Chef = RetrievedDish.Chef,
                Tastiness = RetrievedDish.Tastiness,
                Calories = RetrievedDish.Calories,
                Description = RetrievedDish.Description

            };
            
            return View("Edit", ThisDish);
        }

        //------------POST/SAVE EDITS--------------------------------------------

        [HttpPost]
        [Route("Update/{dish_id}")]
        public IActionResult Update(DishViewModel editDish, int dish_id)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit/{dish_id}");
            }

            Dish RetrievedDish = _context.Dishes.SingleOrDefault(dish=> dish.Id == dish_id);
            RetrievedDish.DishName = editDish.DishName;
            RetrievedDish.Chef = editDish.Chef;
            RetrievedDish.Tastiness = editDish.Tastiness;
            RetrievedDish.Calories = editDish.Calories;
            RetrievedDish.Description = editDish.Description;
            RetrievedDish.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //------------DELETE-------------------------------------

        [HttpGet]
        [Route("Delete/{dish_id}")]
        public IActionResult Delete(int dish_id)
        {
            Dish RetrievedDish = _context.Dishes.SingleOrDefault(dish=> dish.Id == dish_id);
            _context.Dishes.Remove(RetrievedDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
