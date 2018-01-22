using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Data;
using System.IO;
using System.Resources;
using CSharp.cc.Xml;
using CSharp.cc.Data;

namespace CSharp.cc.Data
{

   
    public class XmlDataInterfaceList
    {
        private ArrayList _objectList = new ArrayList();

        public XmlDataInterfaceList() { }
        public XmlDataInterfaceList(ArrayList packages) { AddPackages(packages); }
        public void AddPackages(ArrayList packages) { _objectList = packages; }
    }
    public class XmlDataStore
    {
        protected int _dataRetrieved = 0;
        protected ArrayList _children = new ArrayList();
        protected XmlNode _xmlData;
        protected String OuterXml
        { get { return _xmlData.OuterXml; } }
        protected String InnerXml
        { get { return _xmlData.InnerXml; } }   
        
        public String BoundData
        { get {
            XmlNode baseNode;

            baseNode = _xmlData.FirstChild; // Maybe this does work, maybe it doesn't.
            // Loop through XML data and product output 
            /*
                    baseNode["Name"].InnerText,
                    baseNode["Street1"].InnerText,
                    baseNode["Street2"].InnerText,
                    baseNode["CityName"].InnerText,
                    baseNode["StateOrProvince"].InnerText,
                    baseNode["PostalCode"].InnerText,
                    baseNode["CountryName"].InnerText
             */

            return "Not Implemented.";
        } }

        public XmlDataStore() { }

        public XmlDataStore(XmlNode orderXml)
        {
            _xmlData = orderXml;
        }

        public XmlDataStore(String orderXml)
        {
            XmlDocument xd = new XmlDocument();
            try
            {
             
                xd.LoadXml(orderXml);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid XML: " + orderXml);
                throw ex;
            }
            _xmlData = xd;
        }

        public ArrayList children
        {
            get { return _children; }
        }

        public void AddChild(Object o)
        {
            _children.Add(o);
        }

        public bool Process()
        {
            if (_dataRetrieved == 0)
            {
                _dataRetrieved++;
                foreach (XmlDataStore anItem in _children)
                {
                    anItem.Process();
                }
                return true;
            }
            return false;
        }

        public String Dump()
        {
            String xmlString = String.Empty;
            
            xmlString += "<" + GetType() + ">";
            if (_xmlData == null)
            {
                return CSharp.cc.Xml.Formatters.ErrorAsXml("No Data", "No data exists in object " + GetType());
            }
            else
            {
                xmlString += ToString();
            }
            foreach (XmlDataStore anItem in _children)
            {
                if (anItem._xmlData == null)
                {
                    xmlString += "<Exception>Null Item Data</Exception>";
                }
                else
                {
                    xmlString += anItem._xmlData;
                }
            }
            xmlString += "</" + GetType() + ">";

            String formattedXmlString;
            try
            {
                formattedXmlString = CSharp.cc.Xml.Formatters.ToFormattedString(xmlString);
            } catch {
                Console.WriteLine("Couldn't Format Xml (probably an error in it)");
                formattedXmlString = xmlString;
            }

            return formattedXmlString;
        }

        public void CallbackXmlData(object o, object e)
        {
            XmlDocument itemXmlDoc = (XmlDocument)e;
            _xmlData = itemXmlDoc; 
        }

        
        public bool LoadRecords(String fileName, String recordXPath)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(fileName);
            XmlNodeList xnl = xmldoc.SelectNodes(recordXPath);
            IEnumerator xi = xnl.GetEnumerator();
            XmlNode xn;
            while (xi.MoveNext()) {
                xn = (XmlNode)xi.Current;
                this.AddChild(new XmlDataStore(xn));
            }

            return true;
        }

        public String Get(String path)
        {
            return Formatters.XmlInnerText(_xmlData.SelectSingleNode(path));
        }
        
    }
}
