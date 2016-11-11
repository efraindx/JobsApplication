using System;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Web;
using System.IO;
using System.Xml.Linq;

namespace JobsApplication
{
    public class FeedResult : ActionResult
    {
        public SyndicationFeed Feed { get; set; }

        public FeedResult(SyndicationFeed feed)
        {
            Feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";
            var rssFormatter = new Rss20FeedFormatter(Feed);
            var settings = new XmlWriterSettings
            {
                NewLineHandling = NewLineHandling.None,
                Indent = true,
                Encoding = Encoding.UTF32,
                ConformanceLevel = ConformanceLevel.Document,
                OmitXmlDeclaration = true
            };

            var buffer = new StringBuilder();
            var cachedOutput = new StringWriter(buffer);

            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                if (writer != null)
                {
                    rssFormatter.WriteTo(writer);
                    writer.Close();
                }
            }

            var xmlDoc = XDocument.Parse(buffer.ToString());
            foreach (var element in xmlDoc.Descendants("channel").Descendants("item").Descendants("description"))
                VerifyCdataHtmlEncoding(buffer, element);

            foreach (var element in xmlDoc.Descendants("channel").Descendants("description"))
                VerifyCdataHtmlEncoding(buffer, element);

            buffer.Replace(" xmlns:a10=\"http://www.w3.org/2005/Atom\"", " xmlns:atom=\"http://www.w3.org/2005/Atom\"");
            buffer.Replace("a10:", "atom:");

            context.HttpContext.Response.Output.Write(buffer.ToString());
        }

        private static void VerifyCdataHtmlEncoding(StringBuilder buffer, XElement element)
        {
            if (element.Value.Contains(""))
            {
                string cdataValue = string.Format("", element.Name, element.Value, element.Name);
                buffer.Replace(element.ToString(), cdataValue);
            }
        }
    }
}