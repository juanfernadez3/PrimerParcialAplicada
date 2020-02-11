using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PrimerParcialAplicada.BLL;
using PrimerParcialAplicada.Entidades;

namespace PrimerParcialAplicada.Consulta
{
    /// <summary>
    /// Lógica de interacción para ConsultarArticulos.xaml
    /// </summary>
    public partial class ConsultarArticulos : Window
    {
        public ConsultarArticulos()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Articulos>();
            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltrarComboBox.SelectedIndex)
                {
                    case 0:
                        listado = ArticulosBLL.GetList(p => true);
                        break;

                    case 1:
                        int id;
                        id = Convert.ToInt32(CriterioTextBox.Text);
                        listado = ArticulosBLL.GetList(p => p.ArticuloID == id);

                        break;

                    case 2:
                        listado = ArticulosBLL.GetList(p => p.Descripcion.Contains(CriterioTextBox.Text));
                        break;

                    case 3:
                        int existencia;
                        existencia = Convert.ToInt32(CriterioTextBox.Text);
                        listado = ArticulosBLL.GetList(p => p.Existencia == existencia);
                        break;

                    case 4:
                        decimal costos;
                        costos = Convert.ToDecimal(CriterioTextBox.Text);
                        listado = ArticulosBLL.GetList(p => p.Costo == costos);
                        break;
                }
            }
            else
            {
                listado = ArticulosBLL.GetList(p => true);
            }
            //ConsultarArticulosDataGrid.ItemsSource = null;
            ConsultarArticulosDataGrid.ItemsSource = listado;
        }

    }
}
