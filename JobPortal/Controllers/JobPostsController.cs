
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
using PagedList;
using System.Web;
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
        public  async Task<ActionResult>  Index(int? page,string searchString)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.
            // 2. Nếu page = null thì đặt lại là 1.
            //if (page == null) page = 1;
            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo BookID mới có thể phân trang.
            //var jobPortalWebContext = _context.JobPosts.Include(j => j.Company).
            //    Include(j => j.JobType).OrderBy(p => p.Id);
            var jobPortalWebContext = _context.JobPosts.Include(j => j.Company).
              Include(j => j.JobType);
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            //int pageSize = 4;
            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            //int pageNumber = (page ?? 1);

            if (!String.IsNullOrEmpty(searchString))
            {
                return View( await jobPortalWebContext.Where(j => j.JobType.JobType1.Contains(searchString)).ToListAsync());
            }
            return View(await jobPortalWebContext.ToListAsync());
        }
        public async Task<IActionResult> GetAllJobs(string jobName="")
        {
            var jobPortalWebContext = _context.JobPosts.Include(j => j.Company).Include(j => j.JobType);
            if (!String.IsNullOrEmpty(jobName))
            {
                return View(await jobPortalWebContext.Where(j => j.JobType.JobType1.Contains(jobName)).ToListAsync());
            }
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
            ViewData["ApplyOrCancel"] = "Apply";
            ViewData["Function"] = "ApplyJob";
            if (HttpContext.Session.GetInt32("UserAccountId") != null)
            {
                int userAccountId = 0;
                userAccountId = (int)HttpContext.Session.GetInt32("UserAccountId");
                var isAvailableJob = await _context.JobPostActivities.Where(s => s.JobPostId == id && s.UserAccountId == userAccountId).ToListAsync();
                if (isAvailableJob.Count > 0)
                {
                    ViewData["ApplyOrCancel"] = "Cancel";
                    ViewData["Function"] = "CancelApplyJob";
                }

            }


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
     
        public async Task<IActionResult> Create([Bind("Id,JobTypeId,CompanyId,CreatedDate,JobDescription,JobLocation")] JobPost jobPost)
        {
            //if (ModelState.IsValid)
            //{
                 //_context.Add(jobPost);
                 //await _context.SaveChangesAsync();          
                 //_context.Add(jobPost);
                 //await _context.SaveChangesAsync();
                  //return RedirectToAction(nameof(Index));
            //}
            _context.Add(jobPost);
            await _context.SaveChangesAsync();
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", jobPost.CompanyId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "Id", jobPost.JobTypeId);
            return RedirectToAction(nameof(Index));

            //return View(jobPost);
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
            //Delete jobpost elemenst in jobPostActivities
            var jobPostActivities = await _context.JobPostActivities.Where
                (p => p.JobPostId == id).ToListAsync();
            if (jobPostActivities.Any())
            {
                for(int i = 0;i< jobPostActivities.Count; i++)
                {
                    var activity = jobPostActivities[i];
                    _context.JobPostActivities.Remove(activity);
                }
                await _context.SaveChangesAsync();
            }
            //Delete jobPost elements in JobPosts
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
