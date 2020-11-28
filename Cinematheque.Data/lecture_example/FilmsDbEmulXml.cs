using Cinematheque.Data.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace Cinematheque.Data.lecture_example
{
    [DataContract]
    public class FilmsDbEmulXml
    {
        private static FilmsDbEmulXml _object;

        public static FilmsDbEmulXml Instance
        {
            get
            {
                if (_object == null)
                    _object = new FilmsDbEmulXml()
                    {
                        films = new List<Film>()
                    };

                return _object;
            }
        }

        [DataMember]
        private IEnumerable<Film> films;

        public static IEnumerable<Film> Films
        {
            get { return Instance.films; }
            set { Instance.films = value; }
        }

        private FilmsDbEmulXml() { }

        public static void Serialize()
        {
            var dcs = new DataContractSerializer(typeof(FilmsDbEmulXml));
            var xmlwrt = XmlWriter.Create("C:\\SolutionsC#\\Cinematheque\\FilmsData.xml");
            dcs.WriteObject(xmlwrt, FilmsDbEmulXml.Instance);
            xmlwrt.Close();
        }

        public static FilmsDbEmulXml Deserialize()
        {
            var dcs = new DataContractSerializer(typeof(FilmsDbEmulXml));
            var xmlrdr = XmlReader.Create("C:\\SolutionsC#\\Cinematheque\\FilmsData.xml");
            _object = (FilmsDbEmulXml)dcs.ReadObject(xmlrdr);
            xmlrdr.Close();

            return _object;
        }
    }
}
