using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Apply_For_Jobs_Core_Webapp.BusinessLayer;
using Apply_For_Jobs_MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace Apply_For_Jobs_MVC.Controllers
{
    [Authorize]
    //Employer controller with permission.
    public class EmployersController : Controller
    {
        private readonly Apply_For_Jobs_MVCContext _context;

        public EmployersController(Apply_For_Jobs_MVCContext context)
        {
            _context = context;
        }

        // GET: Employers
        //Get All employers using a linq query.
        public IActionResult Index()
        {
            return View((from employee in _context.Employer select employee).ToList());
        }

        // GET: Employers/Details/5
        //Gets the details of the employer  using a lamda query.
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer =  _context.Employer
                .FirstOrDefault(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // GET: Employers/Create
        //Gets the create form .
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds the employer to the databse.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,ContactNumber,WebSite")] Employer employer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employer);
        }

        // GET: Employers/Edit/5
        //Gets the employer for update. uses a linq query.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = (from emp in _context.Employer
                            where emp.Id == id
                            select emp).FirstOrDefault();
            if (employer == null)
            {
                return NotFound();
            }
            return View(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates the employer 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,ContactNumber,WebSite")] Employer employer)
        {
            if (id != employer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employer);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployerExists(employer.Id))
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
            return View(employer);
        }

        // GET: Employers/Delete/5
        //Gets the employer for delete using  a  lamda .
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = _context.Employer
                .FirstOrDefault(m => m.Id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // POST: Employers/Delete/5
        //Delete the employer  from the databse uses a linq query to select.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employer = (from emp in _context.Employer
                            where emp.Id == id
                            select emp).FirstOrDefault();
            _context.Employer.Remove(employer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the employer for existance .
        private bool EmployerExists(int id)
        {
            return _context.Employer.Any(e => e.Id == id);
        }
    }
}
