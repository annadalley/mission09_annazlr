using Microsoft.AspNetCore.Mvc;
using mission09_annazlr.Models;
using mission09_annazlr.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mission09_annazlr.Controllers
{
    public class HomeController : Controller
    {
        //this is to insulate the application from changes in the data store.
        private IBookStoreRepository repo;
        
        public HomeController(IBookStoreRepository temp)
        {
            repo = temp;
        }

        //This is for telling how many pages, and how many entries to each page, and it also orders the data.
        public IActionResult Index(string category, int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == category || category == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks =
                        (category == null
                            ? repo.Books.Count()
                            : repo.Books.Where(x => x.Category == category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }
    }
}
