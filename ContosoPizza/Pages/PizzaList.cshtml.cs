using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Authorization;

namespace ContosoPizza.Pages
{
    [Authorize]
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get; set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }
        [BindProperty]
        public Pizza NewPizza { get; set; } = default!;
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            _service.AddPizza(NewPizza);

            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            if (!_service.PizzaExists(id))
            {
                // Mensaje de error
                TempData["ErrorMessage"] = "Pizza doesn't exist"; // Puedes personalizar este mensaje seg�n tus necesidades

                // Redireccionar a la vista
                return RedirectToAction("Get");
            }

            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
    }
}
