using System;
using Rss;
using Utility.RSS.NET.Collections;

namespace Utility.RSS.NET.RssItem
{
    /// <summary>A channel may contain any number of items, each of which links to more information about the item, with an optional description</summary>
    [System.Serializable]
    public class RssItem : RssElement
    {
        private readonly RssCategoryCollection categories = new RssCategoryCollection();
        private string author = RssDefault.String;
        private string comments = RssDefault.String;
        private string description = RssDefault.String;
        private RssEnclosure enclosure;
        private RssGuid guid;
        private Uri link = RssDefault.Uri;
        private DateTime pubDate = RssDefault.DateTime;
        private RssSource source;
        private string title = RssDefault.String;

        /// <summary>Title of the item</summary>
        /// <remarks>Maximum length is 100 (For RSS 0.91)</remarks>
        public string Title
        {
            get { return title; }
            set { title = RssDefault.Check(value); }
        }

        /// <summary>URL of the item</summary>
        /// <remarks>Maximum length is 500 (For RSS 0.91)</remarks>
        public Uri Link
        {
            get { return link; }
            set { link = RssDefault.Check(value); }
        }

        /// <summary>Item synopsis</summary>
        /// <remarks>Maximum length is 500 (For RSS 0.91)</remarks>
        public string Description
        {
            get { return description; }
            set { description = RssDefault.Check(value); }
        }

        /// <summary>Email address of the author of the item</summary>
        public string Author
        {
            get { return author; }
            set { author = RssDefault.Check(value); }
        }

        /// <summary>Provide information regarding the location of the subject matter of the channel in a taxonomy</summary>
        public RssCategoryCollection Categories
        {
            get { return categories; }
        }

        /// <summary>URL of a page for comments relating to the item</summary>
        public string Comments
        {
            get { return comments; }
            set { comments = RssDefault.Check(value); }
        }

        /// <summary>Describes an items source</summary>
        public RssSource Source
        {
            get { return source; }
            set { source = value; }
        }

        /// <summary>A reference to an attachment to the item</summary>
        public RssEnclosure Enclosure
        {
            get { return enclosure; }
            set { enclosure = value; }
        }

        /// <summary>A string that uniquely identifies the item</summary>
        public RssGuid Guid
        {
            get { return guid; }
            set { guid = value; }
        }

        /// <summary>Indicates when the item was published</summary>
        public DateTime PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }

        /// <summary>Initialize a new instance of the RssItem class</summary>
        /// <summary>Returns a string representation of the current Object.</summary>
        /// <returns>The item's title, description, or "RssItem" if the title and description are blank.</returns>
        public override string ToString()
        {
            if (title != null)
                return title;
            else if (description != null)
                return description;
            else
                return "RssItem";
        }
    }
}