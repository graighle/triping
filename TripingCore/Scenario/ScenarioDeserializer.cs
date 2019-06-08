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
