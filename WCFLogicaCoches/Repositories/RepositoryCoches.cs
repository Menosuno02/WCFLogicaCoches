using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WCFLogicaCoches.Models;

namespace WCFLogicaCoches.Repositories
{
    public class RepositoryCoches
    {
        private XDocument document;

        public RepositoryCoches()
        {
            // Para leer el contenido de nuestro assembly (dll)
            // necesitamos el namespace del proyecto/carpeta/documento
            string resourceName = "WCFLogicaCoches.coches.xml";
            Stream stream = this.GetType().Assembly.GetManifestResourceStream(resourceName);
            this.document = XDocument.Load(stream);
        }

        public List<Coche> GetCoches()
        {
            var consulta = from datos in
                               document.Descendants("coche")
                           select new Coche()
                           {
                               IdCoche = int.Parse(datos.Element("idcoche").Value),
                               Marca = datos.Element("marca").Value,
                               Modelo = datos.Element("modelo").Value,
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }

        public Coche FindCoche(int idcoche)
        {
            return this.GetCoches().FirstOrDefault(c => c.IdCoche == idcoche);
        }
    }
}
