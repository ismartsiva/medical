using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using medicalappointmentproject.Models;

namespace medicalappointmentproject.Controllers
{
    public class DiseasesDoctorDetailsController : Controller
    {
        private readonly MedicalprojectContext _context;

        public DiseasesDoctorDetailsController(MedicalprojectContext context)
        {
            _context = context;
        }

        // GET: DiseasesDoctorDetails
        public async Task<IActionResult> Index()
        {
            var medicalprojectContext = _context.DiseasesDoctorDetails.Include(d => d.SuitableDoctor);
            return View(await medicalprojectContext.ToListAsync());
        }

        // GET: DiseasesDoctorDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DiseasesDoctorDetails == null)
            {
                return NotFound();
            }

            var diseasesDoctorDetail = await _context.DiseasesDoctorDetails
                .Include(d => d.SuitableDoctor)
                .FirstOrDefaultAsync(m => m.DiseasesName == id);
            if (diseasesDoctorDetail == null)
            {
                return NotFound();
            }

            return View(diseasesDoctorDetail);
        }

        // GET: DiseasesDoctorDetails/Create
        public IActionResult Create()
        {
            ViewData["SuitableDoctorId"] = new SelectList(_context.DoctorDetails, "DoctorId", "DoctorId");
            return View();
        }

        // POST: DiseasesDoctorDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiseasesId,DiseasesName,SuitableDoctorId")] DiseasesDoctorDetail diseasesDoctorDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diseasesDoctorDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SuitableDoctorId"] = new SelectList(_context.DoctorDetails, "DoctorId", "DoctorId", diseasesDoctorDetail.SuitableDoctorId);
            return View(diseasesDoctorDetail);
        }

        // GET: DiseasesDoctorDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DiseasesDoctorDetails == null)
            {
                return NotFound();
            }

            var diseasesDoctorDetail = await _context.DiseasesDoctorDetails.FindAsync(id);
            if (diseasesDoctorDetail == null)
            {
                return NotFound();
            }
            ViewData["SuitableDoctorId"] = new SelectList(_context.DoctorDetails, "DoctorId", "DoctorId", diseasesDoctorDetail.SuitableDoctorId);
            return View(diseasesDoctorDetail);
        }

        // POST: DiseasesDoctorDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DiseasesId,DiseasesName,SuitableDoctorId")] DiseasesDoctorDetail diseasesDoctorDetail)
        {
            if (id != diseasesDoctorDetail.DiseasesName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diseasesDoctorDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseasesDoctorDetailExists(diseasesDoctorDetail.DiseasesName))
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
            ViewData["SuitableDoctorId"] = new SelectList(_context.DoctorDetails, "DoctorId", "DoctorId", diseasesDoctorDetail.SuitableDoctorId);
            return View(diseasesDoctorDetail);
        }

        // GET: DiseasesDoctorDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DiseasesDoctorDetails == null)
            {
                return NotFound();
            }

            var diseasesDoctorDetail = await _context.DiseasesDoctorDetails
                .Include(d => d.SuitableDoctor)
                .FirstOrDefaultAsync(m => m.DiseasesName == id);
            if (diseasesDoctorDetail == null)
            {
                return NotFound();
            }

            return View(diseasesDoctorDetail);
        }

        // POST: DiseasesDoctorDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DiseasesDoctorDetails == null)
            {
                return Problem("Entity set 'MedicalprojectContext.DiseasesDoctorDetails'  is null.");
            }
            var diseasesDoctorDetail = await _context.DiseasesDoctorDetails.FindAsync(id);
            if (diseasesDoctorDetail != null)
            {
                _context.DiseasesDoctorDetails.Remove(diseasesDoctorDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseasesDoctorDetailExists(string id)
        {
          return (_context.DiseasesDoctorDetails?.Any(e => e.DiseasesName == id)).GetValueOrDefault();
        }
    }
}
