#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Providers.Entities;
using Microsoft.AspNetCore.Http;

namespace JobPortal.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly JobPortalWebContext _context;

        public UserAccountsController(JobPortalWebContext context)
        {
            _context = context;
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index()
        {
            var jobPortalWebContext = _context.UserAccounts.Include(u => u.UserType);
            return View(await jobPortalWebContext.ToListAsync());
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "Id", "Id");
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserTypeId,Email,Password,DateOfBirth,Gender,ContactNumber,UserImageUrl")] UserAccount userAccount)
        {
            //     if (ModelState.IsValid)
            //     {
            var check = _context.UserAccounts.Where(s => s.Email.Equals(userAccount.Email));
            if(check == null)
            {
                userAccount.RegistrationDate = DateTime.Now;
                userAccount.Password = GetMD5(userAccount.Password);
                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.error = "Email already exists";
                return View();
            }
            //ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "Id", "Id", userAccount.UserTypeId);
            //return View(userAccount);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = _context.UserAccounts.Where(s => s.Email.Equals(email)
                    && s.Password.Equals(f_password)).ToList();
                
                if (data.Count > 0)
                {
                    //Add Session
                    HttpContext.Session.SetInt32("UserAccountId", data.FirstOrDefault().Id);
                    int a = (int)HttpContext.Session.GetInt32("UserAccountId");
                    HttpContext.Session.SetString("Email", data.FirstOrDefault().Email);
                    HttpContext.Session.SetInt32("UserTypeId", data.FirstOrDefault().UserTypeId);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.error = "Login Failed";
                    return View();
                }

            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();//Remove session of login
            return RedirectToAction("Login");
        }
     
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        //create a string MD5



        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "Id", "Id", userAccount.UserTypeId);
            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserTypeId,Email,Password,DateOfBirth,Gender,ContactNumber,UserImageUrl,RegistrationDate")] UserAccount userAccount)
        {
            if (id != userAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountExists(userAccount.Id))
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
            ViewData["UserTypeId"] = new SelectList(_context.UserTypes, "Id", "Id", userAccount.UserTypeId);
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.UserAccounts.FindAsync(id);
            _context.UserAccounts.Remove(userAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _context.UserAccounts.Any(e => e.Id == id);
        }


     

    }
}
