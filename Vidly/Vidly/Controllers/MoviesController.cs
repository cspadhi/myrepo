using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // movies
        public ActionResult Index()
        {
            //var movies = GetMovies();
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            var movie = movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        public ActionResult Index1(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue) pageIndex = 1;
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "name";

            return Content(string.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));

            // return new ViewResult();
            // return Content("Hello World");
            // return HttpNotFound();
            // return new EmptyResult();
            // return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name"});

        }

        // GET: Movies/Random
        public ViewResult Random()
        {
            var movie = new Movie() { Name = "Movie 1"};
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1" },
                new Customer {Name = "Customer 2" }
            };

            var viewModle = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            //return View(movie);
            return View(viewModle);
        }
        public ActionResult Edit(int id)
        {
            return Content("Id = " + id);

        }


        /*
         Constraints: min, max, minlength, maxlength, int, float, guid
        */
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        //[Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Wall-e" }
            };
        }
    }
}

/*
 
    ActionResult:

        ViewResult              ->      View()
        PartialViewResult       ->      PartialView()
        ContentResult           ->      Content()
        RedirectResult          ->      Redirect()
        RedirectToRouteResult   ->      RedirectToAction()
        JsonResult              ->      Json()
        Fileresult              ->      File()
        HttpNotFoundResult      ->      HttpNotFound()
        EmptyResult             ->      new EmptyResult()
 
 */