using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnetapp.Controllers
{

    public class SalespersonController : Controller
    {
        private readonly SalesContext _context;

        public SalespersonController(SalesContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<SalesPerson> salesPerson = Task.Run(GetSalesPerson).Result.Value.ToList();
            return View(salesPerson);
        }

        // Organiza por ordem alfabetica (Item 1C - Check)
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<SalesPerson>>> GetSalesPerson()
        {
            var salesPeople = from sp in _context.SalesPersonData select sp;
            salesPeople = salesPeople.OrderBy(sp => sp.Name);


            return await salesPeople.AsNoTracking().ToListAsync();
        }
        public IActionResult Create()
        {
            return View();
        }

        //Method POST SalesPerson (Item 1D - Reference)
       [HttpPost]
       [ValidateAntiForgeryToken]        
        public async Task<ActionResult<Sales>> PostSalesPerson([Bind("Id,Name")] SalesPerson salesPerson)
        {
           if (ModelState.IsValid)
            {
                _context.Add(salesPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesPerson);
        }

        //Method GET SalesPerson (Item 1E - Reference)
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPersonData.FindAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }
            return View(salesPerson);
        }

        //Method POST SalesPerson (Item 1E - Reference)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutSalesPerson(long id, [Bind("Id,Name")] SalesPerson salesPerson)
        {
            if (id != salesPerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesPersonExists(salesPerson.Id))
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
            return View(salesPerson);
        }

        //Method DELETE SalesPerson (Item 1E - Reference)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSalesPerson(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var salesPerson = await _context.SalesPersonData.FindAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }else{
                _context.SalesPersonData.Remove(salesPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
        }

        // Checks for id (Item 1E - Reference)
        private bool SalesPersonExists(long id)
        {
            return _context.SalesPersonData.Any(e => e.Id == id);
        }
    }
}