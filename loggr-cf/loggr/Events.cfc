<!---
Events.cfc

Author: Neal Erickson
Email: neal@ink83.com

Copyright 2011, Neal Erickson
--->
<cfcomponent displayname="Events" extends="LoggrBase">
	<cfset variables.instance.logKey = "">
    <cfset variables.instance.apiKey = "">
    
	<cffunction name="init" output="false">
    	<cfargument name="logKey" required="yes">
        <cfargument name="apiKey" required="yes">        
        <cfscript>
		variables.instance.logKey = arguments.logKey;
        variables.instance.apiKey = arguments.apiKey;
        
        return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Create" output="false" returntype="any">
    	<cfscript>
		return CreateObject("component", "FluentEvent").init(variables.instance.logKey,variables.instance.apiKey);
		</cfscript>
    </cffunction>
    
    <cffunction name="CreateFromException" output="false" returntype="any">
    	<cfargument name="exception" required="yes" type="any">
        <cfscript>
			var loc = {};
			
			if (StructKeyExists(arguments.exception, "rootcause"))
			{
				loc.exception = Duplicate(arguments.exception.rootcause);
			}
			else
			{
				loc.exception = Duplicate(arguments.exception);
			}
			
			loc.data = "<b>Error:</b> ";
			
			if (StructKeyExists(loc.exception, "message") && Len(loc.exception.message) > 0)
			{
				loc.data &= loc.exception.message & "<br>";
				loc.message = loc.exception.message;
			}
			else
			{
				loc.data &= "No error message given<br>";
				loc.message = "No error message given";
			}
			
			if (StructKeyExists(loc.exception, "errNumber") && Len(loc.exception.errNumber) > 0)
			{
				loc.data &= "<b>Error Number:</b> " & loc.exception.errNumber & "<br>";
			}
			
			
			if (StructKeyExists(loc.exception, "extendedinfo") && Len(loc.exception.extendedInfo) > 0)
			{
				loc.data &= "<b>Extended Info:</b> " & loc.exception.extendedinfo & "<br>";
			}
			
			if (StructKeyExists(loc.exception, "tagContext") && ArrayLen(loc.exception.tagContext) > 0)
			{
				loc.data &= "<b>Location:</b><br>";
				
				for (loc.i = 1; loc.i <= ArrayLen(loc.exception.tagContext); loc.i++)
				{
					loc.data &= "Line #loc.exception.tagContext[loc.i].line# in #loc.exception.tagContext[loc.i].template#<br>";	
				}
			}
				
			if (StructKeyExists(application, "applicationname"))
			{
				loc.data &= "<b>Application Name:</b> " & application.applicationname & "<br>";	
			}
			
			loc.data &= "<b>URL:</b> #LCase(ListFirst(cgi.server_protocol, '/'))#://#cgi.server_name##cgi.script_name##cgi.path_info#";
			if (cgi.query_string != '')
			{
				loc.data &= "?#cgi.query_string#";
			}
			loc.data &= "<br>";
			
			if (Len(cgi.http_referer))
			{
				loc.data &= "<b>Referrer:</b> #cgi.http_referer#<br>";
			}

			loc.data &= "<b>Method:</b> #cgi.request_method#<br>";
			loc.data &= "<b>IP Address:</b> #cgi.remote_addr#<br>";
			loc.data &= "<b>User Agent:</b> #cgi.http_user_agent#<br>";
			
			return this.Create()
					   .Text(loc.message)
					   .Tags("error")
					   .GeoIP(cgi.remote_addr)
					   .Data(loc.data)
					   .DataType(constants.DataType.html);
			
		</cfscript>
    </cffunction>
</cfcomponent>