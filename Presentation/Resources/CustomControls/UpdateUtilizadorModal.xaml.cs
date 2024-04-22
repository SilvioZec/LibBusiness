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
    /// Interaction logic for UpdateUtilizadorModal.xaml
    /// </summary>
    public partial class UpdateUtilizadorModal : Window
    {
        public bool Sucesso { get; set; }
        public Utilizador Utilizador = new Utilizador();
        public UpdateUtilizadorModal(string username)
        {
            InitializeComponent();
            this.Utilizador.Username = username;
            this.Sucesso = false;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Login.validarUser(this.Utilizador.Username, txtOldPasse.Password))
            {
                if (txtPasse.Password == txtPasseConf.Password)
                {
                    this.Utilizador.Password = txtPasseConf.Password;
                    this.Sucesso = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Passe nova e confirmação não coincidem");
                }
            }
            else
            {
                MessageBox.Show("Passe anterior incorreta");
            }
        }
    }
}
