using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnetapp.Models;
using Microsoft.EntityFrameworkCore;
using aspnetapp.Controllers;

namespace aspnetapp.Controllers
{
    
    public class SalesController : Controller
    {
        private readonly SalesContext _context;

        public SalesController(SalesContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {   
            List<Sales> sales = Task.Run(GetSales).Result.Value.ToList();
            return View(sales);
        }

        public IActionResult Create()
        {
            PopulateListSalesPerson();
            return View();
        }

        public async Task<IActionResult> Edit(long? id)
        {
            PopulateListSalesPerson(); 

            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.SalesData.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            return View(sales);
        }

        public IActionResult Salesperson()
        {
            return View();
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sales>>> GetSales()
        {
            return await _context.SalesData.ToListAsync();
        }

        // GET: api/Sales/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Sales>> GetSalesId(long id)
        {
            var salesItem = await _context.SalesData.FindAsync(id);

            if (salesItem == null)
            {
                return NotFound();
            }

            return salesItem;
        }

        // Create Sale (Item 2A - Check)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostSales([Bind("Id,CustomerName,Salesperson,Price,hasPayment")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sales);
        }

        // Update Sale (Item 2C - Check)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutSales(long id, [Bind("Id,CustomerName,Salesperson,Price,hasPayment")] Sales sales)
        {
            System.Diagnostics.Debug.WriteLine(sales);
            if (id != sales.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sales);
        }

        // Delete Sale (Item 2C - Check)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSales(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sales = await _context.SalesData.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }else{
                _context.SalesData.Remove(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
        }

        private bool SalesExists(long id)
        {
            return _context.SalesData.Any(e => e.Id == id);
        }

        private void PopulateListSalesPerson()
        {
            var listName = from sp in _context.SalesPersonData select sp; //select nos cadastros de SalesPerson
            ViewBag.listName_ = listName.ToList().OrderBy(sp => sp.Name); //Popula lista em ordem alfabética
        }



    }
}