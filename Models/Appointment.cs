using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace medicalappointmentproject.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    [DisplayName("Patient Name")]
    public string PatientName { get; set; } = null!;

    [DisplayName("Medical Issue")]
    public string? MedicalIssue { get; set; }

    [DisplayName("Doctor To Visit")]
    public string? DoctorToVisit { get; set; }
    [DisplayName("Doctor Available Time")]
    public string? DoctorAvalialbeTime { get; set; }

    [DisplayName("Appointment Time")]
    public DateTime? AppointmentTime { get; set; }
    [DisplayName("Medical Issue")]
    public virtual DiseasesDoctorDetail? MedicalIssueNavigation { get; set; }
}
