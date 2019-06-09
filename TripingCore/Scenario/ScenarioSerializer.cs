using Graighle.Triping.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Graighle.Triping.Scenario
{
    /// <summary>
    /// シナリオデータをシリアライズする。
    /// </summary>
    public class ScenarioSerializer
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

        /// <summary>
        /// シナリオパッケージからXMLを作成する。
        /// </summary>
        /// <param name="scenario">シナリオパッケージ。</param>
        /// <returns>XML文書データ。</returns>
        private XDocument CreateXml(ScenarioPackage scenario)
        {
            return new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Triping",
                    new XElement("Scenario",
                        this.CreateOutlineXml(scenario.Outline),
                        new XElement("Prologue",
                            new XElement("Scenery", scenario.Scenery)
                        )
                    )
                )
            );
        }

        /// <summary>
        /// シナリオ概要からXMLを作成する。
        /// </summary>
        /// <param name="outline">シナリオ概要。</param>
        /// <returns>XML概要ノード。</returns>
        private XElement CreateOutlineXml(ScenarioOutline outline)
        {
            return new XElement("Outline",
                new XElement("Title", outline.Title),
                new XElement("Author", outline.Author)
            );
        }

    }
}
