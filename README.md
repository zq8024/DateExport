Formatted Data Export for C#
Add an export function to your application from database data into a stylized formatted file for print or sending to others through email.

Data export with stylized format
Drop-in classes for exporting data in a C# application into stylized tabular format. Use it to provide export option in your application.

Features
1. Export data with stylized format
2. User can get data from multiple tables
3. Use XSLT to tranform the XML to target content

Package contents
bin -------------------------  binary files.
doc -------------------------  document files.
src -------------------------  source code and demo.
database --------------------  sample database for demo.

files in folder src\DataExport.Demo\Template
Order-excel.xml --- excel spreadsheet 2003 xml format, it can open with excel application.
OrderData.xml ----- sample data xml, it is a DataSet.
Order.xslt -------- sample template, use it to generate the file.

How to Use
	For example, we want export a order with detail to a excel.
	1. Declare a param list, and set the OrderId parameter.	
	Dictionary<string, string> paramList = new Dictionary<string, string>();
	paramList["OrderId"] = txtOrderId.Text.Trim();
	
	2. Export the file with template and parameter list	
	ExportEngine engine = new ExportEngine();
	ExportResult result = engine.Export(txtTemplate.Text.Trim(), paramList);

	3. Get file path to save
	string folder = "Temp";
	string ext = Path.GetExtension(result.FileDisplayName);
	string filePath = GenerateFilePath(folder, ext);

	4. Save file to given path
	SaveFile(result.FileContent, filePath);
	

How to configure a template

	A template is a XSLT file, it contain three main templates, a sample can be found in the demo, and looks as follows:
	
	<xsl:template match="/">
        <xsl:choose>
            <xsl:when test="$Type = 'PrepareData'">
                <xsl:call-template name="PrepareData"></xsl:call-template>
            </xsl:when>
            <xsl:when test="$Type = 'GetFileName'">
                <xsl:call-template name="GetFileName"></xsl:call-template>
            </xsl:when>
            <xsl:when test="$Type = 'GenerateFile' ">
                <xsl:call-template name="GenerateFile" ></xsl:call-template>
            </xsl:when>
        </xsl:choose>
    </xsl:template>	
	
	1. PrepareData template
	Use it to get the data from database. Replace the parameter token with value and run SQL to fill a DataSet. Each data represent a data table in the result DataSet.
		'name' attribute is the data table name, and 'database' attribute is the connection string key in the application configuration, such as app.config or web.config.
		Parameter token is surround with '$', for example $OrderId$ , it will replace with the OrderId parameter.
		
	    <xsl:template name="PrepareData">
        <root>
            <data name="Order" database="Northwind" >
                SELECT O.OrderID,
                c.ContactName,
                O.OrderDate,
                O.ShippedDate,
                O.ShipCountry,
                O.ShipCity,
                O.ShipAddress
                FROM Orders O INNER JOIN Customers c ON c.CustomerID = O.CustomerID
                WHERE OrderID = '$OrderId$'
            </data>
            <data name="OrderDetail" database="Northwind" >
                SELECT OrderId,
                p.ProductName,
                d.UnitPrice,
                Quantity,
                d.UnitPrice * Quantity Total
                FROM [Order Details] d INNER JOIN Products p ON p.ProductID = d.ProductID
                WHERE OrderID = '$OrderId$'
            </data>
        </root>
    </xsl:template>
	
	2. GetFileName template
	Use it to get file display name.
	<xsl:template name="GetFileName">
        <root>
            Order_<xsl:value-of select="$order/OrderID"/>.xml
        </root>
    </xsl:template>
	
	3. GenerateFile template
	Use it to generate the file main content. it is too large to display it, so please see in the demo.	We prepare a excel file, and set the data format, then save as 'XML Spreadsheet 2003' format, and then open it with notepad, copy the conent to the template,
	and replace the content with xslt token.
	
	







