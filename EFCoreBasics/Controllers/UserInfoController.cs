using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCoreBasics.Models;

namespace EFCoreBasics.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly UserRepo _context;

        public UserInfoController(UserRepo context)
        {
            _context = context;
        }

        // GET: UserInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserTable.ToListAsync());
        }

        // GET: UserInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfoModel = await _context.UserTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInfoModel == null)
            {
                return NotFound();
            }

            return View(userInfoModel);
        }

        // GET: UserInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Mobile,Email")] UserInfoModel userInfoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInfoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userInfoModel);
        }

        // GET: UserInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfoModel = await _context.UserTable.FindAsync(id);
            if (userInfoModel == null)
            {
                return NotFound();
            }
            return View(userInfoModel);
        }

        // POST: UserInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Mobile,Email")] UserInfoModel userInfoModel)
        {
            if (id != userInfoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInfoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInfoModelExists(userInfoModel.Id))
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
            return View(userInfoModel);
        }

        // GET: UserInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userInfoModel = await _context.UserTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userInfoModel == null)
            {
                return NotFound();
            }

            return View(userInfoModel);
        }

        // POST: UserInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userInfoModel = await _context.UserTable.FindAsync(id);
            _context.UserTable.Remove(userInfoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInfoModelExists(int id)
        {
            return _context.UserTable.Any(e => e.Id == id);
        }
    }
}
