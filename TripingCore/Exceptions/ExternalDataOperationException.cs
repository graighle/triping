using System;
using System.Runtime.Serialization;

namespace Graighle.Triping.Exceptions
{
    /// <summary>
    /// システム外データの操作エラー。
    /// </summary>
    [Serializable()]
    public class ExternalDataOperationException : Exception
    {
        public ExternalDataOperationException()
            : base()
        {
        }

        public ExternalDataOperationException(string message)
            : base(message)
        {
        }

        public ExternalDataOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 逆シリアライズ用。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ExternalDataOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
