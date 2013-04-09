
namespace MarkupParser
{
    public class XMLDocument : MarkupDocument
    {
        private XMLDocument(string xmlText) : base(xmlText, false, (xmlText.Length > CACHE_BOUNDARY ? true : false), true) { }

        public new static XMLDocument Load(string xmlText)
        {
            return new XMLDocument(xmlText);
        }
    }
}
