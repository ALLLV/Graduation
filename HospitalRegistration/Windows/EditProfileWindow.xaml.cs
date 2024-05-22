using HospitalRegistration.DbModel;
using HospitalRegistration.Helpers;
using HospitalRegistration.Validators;
using System;
using System.Windows;
using System.Windows.Input;

namespace HospitalRegistration.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditProfileWindow.xaml
    /// </summary>
    public partial class EditProfileWindow : Window
    {
        private readonly User currentUser;

        public EditProfileWindow(User currentUser)
        {
            InitializeComponent();

            this.currentUser = currentUser;

            TbEmail.Text = currentUser.email;
            TbFirstName.Text = currentUser.firstName;
            TbLastName.Text = currentUser.lastName;
            TbPhone.Text = currentUser.phone;
            TbInsurance.Text = currentUser.insuranceNumber;
            CbGender.SelectedIndex = currentUser.idGender == 1 ? 0 : 1;
            TbBirthday.Text = currentUser.birthday.ToString("d");
        }

        private void MainBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (!NullObjectsValidator.Validate(
                TbEmail.Text,
                TbFirstName.Text,
                TbLastName.Text,
                TbPhone.Text,
                TbInsurance.Text,
                CbGender.Text,
                TbBirthday.Text) && !DateTextboxValidator.Validate(TbBirthday.Text))
            {
                MessageBox.Show("Ошибка входных данных!");
                return;
            }

            currentUser.email = TbEmail.Text;
            currentUser.firstName = TbFirstName.Text;
            currentUser.lastName = TbLastName.Text;
            currentUser.middleName = NullObjectsValidator.Validate(TbMiddleName.Text) ? TbMiddleName.Text : null;
            currentUser.phone = TbPhone.Text;
            currentUser.insuranceNumber = TbInsurance.Text;
            currentUser.idGender = CbGender.Text == "М" ? 1 : 2;
            currentUser.birthday = Convert.ToDateTime(TbBirthday.Text);

            try
            {
                var result = MessageBox.Show("Обновить?", "Подтвердите действие", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                    return;

                DbConnectionHelper.Instance.DbContext.SaveChanges();

                MessageBox.Show("Информация о профиле успешно обновлена!");
            }
            catch
            {
                MessageBox.Show($"При обновлении данных о профиля произошла ошибка, " +
                    $"проверьте правильность введенных данных!");
            }
        }

        private void TbInsurance_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!DigitTextboxValidator.Validate(e.Key))
                e.Handled = true;
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationHelper.Navigate(this, new ProfileWindow(currentUser));
        }
    }
}
