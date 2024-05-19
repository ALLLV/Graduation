using HospitalRegistration.DbModel;
using HospitalRegistration.Helpers;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace HospitalRegistration.Dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string AppointmentDate { get; set; }
        public bool IsActive { get; set; }
        public string Doctor { get; set; }
        public string Services { get; set; }

        public AppointmentDto(Appointment appointment)
        {
            Id = appointment.id;
            AppointmentDate = appointment.appointmentDate.ToString();
            IsActive = appointment.isActive;
            Doctor = appointment.Doctor.firstName + " " + appointment.Doctor.lastName + " " + appointment.Doctor.middleName;
            foreach (var service in appointment.Service)
            {
                Services += service.name;
                Services += " ";
            }
        }
    }
}
