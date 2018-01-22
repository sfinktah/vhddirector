using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Data;
using System.IO;
using System.Resources;
using CSharp.cc.Data;
using CSharp.cc.Xml;

namespace CSharp.cc.Xml
{
    public class XmlData
    {

        // DataGridView control looks for the public properties of the object to which it is binding
        // http://www.devx.com/dotnet/Article/33748

        // If we are careful not to use any properties, we can pass an array of Objects that 
        // inherit this class directly to DataGridView.

        // Data Goes Here!
        protected XmlDocument _xmldata = new XmlDocument();
        protected String _recordPath = String.Empty;
        public ArrayList children = new ArrayList();


        public XmlData() { }
        public XmlData(String s) { LoadXml(s); }
        public XmlData(XmlDocument xd) { Load(xd);  }

        public void SetRecordExpression(String xPath) { _recordPath = xPath; }
        public XmlNode SelectBaseNode() { return _xmldata.SelectSingleNode("*"); }
        public String SelectBaseNodeName() { return _xmldata.SelectSingleNode("*").Name; }
        public String InnerXml() { return _xmldata.InnerXml; }
        
        

        /// <summary>
        /// Load XML data from specified string
        /// </summary>
        /// <param name="xmlString">String with XML data</param>
        public void LoadXml(String xmlString) { _xmldata.LoadXml(xmlString); }

        /// <summary>
        /// Load XML data from specified file
        /// </summary>
        /// <param name="xmlString">filename with xml</param>
        public void Load(String filename) { _xmldata.Load(filename); }


        /// <summary>
        /// Load XML from XmlDocument
        /// </summary>
        /// <param name="xmlString">XmlDocument</param>
        public void Load(XmlDocument xd) { _xmldata = xd; }


        /// <summary>
        /// Get value of single node
        /// </summary>
        /// <param name="path">XMLPath</param>
        /// <returns></returns>
        public String Get(String path) { return Formatters.XmlInnerText(_xmldata.SelectSingleNode(path)); }
        public void Set(String path, String value) { XPath.SetSingleNode(_xmldata, path, value); }

        /// <summary>
        /// Add child object
        /// </summary>
        /// <param name="child">object</param>
        public void AddChild(XmlData child) { children.Add(child); }
        public virtual XmlData New() { return new XmlData(); }
        public void Process()
        {
            foreach (String nodeXml in XPath.Split(_xmldata, _recordPath)) 
            {
                XmlData data = New();
                data.LoadXml(nodeXml);
                AddChild(data);
            }
        }

        public void _Process() {
            // Replace(@" xmlns=""urn:ebay:apis:eBLBaseComponents""", ""))
            XmlNodeList list = _xmldata.SelectNodes(_recordPath);
            IEnumerator i = list.GetEnumerator();
            
            while (i.MoveNext()) {
                XmlDocument xn = new XmlDocument();
                xn = (XmlDocument)i.Current;
                AddChild(new XmlData(xn));
            }

        }

    }
}
