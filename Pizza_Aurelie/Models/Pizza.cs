using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pizza_Aurelie.Models
{
    public class Pizza
    {
        //Je dis que lors de la sérialisation avec la méthode json de controlleur, que l'id soit ignoré ;using System.Text.Json.Serialization;
        [JsonIgnore]
        public int PizzaID { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Prix (€)")]
        public float Prix { get; set; }
        [Display(Name = "Végétarienne")]
        public bool EstVegetarienne { get; set; }
        [JsonIgnore]
        public string Ingredients { get; set; }
        [NotMapped]
        [JsonPropertyName("Ingredients")]
        public List<string> ListeIngredients
        {
            get
            {
                if (Ingredients == null || Ingredients.Count() == 0)
                {
                    return null;
                }
                return Ingredients.Split(", ").ToList();
            }
        }
    }
}
