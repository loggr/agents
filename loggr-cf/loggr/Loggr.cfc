<!---
Loggr.cfc

Author: Neal Erickson
Email: neal@ink83.com

Copyright 2011, Neal Erickson
--->
<cfcomponent displayname="Loggr" extends="LoggrBase">
	<cfset variables.instance.logKey = "">
    <cfset variables.instance.apiKey = "">
    
	<cffunction name="init" output="false" returntype="any">
    	<cfargument name="logKey" required="yes" type="string">
        <cfargument name="apiKey" required="yes" type="string">
        <cfscript>
        variables.instance.logKey = arguments.logKey;
        variables.instance.apiKey = arguments.apiKey;
        
        this.Events = CreateObject("component", "Events").init(variables.instance.logKey,variables.instance.apiKey);
        
        return this;
		</cfscript>
    </cffunction>
    
</cfcomponent>