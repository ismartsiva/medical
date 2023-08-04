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
    public class AppointmentsController : Controller
    {
        private readonly MedicalprojectContext _context;

        public AppointmentsController(MedicalprojectContext context)
        {
            _context = context;
        }

        // GET: Appointments

        public IActionResult GetData(IFormCollection data)
        {
            string disease = data["Disease"];
            int doctorid = Convert.ToInt32(_context.DiseasesDoctorDetails.FirstOrDefault(d=> d.DiseasesName == disease).SuitableDoctorId);
            DoctorDetail doctor = _context.DoctorDetails.FirstOrDefault(d => d.DoctorId == doctorid);
            return Json(new { doctorname = doctor.DoctorName,timeslot = doctor.AvailableTime});
        }
        public async Task<IActionResult> Index()
        {
            var medicalprojectContext = _context.Appointments.Include(a => a.MedicalIssueNavigation);
            return View(await medicalprojectContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.MedicalIssueNavigation)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["MedicalIssue"] = new SelectList(_context.DiseasesDoctorDetails, "DiseasesName", "DiseasesName");
            //ViewData["doctor"] = new SelectList(_context.DoctorDetails, "DoctorName", "DoctorName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,PatientName,MedicalIssue,DoctorToVisit,DoctorAvalialbeTime,AppointmentTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicalIssue"] = new SelectList(_context.DiseasesDoctorDetails, "DiseasesName", "DiseasesName", appointment.MedicalIssue);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["MedicalIssue"] = new SelectList(_context.DiseasesDoctorDetails, "DiseasesName", "DiseasesName", appointment.MedicalIssue);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,PatientName,MedicalIssue,DoctorToVisit,DoctorAvalialbeTime,AppointmentTime")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["MedicalIssue"] = new SelectList(_context.DiseasesDoctorDetails, "DiseasesName", "DiseasesName", appointment.MedicalIssue);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.MedicalIssueNavigation)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'MedicalprojectContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
