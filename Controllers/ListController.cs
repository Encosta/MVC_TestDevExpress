using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Test.Data;
using MVC_Test.Models;

namespace MVC_Test.Controllers
{
    public class ListController : Controller
    {
        private readonly EncostaContext _context;

        public ListController(EncostaContext context)
        {
            _context = context;
        }

        // GET: List
        public async Task<IActionResult> Index()
        {
            return View(await _context.TreeList.ToListAsync());
        }

        // GET: List/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeList = await _context.TreeList
                .FirstOrDefaultAsync(m => m.TreeListId == id);
            if (treeList == null)
            {
                return NotFound();
            }

            return View(treeList);
        }

        // GET: List/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: List/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreeListId,Id,Items,ParentId,HasChild")] TreeList treeList)
        {
            if (ModelState.IsValid)
            {
                treeList.TreeListId = Guid.NewGuid();
                _context.Add(treeList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treeList);
        }

        // GET: List/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeList = await _context.TreeList.FindAsync(id);
            if (treeList == null)
            {
                return NotFound();
            }
            return View(treeList);
        }

        // POST: List/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Items,ParentId,HasChild")] TreeList treeList)
        {
            if (id != treeList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treeList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreeListExists(treeList.TreeListId))
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
            return View(treeList);
        }

        // GET: List/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treeList = await _context.TreeList
                .FirstOrDefaultAsync(m => m.TreeListId == id);
            if (treeList == null)
            {
                return NotFound();
            }

            return View(treeList);
        }

        // POST: List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var treeList = await _context.TreeList.FindAsync(id);
            _context.TreeList.Remove(treeList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreeListExists(Guid id)
        {
            return _context.TreeList.Any(e => e.TreeListId == id);
        }
    }
}
