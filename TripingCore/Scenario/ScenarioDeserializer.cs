using Graighle.Triping.Exceptions;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using Windows.ApplicationModel.Resources;

namespace Graighle.Triping.Scenario
{
    /// <summary>
    /// シナリオをデシリアライズする。
    /// </summary>
    public class ScenarioDeserializer
    {
        /// <summary>
        /// シナリオパッケージをポータブル形式から解析する。
        /// </summary>
        /// <param name="serialized">シリアライズ済のシナリオテキスト。</param>
        /// <returns>解析されたシナリオパッケージ。</returns>
        public ScenarioPackage DeserializePackageFromPortableFormat(string serialized)
        {
            var reader = new StringReader(serialized);
            var xml = XDocument.Load(reader, LoadOptions.None);

            if(xml.Root.Name != "Triping")
            {
                var rc = new ResourceLoader();
                throw new UserDataFormatException(rc.GetString("ScenarioFileFormatError"));
            }

            return this.ParsePackageFromXml(xml.Root);
        }

        /// <summary>
        /// シナリオの概要をポータブル形式から解析する。
        /// </summary>
        /// <param name="serialized">シリアライズ済のシナリオテキスト。</param>
        /// <returns>解析されたシナリオの概要。</returns>
        public ScenarioOutline DeserializeOutlineFromPortableFormat(string serialized)
        {
            var reader = new StringReader(serialized);
            var xml = XDocument.Load(reader, LoadOptions.None);

            if(xml.Root.Name != "Triping")
            {
                var rc = new ResourceLoader();
                throw new UserDataFormatException(rc.GetString("ScenarioFileFormatError"));
            }

            return this.ParseOutlineFromXml(xml.Root);
        }

        /// <summary>
        /// シナリオパッケージXMLからパースする。
        /// </summary>
        /// <param name="tripingNode">ルートノード。</param>
        /// <returns>パースされたシナリオパッケージ。</returns>
        private ScenarioPackage ParsePackageFromXml(XElement tripingNode)
        {
            return new ScenarioPackage
            {
                Outline = this.ParseOutlineFromXml(tripingNode),
                Scenery = string.Empty,
            };
        }

        /// <summary>
        /// シナリオの概要をXMLからパースする。
        /// </summary>
        /// <param name="tripingNode">ルートノード。</param>
        /// <returns>パースされたシナリオの概要。</returns>
        private ScenarioOutline ParseOutlineFromXml(XElement tripingNode)
        {
            var outline = new ScenarioOutline();

            var outlineNode = tripingNode.XPathSelectElement("/Triping/Scenario/Outline");
            if(outlineNode == null)
            {
                return outline;
            }

            // タイトル
            if(outlineNode.Element("Title") is var titleNode)
            {
                outline.Title = titleNode.Value;
            }

            // 作者
            if(outlineNode.Element("Author") is var authorNode)
            {
                outline.Author = authorNode.Value;
            }

            return outline;
        }
    }
}
