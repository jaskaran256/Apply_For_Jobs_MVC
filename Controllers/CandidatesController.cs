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
    //Candidate controller with permission.
    public class CandidatesController : Controller
    {
        private readonly Apply_For_Jobs_MVCContext _context;

        public CandidatesController(Apply_For_Jobs_MVCContext context)
        {
            _context = context;
        }

        // GET: Candidates
        //Gets all candidates using a linq query.
        public IActionResult Index()
        {
            return View( (from candidate in _context.Candidate select candidate).ToList());
        }

        // GET: Candidates/Details/5
        //Get the details of the candidate using a linq query.
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate =  _context.Candidate
                .FirstOrDefault(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // GET: Candidates/Create
        //Gets the create candidate form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds  a candidate to the  database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,ContactNumber")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidate);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        //Get the candidate for update using a linq  query.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = (from candid in _context.Candidate
                             where candid.Id == id
                             select candid).FirstOrDefault();
            if (candidate == null)
            {
                return NotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates the candidate 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,ContactNumber")] Candidate candidate)
        {
            if (id != candidate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidate);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.Id))
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
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        //Gets the candidate for delete 
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate =  _context.Candidate
                .FirstOrDefault(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        //Deletes the candidate selects the candidate using a linq query.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var candidate = (from candid in _context.Candidate
                             where candid.Id == id
                             select candid).FirstOrDefault();
            _context.Candidate.Remove(candidate);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Checks the candidate exists.
        private bool CandidateExists(int id)
        {
            return _context.Candidate.Any(e => e.Id == id);
        }
    }
}
