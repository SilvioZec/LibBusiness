using LibBusiness.Presentation.Views.Homepage_pages;
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

namespace LibBusiness.Presentation.Views
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Window
    {
        //criando um dicionario com os botoes e paginas
        private Dictionary<RadioButton, Page> keyValuePairs = new Dictionary<RadioButton, Page>();
        public Homepage()
        {
            InitializeComponent();

            //adicionando botoes ao dicionario
            keyValuePairs.Add(btnHome, new Home());
            keyValuePairs.Add(btnLivros, new Livros());
            keyValuePairs.Add(btnLeitores, new Leitores());
            keyValuePairs.Add(btnEmprestimos, new Emprestimos());
            keyValuePairs.Add(btnCategorias, new Categorias());
            keyValuePairs.Add(btnUtilizadores, new Utilizadores());

            // Adicionando o evento de clique para cada RadioButton
            foreach (var pair in keyValuePairs)
            {
                pair.Key.Click += RadioButton_Click;
            }

            // Selecionando o btnHome por padrão
            btnHome.IsChecked = true;
            painelPaginas.Content = keyValuePairs[btnHome];
        }

        /// <summary>
        /// Direciona a pagina correta de acordo com o botão selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            // Verificando qual RadioButton foi clicado
            RadioButton clickedButton = sender as RadioButton;

            // Verificando se o RadioButton clicado está no dicionário
            if (keyValuePairs.ContainsKey(clickedButton))
            {
                // Definindo o conteúdo do ContentControl como a página correspondente ao RadioButton clicado
                painelPaginas.Content = keyValuePairs[clickedButton];
            }
        }

        private void imgMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void gridCorpo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
