
#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Controllers
{
    public class JobPostsController : Controller
    {
        private readonly JobPortalWebContext _context;

        public JobPostsController(JobPortalWebContext context)
        {
            _context = context;
        }

        // GET: JobPosts
        public async Task<IActionResult> Index()
        {
            var jobPortalWebContext = _context.JobPosts.Include(j => j.Company).Include(j => j.JobType);
              
            return View(await jobPortalWebContext.ToListAsync());
        }

        // GET: JobPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts
                .Include(j => j.Company)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // GET: JobPosts/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "Id");
            return View();
        }

        // POST: JobPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Create([Bind("Id,JobTypeId,CompanyId,CreatedDate,JobDescription")] JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
               _context.JobPosts.Add(jobPost);
               _context.Add(jobPost);
              await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           // _context.Add(jobPost);
           // await _context.SaveChangesAsync();
           // return RedirectToAction(nameof(Index));
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", jobPost.CompanyId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "Id", jobPost.JobTypeId);

            return View(jobPost);
        }

        // GET: JobPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts.FindAsync(id);
            if (jobPost == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", jobPost.CompanyId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "Id", jobPost.JobTypeId);
            return View(jobPost);
        }

        // POST: JobPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTypeId,CompanyId,CreatedDate,JobDescription,JobLocation")] JobPost jobPost)
        {
            if (id != jobPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostExists(jobPost.Id))
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
            _context.Update(jobPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", jobPost.CompanyId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "Id", jobPost.JobTypeId);
            return View(jobPost);
        }

        // GET: JobPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts
                .Include(j => j.Company)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPost = await _context.JobPosts.FindAsync(id);
            _context.JobPosts.Remove(jobPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostExists(int id)
        {
            return _context.JobPosts.Any(e => e.Id == id);
        }
    }
}
