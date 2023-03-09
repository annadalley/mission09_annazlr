using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using mission09_annazlr.Infrastructure;
using mission09_annazlr.Models;

namespace mission09_annazlr.Pages
{
    //Purchase Model for the Purchase page.
    public class PurchaseModel : PageModel
    {
        private IBookStoreRepository repo { get; set; }

        public PurchaseModel (IBookStoreRepository temp)
        {
            repo = temp;
        }

        //Makes cart object of type cart
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        //OnGet request to get the cart.
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        //Onpost request to add something to the cart cart
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(b, 1);

            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
