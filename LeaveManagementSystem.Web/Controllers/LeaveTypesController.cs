using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Application.Models.LeaveTypes;
using AutoMapper;
using LeaveManagementSystem.Application.Services.LeaveTypes;


namespace LeaveManagementSystem.Web.Controllers {

[Authorize(Roles = "Administrator")]
public class LeaveTypesController(ILeaveTypeService _leaveTypeService) : Controller
{
  
    private const string NameExistValidationMessage = "This leave type already exist in the database";



        //Get LeaveTypes

    public async Task<IActionResult> Index()
    {
    var viewData = await _leaveTypeService.GetAll();

        return View(viewData);
    }

    //Get LeavesType/Details/5

    public async Task<IActionResult> Details(Guid? id)
    {
        if(id == null) return NotFound();

        var leaveType = await _leaveTypeService.Get<DetailsVM>(id.Value);

        if (leaveType == null) return NotFound();


        return View(leaveType);


    }

//GET: LeaveTypes/Create
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
        //Adding custom validation and Model state error
        // if(leaveTypeCreate.Name.Contains("vacation")){
        //     ModelState.AddModelError(nameof(leaveTypeCreate.Name), "Leave Name should not contain vacation");
        // }
        if(await _leaveTypeService.CheckIfLeaveTypeNameExist(leaveTypeCreate.Name))
        {
            ModelState.AddModelError(nameof(leaveTypeCreate.Name),NameExistValidationMessage);
        }

         if (ModelState.IsValid)
         {
            await _leaveTypeService.Create(leaveTypeCreate);
             return RedirectToAction(nameof(Index));
         }
         return View(leaveTypeCreate);
     }


    //Update LeaveType by Id
    public async Task<IActionResult> Edit(Guid? id)
     {
        if(id==null) return NotFound();

        var leaveType = await _leaveTypeService.Get<LeaveTypeEditVM>(id.Value);

        if (leaveType == null) return NotFound();

        return View(leaveType);
     }



     [HttpPost]
     [ValidateAntiForgeryToken]

     public async Task<IActionResult> Edit(Guid id, LeaveTypeEditVM leaveTypeEdit )
     {

        if(id != leaveTypeEdit.Id)
        {
            return NotFound();
        }

        if(await _leaveTypeService.CheckIfLeaveTypeNameExistForEdit(leaveTypeEdit))
        {
            ModelState.AddModelError(nameof(leaveTypeEdit.Name),NameExistValidationMessage);
        }

    

        if(ModelState.IsValid)
        {
            try
            {
                await _leaveTypeService.Edit(leaveTypeEdit);
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!_leaveTypeService.LeaveTypeExists(leaveTypeEdit.Id))
                {
                    return NotFound();
                }else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }
        return View(leaveTypeEdit);

     }







    //GET: LeaveTypes/Delete/5

    //Delete LeaveType

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveType = await _leaveTypeService.Get<LeaveTypeDeleteVm>(id.Value);

        if (leaveType == null) return NotFound();
        return View(leaveType);

    }


    [HttpPost, ActionName("Delete")]

    [ValidateAntiForgeryToken]

    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        
                await _leaveTypeService.Remove(id);
            return RedirectToAction(nameof(Index));


    }









}

}

