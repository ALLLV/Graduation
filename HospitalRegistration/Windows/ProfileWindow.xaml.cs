using HospitalRegistration.DbModel;
using HospitalRegistration.Dto;
using HospitalRegistration.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

            ObservableCollection<AppointmentDto> appointments = new ObservableCollection<AppointmentDto>();

            foreach (var appointmentFromDb in currentUser.Appointment)
            {
                appointments.Add(new AppointmentDto(appointmentFromDb));
            }
            
            DgAppointments.ItemsSource = appointments;

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

        private void BtnEditProfile_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationHelper.Navigate(this, new EditProfileWindow(currentUser));
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddRow_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationHelper.Navigate(this, new AddAppointmentWindow(currentUser));
        }

        private void BtnChangeRow_Click(object sender, RoutedEventArgs e)
        {
            if (DgAppointments.SelectedItem is null) 
                return;

            Appointment currentAppointment = DbConnectionHelper.Instance.DbContext.Appointment
                .FirstOrDefault(x => x.id == ((AppointmentDto)DgAppointments.SelectedItem).Id);
            WindowNavigationHelper.Navigate(this, new ChangeAppointmentWindow(currentUser, currentAppointment));
        }

        private void BtnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (DgAppointments.SelectedItem is null)
                return;

            Appointment currentAppointment = DbConnectionHelper.Instance.DbContext.Appointment
                .FirstOrDefault(x => x.id == ((AppointmentDto)DgAppointments.SelectedItem).Id);

            try
            {
                var result = MessageBox.Show("Удалить?", "Подтвердите действие", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                DbConnectionHelper.Instance.DbContext.Appointment.Remove(currentAppointment);
                DbConnectionHelper.Instance.DbContext.SaveChanges();

                MessageBox.Show("Запись успешно удалена!");

                WindowNavigationHelper.Navigate(this, new ProfileWindow(currentUser));
            }
            catch
            {
                MessageBox.Show($"При создании записи произошла ошибка, проверьте правильность введенных данных!");
            }
        }
    }
}
