using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using LeaveManagementSystem.Web.Data;

namespace LeaveManagementSystem.Web.Controllers;

public class LeaveTypesController : Controller
{
    private readonly ApplicationDbContext _context;

//Dependency Injection
    public LeaveTypesController(ApplicationDbContext context)
    {
       this._context = context; 
    }

    //Get LeaveTypes

    public async Task<IActionResult> Index()
    {
        var data = await _context.LeaveTypes.ToListAsync();

        return View(data);
    }

    //Get LeavesType/Details/5

    public async Task<IActionResult> Details(Guid? id)
    {
        if(id == null) return NotFound();

        var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);

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
     public async Task<IActionResult> Create([Bind("Id,Name,NumberOfDays")] LeaveType leaveType)
     {
         if (ModelState.IsValid)
         {
             leaveType.Id = Guid.NewGuid();
             _context.Add(leaveType);
             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }
         return View(leaveType);
     }

        //Update LeaveType by Id
     public async Task<IActionResult> Edit(Guid? id)
     {
        if(id==null) return NotFound();

        var leaveType = await _context.LeaveTypes.FindAsync(id);

        if (leaveType == null) return NotFound();

        return View(leaveType);



     }


     [HttpPost]
     [ValidateAntiForgeryToken]

     public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,NumberOfDays")] LeaveType leaveType)
     {
        if(id != leaveType.Id)
        {
            return NotFound();
        }

        if(ModelState.IsValid)
        {
            try
            {
                _context.Update(leaveType);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!LeaveTypeExists(leaveType.Id))
                {
                    return NotFound();
                }else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }
        return View(leaveType);

     }

//GET: LeaveTypes/Delete/5

    //Delete LeaveType

    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);

        if (leaveType == null) return NotFound();

        return View(leaveType);

    }


    [HttpPost, ActionName("Delete")]

    [ValidateAntiForgeryToken]

    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var leaveType = await _context.LeaveTypes.FindAsync(id);

        if (leaveType != null)
        {
            _context.LeaveTypes.Remove(leaveType);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));


    }








     private bool LeaveTypeExists(Guid id)
      {
          return _context.LeaveTypes.Any(e => e.Id == id);
      }

}



