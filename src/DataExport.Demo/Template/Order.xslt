<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:msxsl="urn:schemas-microsoft-com:xslt"
xmlns:my="urn:myns"
extension-element-prefixes ="msxsl my"
>

    <msxsl:script language="C#" implements-prefix="my">
        <![CDATA[        
        
        public decimal Max(decimal a, decimal b)
        {
            return a > b ? a : b;
        }

      ]]>
    </msxsl:script>

    <xsl:output encoding="utf-8" indent="yes" omit-xml-declaration="no" />
    <xsl:param name="Type" select="'GenerateFile'" />

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

    <xsl:variable name="vars" select="/root/vars" />
    <xsl:variable name="order" select="/root/Order" />
    <xsl:variable name="details" select="/root/OrderDetail" />

    <xsl:template name="GetFileName">
        <root>
            Order_<xsl:value-of select="$order/OrderID"/>.xml
        </root>
    </xsl:template>


    <xsl:template name="GenerateFile">
        <xsl:processing-instruction name="mso-application">progid="Excel.Sheet"</xsl:processing-instruction>
        <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:o="urn:schemas-microsoft-com:office:office"
 xmlns:x="urn:schemas-microsoft-com:office:excel"
 xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:html="http://www.w3.org/TR/REC-html40">
            <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
                <Author>Administrator</Author>
                <LastAuthor>2012</LastAuthor>
                <Created>2013-11-30T11:04:31Z</Created>
                <LastSaved>2013-11-30T11:03:56Z</LastSaved>
                <Version>14.00</Version>
            </DocumentProperties>
            <OfficeDocumentSettings xmlns="urn:schemas-microsoft-com:office:office">
                <AllowPNG/>
            </OfficeDocumentSettings>
            <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
                <WindowHeight>9150</WindowHeight>
                <WindowWidth>14940</WindowWidth>
                <WindowTopX>360</WindowTopX>
                <WindowTopY>270</WindowTopY>
                <ProtectStructure>False</ProtectStructure>
                <ProtectWindows>False</ProtectWindows>
            </ExcelWorkbook>
            <Styles>
                <Style ss:ID="Default" ss:Name="Normal">
                    <Alignment ss:Vertical="Bottom"/>
                    <Borders/>
                    <Font ss:FontName="Arial" x:Family="Swiss"/>
                    <Interior/>
                    <NumberFormat/>
                    <Protection/>
                </Style>
                <Style ss:ID="s62">
                    <Alignment ss:Horizontal="Left" ss:Vertical="Bottom"/>
                    <NumberFormat ss:Format="@"/>
                </Style>
                <Style ss:ID="s63">
                    <Alignment ss:Horizontal="Left" ss:Vertical="Bottom"/>
                    <NumberFormat/>
                </Style>
                <Style ss:ID="s65">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                </Style>
                <Style ss:ID="s66">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                </Style>
                <Style ss:ID="s67">
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                </Style>
                <Style ss:ID="s68">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                    <NumberFormat ss:Format="0.00_);\(0.00\)"/>
                </Style>
                <Style ss:ID="s69">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                    <Font ss:FontName="Arial" x:Family="Swiss" ss:Italic="1"/>
                    <Interior ss:Color="#95B3D7" ss:Pattern="Solid"/>
                </Style>
                <Style ss:ID="s70">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                    <Font ss:FontName="Arial" x:Family="Swiss" ss:Italic="1"/>
                    <Interior ss:Color="#95B3D7" ss:Pattern="Solid"/>
                    <NumberFormat ss:Format="@"/>
                </Style>
                <Style ss:ID="s71">
                    <Alignment ss:Horizontal="Center" ss:Vertical="Bottom"/>
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                    <Font ss:FontName="Arial" x:Family="Swiss" ss:Italic="1"/>
                    <Interior ss:Color="#95B3D7" ss:Pattern="Solid"/>
                </Style>
                <Style ss:ID="s72">
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                    <Interior ss:Color="#95B3D7" ss:Pattern="Solid"/>
                </Style>
                <Style ss:ID="s73">
                    <Alignment ss:Horizontal="Left" ss:Vertical="Bottom"/>
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                    <Interior ss:Color="#95B3D7" ss:Pattern="Solid"/>
                    <NumberFormat/>
                </Style>
                <Style ss:ID="s74">
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                    <Font ss:FontName="Arial" x:Family="Swiss" ss:Bold="1"/>
                    <Interior ss:Color="#95B3D7" ss:Pattern="Solid"/>
                </Style>
                <Style ss:ID="s75">
                    <Borders>
                        <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/>
                        <Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/>
                    </Borders>
                    <Font ss:FontName="Arial" x:Family="Swiss" ss:Bold="1"/>
                    <Interior ss:Color="#95B3D7" ss:Pattern="Solid"/>
                    <NumberFormat ss:Format="0.00_);\(0.00\)"/>
                </Style>
                <Style ss:ID="s76">
                    <Borders/>
                    <Font ss:FontName="Arial" x:Family="Swiss"/>
                </Style>
                <Style ss:ID="s77">
                    <Alignment ss:Horizontal="Left" ss:Vertical="Bottom"/>
                    <Borders/>
                    <NumberFormat ss:Format="@"/>
                </Style>
                <Style ss:ID="s78">
                    <Borders/>
                </Style>
                <Style ss:ID="s79">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                    <Borders/>
                </Style>
                <Style ss:ID="s80">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                    <Borders/>
                    <Font ss:FontName="Arial" x:Family="Swiss" ss:Italic="1"/>
                </Style>
                <Style ss:ID="s81">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                    <Borders/>
                    <NumberFormat ss:Format="Short Date"/>
                </Style>
                <Style ss:ID="s82">
                    <Alignment ss:Horizontal="Right" ss:Vertical="Bottom"/>
                    <Borders/>
                    <NumberFormat ss:Format="@"/>
                </Style>
                <Style ss:ID="s83">
                    <Borders/>
                    <Font ss:FontName="Arial" x:Family="Swiss" ss:Italic="1"/>
                </Style>
                <Style ss:ID="s89">
                    <Alignment ss:Horizontal="Center" ss:Vertical="Center"/>
                    <Font ss:FontName="Arial" x:Family="Swiss" ss:Size="12" ss:Bold="1"/>
                </Style>
            </Styles>
            <Worksheet ss:Name="Order">
                <Table x:FullColumns="1"
                 x:FullRows="1">
                    <Column ss:AutoFitWidth="0" ss:Width="74.25"/>
                    <Column ss:StyleID="s62" ss:AutoFitWidth="1" ss:Width="160"/>
                    <Column ss:AutoFitWidth="0" ss:Width="56.25"/>
                    <Column ss:Width="75.75"/>
                    <Column ss:StyleID="s65" ss:AutoFitWidth="0" ss:Width="64.5"/>
                    <Row ss:AutoFitHeight="0">
                        <Cell ss:MergeAcross="4" ss:MergeDown="1" ss:StyleID="s89">
                            <Data ss:Type="String">Order</Data>
                        </Cell>
                    </Row>
                    <Row ss:AutoFitHeight="0" ss:Height="22.5"/>
                    <Row ss:AutoFitHeight="0">
                        <Cell ss:StyleID="s76"/>
                        <Cell ss:StyleID="s77"/>
                        <Cell ss:StyleID="s76"/>
                        <Cell ss:StyleID="s78"/>
                        <Cell ss:StyleID="s79"/>
                    </Row>
                    <Row ss:AutoFitHeight="0">
                        <Cell ss:StyleID="s80">
                            <Data ss:Type="String">Order ID:</Data>
                        </Cell>
                        <Cell ss:StyleID="s77">
                            <Data ss:Type="String">
                                <xsl:value-of select="$order/OrderID"/>
                            </Data>
                        </Cell>
                        <Cell ss:StyleID="s78"/>
                        <Cell ss:StyleID="s80">
                            <Data ss:Type="String">Order Date:</Data>
                        </Cell>
                        <Cell ss:StyleID="s81">
                            <Data ss:Type="DateTime">
                                <xsl:value-of select="substring-before($order/OrderDate, 'T')"/>
                            </Data>
                        </Cell>
                    </Row>
                    <Row ss:AutoFitHeight="0">
                        <Cell ss:StyleID="s80">
                            <Data ss:Type="String">Contact name:</Data>
                        </Cell>
                        <Cell ss:StyleID="s77">
                            <Data ss:Type="String">
                                <xsl:value-of select="$order/ContactName"/>
                            </Data>
                        </Cell>
                        <Cell ss:StyleID="s78"/>
                        <Cell ss:StyleID="s80">
                            <Data ss:Type="String">Shipped Date:</Data>
                        </Cell>
                        <Cell ss:StyleID="s81">
                            <Data ss:Type="DateTime">
                                <xsl:value-of select="substring-before($order/ShippedDate, 'T')"/>
                            </Data>
                        </Cell>
                    </Row>
                    <Row ss:AutoFitHeight="0">
                        <Cell ss:StyleID="s80">
                            <Data ss:Type="String">Country:</Data>
                        </Cell>
                        <Cell ss:StyleID="s77">
                            <Data ss:Type="String">
                                <xsl:value-of select="$order/ShipCountry"/>
                            </Data>
                        </Cell>
                        <Cell ss:StyleID="s78"/>
                        <Cell ss:StyleID="s80">
                            <Data ss:Type="String">City:</Data>
                        </Cell>
                        <Cell ss:StyleID="s82">
                            <Data ss:Type="String">
                                <xsl:value-of select="$order/ShipCity"/>
                            </Data>
                        </Cell>
                    </Row>
                    <Row ss:AutoFitHeight="0">
                        <Cell ss:StyleID="s80">
                            <Data ss:Type="String">Address:</Data>
                        </Cell>
                        <Cell ss:StyleID="s77" ss:MergeAcross="3">
                            <Data ss:Type="String">
                                <xsl:value-of select="$order/ShipAddress"/>
                            </Data>
                        </Cell>
                    </Row>
                    <Row ss:Index="10" ss:AutoFitHeight="0" ss:Height="16.5">
                        <Cell ss:StyleID="s69">
                            <Data ss:Type="String">Num</Data>
                        </Cell>
                        <Cell ss:StyleID="s71">
                            <Data ss:Type="String">Product</Data>
                        </Cell>
                        <Cell ss:StyleID="s70">
                            <Data ss:Type="String">Price</Data>
                        </Cell>
                        <Cell ss:StyleID="s69">
                            <Data ss:Type="String">Quantity</Data>
                        </Cell>
                        <Cell ss:StyleID="s69">
                            <Data ss:Type="String">Cost</Data>
                        </Cell>
                    </Row>
                    <xsl:for-each select="$details">
                        <Row ss:AutoFitHeight="0">
                            <Cell ss:StyleID="s67">
                                <Data ss:Type="Number">
                                    <xsl:value-of select="position()"/>
                                </Data>
                            </Cell>
                            <Cell ss:StyleID="s67">
                                <Data ss:Type="String"><xsl:value-of select="ProductName"/></Data>
                            </Cell>
                            <Cell ss:StyleID="s68">
                                <Data ss:Type="Number"><xsl:value-of select="UnitPrice"/></Data>
                            </Cell>
                            <Cell ss:StyleID="s66">
                                <Data ss:Type="Number"><xsl:value-of select="Quantity"/></Data>
                            </Cell>
                            <Cell ss:StyleID="s68" ss:Formula="=RC[-2]*RC[-1]">
                                <Data ss:Type="Number"></Data>
                            </Cell>
                        </Row>
                    </xsl:for-each>

                    <Row ss:AutoFitHeight="0">
                        <Cell ss:StyleID="s72"/>
                        <Cell ss:StyleID="s74">
                            <Data ss:Type="String">Total</Data>
                        </Cell>
                        <Cell ss:StyleID="s73"/>
                        <Cell ss:StyleID="s72"/>
                        <Cell ss:StyleID="s75" ss:Formula="=SUM(R11C:R[-1]C)">
                            <Data ss:Type="Number"></Data>
                        </Cell>
                    </Row>
                    <Row ss:AutoFitHeight="0">
                        <Cell ss:Index="2" ss:StyleID="s63"/>
                    </Row>
                    <Row ss:AutoFitHeight="0">
                        <Cell ss:Index="2" ss:StyleID="s63"/>
                    </Row>
                </Table>
                <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
                    <Unsynced/>
                    <Print>
                        <ValidPrinterInfo/>
                        <HorizontalResolution>300</HorizontalResolution>
                        <VerticalResolution>300</VerticalResolution>
                    </Print>
                    <Selected/>
                    <Panes>
                        <Pane>
                            <Number>3</Number>
                            <ActiveRow>19</ActiveRow>
                            <ActiveCol>3</ActiveCol>
                        </Pane>
                    </Panes>
                    <ProtectObjects>False</ProtectObjects>
                    <ProtectScenarios>False</ProtectScenarios>
                </WorksheetOptions>
            </Worksheet>
        </Workbook>

    </xsl:template>

</xsl:stylesheet>