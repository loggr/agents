<!---
Event.cfc

Author: Neal Erickson
Email: neal@ink83.com

Copyright 2011, Neal Erickson
--->
<cfcomponent displayname="Event" extends="LoggrBase">
	<cfset this.Text="">
	<cfset this.Source="">
	<cfset this.User="">
	<cfset this.Link="">
	<cfset this.Data="">
	<cfset this.Value="">
	<cfset this.Tags="">
	<cfset this.Geo="">
	<cfset this.DataType = constants.DataType.plaintext>

	<cffunction name="init" output="false" returntype="any">     
        <cfreturn this />
    </cffunction>
    
</cfcomponent>