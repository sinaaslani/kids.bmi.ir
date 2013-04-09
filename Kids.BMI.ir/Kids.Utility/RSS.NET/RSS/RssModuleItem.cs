using Rss;
using Utility.RSS.NET.Collections;

namespace Utility.RSS.NET.RSS
{
    /// <summary>A module may contain any number of items (either channel-based or item-based).</summary>
    [System.Serializable]
    public class RssModuleItem : RssElement
    {
        private readonly bool _bRequired;
        private RssModuleItemCollection _rssSubElements = new RssModuleItemCollection();
        private string _sElementName = RssDefault.String;
        private string _sElementText = RssDefault.String;

        /// <summary>Initialize a new instance of the RssModuleItem class</summary>
        public RssModuleItem()
        {
        }

        /// <summary>Initialize a new instance of the RssModuleItem class</summary>
        /// <param name="name">The name of this RssModuleItem.</param>
        public RssModuleItem(string name)
        {
            _sElementName = RssDefault.Check(name);
        }

        /// <summary>Initialize a new instance of the RssModuleItem class</summary>
        /// <param name="name">The name of this RssModuleItem.</param>
        /// <param name="required">Is text required for this RssModuleItem?</param>
        public RssModuleItem(string name, bool required)
            : this(name)
        {
            _bRequired = required;
        }

        /// <summary>Initialize a new instance of the RssModuleItem class</summary>
        /// <param name="name">The name of this RssModuleItem.</param>
        /// <param name="text">The text contained within this RssModuleItem.</param>
        public RssModuleItem(string name, string text)
            : this(name)
        {
            _sElementText = RssDefault.Check(text);
        }

        /// <summary>Initialize a new instance of the RssModuleItem class</summary>
        /// <param name="name">The name of this RssModuleItem.</param>
        /// <param name="required">Is text required for this RssModuleItem?</param>
        /// <param name="text">The text contained within this RssModuleItem.</param>
        public RssModuleItem(string name, bool required, string text)
            : this(name, required)
        {
            _sElementText = RssDefault.Check(text);
        }

        /// <summary>Initialize a new instance of the RssModuleItem class</summary>
        /// <param name="name">The name of this RssModuleItem.</param>
        /// <param name="text">The text contained within this RssModuleItem.</param>
        /// <param name="subElements">The sub-elements of this RssModuleItem (if any exist).</param>
        public RssModuleItem(string name, string text, RssModuleItemCollection subElements)
            : this(name, text)
        {
            _rssSubElements = subElements;
        }

        /// <summary>Initialize a new instance of the RssModuleItem class</summary>
        /// <param name="name">The name of this RssModuleItem.</param>
        /// <param name="required">Is text required for this RssModuleItem?</param>
        /// <param name="text">The text contained within this RssModuleItem.</param>
        /// <param name="subElements">The sub-elements of this RssModuleItem (if any exist).</param>
        public RssModuleItem(string name, bool required, string text, RssModuleItemCollection subElements)
            : this(name, required, text)
        {
            _rssSubElements = subElements;
        }

        /// <summary>
        /// The name of this RssModuleItem.
        /// </summary>
        public string Name
        {
            get { return _sElementName; }
            set { _sElementName = RssDefault.Check(value); }
        }

        /// <summary>
        /// The text contained within this RssModuleItem.
        /// </summary>
        public string Text
        {
            get { return _sElementText; }
            set { _sElementText = RssDefault.Check(value); }
        }

        /// <summary>
        /// The sub-elements of this RssModuleItem (if any exist).
        /// </summary>
        public RssModuleItemCollection SubElements
        {
            get { return _rssSubElements; }
            set { _rssSubElements = value; }
        }

        /// <summary>
        /// Is text for this element required?
        /// </summary>
        public bool IsRequired
        {
            get { return _bRequired; }
        }

        /// <summary>Returns a string representation of the current Object.</summary>
        /// <returns>The item's title, description, or "RssModuleItem" if the title and description are blank.</returns>
        public override string ToString()
        {
            if (_sElementName != null)
                return _sElementName;
            else if (_sElementText != null)
                return _sElementText;
            else
                return "RssModuleItem";
        }
    }
}