using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pizza_Aurelie.Data;
using Pizza_Aurelie.Models;

namespace Pizza_Aurelie.Pages
{
    public class MenuPizzaModel : PageModel
    {
        private readonly DataContext _dataContext;
        public IList<Pizza> Pizzas { get; set; }//Il y'a une difference entre IList et List comme par exemple l'indexeur

        public MenuPizzaModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task OnGetAsync()
        {
            Pizzas = await _dataContext.Pizzas.ToListAsync();
            Pizzas.OrderBy(p => p.Prix).ToList();
        }
    }
}
