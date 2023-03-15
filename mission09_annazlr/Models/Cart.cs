using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mission09_annazlr.Models
{
    public class Cart
    {
        //This is for adding an item to the cart, and keeping track of quantity.
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();

        public virtual void AddItem (Book bo, int qty)
        {
            CartLineItem line = Items
                .Where(b => b.Book.BookId == bo.BookId)
                .FirstOrDefault();

            if(line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = bo,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }
        //virtual allows it to be overwritten
        public virtual void DeleteItem (Book book)
        {
            Items.RemoveAll(x => x.Book.BookId == book.BookId);
        }

        public virtual void clearCart()
        {
            Items.Clear();
        }

        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }
    }



    //This is for initializing the items needed in the cart.
    public class CartLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }

    }
}
