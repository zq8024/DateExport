using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DataExport.Demo
{
    /// <summary>
    /// Data export demo
    /// </summary>
    public partial class DataExportDemo : Form
    {
        public DataExportDemo()
        {
            InitializeComponent();
            txtTemplate.Text = "Template/Order.xslt";
            txtOrderId.Text = "10458";
        }

        /// <summary>
        /// Select template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectTemplate_Click(object sender, EventArgs e)
        {
            const string templateFilter = "Template file(*.xslt)|*.xslt|Xml(*.xml)|*.xml|All File(*.*)|*.*";
            string templatePath = OpenTemplate(templateFilter);
            if (!string.IsNullOrEmpty(templatePath))
            {
                txtTemplate.Text = templatePath;
            }
        }

        /// <summary>
        /// Open template
        /// </summary>
        /// <param name="filter">file filter</param>
        /// <returns>file path</returns>
        private string OpenTemplate(string filter)
        {
            string templatePath = "";
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = filter;
            dlgOpen.Multiselect = false;
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                templatePath = dlgOpen.FileName;
            }
            return templatePath;
        }

        /// <summary>
        /// Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (CheckUserInput() == false) return;

            ExportEngine engine = new ExportEngine();
            Dictionary<string, string> paramList = new Dictionary<string, string>();
            paramList["OrderId"] = txtOrderId.Text.Trim();
            ExportResult result = engine.Export(txtTemplate.Text.Trim(), paramList);

            string folder = "Temp";
            string ext = Path.GetExtension(result.FileDisplayName);
            string filePath = GenerateFilePath(folder, ext);

            SaveFile(result.FileContent, filePath);

            txtExportFileName.Text = result.FileDisplayName;
            txtExportFilePath.Text = filePath;
        }

        /// <summary>
        /// generate data xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateData_Click(object sender, EventArgs e)
        {
            if (CheckUserInput() == false) return;

            ExportEngine engine = new ExportEngine();
            Dictionary<string, string> paramList = new Dictionary<string, string>();
            paramList["OrderId"] = txtOrderId.Text.Trim();
            string dataXml = engine.GenerateData(txtTemplate.Text.Trim(), paramList);

            string dataPath = "temp/data.xml";
            SaveFile(dataXml, dataPath);

            MessageBox.Show("Generate data success, path: " + dataPath);
        }

        /// <summary>
        /// check user input
        /// </summary>
        /// <returns>return true if valid, or false</returns>
        private bool CheckUserInput()
        {
            if (string.IsNullOrEmpty(txtTemplate.Text.Trim()))
            {
                MessageBox.Show("Please select a template");
                return false;
            }

            if (string.IsNullOrEmpty(txtOrderId.Text.Trim()))
            {
                MessageBox.Show("Please input order ID");
                return false;
            }

            return true;
        }

        /// <summary>
        /// generate a random file path with a given file extension
        /// </summary>
        /// <param name="relativeFolder">relative file folder</param>
        /// <param name="ext">file extension</param>
        /// <returns>a file path</returns>
        private string GenerateFilePath(string relativeFolder, string ext)
        {
            if (string.IsNullOrEmpty(ext)) throw new ArgumentNullException("ext");
            if (ext.StartsWith(".") == false) ext = "." + ext;

            string relativePath = string.Format(@"{0}\{1}{2}", relativeFolder, Guid.NewGuid().ToString("N"), ext);

            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

            string folder = Path.GetDirectoryName(fullPath);
            if (folder != null && Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }
            return fullPath;
        }

        /// <summary>
        /// save file content to file
        /// </summary>
        /// <param name="content">file content</param>
        /// <param name="filePath">file path</param>
        public static void SaveFile(string content, string filePath)
        {
            filePath = filePath.Trim();
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            string folder = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(folder))
            {
                if (Directory.Exists(folder) == false) Directory.CreateDirectory(folder);
            }
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(content);
                sw.Close();
            }
        }

    }
}
