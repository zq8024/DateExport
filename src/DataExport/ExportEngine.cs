using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace DataExport
{
    /// <summary>
    /// Export engine
    /// </summary>
    public class ExportEngine
    {
        /// <summary>
        /// Export file 
        /// </summary>
        /// <param name="templatePath">template file path</param>
        /// <param name="paramList">parameter list</param>
        /// <returns>a export result that contain file display name and file content</returns>
        public ExportResult Export(string templatePath, Dictionary<string, string> paramList)
        {
            if (string.IsNullOrEmpty(templatePath)) throw new ArgumentNullException("templatePath");

            XslCompiledTransform xslt = GetXslt(templatePath);
            string dataXmlContent = PrepareData(xslt, paramList);

            XmlDocument dataXml = new XmlDocument();
            dataXml.LoadXml(dataXmlContent);

            string fileName = GetFileName(xslt, dataXml);
            string fileContent = GenerateFile(xslt, dataXml);

            return new ExportResult(fileName, fileContent);
        }

        /// <summary>
        /// Export data xml
        /// </summary>
        /// <param name="templatePath">template file path</param>
        /// <param name="paramList">parameter list</param>
        /// <returns>data xml</returns>
        public string GenerateData(string templatePath, Dictionary<string, string> paramList)
        {
            XslCompiledTransform xslt = GetXslt(templatePath);
            string dataXmlContent = PrepareData(xslt, paramList);
            return dataXmlContent;
        }

        /// <summary>
        /// Get xslt
        /// </summary>
        /// <param name="templatePath">template file path</param>
        /// <returns>xslt transform object</returns>
        private XslCompiledTransform GetXslt(string templatePath)
        {
            XsltSettings setting = new XsltSettings(false, true);
            XmlResolver resolver = new XmlUrlResolver();
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(templatePath, setting, resolver);
            return xslt;
        }

        /// <summary>
        /// Prepare data
        /// </summary>
        /// <param name="xslt">xslt transform object</param>
        /// <param name="paramList">parameter list</param>
        /// <returns>data xml string</returns>
        private string PrepareData(XslCompiledTransform xslt, Dictionary<string, string> paramList)
        {
            DataSet ds = new DataSet("root");
            var dtParam = PrepareParamTable(paramList);
            ds.Tables.Add(dtParam);

            XmlDocument dummyXml = GetDummyXml();
            XsltArgumentList args = new XsltArgumentList();
            args.AddParam("Type", "", "PrepareData");
            StringWriter sqlWriter = new StringWriter();
            xslt.Transform(dummyXml.CreateNavigator(), args, sqlWriter);
            string sqlXml = sqlWriter.ToString();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sqlXml);
            XmlNodeList dataNodes = doc.SelectNodes("//data");
            foreach (XmlNode node in dataNodes)
            {
                string name = GetStringAttribute(node, "name");
                string dataSourceKey = GetStringAttribute(node, "database");
                string sql = node.InnerText;

                sql = ReplaceParams(sql, paramList);

                ConnectionStringSettings connSetting = ConfigurationManager.ConnectionStrings[dataSourceKey];
                DbProviderFactory dbFactory = DbProviderFactories.GetFactory(connSetting.ProviderName);
                using (IDbConnection conn = dbFactory.CreateConnection())
                {
                    conn.ConnectionString = connSetting.ConnectionString;
                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;

                    IDbDataAdapter adapter = dbFactory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;

                    conn.Open();
                    DataSet dsTemp = new DataSet();
                    adapter.Fill(dsTemp);
                    conn.Close();

                    DataTable dt = dsTemp.Tables[0];
                    dt.TableName = name;
                    ds.Merge(dt);
                }
            }

            StringWriter tw = new StringWriter();
            ds.WriteXml(tw, XmlWriteMode.IgnoreSchema);
            string dataXml = tw.ToString();

            return dataXml;
        }

        /// <summary>
        /// Get file display name
        /// </summary>
        /// <param name="xslt">xslt transform object</param>
        /// <param name="dataXml">data xml</param>
        /// <returns>file display name</returns>
        private string GetFileName(XslCompiledTransform xslt, XmlDocument dataXml)
        {
            XsltArgumentList args = new XsltArgumentList();
            args.AddParam("Type", "", "GetFileName");
            StringWriter fileNameWriter = new StringWriter();
            xslt.Transform(dataXml.CreateNavigator(), args, fileNameWriter);
            string fileNameXml = fileNameWriter.ToString();

            XmlDocument fileNameDoc = new XmlDocument();
            fileNameDoc.LoadXml(fileNameXml);
            string fileName = fileNameDoc.DocumentElement.InnerText;
            fileName = fileName.Trim().Replace("\r", "").Replace("\n", "");

            return fileName;
        }

        /// <summary>
        /// Generate file with xslt
        /// </summary>
        /// <param name="xslt">xslt transform object</param>
        /// <param name="dataXml">data xml</param>
        /// <returns>xslt transform content</returns>
        private string GenerateFile(XslCompiledTransform xslt, XmlDocument dataXml)
        {
            XsltArgumentList args = new XsltArgumentList();
            args.AddParam("Type", "", "GenerateFile");
            MemoryStream dataWriter = new MemoryStream();
            xslt.Transform(dataXml.CreateNavigator(), args, dataWriter);
            string contentXml = ConvertToUtf8(dataWriter);

            return contentXml;
        }

        /// <summary>
        /// get dummy xml
        /// </summary>
        /// <returns>xml document object</returns>
        private static XmlDocument GetDummyXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("root"));
            return doc;
        }

        /// <summary>
        /// convert memory stream to UTF-8 content
        /// </summary>
        /// <param name="ms">memory stream</param>
        /// <returns>string encoding with UTF-8</returns>
        private static string ConvertToUtf8(MemoryStream ms)
        {
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            string content = sr.ReadToEnd();
            return content;
        }

        /// <summary>
        /// prepare parameter to a data table
        /// </summary>
        /// <param name="paramList">parameter list</param>
        /// <returns>data table</returns>
        private DataTable PrepareParamTable(Dictionary<string, string> paramList)
        {
            DataTable dtParam = new DataTable();
            dtParam.TableName = "vars";
            dtParam.Columns.Add("key", typeof(string));
            dtParam.Columns.Add("value", typeof(string));
            if (paramList != null)
            {
                foreach (string key in paramList.Keys)
                {
                    dtParam.Rows.Add(key, paramList[key]);
                }
            }
            return dtParam;
        }

        /// <summary>
        /// replace params
        /// </summary>
        /// <param name="s">string to replace</param>
        /// <param name="paramList">parameter list</param>
        /// <returns>replaced string</returns>
        private string ReplaceParams(string s, Dictionary<string, string> paramList)
        {
            StringBuilder sb = new StringBuilder(s);
            if (paramList != null)
            {
                foreach (string key in paramList.Keys)
                {
                    sb.Replace("$" + key + "$", string.Format("{0}", paramList[key]));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// get node string attribute
        /// </summary>
        /// <param name="node">node</param>
        /// <param name="attributeName">attribute name</param>
        /// <returns>attribute value</returns>
        private string GetStringAttribute(XmlNode node, string attributeName)
        {
            if (node.Attributes == null) throw new NullReferenceException("node.Attributes can not be null.");

            if (node.Attributes[attributeName] == null) return string.Empty;

            return node.Attributes[attributeName].Value;
        }

    }
}
