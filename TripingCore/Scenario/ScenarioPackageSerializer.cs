using Graighle.Triping.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Graighle.Triping.Scenario
{
    /// <summary>
    /// シナリオデータをシリアライズする。
    /// </summary>
    public class ScenarioPackageSerializer
    {
        /// <summary>
        /// シナリオデータをポータブル形式へシリアライズする。
        /// </summary>
        /// <param name="scenario">シナリオパッケージ。</param>
        /// <returns>シリアライズ済の文字列。</returns>
        public string SerializeToPortableFormat(ScenarioPackage scenario)
        {
            var xml = this.CreateXml(scenario);

            var strWriter = new StringWriterUTF8();
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
            };
            using(var xmlWriter = XmlWriter.Create(strWriter, settings))
            {
                xml.WriteTo(xmlWriter);
            }

            return strWriter.ToString();
        }

        private XDocument CreateXml(ScenarioPackage scenario)
        {
            return new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Triping",
                    new XElement("Scenario",
                        new XElement("Outline",
                            new XElement("Title", scenario.Outline.Title),
                            new XElement("Author", scenario.Outline.Author)
                        ),
                        new XElement("Prologue",
                            new XElement("Scenery", scenario.Scenery)
                        )
                    )
                )
            );
        }
    }
}
