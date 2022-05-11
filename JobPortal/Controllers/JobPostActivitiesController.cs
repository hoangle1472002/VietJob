#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;

namespace JobPortal.Controllers
{
    public class JobPostActivitiesController : Controller
    {
        private readonly JobPortalWebContext _context;

        public JobPostActivitiesController(JobPortalWebContext context)
        {
            _context = context;
        }

        // GET: JobPostActivities
        public async Task<IActionResult> Index()
        {
            var jobPortalWebContext = _context.JobPostActivities.Include(j => j.JobPost).Include(j => j.UserAccount);
            return View(await jobPortalWebContext.ToListAsync());
        }

        // GET: JobPostActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPostActivity = await _context.JobPostActivities
                .Include(j => j.JobPost)
                .Include(j => j.UserAccount)
                .FirstOrDefaultAsync(m => m.UserAccountId == id);
            if (jobPostActivity == null)
            {
                return NotFound();
            }

            return View(jobPostActivity);
        }

        // GET: JobPostActivities/Create
        public IActionResult Create()
        {
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id");
            ViewData["UserAccountId"] = new SelectList(_context.UserAccounts, "Id", "Id");
            return View();
        }

        // POST: JobPostActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserAccountId,JobPostId,ApplyDate")] JobPostActivity jobPostActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPostActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id", jobPostActivity.JobPostId);
            ViewData["UserAccountId"] = new SelectList(_context.UserAccounts, "Id", "Id", jobPostActivity.UserAccountId);
            return View(jobPostActivity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ApplyJob(int id)
        {
            JobPostActivity data = new JobPostActivity();
            int userAccountsId = 0;
            bool isValued = false;
            if(HttpContext.Session.GetInt32("UserAccountId") != null)
            {
                isValued = true;
                userAccountsId = (int)HttpContext.Session.GetInt32("UserAccountId");
                if (userAccountsId > 0 && isValued == true)
                {
                    data.JobPostId = id;
                    data.UserAccountId = userAccountsId;
                    data.ApplyDate = DateTime.Now;
                    _context.Add(data);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("GetAllJobs", "JobPosts");
                }
                else
                {
                    return RedirectToAction("Login", "UserAccounts");
                }
            }
            else
            {
                return RedirectToAction("Login", "UserAccounts");
            }
          
         
        }

        // GET: JobPostActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPostActivity = await _context.JobPostActivities.FindAsync(id);
            if (jobPostActivity == null)
            {
                return NotFound();
            }
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id", jobPostActivity.JobPostId);
            ViewData["UserAccountId"] = new SelectList(_context.UserAccounts, "Id", "Id", jobPostActivity.UserAccountId);
            return View(jobPostActivity);
        }

        // POST: JobPostActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserAccountId,JobPostId,ApplyDate")] JobPostActivity jobPostActivity)
        {
            if (id != jobPostActivity.UserAccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPostActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostActivityExists(jobPostActivity.UserAccountId))
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
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id", jobPostActivity.JobPostId);
            ViewData["UserAccountId"] = new SelectList(_context.UserAccounts, "Id", "Id", jobPostActivity.UserAccountId);
            return View(jobPostActivity);
        }

        // GET: JobPostActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPostActivity = await _context.JobPostActivities
                .Include(j => j.JobPost)
                .Include(j => j.UserAccount)
                .FirstOrDefaultAsync(m => m.UserAccountId == id);
            if (jobPostActivity == null)
            {
                return NotFound();
            }

            return View(jobPostActivity);
        }

        // POST: JobPostActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPostActivity = await _context.JobPostActivities.FindAsync(id);
            _context.JobPostActivities.Remove(jobPostActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostActivityExists(int id)
        {
            return _context.JobPostActivities.Any(e => e.UserAccountId == id);
        }
    }
}
