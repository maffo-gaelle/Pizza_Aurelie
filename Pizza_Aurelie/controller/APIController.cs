using Microsoft.AspNetCore.Mvc;
using Pizza_Aurelie.Data;
using Pizza_Aurelie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pizza_Aurelie.controller
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : Controller //On hérite ici de json au lieu de controlleurBase. Controller hérite de contrllerBase et possède plusieurs autre implémentation comme le json
    {
        private readonly DataContext _dataContext = null;

        public APIController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Route("GetPizzas")]//mon url(ma route) sera donc api/getPizzas
        public IActionResult GetPizzas()
        {
            //var pizzaTest = new Pizza()
            //{
            //    Nom = "pizza test",
            //    Prix = 8,
            //    EstVegetarienne = false,
            //    Ingredients = "tomate, salade, olives"
            //};

            var pizzas = _dataContext.Pizzas.ToList();
            //J'aurai pu télécharger le package newtonsoft.json et par serialise mais je prefère utiliser le json de la classe heritée Controller
            //en faisant ça ici mon objet pizza est directement sérialisé en objet json
            return Json(pizzas); 
        }

        // GET api/<APIController>/5
        [HttpGet("{id}")]
        
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<APIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<APIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<APIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
