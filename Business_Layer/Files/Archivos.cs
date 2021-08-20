using Data_Layer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Business_Layer
{
    public abstract class Archivos
    {
        private static readonly string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\V&D\\Supervisor de Comercio\\";


        public static void DoBackup(IEnumerable<Articulo> articulos)
        {
            List<FileInfo> files;

            DirectoryInfo dir = new DirectoryInfo(baseDir);
            if (!dir.Exists)
            {
                dir.Create();
            }

            files = new List<FileInfo>(dir.GetFiles("*.stk"));
            files.Sort(delegate (FileInfo x, FileInfo y)
            {
                if (x.CreationTime == null && y.CreationTime == null)
                {
                    return 0;
                }
                else if (x.CreationTime == null)
                {
                    return -1;
                }
                else if (y.CreationTime == null)
                {
                    return 1;
                }
                else
                {
                    return x.CreationTime.CompareTo(y.CreationTime);
                }
            });
            if (files.Count + 1 > 5)
            {
                File.Delete(files[0].FullName);
                files.RemoveAt(0);
            }
            int numero = 0;
            if (files.Count > 0)
            {
                var s = files[files.Count - 1].Name;
                s = s.Split('-', '.')[1];
                int.TryParse(s, out numero);
            }
            string pathDestino = $"BackUp-{numero+1}.stk";
            if (!File.Exists(pathDestino))
            {
                SobreescribirArchivo(pathDestino, articulos);
            }
        }

        public static void DoBackup(IEnumerable<Articulo> articulos, string fileName)
        {
            List<FileInfo> files;
            DirectoryInfo dir = new DirectoryInfo(baseDir);
            if (!dir.Exists)
            {
                dir.Create();
            }

            files = new List<FileInfo>(dir.GetFiles("*.stk"));
            files.Sort(delegate (FileInfo x, FileInfo y)
            {
                if (x.CreationTime == null && y.CreationTime == null)
                {
                    return 0;
                }
                else if (x.CreationTime == null)
                {
                    return -1;
                }
                else if (y.CreationTime == null)
                {
                    return 1;
                }
                else
                {
                    return x.CreationTime.CompareTo(y.CreationTime);
                }
            });

            if (files.Count + 1 > 5)
            {
                File.Delete(files[0].FullName);
                files.RemoveAt(0);
            }
            if (!File.Exists(baseDir + fileName))
            {
                SobreescribirArchivo(fileName, articulos);
            }
        }

        public static IEnumerable<T> LeerArchivo<T>(string ruta, Predicate<T> condicion = null)
        {
            List<T> list = new List<T>();

            try
            {
                using (Stream st = File.OpenRead(baseDir + ruta))
                {
                  //List<T> list = new List<T>();
              
                    BinaryFormatter binfor = new BinaryFormatter();
                    long t = st.Length;
                    while (st.Position < t)
                    {
                        T a = (T)binfor.Deserialize(st);
                        if (condicion != null && condicion(a))
                        {
                            list.Add(a);
                        }
                        else if (condicion == null)
                        {
                            list.Add(a);
                        }
                    }
                    st.Close();
                    return list;
                }
            }
            catch (Exception e)
            {
                return list.Count > 0 ? list : Enumerable.Empty<T>();
            }
        }

        public static IEnumerable<T> LeerArchivoRutaCompleta<T>(string ruta, Predicate<T> condicion = null)
        {
            try
            {
                using (Stream st = File.OpenRead(ruta))
                {
                    List<T> list = new List<T>();
                    BinaryFormatter binfor = new BinaryFormatter();
                    long t = st.Length;
                    while (st.Position < t)
                    {
                        T a = (T)binfor.Deserialize(st);
                        if (condicion != null && condicion(a))
                        {
                            list.Add(a);
                        }
                        else if (condicion == null)
                        {
                            list.Add(a);
                        }
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

        public static int ActualizarArchivo<T>(string ruta, IEnumerable<T> list, bool backUp, Func<T, T, bool> match)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool finalizar = true;

            do
            {
                try
                {
                    int r = ruta.LastIndexOf('/');
                    string carpeta = ruta.Substring(0, r);
                    Directory.CreateDirectory(baseDir + carpeta);
                    List<T> lista = list.ToList();
                    if (File.Exists(baseDir + ruta))
                    {
                        using (Stream st = File.Open(baseDir + ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();

                            long t = st.Length;
                            T temp = default;
                            while (st.Position < t)
                            {
                                long pos = st.Position;
                                T readed = (T)formatter.Deserialize(st);
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
                                if (lista.Count <= 0)
                                {
                                    break;
                                }
                            }
                            foreach (T obj in lista)
                            {
                                formatter.Serialize(st, obj);
                            }
                            st.Close();
                            finalizar = true;
                        }
                    }
                    else
                    {
                        using (Stream st = File.Open(baseDir + ruta, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            foreach (T articulo in lista)
                            {
                                formatter.Serialize(st, articulo);
                            }
                            st.Close();
                            finalizar = true; ;
                        }
                    }
                }

                catch (OutOfMemoryException)
                {
                    finalizar = false;
                    if (sw.ElapsedMilliseconds > 10000)
                    {
                        Debug.WriteLine($"OutOfMemoryException to long (duration:{sw.ElapsedMilliseconds}ms");
                        break;
                    }
                }
                catch (FileLoadException)
                {
                    finalizar = false;
                    if (sw.ElapsedMilliseconds > 10000)
                    {
                        Debug.WriteLine($"FileLoadException to long (duration:{sw.ElapsedMilliseconds}ms");
                        break;
                    }
                }
                catch (FileFormatException)
                {
                    return 2;
                }
                catch
                {
                    finalizar = false;
                    break;
                }
            } while (!finalizar);

            return finalizar ? 0 : 1;
        }

        public static int ActualizarArchivo<T>(string ruta, T item, bool backUp, Predicate<T> match)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool finalizar = true;


            do
            {
                try
                {
                    int r = ruta.LastIndexOf('/');
                    string carpeta = ruta.Substring(0, r);
                    Directory.CreateDirectory(baseDir + carpeta);

                    if (File.Exists(baseDir + ruta))
                    {
                        using (Stream st = File.Open(baseDir + ruta, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            bool leido = true;
                            long t = st.Length;
                            while (st.Position < t)
                            {
                                long pos = st.Position;
                                T readed = (T)formatter.Deserialize(st);
                                if (match(readed))
                                {
                                    st.Seek(pos, SeekOrigin.Begin);
                                    formatter.Serialize(st, item);
                                    leido = false;
                                    finalizar = true;
                                    break;
                                }
                            }
                            if (!leido)
                            {
                                break;
                            }

                            formatter.Serialize(st, item);
                            st.Close();
                            finalizar = true;
                            break;
                        }
                    }
                    else
                    {
                        using (Stream st = File.Open(baseDir + ruta, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(st, item);
                            st.Close();
                            finalizar = true;
                            break;
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    finalizar = false;
                    if (sw.ElapsedMilliseconds > 10000)
                    {
                        Debug.WriteLine($"OutOfMemoryException to long (duration:{sw.ElapsedMilliseconds}ms");
                        break;
                    }
                }
                catch (FileLoadException)
                {
                    finalizar = false;
                    if (sw.ElapsedMilliseconds > 10000)
                    {
                        Debug.WriteLine($"FileLoadException to long (duration:{sw.ElapsedMilliseconds}ms");
                        break;
                    }
                }
                catch (FileFormatException)
                {
                    return 2;
                }
                catch
                {
                    finalizar = false;
                    break;
                }
            } while (!finalizar);

            return finalizar ? 0 : 1;
        }

        public static int SobreescribirArchivo<T>(string ruta, IEnumerable<T> lista, bool backUp = false, Predicate<T> condicion = null)
        {

            try
            {
                //string carpeta = ruta.Substring(0, ruta.LastIndexOf('/'));
                //string temp = "./.temp/" + ruta.Substring(ruta.LastIndexOf('/') + 1);
                //DirectoryInfo Dif = new DirectoryInfo(@".\.temp");
                //Dif.Create();
                //Dif.Attributes = FileAttributes.Hidden;
                //Directory.CreateDirectory("./" + carpeta);
                //if (File.Exists(temp))
                //{
                //    File.Delete(temp);
                //}

                //if (File.Exists(ruta))
                //{
                //    File.Move(ruta, temp);
                //}
                if (File.Exists(baseDir + ruta))
                {
                    File.Delete(baseDir + ruta);
                }

                using (Stream st = File.Open(baseDir + ruta, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    foreach (T obj in lista)
                    {
                        if (condicion != null && condicion(obj))
                        {
                            formatter.Serialize(st, obj);
                        }
                        else if (condicion == null)
                        {
                            formatter.Serialize(st, obj);
                        }
                    }
                    st.Close();
                    //File.Delete(temp);
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
