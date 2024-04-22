using LibBusiness.Business.Core;
using LibBusiness.Models;
using LibBusiness.Presentation.Resources.CustomControls;
using System.Windows;
using System.Windows.Controls;

namespace LibBusiness.Presentation.Views.Homepage_pages
{
    /// <summary>
    /// Interaction logic for Utilizadores.xaml
    /// </summary>
    public partial class Utilizadores : Page
    {
        private string definirTxtTitle()
        {
            if (Login.countUsers() > 1)
            {
                return $"Existem {Login.countUsers()} utilizadores cadastrados";
            }
            else
            {
                return "Existe apenas um utilizador cadastrado";
            }
        }

        public Utilizadores()
        {
            InitializeComponent();
            txtTitle.Text = definirTxtTitle();
            lstUsers.ItemsSource = Login.getUsernames();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AdicionarUtilizadorModal addUtilizadorModal = new AdicionarUtilizadorModal();
            addUtilizadorModal.ShowDialog();
            if (addUtilizadorModal.Sucesso)
            {
                Login.addUser(addUtilizadorModal.Utilizador.Username, addUtilizadorModal.Utilizador.Password);
                txtTitle.Text = definirTxtTitle();
                lstUsers.ItemsSource = Login.getUsernames();
            }
        }

        private void btnRmv_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedItem!= null)
            {
                if (Login.countUsers() > 1)
                {
                    var result = MessageBox.Show($"Deseja mesmo excluir o utilizador {lstUsers.SelectedItem} ?", "Confirmação de exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Login.removeUser(lstUsers.SelectedItem.ToString());
                        lstUsers.ItemsSource = Login.getUsernames();
                        txtTitle.Text = definirTxtTitle();

                    }
                }
                else
                {
                    MessageBox.Show("Não é possivel remover o único utilizador restante");
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um utilizador");
            }
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                UpdateUtilizadorModal update = new UpdateUtilizadorModal(lstUsers.SelectedItem.ToString());
                update.ShowDialog();
                if (update.Sucesso)
                {
                    Login.resetPassword(update.Utilizador);
                    MessageBox.Show("Passe atualizada com sucesso");
                    txtTitle.Text = definirTxtTitle();
                    lstUsers.ItemsSource = Login.getUsernames();
                }
            }
            
        }
    }
}
