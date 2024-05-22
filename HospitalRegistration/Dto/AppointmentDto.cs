using HospitalRegistration.DbModel;

namespace HospitalRegistration.Dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string AppointmentDate { get; set; }
        public bool IsActive { get; set; }
        public string Doctor { get; set; }
        public Service Service { get; set; }

        public AppointmentDto(Appointment appointment)
        {
            Id = appointment.id;
            AppointmentDate = appointment.appointmentDate.ToString();
            IsActive = appointment.isActive;
            Doctor = appointment.Doctor.firstName + " " + appointment.Doctor.lastName + " " + appointment.Doctor.middleName;
            Service = appointment.Service;
        }
    }
}
