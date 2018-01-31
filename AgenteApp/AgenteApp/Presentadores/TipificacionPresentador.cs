using AgenteApp.Clases;
using AgenteApp.Modelos;
using AgenteApp.Repositorios;
using AgenteApp.Presenters;
using AgenteApp.Vistas;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Xml;
using System.Windows;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace AgenteApp.Presentadores
{

    class TipificacionPresentador
    {
        ITipificacionVista vista;
        public TipificacionPresentador(ITipificacionVista vista)
        {
            this.vista = vista;
        }

        public async void ConsultarConfiguracion()
        {
            //List<Filtro> filtros = vista.Filtros;
            TipificacionRepositorio repositorio = new TipificacionRepositorio();
            Resultado<List<Tipificacion>> resultado = await repositorio.ConsultarConfiguracion(Constantes.VERSION.ToString());
            if (resultado.mensajeError == string.Empty)
            {
                vista.tipificacion = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        public async void ConsultarParametros()
        {
            ParametrosRepositorio repositorio = new ParametrosRepositorio();
            Resultado<List<Parametros>> resultado = await repositorio.Consultar(vista.Parametro);
            if (resultado.mensajeError == string.Empty)
            {
                vista.Parametros = resultado.valor;
            }
            else
            {
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
            }
        }

        public async void ConsultarDatosAsistente(DatosAsistente DA)
        {
            //List<Filtro> filtros = vista.Filtros;
            TipificacionRepositorio repositorio = new TipificacionRepositorio();
            Resultado<List<DatosAsistente>> resultado = await repositorio.ConsultarDatosAsistente(DA);
            if (resultado.mensajeError == string.Empty)
            {
                vista.datosAsistente = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        public void CrearCombo(ref Grid xamlHeaderTemplate, Tipificacion campo, int i)
        {

        }

        public void CrearCheckBox(ref Grid xamlHeaderTemplate, Tipificacion campo, int i)
        {
          
        }

        public void CrearRadioButon(ref Grid xamlHeaderTemplate, Tipificacion campo, int i)
        {
            
        }

        public void CrearFecha(ref Grid xamlHeaderTemplate, Tipificacion campo, int i)
        {
            
        }

        public void CrearLineaDivisoria(ref Grid xamlHeaderTemplate, Tipificacion campo, int i)
        {
            VariableSizedWrapGrid gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 0);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBlock block = new TextBlock();
            block.Text = campo.Descripcion;
            block.Foreground = GetColor(campo.Colorletra);
            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 1);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            block = new TextBlock();
            block.Text = "";
            block.Foreground = GetColor(campo.Colorletra);
            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            block = new TextBlock();
            block.Text = "";
            block.Foreground = GetColor(campo.Colorletra);
            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 3);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            block = new TextBlock();
            block.Text = "";
            block.Foreground = GetColor(campo.Colorletra);
            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 4);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            block = new TextBlock();
            block.Text = "";
            block.Foreground = GetColor(campo.Colorletra);
            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

        }

        public void CrearLineaDivisoriaPie(ref Grid xamlHeaderTemplate, int i)
        {
            VariableSizedWrapGrid gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 0);

            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBlock block = new TextBlock();

            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 1);

            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            block = new TextBlock();
            block.Text = "";

            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);

            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            block = new TextBlock();
            block.Text = "";

            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 3);

            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            block = new TextBlock();
            block.Text = "";

            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 4);

            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            block = new TextBlock();
            block.Text = "";

            block.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

        }

        public void CrearTexto(ref Grid xamlHeaderTemplate, Tipificacion campo, int i)
        {

            VariableSizedWrapGrid gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 0);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBlock block = new TextBlock();
            block.Text = campo.Descripcion;
            block.Foreground = GetColor(campo.Colorletra);
            block.VerticalAlignment = VerticalAlignment.Center;
            //gridCombo.Visibility = SeleccionarPresntacion(campo.Presentar);
            gridCombo.Name = "GridLabel-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            gridCombo.Children.Add(block);
            gridCombo.Visibility = SeleccionarPresntacion(campo.Presentar);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.SetValue(Grid.ColumnSpanProperty, 3);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.Width = 500;
            gridCombo.Name = "Grid_texto-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            //gridCombo.Visibility = SeleccionarPresntacion(campo.Presentar);
            TextBox text = new TextBox();
            text.Name = "Texto-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid; ;
            text.Width = 390;
            text.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(text);
            gridCombo.Visibility = SeleccionarPresntacion(campo.Presentar);
            xamlHeaderTemplate.Children.Add(gridCombo);
        }

        public void CrearTextoDetalleCRM(ref Grid xamlHeaderTemplate, Tipificacion campo, int i)
        {

            VariableSizedWrapGrid gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 0);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBlock block = new TextBlock();
            block.Text = campo.Descripcion;
            block.Foreground = GetColor(campo.Colorletra);
            block.VerticalAlignment = VerticalAlignment.Center;
            //gridCombo.Visibility = SeleccionarPresntacion(campo.Presentar);
            gridCombo.Name = "GridLabel-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.SetValue(Grid.ColumnSpanProperty, 3);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.Width = 500;
            gridCombo.Name = "Grid_texto-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            //gridCombo.Visibility = SeleccionarPresntacion(campo.Presentar);
            TextBox text = new TextBox();
            text.Name = "Texto-" + campo.Version + "." + campo.Secuencia + "." + campo.Campoid; ;
            text.Width = 390;
            text.VerticalAlignment = VerticalAlignment.Center;
            gridCombo.Children.Add(text);
            xamlHeaderTemplate.Children.Add(gridCombo);
        }

        public SolidColorBrush GetColor(string color)
        {
            SolidColorBrush colorBrush = new SolidColorBrush(Colors.White);
            switch (color)
            {
                case "Black":
                    colorBrush = new SolidColorBrush(Colors.Black);
                    break;
                case "White":
                    colorBrush = new SolidColorBrush(Colors.White);
                    break;
                case "Blue":
                    colorBrush = new SolidColorBrush(Colors.Blue);
                    break;
                case "DodgerBlue":
                    colorBrush = new SolidColorBrush(Colors.DodgerBlue);
                    break;
                    

            }

            return colorBrush;
        }

        public async void GuardarTipificacion(List<CRM> crm1,List<Tipificacion> tipificaciones)
        {
            //List<Filtro> filtros = vista.Filtros;
            TipificacionRepositorio repositorio = new TipificacionRepositorio();
            Resultado<object> resultado = await repositorio.GuardarTipificacion(crm1,tipificaciones);
            if (resultado.mensajeError == string.Empty)
            {
                vista.IDCRM = resultado.valor.ToString();
                vista.MostrarMensajeAsync("Aviso", "Los datos se guardaron correctamente.");
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }
        
        public async void ActulizarTipificacion(List<CRM> crm1, List<Tipificacion> tipificaciones)
        {
            //List<Filtro> filtros = vista.Filtros;
            TipificacionRepositorio repositorio = new TipificacionRepositorio();
            Resultado<object> resultado = await repositorio.ActulizarTipificacion(crm1, tipificaciones);
            if (resultado.mensajeError == string.Empty)
            {
                vista.IDCRM = resultado.valor.ToString();
                vista.MostrarMensajeAsync("Aviso", "Los datos se guardaron correctamente.");
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

        public Visibility SeleccionarPresntacion(string presentar)
        {
            Visibility vis = new Visibility();
            if (presentar == "SI")
            {
                vis = Visibility.Visible;
            }
            else
            {
                vis = Visibility.Collapsed;
            }
            return vis;
        }

        public string TomarPresntacion(Visibility vis)
        {

            if (vis==Visibility.Visible)
            {
                return "SI";
            }
            else
            {
                return "NO";
            }
        }

        public async void ConsultarDetalleTipificacion(List<Tipificacion>arrCampos)
        {
            //List<Filtro> filtros = vista.Filtros;
            TipificacionRepositorio repositorio = new TipificacionRepositorio();
            Resultado<List<Tipificacion>> resultado = await repositorio.ConsultarDetalleTipificacion(Constantes.VERSION.ToString(), arrCampos);
            if (resultado.mensajeError == string.Empty)
            {
                vista.tipificacion = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
        }

    }
}
