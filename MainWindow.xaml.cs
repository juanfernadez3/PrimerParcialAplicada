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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrimerParcialAplicada.Entidades;
using PrimerParcialAplicada.BLL;
using PrimerParcialAplicada.DAL;
using PrimerParcialAplicada.Consulta;


namespace PrimerParcialAplicada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Limpiar()
        {
            ProductoIDTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            ExistenciaTextBox.Text = "0";
            CostosTextBox.Text = "0";
            ValorInventarioTextBox.Text = "0";
        }
        public void LlenaCampo(Articulos articulo)
        {
            ProductoIDTextBox.Text = Convert.ToString(articulo.ArticuloID);
            DescripcionTextBox.Text = articulo.Descripcion;
            ExistenciaTextBox.Text = Convert.ToString(articulo.Existencia);
            CostosTextBox.Text = Convert.ToString(articulo.Costo);
            ValorInventarioTextBox.Text = Convert.ToString(articulo.ValorInventario);

        }

        private Articulos LlenaClase()
        {
            Articulos articulo = new Articulos();

            //articulo.ArticuloID = Convert.ToInt32(ProductoIDTextBox.Text);
            articulo.Descripcion = DescripcionTextBox.Text;
            articulo.Existencia = Convert.ToInt32(ExistenciaTextBox.Text);
            articulo.Costo = Convert.ToDecimal(CostosTextBox.Text);
            articulo.ValorInventario = Convert.ToDecimal(ValorInventarioTextBox.Text);

            return articulo;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Articulos articulo = ArticulosBLL.Buscar(Convert.ToInt32(ProductoIDTextBox.Text));
            return (articulo != null);

        }
        private bool Validar()
        {
            bool paso = true;


            if (DescripcionTextBox.Text == string.Empty)
            {
                MessageBox.Show("Dede Ingresar Descripccion ");
                DescripcionTextBox.Focus();
                paso = false;
            }

            if (ExistenciaTextBox.Text == string.Empty)
            {
                MessageBox.Show("Dede Ingresar Existencia ");
                ExistenciaTextBox.Focus();
                paso = false;
            }

            if (CostosTextBox.Text == string.Empty)
            {
                MessageBox.Show("Dede Ingresar Costos ");
                CostosTextBox.Focus();
                paso = false;
            }
            if (ValorInventarioTextBox.Text == string.Empty)
            {
                MessageBox.Show("Dede Ingresar Valor Inventario ");
                ValorInventarioTextBox.Focus();
                paso = false;
            }
            return paso;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Articulos a;
            bool paso = false;

            if (!Validar())
                return;

            a = LlenaClase();

            if (string.IsNullOrEmpty(ProductoIDTextBox.Text) || ProductoIDTextBox.Text == "0")
                paso = ArticulosBLL.Guardar(a);


            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("ERROR", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = ArticulosBLL.Modificar(a);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
                MessageBox.Show("ERROR", "opción no valida", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(ProductoIDTextBox.Text, out id);
            Limpiar();

            if (ArticulosBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("No se pudo Eliminar");
            }
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Articulos articulo = new Articulos();
            int.TryParse(ProductoIDTextBox.Text, out id);

            Limpiar();

            articulo = ArticulosBLL.Buscar(id);

            if (articulo != null)
            {
                MessageBox.Show("Persona valida");
                LlenaCampo(articulo);
            }
            else
            {
                MessageBox.Show("persona no valida");
            }
        }

        private void ValorInventarioTexBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Articulos a = new Articulos();

            a.ValorInventario = a.Existencia * a.Costo;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ConsultarArticulos ca = new ConsultarArticulos();
            ca.Show();
        }
    }
}
