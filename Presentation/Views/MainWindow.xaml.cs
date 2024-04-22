using LibBusiness.Business.Core;
using LibBusiness.Presentation.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibBusiness
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Login.firstInit();
        }

        private void gridLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Image)
            {
                Application.Current.Shutdown();
            }
        }

        private void textoUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUser.Focus();
        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text) && txtUser.Text.Length > 0)
            {
                textoUser.Visibility = Visibility.Collapsed;
            }
            else { textoUser.Visibility = Visibility.Visible; }
        }

        private void textoPasse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPasse.Focus();
        }

        private void txtPasse_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPasse.Password) && txtPasse.Password.Length > 0)
            {
                textoPasse.Visibility = Visibility.Collapsed;
            }
            else { textoPasse.Visibility = Visibility.Visible; }
        }

        private void btnAceder_Click(object sender, RoutedEventArgs e)
        {
            if (Login.validarUser(txtUser.Text, txtPasse.Password))
            {
                this.Hide();
                // Abrir a janela homepage.xaml
                Homepage homePage = new Homepage();
                //termina o programa ao fechar a homepage
                homePage.Closed += (s, args) => { Application.Current.Shutdown(); };
                homePage.Show();
            }
            else
            {
                MessageBox.Show("Falha de acesso", "Dados incorretos/inexistentes");
            }
        }
    }
}