using System.IO;
using System.Text;

namespace Graighle.Triping.IO
{
    /// <summary>
    /// UTF8エンコーディングを指定したStringWriter。
    /// </summary>
    class StringWriterUTF8 : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
