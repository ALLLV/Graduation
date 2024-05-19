using HospitalRegistration.DbModel;
using HospitalRegistration.Dto;
using HospitalRegistration.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace HospitalRegistration.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private readonly User currentUser;

        public ProfileWindow(User currentUser)
        {
            InitializeComponent();
            
            this.currentUser = currentUser;

            ObservableCollection<AppointmentDto> activeAppointments = new ObservableCollection<AppointmentDto>();

            foreach (var appointmentFromDb in currentUser.Appointment.Where(x => x.isActive = true))
            {
                activeAppointments.Add(new AppointmentDto(appointmentFromDb));
            }
            
            DgActiveAppointments.ItemsSource = activeAppointments;

            TbFirstName.Text += currentUser.firstName;
            TbLastName.Text += currentUser.lastName;
            TbMiddleName.Text += string.IsNullOrWhiteSpace(currentUser.middleName) ? "-" : currentUser.middleName;
            TbLogin.Text += currentUser.email;
            TbPhone.Text += currentUser.phone;
            TbInsurance.Text += currentUser.insuranceNumber;
            TbBirthday.Text += currentUser.birthday.ToString("D");
        }

        private void MainBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationHelper.Navigate(this, new AuthorizationWindow());
        }

        private void BtnAppointments_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationHelper.Navigate(this, new AppointmentsWindow());
        }

        private void BtnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationHelper.Navigate(this, new EditProfileWindow(currentUser));
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
