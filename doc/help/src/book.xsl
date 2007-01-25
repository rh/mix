<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

  <xsl:import href="../../../../tools/docbook/htmlhelp/htmlhelp.xsl"/>

  <xsl:param name="generate.legalnotice.link" select="1"/>
  <xsl:param name="suppress.navigation" select="0"/>
  <xsl:param name="admon.graphics" select="1"/>
  <xsl:param name="admon.graphics.path" select="'images/'"/>
  <xsl:param name="admon.graphics.extension" select="'.gif'"/>
  <xsl:param name="html.stylesheet" select="'style.css'"/>
  <xsl:param name="htmlhelp.hhc.binary" select="0"/>
  <xsl:param name="htmlhelp.hhc.folders.instead.books" select="0"/>
  <xsl:param name="toc.section.depth" select="4"/>
  <xsl:param name="use.id.as.filename" select="1"/>

  <xsl:template name="user.header.navigation">
    <br/>
    <p>Header Information</p>
    <br/>
  </xsl:template>

  <xsl:template name="user.footer.navigation">
    <br/>
    <p>Footer Information</p>
  </xsl:template>

</xsl:stylesheet>
