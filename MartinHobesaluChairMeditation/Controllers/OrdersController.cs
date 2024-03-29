﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MartinHobesaluChairMeditation.Data;
using MartinHobesaluChairMeditation.Models;
using Microsoft.AspNetCore.Authorization;
using MartinHobesaluChairMeditation.Models.ViewModels;

namespace MartinHobesaluChairMeditation.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> CompletedAmount()
        {
            return View(await _context.Order.ToListAsync());
        }

        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> CompletedOrdersAsync()
        {


            var completedOrders = await _context.Order
                .Where(r => r.CompletedAmount.Equals(r.OrderAmount))
                .ToListAsync();

            var completedAmount = await _context.Order
                .Where(r => r.CompletedAmount.Equals(r.OrderAmount))
                .CountAsync();


            int totalOrders = _context.Order.Count();

            int totalCompletedOrders = _context.Order
                .Where(r => r.CompletedAmount.Equals(r.OrderAmount))
                .Count();








            var result = new CompletedOrdersViewModel()
            {
                CompletedOrders = completedOrders,
                TotalOrders = totalOrders,
                TotalCompletedOrders = totalCompletedOrders
            };

            return View(result);
        }

        public async Task<IActionResult> TableStatus()
        {
            return View(await _context.Order.ToListAsync());
        }
        
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tone,OrderAmount,TimeOfArrival,Price")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        [Authorize]
        public IActionResult OrderCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> OrderCreate([Bind("Id,OrdererName,Tone,CompletedAmount,OrderAmount,TimeOfArrival,Price")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                order.TimeOfArrival = DateTime.Now.AddDays(7);
                order.Price = order.OrderAmount * 21;
                await _context.SaveChangesAsync();
                await _context.SaveChangesAsync();
                return View("ThankYou");
            }
            return View(order);
        }

        [Authorize]
        public async Task<IActionResult> IncreaseCompletedAmount(int id)
        {
            var order = _context.Order.FirstOrDefault(x => x.Id == id);

            if (order.CompletedAmount < order.OrderAmount)
            {
                if (order == null)
                {
                    return NotFound();
                }
                order.CompletedAmount++; 
                await _context.SaveChangesAsync();
            }
            return View("CompletedAmount", await _context.Order.ToListAsync());

        }
        // GET: Orders/Create

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.



        // GET: Orders/Edit/5

        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tone,OrderAmount,TimeOfArrival,Price")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TableStatus));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CompletedAmount));
        }

        private bool OrderExists(int id)
        {
          return _context.Order.Any(e => e.Id == id);
        }
        //Self created actions

        public async Task<IActionResult> OrderEdit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderEdit(int id, [Bind("Id,Tone,OrderAmount,TimeOfArrival,Price")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TableStatus));
            }
            return View(order);
        }



        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderDelete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }






    }
}
