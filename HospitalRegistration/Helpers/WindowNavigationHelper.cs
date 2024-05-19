using System.Windows;

namespace HospitalRegistration.Helpers
{
    public static class WindowNavigationHelper
    {
        public static void Navigate(Window startup, Window destination, bool closeCurrentWindow = true)
        {
            destination.Show();
         
            if(closeCurrentWindow)
                startup.Close();
        }
    }
}
