using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Data_Layer;
using System.Collections.ObjectModel;

namespace Business_Layer
{
    public abstract class Archivos
    {

        public static IEnumerable<T> LeerArchivo<T>(string ruta, Predicate<T> condicion = null)
        {
            try
            {
                using (Stream st = File.OpenRead(ruta))
                {
                    List<T> list = new List<T>();
                    var binfor = new BinaryFormatter();
                    var t = st.Length;
                    while (st.Position < t)
                    {
                        var a = (T)binfor.Deserialize(st);
                        if (condicion != null && condicion(a)) list.Add(a);
                        else if(condicion == null) list.Add(a);
                    }
                    st.Close();
                    return list;
                }
            }
            catch (Exception)
            {
                return Enumerable.Empty<T>();
            }
        }

        public static int ActualizarArchivo<T>(string ruta, IEnumerable<T> list, Func<T, T, bool> match)
        {
            try
            {
                var r = ruta.LastIndexOf('/');
                var carpeta = ruta.Substring(0, r);
                Directory.CreateDirectory("./" + carpeta);
                var lista = list.ToList();
                if (File.Exists(ruta))
                {
                    using (Stream st = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        
                        var t = st.Length;
                        T temp = default;
                        while (st.Position < t)
                        {
                            var pos = st.Position;
                            var readed = (T)formatter.Deserialize(st);
                            foreach (T obj in lista)
                            {
                                if (match(obj, readed))
                                {
                                    st.Seek(pos, SeekOrigin.Begin);
                                    formatter.Serialize(st, obj);
                                    temp = obj;
                                    continue;
                                }
                            }
                            lista.Remove(temp);
                        }
                        foreach (T obj in lista)
                        {
                            formatter.Serialize(st, obj);
                        }
                        st.Close();
                        return 0;
                    }
                }
                else
                {
                    using (Stream st = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        foreach (T articulo in lista)
                        {
                            formatter.Serialize(st, articulo);
                        }
                        st.Close();
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public static int ActualizarArchivo<T>(string ruta, T item, Predicate<T> match)
        {
            try
            {
                var r = ruta.LastIndexOf('/');
                var carpeta = ruta.Substring(0, r);
                Directory.CreateDirectory("./" + carpeta);

                if (File.Exists(ruta))
                {
                    using (Stream st = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        var t = st.Length;
                        while (st.Position < t)
                        {
                            var pos = st.Position;
                            var readed = (T)formatter.Deserialize(st);
                            if (match(readed))
                            {
                                st.Seek(pos, SeekOrigin.Begin);
                                formatter.Serialize(st, item);
                                return 0;
                            }
                        }
                        formatter.Serialize(st, item);
                        st.Close();
                        return 0;
                    }
                }
                else
                {
                    using (Stream st = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(st, item);
                        st.Close();
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public static int SobreescribirArchivo<T>(string ruta, IEnumerable<T> lista, Predicate<T> condicion = null)
        {
            try
            {
                var carpeta = ruta.Substring(0, ruta.LastIndexOf('/'));
                var temp = "./.temp/" + ruta.Substring(ruta.LastIndexOf('/') + 1);
                DirectoryInfo Dif = new DirectoryInfo(@".\.temp");
                Dif.Create();
                Dif.Attributes = FileAttributes.Hidden;
                Directory.CreateDirectory("./" + carpeta);
                if (File.Exists(temp)) File.Delete(temp);
                if (File.Exists(ruta)) File.Move(ruta, temp);
                using (Stream st = File.Open(ruta, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    foreach (T obj in lista)
                    {
                        if(condicion != null && condicion(obj)) formatter.Serialize(st, obj);
                        else if(condicion == null) formatter.Serialize(st, obj);
                    }
                    st.Close();
                    File.Delete(temp);
                    return 0;
                }
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}
