<!---
LoggrBase.cfc

Author: Neal Erickson
Email: neal@ink83.com

Copyright 2011, Neal Erickson
--->
<cfcomponent displayname="LoggrBase" hint="Base component from which all other Loggr components are extended">
	<cfset variables.instance = {}>
    <cfset constants = {}>
    
	<cfset constants.DataType.html = 0>
	<cfset constants.DataType.plaintext = 1>
    
    <cffunction name="$throw" returntype="void" output="false" hint="Wrapper function for cfthrow">
        <cfthrow attributeCollection="#arguments#">
    </cffunction>
    
</cfcomponent>