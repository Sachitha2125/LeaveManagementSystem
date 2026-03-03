using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Services;
using LeaveManagementSystem.Models.LeaveTypes;
using AutoMapper;

namespace LeaveManagementSystem.Controllers
{
    public class LeaveTypesController(ILeaveTypeService _leaveTypeSevice) : Controller
    {
        private const string NameExistsValidationMessage = "This leave type already exists in the database";

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
           
            var viewData = await _leaveTypeSevice.GetAllLeaveTypes();
            // return view model to view  
            return View(viewData);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType =await  _leaveTypeSevice.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
           //Adding custom validation and model state erro
            if (await _leaveTypeSevice.CheckIfLeaveTypeNameExists(leaveTypeCreate.LeaveTypeName))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.LeaveTypeName), "This Leave Type already exists in the database");
            }
            if (ModelState.IsValid)
            {
                await _leaveTypeSevice.Create(leaveTypeCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCreate);
        }
        
        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeSevice.Get<LeaveTypeEditVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.LeaveTypeId)
            {
                return NotFound();
            }
            //Adding Custom validation and model state error
            if(await _leaveTypeSevice.CheckIfLeaveTypeNameExistsForEdit(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.LeaveTypeName), NameExistsValidationMessage);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypeSevice.Edit(leaveTypeEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_leaveTypeSevice.LeaveTypeExists(leaveTypeEdit.LeaveTypeId))
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
            return View(leaveTypeEdit);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeSevice.Get<LeaveTypeReadOnlyVM>(id.Value);
                
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leaveTypeSevice.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
