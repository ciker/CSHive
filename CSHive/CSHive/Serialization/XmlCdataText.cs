using System.Xml.Serialization;

namespace CS.Serialization
{
    /// <summary>
    /// CDATA XML数据
    /// </summary>
    public class XmlCdataText
    {
        [XmlText]
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}