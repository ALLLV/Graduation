using HospitalRegistration.DbModel;
using HospitalRegistration.Helpers;
using HospitalRegistration.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace HospitalRegistration.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddAppointmentWindow.xaml
    /// </summary>
    public partial class AddAppointmentWindow : Window
    {
        private readonly User currentUser;
        public AddAppointmentWindow(User currentUser)
        {
            InitializeComponent();

            List<string> doctors = new List<string>();
            List<string> services = new List<string>();

            foreach (var service in DbConnectionHelper.Instance.DbContext.Service)
            {
                services.Add(service.name);
            }

            foreach (var doctor in DbConnectionHelper.Instance.DbContext.Doctor)
            {
                doctors.Add(string.Concat(doctor.firstName + " " + doctor.lastName + " " + doctor.middleName));
            }

            this.currentUser = currentUser;
            CbDoctor.ItemsSource = doctors;
            CbService.ItemsSource = services;
        }

        private void MainBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!NullObjectsValidator.Validate(
                CbDoctor.Text,
                CbService.Text) && !DateTextboxValidator.Validate(TbDate.Text))
            {
                MessageBox.Show("Ошибка входных данных!");
                return;
            }

            Appointment appointment = new Appointment
            {
                User = currentUser,
                isActive = true,
                appointmentDate = DateTime.Parse(TbDate.Text)
            };

            Doctor doctor = DbConnectionHelper.Instance.DbContext.Doctor.FirstOrDefault(x => string.Concat(x.firstName + " " + x.lastName + " " + x.middleName) == 
                CbDoctor.Text);
            appointment.Doctor = doctor;

            Service service = DbConnectionHelper.Instance.DbContext.Service.FirstOrDefault(x => x.name == CbService.Text);
            appointment.Service = service;

            try
            {
                var result = MessageBox.Show("Добавить?", "Подтвердите действие", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                if (DbConnectionHelper.Instance.DbContext.Appointment
                    .FirstOrDefault(x => x.appointmentDate == appointment.appointmentDate) == null)
                    DbConnectionHelper.Instance.DbContext.Appointment.Add(appointment);

                DbConnectionHelper.Instance.DbContext.SaveChanges();

                MessageBox.Show("Запись успешно создана!");

                WindowNavigationHelper.Navigate(this, new ProfileWindow(currentUser));
            }
            catch
            {
                MessageBox.Show($"При создании записи произошла ошибка, проверьте правильность введенных данных!");
            }
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationHelper.Navigate(this, new ProfileWindow(currentUser));
        }
    }
}
