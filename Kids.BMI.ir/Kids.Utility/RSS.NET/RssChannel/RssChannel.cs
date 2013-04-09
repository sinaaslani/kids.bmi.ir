using System;
using Rss;
using Site.Kids.bmi.ir.Classes.RSS.NET.RssChannel;
using Utility.RSS.NET.Collections;

namespace DSD.Site.UtilityClasses.RSS.NET.RssChannel
{
    /// <summary>Grouping of related content items on a site</summary>
    [System.Serializable]
    public class RssChannel : RssElement
    {
        private readonly RssCategoryCollection categories = new RssCategoryCollection();
        private readonly RssItemCollection items = new RssItemCollection();
        private RssCloud cloud;
        private string copyright = RssDefault.String;
        private string description = RssDefault.String;
        private string docs = RssDefault.String;
        private string generator = RssDefault.String;
        private RssImage image;
        private string language = RssDefault.String;
        private string lastBuildDate = RssDefault.DateTime.ToString("yyyy/MM/dd");
        private Uri link = RssDefault.Uri;
        private string managingEditor = RssDefault.String;
        private DateTime pubDate = RssDefault.DateTime;
        private string rating = RssDefault.String;
        private bool[] skipDays = new bool[7];
        private bool[] skipHours = new bool[24];
        private RssTextInput textInput;
        private int timeToLive = RssDefault.Int;
        private string title = RssDefault.String;
        private string webMaster = RssDefault.String;

        /// <summary>The name of the channel</summary>
        /// <remarks>Maximum length is 100 characters (For RSS 0.91)</remarks>
        public string Title
        {
            get { return title; }
            set { title = RssDefault.Check(value); }
        }

        /// <summary>URL of the website named in the title</summary>
        /// <remarks>Maximum length is 500 characters (For RSS 0.91)</remarks>
        public Uri Link
        {
            get { return link; }
            set { link = RssDefault.Check(value); }
        }

        /// <summary>Description of the channel</summary>
        /// <remarks>Maximum length is 500 characters (For RSS 0.91)</remarks>
        public string Description
        {
            get { return description; }
            set { description = RssDefault.Check(value); }
        }

        /// <summary>Language the channel is written in</summary>
        public string Language
        {
            get { return language; }
            set { language = RssDefault.Check(value); }
        }

        /// <summary>A link and description for a graphic icon that represent a channel</summary>
        public RssImage Image
        {
            get { return image; }
            set { image = value; }
        }

        /// <summary>Copyright notice for content in the channel</summary>
        /// <remarks>Maximum length is 100 (For RSS 0.91)</remarks>
        public string Copyright
        {
            get { return copyright; }
            set { copyright = RssDefault.Check(value); }
        }

        /// <summary>The email address of the managing editor of the channel, the person to contact for editorial inquiries</summary>
        /// <remarks>
        /// <para>Maximum length is 100 (For RSS 0.91)</para>
        /// <para>The suggested format for email addresses in RSS elements is</para>
        /// <para>bull@mancuso.com (Bull Mancuso)</para>
        /// </remarks>
        public string ManagingEditor
        {
            get { return managingEditor; }
            set { managingEditor = RssDefault.Check(value); }
        }

        /// <summary>The email address of the webmaster for the channel</summary>
        /// <remarks>
        /// <para>Person to contact if there are technical problems</para>
        /// <para>Maximum length is 100 (For RSS 0.91)</para>
        /// <para>The suggested format for email addresses in RSS elements is</para>
        /// <para>bull@mancuso.com (Bull Mancuso)</para>
        /// </remarks>
        public string WebMaster
        {
            get { return webMaster; }
            set { webMaster = RssDefault.Check(value); }
        }

        /// <summary>The PICS rating for the channel</summary>
        /// <remarks>Maximum length is 500 (For RSS 0.91)</remarks>
        public string Rating
        {
            get { return rating; }
            set { rating = RssDefault.Check(value); }
        }

        /// <summary>The publication date for the content in the channel, expressed as the coordinated universal time (UTC)</summary>
        public DateTime PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }

        /// <summary>The date-time the last time the content of the channel changed, expressed as the coordinated universal time (UTC)</summary>
        public string LastBuildDate
        {
            get { return lastBuildDate; }
            set { lastBuildDate = value; }
        }

        /// <summary>One or more categories the channel belongs to.</summary>
        public RssCategoryCollection Categories
        {
            get { return categories; }
        }

        /// <summary>A string indicating the program used to generate the channel</summary>
        public string Generator
        {
            get { return generator; }
            set { generator = RssDefault.Check(value); }
        }

        /// <summary>A URL, points to the documentation for the format used in the RSS file</summary>
        /// <remarks>Maximum length is 500 (For RSS 0.91).</remarks>
        public string Docs
        {
            get { return docs; }
            set { docs = RssDefault.Check(value); }
        }

        /// <summary>Provides information about an HTTP GET feature, typically for a search or subscription</summary>
        public RssTextInput TextInput
        {
            get { return textInput; }
            set { textInput = value; }
        }

        /// <summary>Readers should not read the channel during days listed. (UTC)</summary>
        /// <remarks>Days are listed in the array in the following order:<list type="number">
        /// <item><description>Monday</description></item>
        /// <item><description>Tuesday</description></item>
        /// <item><description>Wednesday</description></item>
        /// <item><description>Thursday</description></item>
        /// <item><description>Friday</description></item>
        /// <item><description>Saturday</description></item>
        /// <item><description>Sunday</description></item>
        /// <item><description>Monday</description></item>
        /// </list></remarks>
        public bool[] SkipDays
        {
            get { return skipDays; }
            set { skipDays = value; }
        }

        /// <summary>Readers should not read the channel during hours listed (UTC)</summary>
        /// <remarks>Represents a time in UTC - 1.</remarks>
        public bool[] SkipHours
        {
            get { return skipHours; }
            set { skipHours = value; }
        }

        /// <summary>Allow processes to register with a cloud to be notified of updates to the channel</summary>
        public RssCloud Cloud
        {
            get { return cloud; }
            set { cloud = value; }
        }

        /// <summary>The number of minutes that a channel can be cached.</summary>
        public int TimeToLive
        {
            get { return timeToLive; }
            set { timeToLive = RssDefault.Check(value); }
        }

        /// <summary>All items within the channel</summary>
        public RssItemCollection Items
        {
            get { return items; }
        }

        /// <summary>Initialize a new instance of the RssChannel class.</summary>
        /// <summary>Returns a string representation of the current Object.</summary>
        /// <returns>The channel's title, description, or "RssChannel" if the title and description are blank.</returns>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(title))
                return title;
            else if (!string.IsNullOrEmpty(description))
                return description;
            else
                return "RssChannel";
        }
    }
}