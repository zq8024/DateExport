namespace DataExport
{
    /// <summary>
    /// Export result
    /// </summary>
    public class ExportResult
    {
        /// <summary>
        /// File display name
        /// </summary>
        public string FileDisplayName { get; set; }

        /// <summary>
        /// File content
        /// </summary>
        public string FileContent { get; set; }

        /// <summary>
        /// Construct export result
        /// </summary>
        /// <param name="fileDisplayName">File display name</param>
        /// <param name="fileContent">File content</param>
        public ExportResult(string fileDisplayName, string fileContent)
        {
            FileDisplayName = fileDisplayName;
            FileContent = fileContent;
        }

    }
}