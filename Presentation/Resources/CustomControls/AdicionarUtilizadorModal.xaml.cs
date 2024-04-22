using LibBusiness.Business.Core;
using LibBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibBusiness.Presentation.Resources.CustomControls
{
    /// <summary>
    /// Interaction logic for AdicionarUtilizadorModal.xaml
    /// </summary>
    public partial class AdicionarUtilizadorModal : Window
    {
        private bool sucesso;


        public Utilizador Utilizador = new Utilizador();


        public bool Sucesso
        {
            get { return sucesso; }
            set { sucesso = value; }
        }
        public AdicionarUtilizadorModal()
        {
            InitializeComponent();
            this.sucesso = false;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Login.IsEqualTo(txtPasse.SecurePassword, txtPasseConf.SecurePassword))
            {

                this.Utilizador.Username = txtUsername.Text;
                this.Utilizador.Password = txtPasse.Password;
                this.sucesso = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Passe e confirmação não condizem", "Erro");
            }
        }
    }
}
