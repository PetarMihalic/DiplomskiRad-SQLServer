using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SneakerShopSQLServer.Data;
using SneakerShopSQLServer.Models;

namespace SneakerShopSQLServer.Pages.Carts
{
    public class DeleteModel : PageModel
    {
        private readonly SneakerShopContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(SneakerShopContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Cart Cart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? quantity)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var cart = await _context.Carts.FindAsync(id);

            if (cart != null)
            {
                Cart = cart;
                _context.Carts.Remove(Cart);
                await _context.SaveChangesAsync();

                int? previousQuantity = int.Parse(HttpContext.Session.GetString("Cart"));
                int? newQuantity = previousQuantity - quantity;
                HttpContext.Session.SetString("Cart", newQuantity.ToString());
            }
            stopwatch.Stop();
            _logger.LogInformation("Cart Delete Time: {0}", stopwatch.ElapsedMilliseconds);
            return RedirectToPage("./Index");
        }
    }
}
