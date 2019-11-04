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
    //Advertisement controller with access permission.
    public class AdvertisementsController : Controller
    {
        private readonly Apply_For_Jobs_MVCContext _context;

        public AdvertisementsController(Apply_For_Jobs_MVCContext context)
        {
            _context = context;
        }

        // GET: Advertisements
        //Get all advertisements using a lamda query.
        public IActionResult Index()
        {
            var apply_For_Jobs_MVCContext = _context.Advertisement.Include(a => a.Employer);
            return View( apply_For_Jobs_MVCContext.ToList());
        }

        // GET: Advertisements/Details/5
        //Get  advertisement details using a lamda query.
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement =  _context.Advertisement
                .Include(a => a.Employer)
                .FirstOrDefault(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        //Returns the create form.
        public IActionResult Create()
        {
            ViewData["EmployerId"] = new SelectList(_context.Set<Employer>(), "Id", "Name");
            ViewData["JobType"] = new SelectList(Enum.GetValues(typeof(JobType)));
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds an Advertisement to the database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,EmployerId,SalaryInformation,JobType")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertisement);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployerId"] = new SelectList(_context.Set<Employer>(), "Id", "Id", advertisement.EmployerId);
            return View(advertisement);
        }

        // GET: Advertisements/Edit/
        //Gets an advert for update using a liq query.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = (from advert in _context.Advertisement
                                 where advert.Id == id
                                 select advert).FirstOrDefault();
            if (advertisement == null)
            {
                return NotFound();
            }
            ViewData["EmployerId"] = new SelectList(_context.Set<Employer>(), "Id", "Name", advertisement.EmployerId);
            ViewData["JobType"] = new SelectList(Enum.GetValues(typeof(JobType)), advertisement.JobType);
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Advertisement update 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description,EmployerId,SalaryInformation,JobType")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
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
            ViewData["EmployerId"] = new SelectList(_context.Set<Employer>(), "Id", "Id", advertisement.EmployerId);
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        //Gets the advert for delete.
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement =  _context.Advertisement
                .Include(a => a.Employer)
                .FirstOrDefault(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        //Removes the advert selects the advert using  linq query.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var advertisement = (from advert in _context.Advertisement
                                 where advert.Id == id
                                 select advert).FirstOrDefault();
            _context.Advertisement.Remove(advertisement);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the advertisement for existance.
        private bool AdvertisementExists(int id)
        {
            return _context.Advertisement.Any(e => e.Id == id);
        }
    }
}
