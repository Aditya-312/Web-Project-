using CampusCare.Data;
using CampusCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusCare.Controllers;

public class ComplaintsController : Controller
{
    private readonly ApplicationDbContext _context;

    private static readonly string[] Categories =
    {
        "Wi-Fi / Internet", "Classroom Maintenance", "Cleanliness",
        "Electrical", "Water Supply", "Library", "Other"
    };

    private static readonly string[] Priorities = { "Low", "Medium", "High" };
    private static readonly string[] Statuses = { "Submitted", "In Progress", "Resolved", "Rejected" };

    public ComplaintsController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? search, string? status)
    {
        var query = _context.Complaints.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c =>
                c.StudentName.Contains(search) ||
                c.Title.Contains(search) ||
                c.Category.Contains(search) ||
                c.Email.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(status) && Statuses.Contains(status))
            query = query.Where(c => c.Status == status);

        ViewBag.Search = search;
        ViewBag.SelectedStatus = status;
        ViewBag.Statuses = Statuses;

        return View(await query.OrderByDescending(c => c.CreatedAt).ToListAsync());
    }

    public IActionResult Create()
    {
        SetFormOptions();
        return View(new Complaint());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Complaint complaint)
    {
        if (!Categories.Contains(complaint.Category)) ModelState.AddModelError(nameof(complaint.Category), "Choose a valid category.");
        if (!Priorities.Contains(complaint.Priority)) ModelState.AddModelError(nameof(complaint.Priority), "Choose a valid priority.");

        if (ModelState.IsValid)
        {
            complaint.Status = "Submitted";
            complaint.CreatedAt = DateTime.Now;
            _context.Add(complaint);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Complaint submitted successfully.";
            return RedirectToAction(nameof(Index));
        }

        SetFormOptions();
        return View(complaint);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var complaint = await _context.Complaints.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return complaint == null ? NotFound() : View(complaint);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var complaint = await _context.Complaints.FindAsync(id);
        if (complaint == null) return NotFound();
        SetFormOptions();
        ViewBag.Statuses = Statuses;
        return View(complaint);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Complaint input)
    {
        if (id != input.Id) return NotFound();

        if (!Categories.Contains(input.Category)) ModelState.AddModelError(nameof(input.Category), "Choose a valid category.");
        if (!Priorities.Contains(input.Priority)) ModelState.AddModelError(nameof(input.Priority), "Choose a valid priority.");
        if (!Statuses.Contains(input.Status)) ModelState.AddModelError(nameof(input.Status), "Choose a valid status.");

        if (ModelState.IsValid)
        {
            var existing = await _context.Complaints.FindAsync(id);
            if (existing == null) return NotFound();

            existing.StudentName = input.StudentName;
            existing.Email = input.Email;
            existing.Category = input.Category;
            existing.Title = input.Title;
            existing.Description = input.Description;
            existing.Priority = input.Priority;
            existing.Status = input.Status;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Complaint updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        SetFormOptions();
        ViewBag.Statuses = Statuses;
        return View(input);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var complaint = await _context.Complaints.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return complaint == null ? NotFound() : View(complaint);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var complaint = await _context.Complaints.FindAsync(id);
        if (complaint != null)
        {
            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Complaint deleted.";
        }
        return RedirectToAction(nameof(Index));
    }

    private void SetFormOptions()
    {
        ViewBag.Categories = Categories;
        ViewBag.Priorities = Priorities;
    }
}
