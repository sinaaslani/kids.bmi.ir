using System;
using System.Collections;
using Rss;
using Utility.RSS.NET.Collections;

namespace Utility.RSS.NET.RSS
{
    /// <summary>Base class for all RSS modules</summary>
    [System.Serializable]
    public abstract class RssModule
    {
        private readonly ArrayList _alBindTo = new ArrayList();
        private RssModuleItemCollection _rssChannelExtensions = new RssModuleItemCollection();
        private RssModuleItemCollectionCollection _rssItemExtensions = new RssModuleItemCollectionCollection();
        private string _sNamespacePrefix = RssDefault.String;
        private Uri _uriNamespaceURL = RssDefault.Uri;


        /// <summary>Collection of RSSModuleItem that are to be placed in the channel</summary>
        internal RssModuleItemCollection ChannelExtensions
        {
            get { return _rssChannelExtensions; }
            set { _rssChannelExtensions = value; }
        }

        /// <summary>Collection of RSSModuleItemCollection that are to be placed in the channel item</summary>
        internal RssModuleItemCollectionCollection ItemExtensions
        {
            get { return _rssItemExtensions; }
            set { _rssItemExtensions = value; }
        }

        /// <summary>Prefix for the given module namespace</summary>
        public string NamespacePrefix
        {
            get { return _sNamespacePrefix; }
            set { _sNamespacePrefix = RssDefault.Check(value); }
        }

        /// <summary>URL for the given module namespace</summary>
        public Uri NamespaceURL
        {
            get { return _uriNamespaceURL; }
            set { _uriNamespaceURL = RssDefault.Check(value); }
        }

        /// <summary>Bind a particular channel to this module</summary>
        /// <param name="channelHashCode">Hash code of the channel</param>
        public void BindTo(int channelHashCode)
        {
            _alBindTo.Add(channelHashCode);
        }

        /// <summary>Check if a particular channel is bound to this module</summary>
        /// <param name="channelHashCode">Hash code of the channel</param>
        /// <returns>true if this channel is bound to this module, otherwise false</returns>
        public bool IsBoundTo(int channelHashCode)
        {
            return (_alBindTo.BinarySearch(0, _alBindTo.Count, channelHashCode, null) >= 0);
        }
    }
}