using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services
{
    public class PizzaService
    {
        private readonly PizzaContext _context = default!;

        public PizzaService(PizzaContext context) 
        {
            _context = context;
        }
        
        public IList<Pizza> GetPizzas()
        {
            if(_context.Pizzas != null)
            {
                return _context.Pizzas.ToList();
            }
            return new List<Pizza>();
        }

        public void AddPizza(Pizza pizza)
        {
            if (_context.Pizzas != null)
            {
                _context.Pizzas.Add(pizza);
                _context.SaveChanges();
            }
        }

        public void DeletePizza(int id)
        {
            if (_context.Pizzas != null)
            {
                var pizza = _context.Pizzas.Find(id);
                if (pizza != null)
                {
                    _context.Pizzas.Remove(pizza);
                    
                    
                        _context.SaveChanges();
                   
                }
            }            
        }
        public bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.Id == id);
        }
        public void EditPizza(Pizza pizza)
        {
            if (_context.Pizzas != null)
            {
                var pizzaOld = _context.Pizzas.Find(pizza.Id);
                if (pizzaOld != null)
                {
                    pizzaOld = pizza;
                    
                    try
                    {
                        _context.Pizzas.Update(pizzaOld);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (PizzaExists(pizza.Id))
                        {                           
                            throw;
                        }
                    }
                }
            }
        }
    }
}
