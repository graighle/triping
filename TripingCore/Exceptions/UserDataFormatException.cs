using System;
using System.Runtime.Serialization;

namespace Graighle.Triping.Exceptions
{
    /// <summary>
    /// ユーザデータファイルのフォーマットエラー。
    /// </summary>
    [Serializable()]
    public class UserDataFormatException : Exception
    {
        public UserDataFormatException()
            : base()
        {
        }

        public UserDataFormatException(string message)
            : base(message)
        {
        }

        public UserDataFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 逆シリアライズ用。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected UserDataFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
