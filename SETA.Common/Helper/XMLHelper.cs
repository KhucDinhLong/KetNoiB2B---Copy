using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SETA.Common
{
    public class XMLHelper
    {
        public static T DeserializeXML<T>(string Xml)
        {

            XmlSerializer ser;
            ser = new XmlSerializer(typeof(T));
            StringReader stringReader;
            stringReader = new StringReader(Xml);
            XmlTextReader xmlReader;
            xmlReader = new XmlTextReader(stringReader);
            T obj;
            obj = (T)ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();
            return obj;
        }

        //Serializes the <i>Obj</i> to an XML string.
        public static string SerializeXML<T>(object Obj)
        {

            XmlSerializer ser;
            ser = new XmlSerializer(typeof(T));
            MemoryStream memStream;
            memStream = new MemoryStream();
            XmlTextWriter xmlWriter;
            xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
            xmlWriter.Namespaces = true;
            ser.Serialize(xmlWriter, Obj);
            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));

            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "")
                .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "")
                .Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "")
                .Insert(0, "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
        }


        private static void RemoveNullNote(XmlNode note)
        {
            foreach (XmlNode n in note.ChildNodes)
            {
                if (n.HasChildNodes)
                    RemoveNullNote(n);
                else if (n.Attributes["xsi:nil"] != null && n.Attributes["xsi:nil"].Equals("true"))
                {
                    note.RemoveChild(n);
                }
            }
        }

        public static string SerializeXMLWithNoSpace<T>(object Obj)
        {

            XmlSerializer ser;
            // instantiate the container for all attribute overrides
            XmlAttributeOverrides xOver = new XmlAttributeOverrides();

            // define a set of XML attributes to apply to the root element
            XmlAttributes xAttrs1 = new XmlAttributes();

            // define an XmlRoot element (as if [XmlRoot] had decorated the type)
            // The namespace in the attribute override is the empty string. 
            XmlRootAttribute xRoot = new XmlRootAttribute() { Namespace = "" };

            // add that XmlRoot element to the container of attributes
            xAttrs1.XmlRoot = xRoot;

            // add that bunch of attributes to the container holding all overrides
            xOver.Add(typeof(T), xAttrs1);
            //Create our own namespaces for the output
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            //Add an empty namespace and empty value
            ns.Add("", "");

            ser = new XmlSerializer(typeof(T), xOver);
            MemoryStream memStream;
            memStream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.Encoding = UTF8Encoding.UTF8;
            XmlWriter xmlWriter = XmlWriter.Create(memStream, settings);
            //XmlTextWriter xmlWriter;
            //xmlWriter = new XmlTextWriter(memStream, UTF8Encoding.UTF8);
            //xmlWriter.Namespaces = true;
            ser.Serialize(xmlWriter, Obj, ns);
            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.ToArray());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
            //xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            //xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            //return xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "")
            //    .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "")
            //    .Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
        }

        public static string CleanEmptyTags(String xml)
        {
            Regex regex = new Regex(@"(\s)*<(\w)*(\s)*/>");
            return regex.Replace(xml, string.Empty);
        }

        /// <summary>
        /// Serializes the object to XML based on encoding and name spaces.
        /// </summary>
        /// <param name="serializer">XmlSerializer object 
        /// (passing as param to avoid creating one every time)</param>
        /// <param name="encoding">The encoding of the serialized Xml</param>
        /// <param name="ns">The namespaces to be used by the serializer</param>
        /// <param name="omitDeclaration">Whether to omit Xml declarartion or not</param>
        /// <param name="objectToSerialize">The object we want to serialize to Xml</param>
        /// <returns></returns>
        public static string Serialize(XmlSerializer serializer,
                                       Encoding encoding,
                                       XmlSerializerNamespaces ns,
                                       bool omitDeclaration,
                                       object objectToSerialize)
        {
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = omitDeclaration;
            settings.Encoding = encoding;
            XmlWriter writer = XmlWriter.Create(ms, settings);
            serializer.Serialize(writer, objectToSerialize, ns);
            return encoding.GetString(ms.ToArray()); ;
        }

        /// <summary>
        /// SuNV added: convert OBJ -> XMLDocument. Only using for ECS Payment
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public static XmlDocument ObjToXmlDoc<T>(object Obj)
        {
            try
            {
                //var ser = new XmlSerializer(typeof(T));                
                //XmlDocument xd = null;

                //using (MemoryStream memStm = new MemoryStream())
                //{
                //    ser.Serialize(memStm, Obj);
                //    memStm.Position = 0;

                //    var settings = new XmlReaderSettings {IgnoreWhitespace = true};

                //    using (var xtr = XmlReader.Create(memStm, settings))
                //    {
                //        xd = new XmlDocument();
                //        xd.Load(xtr);
                //    }
                //}

                //return xd;
                
                XmlSerializer ser;
                ser = new XmlSerializer(typeof(T));
                MemoryStream memStream;
                memStream = new MemoryStream();
                XmlTextWriter xmlWriter;
                xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
                xmlWriter.Namespaces = true;
                ser.Serialize(xmlWriter, Obj);
                xmlWriter.Close();
                memStream.Close();
                var xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));

                xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
                xml = xml
                    .Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "")
                    .Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");

                //format xml request
                xml = FormatXMLForESCPayment(xml);

                var xd = new XmlDocument();
                xd.LoadXml(xml);
                return xd;
            }
            catch (Exception ex)
            {
                return new XmlDocument();
            }            
        }

        private static string FormatXMLForESCPayment(string xml)
        {
            try
            {
                xml = xml
                    .Replace("<CardValidate  >", "<validate>")
                    .Replace("</CardValidate>", "</validate>")

                    .Replace("<AddCustomer  >", "<add-customer>")
                    .Replace("</AddCustomer>", "</add-customer>")

                    .Replace("<UpdateCustomer  >", "<update-customer>")
                    .Replace("</UpdateCustomer>", "</update-customer>")

                    .Replace("<api_key>", "<api-key>")
                    .Replace("</api_key>", "</api-key>")

                    .Replace("<redirect_url>", "<redirect-url>")
                    .Replace("</redirect_url>", "</redirect-url>")

                    .Replace("<customer_vault_id>", "<customer-vault-id>")
                    .Replace("</customer_vault_id>", "</customer-vault-id>")

                    .Replace("<first_name>", "<first-name>")
                    .Replace("</first_name>", "</first-name>")

                    .Replace("<last_name>", "<last-name>")
                    .Replace("</last_name>", "</last-name>")

                    .Replace("<token_id>", "<token-id>")
                    .Replace("</token_id>", "</token-id>")

                    .Replace("<CompleteAction  >", "<complete-action>")
                    .Replace("</CompleteAction>", "</complete-action>")

                    .Replace("<DeleteCustomer  >", "<delete-customer>")
                    .Replace("</DeleteCustomer>", "</delete-customer>");

            }
            catch (Exception)
            {
                return xml;                
            }
            return xml;
        }

        public static string FormatResponseFromESCPayment(string xml)
        {
            try
            {
                xml = xml
                    .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "")
                    .Replace("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "")

                    .Replace("<response>", "<Response>")
                    .Replace("</response>", "</Response>")

                    .Replace("<result-text>", "<result_text>")
                    .Replace("</result-text>", "</result_text>")

                    .Replace("<transaction-id>", "<transaction_id>")
                    .Replace("</transaction-id>", "</transaction_id>")

                    .Replace("<result-code>", "<result_code>")
                    .Replace("</result-code>", "</result_code>")

                    .Replace("<action-type>", "<action_type>")
                    .Replace("</action-type>", "</action_type>")

                    .Replace("<form-url>", "<form_url>")
                    .Replace("</form-url>", "</form_url>")

                    .Replace("<customer-vault-id>", "<customer_vault_id>")
                    .Replace("</customer-vault-id>", "</customer_vault_id>")

                    .Replace("<authorization-code>", "<authorization_code>")
                    .Replace("</authorization-code>", "</authorization_code>")

                    .Replace("<avs-result>", "<avs_result>")
                    .Replace("</avs-result>", "</avs_result>")

                    .Replace("<cvv-result>", "<cvv_result>")
                    .Replace("</cvv-result>", "</cvv_result>")

                    .Replace("<amount-authorized>", "<amount_authorized>")
                    .Replace("</amount-authorized>", "</amount_authorized>")

                    .Replace("<ip-address>", "<ip_address>")
                    .Replace("</ip-address>", "</ip_address>")

                    .Replace("<processor-id>", "<processor_id>")
                    .Replace("</processor-id>", "</processor_id>")

                    .Replace("<customer-id>", "<customer_id>")
                    .Replace("</customer-id>", "</customer_id>")

                    .Replace("<merchant-receipt-email>", "<merchant_receipt_email>")
                    .Replace("</merchant-receipt-email>", "</merchant_receipt_email>")

                    .Replace("<customer-receipt>", "<customer_receipt>")
                    .Replace("</customer-receipt>", "</customer_receipt>")

                    .Replace("<first-name>", "<first_name>")
                    .Replace("</first-name>", "</first_name>")

                    .Replace("<last-name>", "<last_name>")
                    .Replace("</last-name>", "</last_name>")

                    .Replace("<cc-number>", "<cc_number>")
                    .Replace("</cc-number>", "</cc_number>")

                    .Replace("<cc-exp>", "<cc_exp>")
                    .Replace("</cc-exp>", "</cc_exp>");

                return xml;
            }
            catch (Exception)
            {
                return xml;
            }
        }
    }
}
