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
            ProductoIDTexBox.Text = "0";
            DescripcionTexBox.Text = string.Empty;
            ExistenciaTexBox.Text = "0";
            CostosTexbox.Text = "0";
            ValorInventarioTexBox.Text = "0";
        }
        public void LlenaCampo(Articulos articulo)
        {
            ProductoIDTexBox.Text = Convert.ToString(articulo.ArticuloID);
            DescripcionTexBox.Text = articulo.Descripcion;
            ExistenciaTexBox.Text = Convert.ToString(articulo.Existencia);
            CostosTexbox.Text = Convert.ToString(articulo.Costo);
            ValorInventarioTexBox.Text = Convert.ToString(articulo.ValorInventario);

        }

        private Articulos LlenaClase()
        {
            Articulos articulo = new Articulos();

            //articulo.ArticuloID = Convert.ToInt32(ProductoIDTexBox);
            articulo.Descripcion = DescripcionTexBox.Text;
           // articulo.Existencia = Convert.ToInt32(ExistenciaTexBox);
           // articulo.Costo = Convert.ToDecimal(CostosTexbox);
           // articulo.ValorInventario = Convert.ToDecimal(ValorInventarioTexBox);

            return articulo;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Articulos articulo = ArticulosBLL.Buscar(Convert.ToInt32(ProductoIDTexBox.Text));
            return (articulo != null);

        }
        private bool Validar()
        {
            bool paso = true;


            if (DescripcionTexBox.Text == string.Empty)
            {
                MessageBox.Show("Dede Ingresar Descripccion ");
                DescripcionTexBox.Focus();
                paso = false;
            }

            if (ExistenciaTexBox.Text == string.Empty)
            {
                MessageBox.Show("Dede Ingresar Existencia ");
                ExistenciaTexBox.Focus();
                paso = false;
            }

            if (CostosTexbox.Text == string.Empty)
            {
                MessageBox.Show("Dede Ingresar Costos ");
                CostosTexbox.Focus();
                paso = false;
            }
            if (ValorInventarioTexBox.Text == string.Empty)
            {
                MessageBox.Show("Dede Ingresar Valor Inventario ");
                ValorInventarioTexBox.Focus();
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

            if (string.IsNullOrEmpty(ProductoIDTexBox.Text) || ProductoIDTexBox.Text == "0")
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
            int.TryParse(ProductoIDTexBox.Text, out id);
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
            int.TryParse(ProductoIDTexBox.Text, out id);

            Limpiar();

            articulo = ArticulosBLL.Buscar(id);

            if (articulo != null)
            {
                MessageBox.Show("Persona valida");
                LlenaClase();
            }
            else
            {
                MessageBox.Show("persona no valida");
            }
        }
    }
}
