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
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return _context.Students != null ?
                            View(await _context.Students.ToListAsync()) :
                            Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            return RedirectToAction("Login", "User");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.Students == null)
                {
                    return NotFound();
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(m => m.StudentID == id);
                if (student == null)
                {
                    return NotFound();
                }

                return View(student);
            }
            return RedirectToAction("Login", "User");
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,StudentName,StudentAge,StudentAdd,StudentClass")] Student student)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(student);
            }
            return RedirectToAction("Login", "User");
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.Students == null)
                {
                    return NotFound();
                }

                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                return View(student);
            }
            return RedirectToAction("Login", "User");
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentID,StudentName,StudentAge,StudentAdd,StudentClass")] Student student)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id != student.StudentID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(student);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StudentExists(student.StudentID))
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
                return View(student);
            }
            return RedirectToAction("Login", "User");
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (id == null || _context.Students == null)
                {
                    return NotFound();
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(m => m.StudentID == id);
                if (student == null)
                {
                    return NotFound();
                }

                return View(student);
            }
            return RedirectToAction("Login", "User");
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (_context.Students == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
                }
                var student = await _context.Students.FindAsync(id);
                if (student != null)
                {
                    _context.Students.Remove(student);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "User");
        }

        private bool StudentExists(string id)
        {
            return (_context.Students?.Any(e => e.StudentID == id)).GetValueOrDefault();
        }
    }
}
