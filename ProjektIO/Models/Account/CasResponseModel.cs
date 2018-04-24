using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ProjektIO.Models.Account
{
    [XmlRoot(ElementName = "attributes", Namespace = "cas")]
    public class casAttributes
    {
        [XmlElement(Namespace = "cas")]
        public string uid { get; set; }
        [XmlElement(Namespace = "cas")]
        public string mail { get; set; }
        [XmlElement(Namespace = "cas")]
        public int usos_id { get; set; }
        [XmlElement(Namespace = "cas")]
        public string employeetype { get; set; }
        [XmlElement(Namespace = "cas")]
        public string registeredaddress { get; set; }
        [XmlElement(Namespace = "cas")]
        public string departmentnumber { get; set; }
        [XmlElement(Namespace = "cas")]
        public string givenname { get; set; }
        [XmlElement(Namespace = "cas")]
        public string sn { get; set; }
    }

    [XmlRoot(ElementName = "authenticationSuccess", Namespace = "cas")]
    public class casAuthSuccess
    {
        [XmlElement(Namespace = "cas")]
        public string user { get; set; }
        [XmlElement(Namespace = "cas")]
        public casAttributes attributes { get; set; }
    }

    [XmlRoot(ElementName = "serviceResponse", Namespace = "cas")]
    public class casServiceResponse
    {
        [XmlElement(Namespace = "cas")]
        public casAuthSuccess authenticationSuccess { get; set; }
    }
}