using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCompleteAPI.Models
{
    public class AutocompleteContext : DbContext
    {
        public AutocompleteContext(DbContextOptions<AutocompleteContext> options) : base(options)
        {
        }
        public DbSet<Autocomplete> Autocomplete { get; set; } 
    }
}
