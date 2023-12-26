using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Pages
{
    public class EditModel : PageModel
    {
        private readonly PizzaService _service;
        public EditModel(PizzaService service)
        {
            _service = service;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            

            //try
            //{
            //     _service.EditPizza(Pizza);
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!MovieExists(pizza.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return RedirectToPage("./Index");
        }

        //private bool MovieExists(int id)
        //{
        //    return _context.Movie.Any(e => e.Id == id);
        //}
    }
}
