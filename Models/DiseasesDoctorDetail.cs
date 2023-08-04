using System;
using System.Collections.Generic;

namespace medicalappointmentproject.Models;

public partial class DiseasesDoctorDetail
{
    public int DiseasesId { get; set; }

    public string DiseasesName { get; set; } = null!;

    public int? SuitableDoctorId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual DoctorDetail? SuitableDoctor { get; set; }
}
