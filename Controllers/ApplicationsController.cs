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
    //Applications controller with permission.
    public class ApplicationsController : Controller
    {
        private readonly Apply_For_Jobs_MVCContext _context;

        public ApplicationsController(Apply_For_Jobs_MVCContext context)
        {
            _context = context;
        }

        // GET: Applications
        //Gets all applications usinga a lamda query.
        public IActionResult Index()
        {
            var apply_For_Jobs_MVCContext = _context.Application.Include(a => a.Advertisement).Include(a => a.Advertisement.Employer).Include(a => a.Candidate);
            return View( apply_For_Jobs_MVCContext.ToList());
        }

        // GET: Applications/Details/5
        //Gets the details for an application
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = _context.Application
                .Include(a => a.Advertisement)
                 .Include(a => a.Advertisement.Employer)
                .Include(a => a.Candidate)
                .FirstOrDefault(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        //Gets the create application form.
        public IActionResult Create()
        {
            ViewData["AdvertisementId"] = new SelectList(_context.Advertisement.Include(a=>a.Employer), "Id", "AdvertisementDisplayId");
            ViewData["CandidateId"] = new SelectList(_context.Set<Candidate>(), "Id", "RegsitrationNumber");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds an appplication to the databse.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,AdvertisementId,CandidateId")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdvertisementId"] = new SelectList(_context.Advertisement.Include(a=> a.Employer), "Id", "AdvertisementDisplayId", application.AdvertisementId);
            ViewData["CandidateId"] = new SelectList(_context.Set<Candidate>(), "Id", "RegsitrationNumber", application.CandidateId);
            return View(application);
        }

        // GET: Applications/Edit/5
        //Gets the application for edit  using a linq query.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = (from app in _context.Application
                               where app.Id == id
                               select app).FirstOrDefault();
            if (application == null)
            {
                return NotFound();
            }
            ViewData["AdvertisementId"] = new SelectList(_context.Advertisement.Include(a => a.Employer), "Id", "AdvertisementDisplayId", application.AdvertisementId);
            ViewData["CandidateId"] = new SelectList(_context.Set<Candidate>(), "Id", "RegsitrationNumber", application.CandidateId);
            return View(application);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates an application.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,AdvertisementId,CandidateId")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
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
            ViewData["AdvertisementId"] = new SelectList(_context.Advertisement, "Id", "Id", application.AdvertisementId);
            ViewData["CandidateId"] = new SelectList(_context.Set<Candidate>(), "Id", "Id", application.CandidateId);
            return View(application);
        }

        // GET: Applications/Delete/5
        //Gets the application for delete using a lamda query.
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application =  _context.Application
                .Include(a => a.Advertisement)
                .Include(a => a.Candidate)
                .FirstOrDefault(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        //Removes the application uses a linq query to select the app
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var application = (from app in _context.Application
                               where app.Id == id
                               select app).FirstOrDefault();
            _context.Application.Remove(application);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the appliation  for existance.
        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.Id == id);
        }
    }
}
