using HospitalRegistration.DbModel;
using HospitalRegistration.Helpers;
using HospitalRegistration.Validators;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HospitalRegistration.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
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

        private bool eyeFocused = false;
        private void BtnEye_Click(object sender, RoutedEventArgs e)
        {
            if (!eyeFocused)
            {
                TbPassword.Text = PbPassword.Password;
                PbPassword.Visibility = Visibility.Hidden;
                TbPassword.Visibility = Visibility.Visible;

                ImgEye.Source = new BitmapImage(new Uri(@"\Media\visibility.png", UriKind.Relative));
                eyeFocused = true;
            }
            else
            {
                PbPassword.Password = TbPassword.Text;
                PbPassword.Visibility = Visibility.Visible;
                TbPassword.Visibility = Visibility.Hidden;

                ImgEye.Source = new BitmapImage(new Uri(@"\Media\visibility_off.png", UriKind.Relative));
                eyeFocused = false;
            }
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (!NullObjectsValidator.Validate(
                TbEmail.Text, 
                TbFirstName.Text, 
                TbLastName.Text, 
                TbPhone.Text, 
                TbInsurance.Text,
                eyeFocused ? TbPassword.Text : PbPassword.Password,
                CbGender.Text,
                TbBirthday.Text) && !DateTextboxValidator.Validate(TbBirthday.Text))
            {
                MessageBox.Show("Ошибка входных данных!");
                return;
            }

            User newUser = new User
            {
                email = TbEmail.Text,
                password = eyeFocused ? TbPassword.Text : PbPassword.Password,
                firstName = TbFirstName.Text,
                lastName = TbLastName.Text,
                middleName = NullObjectsValidator.Validate(TbMiddleName.Text) ? TbMiddleName.Text : null,
                phone = TbPhone.Text,
                insuranceNumber = TbInsurance.Text,
                idGender = CbGender.Text == "М" ? 1 : 2,
                birthday = Convert.ToDateTime(TbBirthday.Text)
            };

            if (DbConnectionHelper.Instance.DbContext.User.Any(x => x.email == newUser.email))
            {
                MessageBox.Show($"При создании пользователя произошла ошибка, " +
                    $"пользователь с такими данными уже существует!");
                return;
            }

            try
            {
                DbConnectionHelper.Instance.DbContext.User.Add(newUser);
                DbConnectionHelper.Instance.DbContext.SaveChanges();

                MessageBox.Show("Пользователь успешно создан!");

                WindowNavigationHelper.Navigate(this, new AuthorizationWindow());
            }
            catch
            {
                MessageBox.Show($"При создании пользователя произошла ошибка, проверьте правильность введенных данных!");
            }
        }

        private void TbInsurance_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!DigitTextboxValidator.Validate(e.Key))
                e.Handled = true;
        }
    }
}
