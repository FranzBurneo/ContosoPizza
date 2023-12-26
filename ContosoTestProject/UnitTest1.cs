using ContosoPizza.Data;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.EntityFrameworkCore;

namespace ContosoTestProject;

[TestClass]
public class UnitTest1
{
    //Para la revisión: Se debe colocar la ruta donde se encuentra el archivo .db
    const string URL_DB = "Data Source=C:\\Users\\fdburneo\\Documents\\FB\\cursos\\TDD\\ContosoPizza\\ContosoPizza\\ContosoPizza.db";
    [TestMethod]
    public void GetPizzas_ShouldReturnAllPizzas()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PizzaContext>()
            .UseSqlite(URL_DB)
            .Options;

        using (var context = new PizzaContext(options))
        {
            // Agrega pizzas al contexto para la prueba
            context.Pizzas?.AddRange(
                new Pizza { Name = "Pepperoni", Size = PizzaSize.Large, IsGlutenFree = false, Price = 12.99m },
                new Pizza { Name = "Margherita", Size = PizzaSize.Medium, IsGlutenFree = true, Price = 10.99m }
            );
            context.SaveChanges();
        }

        using (var context = new PizzaContext(options)) // Crea un nuevo contexto para la prueba
        {
            var pizzaService = new PizzaService(context);

            // Act
            var result = pizzaService.GetPizzas();

            // Assert
            Assert.IsNotNull(result);
            // Cambiar dependiendo del contexto
            Assert.AreEqual(6, result.Count);
        }
    }

    [TestMethod]
    public void AddPizza_ShouldAddNewPizzaSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PizzaContext>()
            .UseSqlite(URL_DB)
            .Options;

        using (var context = new PizzaContext(options))
        {
            var pizzaService = new PizzaService(context);
            var newPizza = new Pizza { Name = "TestPizza", Size = PizzaSize.Medium, IsGlutenFree = false, Price = 15.99m };

            // Act
            pizzaService.AddPizza(newPizza);

            // Assert
            Assert.IsTrue(context.Pizzas != null && context.Pizzas.Any(p => p.Name == "TestPizza"));
        }
    }

    [TestMethod]
    public void DeletePizza_ShouldRemovePizzaSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PizzaContext>()
            .UseSqlite(URL_DB)
            .Options;

        using (var context = new PizzaContext(options))
        {
            // Agrega una pizza al contexto para la prueba
            var pizzaToDelete = new Pizza { Name = "ToDeletePizza", Size = PizzaSize.Small, IsGlutenFree = true, Price = 8.99m };
            context.Pizzas?.Add(pizzaToDelete);
            context.SaveChanges();

            var pizzaService = new PizzaService(context);

            // Act
            pizzaService.DeletePizza(pizzaToDelete.Id);

            // Assert
            Assert.IsFalse(context.Pizzas != null && context.Pizzas.Any(p => p.Id == pizzaToDelete.Id));
        }
    }

    [TestMethod]
    public void EditPizza_ShouldEditExistingPizzaSuccessfully()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<PizzaContext>()
            .UseSqlite(URL_DB)
            .Options;

        using (var context = new PizzaContext(options))
        {
            // Agrega una pizza al contexto para la prueba
            var initialPizza = new Pizza { Id = 994, Name = "InitialPizza", Size = PizzaSize.Large, IsGlutenFree = true, Price = 20.99m };
            context.Pizzas?.Add(initialPizza);
            context.SaveChanges();

            var pizza = context.Pizzas.FirstOrDefault(m => m.Id == initialPizza.Id);
         
            pizza.Name = "UpdatedPizza";
            pizza.Size = PizzaSize.Small;
            pizza.IsGlutenFree = false;
            pizza.Price = 18.99m;

            //// Act
            context.SaveChanges();

            // Assert
            var editedPizza = context.Pizzas?.Find(pizza.Id);
            Assert.IsNotNull(editedPizza);
            Assert.AreEqual(pizza.Name, editedPizza.Name);
            Assert.AreEqual(pizza.Size, editedPizza.Size);
            Assert.AreEqual(pizza.IsGlutenFree, editedPizza.IsGlutenFree);
            Assert.AreEqual(pizza.Price, editedPizza.Price);
        }
    }
}