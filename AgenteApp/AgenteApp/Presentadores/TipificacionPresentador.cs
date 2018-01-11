﻿using AgenteApp.Clases;
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
            Resultado<List<Tipificacion>> resultado = await repositorio.ConsultarConfiguracion();
            if (resultado.mensajeError == string.Empty)
            {
                vista.tipificacion = resultado.valor;
            }
            else
                vista.MostrarMensajeAsync("Error", resultado.mensajeError);
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
            gridCombo.Children.Add(block);
            xamlHeaderTemplate.Children.Add(gridCombo);

            gridCombo = new VariableSizedWrapGrid();
            gridCombo.SetValue(Grid.RowProperty, i);
            gridCombo.SetValue(Grid.ColumnProperty, 2);
            gridCombo.SetValue(Grid.ColumnSpanProperty, 3);
            gridCombo.Background = GetColor(campo.Colorfondo);
            gridCombo.Height = 50;
            gridCombo.Width = 500;
            gridCombo.VerticalAlignment = VerticalAlignment.Center;
            TextBox text = new TextBox();
            text.Name = campo.Descripcion + "DSC";
            text.Width = 390;
            gridCombo.Children.Add(text);
            xamlHeaderTemplate.Children.Add(gridCombo);
        }


        private SolidColorBrush GetColor(string color)
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

            }

            return colorBrush;
        }
    }
}
