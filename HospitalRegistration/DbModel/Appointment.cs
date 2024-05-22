//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalRegistration.DbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int id { get; set; }
        public int idUser { get; set; }
        public int idService { get; set; }
        public int idDoctor { get; set; }
        public System.DateTime appointmentDate { get; set; }
        public bool isActive { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Service Service { get; set; }
        public virtual User User { get; set; }
    }
}
