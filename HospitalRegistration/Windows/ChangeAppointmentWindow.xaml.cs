using HospitalRegistration.DbModel;
using HospitalRegistration.Helpers;
using HospitalRegistration.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HospitalRegistration.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeAppointmentWindow.xaml
    /// </summary>
    public partial class ChangeAppointmentWindow : Window
    {
        private readonly User currentUser;
        private readonly Appointment currentAppointment;
        public ChangeAppointmentWindow(User currentUser, Appointment currentAppointment)
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
            this.currentAppointment = currentAppointment;
            CbDoctor.ItemsSource = doctors;
            CbService.ItemsSource = services;

            CbDoctor.Text = currentAppointment.Doctor.firstName + " " +
                currentAppointment.Doctor.lastName + " " +
                currentAppointment.Doctor.middleName;

            CbService.Text = currentAppointment.Service.name;

            TbDate.Text = currentAppointment.appointmentDate.ToString("dd.MM.yyyy HH:mm");
        }

        private void MainBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!NullObjectsValidator.Validate(
                CbDoctor.Text,
                CbService.Text) && !DateTextboxValidator.Validate(TbDate.Text))
            {
                MessageBox.Show("Ошибка входных данных!");
                return;
            }

            Doctor doctor = DbConnectionHelper.Instance.DbContext.Doctor.FirstOrDefault(x => string.Concat(x.firstName + " " + x.lastName + " " + x.middleName) ==
                CbDoctor.Text);

            Service service = DbConnectionHelper.Instance.DbContext.Service.FirstOrDefault(x => x.name == CbService.Text);

            try
            {
                var result = MessageBox.Show("Обновить?", "Подтвердите действие", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                if (DateTime.Parse(TbDate.Text) != currentAppointment.appointmentDate)
                    currentAppointment.appointmentDate = DateTime.Parse(TbDate.Text);

                if (currentAppointment.Service != service)
                    currentAppointment.Service = service;

                if (currentAppointment.Doctor != doctor)
                    currentAppointment.Doctor = doctor;

                DbConnectionHelper.Instance.DbContext.SaveChanges();

                MessageBox.Show("Запись успешно обновлена!");

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
