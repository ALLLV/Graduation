using HospitalRegistration.DbModel;
using HospitalRegistration.Helpers;
using HospitalRegistration.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HospitalRegistration
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            DbConnectionHelper.InitializeConnection();

            InitializeComponent();
        }

        private void MainBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
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

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = TbLogin.Text;
            string password = PbPassword.Password;

            if (eyeFocused)
                password = TbPassword.Text;

            User loginUser = DbConnectionHelper.Instance.DbContext.User
               .FirstOrDefault(x => x.email == email && x.password == password);

            if (loginUser != null)
            {
                WindowNavigationHelper.Navigate(this, new ProfileWindow(loginUser));
                return;
            }
            
            MessageBox.Show("Пользователь не найден!");
        }

        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            WindowNavigationHelper.Navigate(this, new RegistrationWindow());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Точно выйти?", "Подтвердите действие", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                this.Close();
        }
    }
}
