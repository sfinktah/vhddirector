// Adding to XML via String Manipulation / File manipulation
// http://stackoverflow.com/questions/849043/fastest-way-to-add-new-node-to-end-of-an-xml

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Specialized;
using CSharp.cc.Xml;

namespace CSharp.cc.Xml
{
    public class XmlTao
    {
        static public String SelectSingleNodeInnerText(XmlDocument xdoc, String xpath)
        {
            try
            {
                XmlNode xn = xdoc.SelectSingleNode(xpath);
                if (xn != null)
                {
                    return xn.InnerText;
                }
            }
            catch (Exception ex)
            {
                return String.Empty;
            }

            return String.Empty;
        }

        static public bool SetNode(XmlDocument doc, String path, XmlElement newNode)
        {
            //XmlTextReader reader = new XmlTextReader(FILE_NAME);
            //XmlDocument doc = new XmlDocument(); 
            //doc.Load(reader);
            //reader.Close();

//Select the cd node with the matching title

            XmlNode oldNode;
            XmlElement root = doc.DocumentElement;
            oldNode = root.SelectSingleNode(path); // oldCd = root.SelectSingleNode("/catalog/cd[title='" + oldTitle + "']");

            //XmlElement newCd = doc.CreateElement("cd");
            //newCd.SetAttribute("country",country.Text);
            //newCd.InnerXml = "<title>" + this.comboBox1.Text + "</title>" + 
            //    "<artist>" + artist.Text + "</artist>" +
            //    "<price>" + price.Text + "</price>";

            // Similarly, use InsertAfter and RemoveChild to insert and remove a node
            root.ReplaceChild(newNode, oldNode);


            // doc.Save(FILE_NAME); //save the output to a file
            return true;
        }
    }


    public class XPath
    {
        static public String SelectSingleNode(XmlDocument xmlDocument, String xPathExpression)
        {
            // Not the fastest way....
            return SelectSingleNode(xmlDocument.OuterXml, xPathExpression);
        }


        static public String SelectSingleNode(String xml, String xPathExpression)
        {
            XPathDocument doc = new XPathDocument(new StringReader(xml));
            XPathNavigator nav = doc.CreateNavigator();
            return SelectSingleNode(nav, xPathExpression);
        }


        static public String SelectSingleNode(XPathNavigator nav, String xPathExpression)
        {
            XPathExpression expr = nav.Compile(xPathExpression);
            XPathNodeIterator iter = nav.Select(xPathExpression);
            if (iter.MoveNext())
                return iter.Current.Value;
            return null;
        }
        static public ArrayList SelectSingleNodes(String xml, String xPathExpression)
        { 
            XPathDocument doc = new XPathDocument(new StringReader(xml));
            XPathNavigator nav = doc.CreateNavigator();
            return SelectSingleNodes(nav, xPathExpression);
        }

        static public ArrayList SelectSingleNodes(XPathNavigator nav2, String xPathExpression2)
        {
            XPathExpression TransExpr = nav2.Compile(xPathExpression2);
            XPathNodeIterator TransIterator = nav2.Select(xPathExpression2);
            ArrayList nodeValues = new ArrayList();

            while (TransIterator.MoveNext())
            {
                XPathNavigator nav3 = TransIterator.Current.Clone();
                nodeValues.Add(TransIterator.Current.Value);
            }

            return nodeValues;
        }

        static public void SetSingleNode(XmlDocument xmlDoc, String path, String value)
        {
            String[] pathArray = value.Split('/');
            SetSingleNodeByPathArray(xmlDoc, value, pathArray);
        }

        static public void SetSingleNodeByPathArray(XmlDocument xmlDoc, String value, String[] path)
        {

            if (xmlDoc != null)
            {
                // Create a complete chain for the new value (we will use as much of it as we need)
                int i = path.Length - 1;

                XmlText txtActor = xmlDoc.CreateTextNode(value);
                XmlElement elmNew = xmlDoc.CreateElement(path[i]);
                elmNew.AppendChild(txtActor);

                XmlNode nodeChain = elmNew.Clone();

                while (--i >= 0)
                {
                    elmNew = xmlDoc.CreateElement(path[i]);
                    elmNew.AppendChild(nodeChain);
                    nodeChain = elmNew.Clone();
                }


                // Get a reference to the root node of the document we're adding/setting

                XmlElement elmRoot = xmlDoc.DocumentElement;
                XmlNode nodes = xmlDoc.DocumentElement;

                XmlNode curNode = nodes;            // Keep a pointer to our constructed chain, and keep it matched
                XmlNode curChain = nodeChain;       // to the real chain - insert when we need to.

                XmlNodeList curNodeList;


                foreach (String nodeString in path)
                {
                    curNodeList = curNode.ChildNodes;

                    Boolean found = false;
                    foreach (XmlNode aNode in curNodeList)
                    {
                        if (aNode.Name == nodeString)
                        {

                            found = true;

                            curNode = aNode;
                            curChain = curChain.FirstChild;

                            continue;
                        }

                        if (!found) break;
                    }

                    // If we got here, we've reached the end of our chain, time to graft.
                    // TODO: May not work if top node does't exist - but we're not meant to be CREATING xmlDocs here...
                    if (!found) curNode.AppendChild(curChain.FirstChild);

                    // If it was found, but we are at the end, then it exists, we can just change it.
                    curNode = curChain;
                    // or maybe
                    // curNode.Value = value
                }
            }
        }
               

            
      static public void SetSingleNodeXP(XmlDocument xmlDocument, String path, String value)
        {
            // xmlDocument.SelectSingleNode(path);
            
            // XmlDocument doc = new XmlDocument();
            // doc.Load("booksort.xml");

            // XmlNode book;
            // XmlNode root = xmlDocument.DocumentElement;
            XmlNode de = xmlDocument.DocumentElement;
            XmlNode xn;

            // XmlNode xn = de.SelectSingleNode("descendant::book[author/last-name='Austen']");
            try
            {
                xn = de.SelectSingleNode(path);
                if (xn != null)
                {
                    xn.InnerText = value;   // book.LastChild.InnerText = "15.95";
                }
            }
            catch (System.Xml.XPath.XPathException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
 
        static public ArrayList Split(XmlDocument xmlDocument, String xPathExpression)
        {
            XPathDocument doc = new XPathDocument(new StringReader(xmlDocument.OuterXml.Replace(@" xmlns=""urn:ebay:apis:eBLBaseComponents""", "")));
            XPathNavigator nav = doc.CreateNavigator();
            XPathExpression expr = nav.Compile(xPathExpression);
            XPathNodeIterator iter = nav.Select(xPathExpression);

            ArrayList nodeValues = new ArrayList();

            while (iter.MoveNext())
            {
                // XPathNavigator nav3 = TransIterator.Current.Clone();
                nodeValues.Add(iter.Current.OuterXml);
            }

            return nodeValues;
        }

        static public ArrayList Split(String xml, String xPathExpression)
        {
            XPathDocument doc = new XPathDocument(new StringReader(xml.Replace(@"xmlns=""urn:ebay:apis:eBLBaseComponents""", "")));
            XPathNavigator nav = doc.CreateNavigator();
            return Split(nav, xPathExpression);
        }

        static public ArrayList Split(XPathNavigator nav, String xPathExpression)
        {


            XPathExpression TransExpr = nav.Compile(xPathExpression);
            XPathNodeIterator TransIterator = nav.Select(xPathExpression);
            ArrayList nodeValues = new ArrayList();

            while (TransIterator.MoveNext())
            {
                // XPathNavigator nav3 = TransIterator.Current.Clone();
                nodeValues.Add(TransIterator.Current.InnerXml);
            }

            return nodeValues;
        }

    }


    public class Formatters
    {

        /// <summary>Format an error as XML</summary>
        /// <param name="ShortMessage">Title</param>
        /// <param name="argsRest">LongMessage, Warning/Error, ErrorCode, ErrorClassification</param>
        /// <returns>XML as a String</returns>

        static public String ErrorAsXml(String ShortMessage, params String[] argsRest)
        {

            String[] argsXmlLine = {
                            "<LongMessage>{0}</LongMessage>",
                            "<SeverityCode>{0}</SeverityCode>",
                            "<ErrorCode>{0}</ErrorCode>",
                            "<ErrorClassification>{0}</ErrorClassification>",
                                   };

            String ErrorInnerXml = String.Empty;
            ErrorInnerXml += String.Format(
                "<ShortMessage>{0}</ShortMessage>", ShortMessage);

            for (int i = 0; i < argsRest.Length && i < argsXmlLine.Length; i++)
            {
                ErrorInnerXml += argsXmlLine[i].Replace("{0}", argsRest[i]);
            }

            String ErrorOuterXml = "<Errors>" + ErrorInnerXml + "</Errors>";
            if (!String.IsNullOrEmpty(argsRest[1]))
            {
                String ErrorOutXml = "<Ack>" + argsRest[1] + "</Ack>" + ErrorOuterXml;
            }

            return ErrorOuterXml;
        }

        /// <summary>
        /// Returns formatted xml string (indent and newlines) from unformatted XML
        /// string for display in eg textboxes.
        /// </summary>
        /// <param name="sUnformattedXml">Unformatted xml string.</param>
        /// <returns>Formatted xml string and any exceptions that occur.</returns>

        static public String ToFormattedString(string sUnformattedXml)
        {
            XmlDocument xd = new XmlDocument();
            String formattedOutput = String.Empty;
            try
            {
                xd.LoadXml(sUnformattedXml);
                formattedOutput = ToFormattedString(xd);
            }
            catch
            {
                formattedOutput = sUnformattedXml;
            }

            return formattedOutput;
        }
        static public String ToFormattedString(XmlDocument xd)
        {
            //load unformatted xml into a dom


            //will hold formatted xml

            StringBuilder sb = new StringBuilder();

            //pumps the formatted xml into the StringBuilder above

            StringWriter sw = new StringWriter(sb);

            //does the formatting

            XmlTextWriter xtw = null;

            try
            {
                //point the xtw at the StringWriter

                xtw = new XmlTextWriter(sw);

                //we want the output formatted

                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 3;

                //get the dom to dump its contents into the xtw 

                xd.WriteTo(xtw);
            }
            finally
            {
                //clean up even if error

                if (xtw != null)
                    xtw.Close();
            }

            //return the formatted xml

            return sb.ToString();
        }

        static public String XmlInnerText(XmlNode xn)
        {
            return XmlInnerText((XmlElement)xn);
        }

        static public String XmlInnerText(XmlElement xe)
        {
            if (xe == null)
                return "";
            return xe.InnerText;
        }


    }
}

