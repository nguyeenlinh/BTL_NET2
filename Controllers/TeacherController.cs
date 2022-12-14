using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL_NET2.Data;
using BTL_NET2.Models;

namespace BTL_NET2.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teacher
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return _context.Teachers != null ?
                        View(await _context.Teachers.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Teachers'  is null.");
            }
            return RedirectToAction("Login", "User");

        }

        // GET: Teacher/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.Teachers == null)
                {
                    return NotFound();
                }

                var teacher = await _context.Teachers
                    .FirstOrDefaultAsync(m => m.TeacherID == id);
                if (teacher == null)
                {
                    return NotFound();
                }

                return View(teacher);
            }
            return RedirectToAction("Login", "User");

        }

        // GET: Teacher/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,TeacherName,TeacherAge,TeacherAdd,Email,Phone")] Teacher teacher)
        {

            //  if (HttpContext.Session.GetString("UserId") != null)
            //  {}
            //  return RedirectToAction("Login", "User");
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(teacher);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(teacher);
            }
            return RedirectToAction("Login", "User");


        }

        // GET: Teacher/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.Teachers == null)
                {
                    return NotFound();
                }

                var teacher = await _context.Teachers.FindAsync(id);
                if (teacher == null)
                {
                    return NotFound();
                }
                return View(teacher);
            }
            return RedirectToAction("Login", "User");


        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TeacherID,TeacherName,TeacherAge,TeacherAdd,Email,Phone")] Teacher teacher)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id != teacher.TeacherID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(teacher);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TeacherExists(teacher.TeacherID))
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
                return View(teacher);
            }
            return RedirectToAction("Login", "User");


        }

        // GET: Teacher/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.Teachers == null)
                {
                    return NotFound();
                }

                var teacher = await _context.Teachers
                    .FirstOrDefaultAsync(m => m.TeacherID == id);
                if (teacher == null)
                {
                    return NotFound();
                }

                return View(teacher);
            }
            return RedirectToAction("Login", "User");


        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (_context.Teachers == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Teachers'  is null.");
                }
                var teacher = await _context.Teachers.FindAsync(id);
                if (teacher != null)
                {
                    _context.Teachers.Remove(teacher);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "User");


        }

        private bool TeacherExists(string id)
        {
            return (_context.Teachers?.Any(e => e.TeacherID == id)).GetValueOrDefault();
        }
    }
}
