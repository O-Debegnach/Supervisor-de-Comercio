using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Data_Layer
{
    public class Ticket
    {
        public Ticket()
        {
        }

        #region Propiedades
        public string NombreComercio { get; set; }
        public string Direccion { get; set; }
        public string Contenido { get; set; }

        public int TitleSize { get; set; } = 36;
        public int TitleAlignment { get; set; } = 1;
        public int SubTitleSize { get; set; } = 22;
        public int SubTitleAlignment { get; set; } = 1;
        public int ContentSize { get; set; } = 18;
        public int ContentAlignment { get; set; } = 1;
        #endregion Propiedades

        public bool GenerarPDF(string filePath)
        {
            try
            {
                using (FileStream st = new FileStream(filePath, FileMode.Create))
                {
                    Document doc = new Document();
                    PdfWriter pdf = PdfWriter.GetInstance(doc, st);

                    doc.Open();
                    Paragraph p = new Paragraph(NombreComercio, new Font(Font.FontFamily.TIMES_ROMAN, TitleSize))
                    {
                        Alignment = TitleAlignment
                    };
                    doc.Add(p);

                    p = new Paragraph(Direccion, new Font(Font.FontFamily.TIMES_ROMAN, SubTitleSize));
                    p.Alignment = SubTitleAlignment;
                    doc.Add(p);

                    p = new Paragraph(DateTime.Now.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, SubTitleSize));
                    p.Alignment = SubTitleAlignment;
                    doc.Add(p);

                    var content = Contenido.Split('\n');
                    
                    foreach(string s in content)
                    {
                        p = new Paragraph(s)
                        {
                            Font = new Font(Font.FontFamily.TIMES_ROMAN, ContentSize),
                            Alignment = ContentAlignment
                        };
                        doc.Add(p);
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                MessageBox.Show($"No se pudo crear el archivo PDF\nError: {e.Message}");
                return false;
            }
        }
    }
}
