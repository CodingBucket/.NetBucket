using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Core1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public IndexModel(AppDbContext db) { _db = db; }

        [TempData]
        public string Message { get; set; }

        public IList<Customer> Customers { get; private set; }

        public async Task OnGetAsync()
        {
            Customers = await _db.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var custmer = await _db.Customers.FindAsync(id);
            if (custmer != null)
            {
                _db.Customers.Remove(custmer);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
