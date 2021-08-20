using Data_Layer;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Business_Layer
{
    public abstract class ListManager
    {
        public static void RemoveElements(DataGrid grid, List<object> list)
        {
            foreach (object o in list)
            {
                grid.Items.Remove(o);
            }
        }

        #region ChangeTypeTo

        #region ChangeTypeToObject
        public static List<object> ChangeTypeToObject(List<Articulo> articulos)
        {
            try
            {
                List<object> lista = new List<object>();
                foreach (Articulo articulo in articulos)
                {
                    lista.Add((object)articulo);
                }
                return lista;
            }
            catch (Exception) { return null; }
        }

        public static List<object> ChangeTypeToObject(List<ElementoVirtual> elementos)
        {
            try
            {
                List<object> lista = new List<object>();
                foreach (ElementoVirtual articulo in elementos)
                {
                    lista.Add((object)articulo);
                }
                return lista;
            }
            catch (Exception) { return null; }
        }

        public static List<object> ChangeTypeToObject(List<Vale> elementos)
        {
            try
            {
                List<object> lista = new List<object>();
                foreach (Vale articulo in elementos)
                {
                    lista.Add((object)articulo);
                }
                return lista;
            }
            catch (Exception) { return null; }
        }
        #endregion
        public static List<Vale> ChangeTypeToVale(List<object> list)
        {
            try
            {
                List<Vale> vales = new List<Vale>();
                foreach (object obj in list)
                {
                    vales.Add((Vale)obj);
                }
                return vales;
            }
            catch (Exception) { return null; }
        }

        public static List<Articulo> ChangeTypeToArticulo(List<object> list)
        {
            try
            {
                List<Articulo> articulos = new List<Articulo>();
                foreach (object obj in list)
                {
                    articulos.Add((Articulo)obj);
                }
                return articulos;
            }
            catch (Exception) { return null; }
        }

        public static List<ElementoVirtual> ChangeTypeToEV(List<object> list)
        {
            try
            {
                List<ElementoVirtual> articulos = new List<ElementoVirtual>();
                foreach (object obj in list)
                {
                    articulos.Add((ElementoVirtual)obj);
                }
                return articulos;
            }
            catch (Exception) { return null; }
        }
        #endregion
    }
}
