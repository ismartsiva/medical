using System;
using System.Collections.Generic;

namespace medicalappointmentproject.Models;

public partial class DoctorDetail
{
    public int DoctorId { get; set; }

    public string DoctorName { get; set; } = null!;

    public string Specialisation { get; set; } = null!;

    public string AvailableTime { get; set; } = null!;

    public int DoctorFee { get; set; }

    public virtual ICollection<DiseasesDoctorDetail> DiseasesDoctorDetails { get; set; } = new List<DiseasesDoctorDetail>();
}
