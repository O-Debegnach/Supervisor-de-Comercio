using Data_Layer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Fusionador_de_Stock
{
    class Program
    {
        private static List<Data_Layer.Articulo> articulos = new List<Articulo>();
        private static List<string> lista = new List<string>();
        static void Main(string[] args)
        {
            foreach(string f in Directory.GetFiles("./data", "*.stk"))
            {
                using(Stream st = File.OpenRead(f))
                {
                    BinaryFormatter binFor = new BinaryFormatter();
                    var size = st.Length;
                    while(st.Position < size)
                    {
                        Articulo a = binFor.Deserialize(st) as Articulo;
                        if (articulos.Contains(a)) continue;
                        else articulos.Add(a);
                    }
                }
            }
            Directory.CreateDirectory("./Resultado");
            using(Stream st = File.Open("./Resultado/resultado.stk", FileMode.OpenOrCreate))
            {
                BinaryFormatter b = new BinaryFormatter();
                foreach(Articulo a in articulos)
                {
                    lista.Add($"{a.Producto} --- {a.Precio}");
                    b.Serialize(st, a);
                }
            }
            File.WriteAllLines("./Resultado/lista.txt", lista);
        }
    }
}
