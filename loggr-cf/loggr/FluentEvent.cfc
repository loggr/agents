<!---
FluentEvent.cfc

Author: Neal Erickson
Email: neal@ink83.com

Copyright 2011, Neal Erickson
--->
<cfcomponent displayname="FluentEvent" extends="LoggrBase">
	
    <cfset variables.instance.logKey = "">
    <cfset variables.instance.apiKey = "">
    
	<cffunction name="init" output="false" returntype="any">
    	<cfargument name="logKey" required="yes" type="string">
        <cfargument name="apiKey" required="yes" type="string">        
        <cfscript>
		variables.instance.logKey = arguments.logKey;
        variables.instance.apiKey = arguments.apiKey;
		
        this.Event = CreateObject("component", "Event").init();
		
        return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Post" output="false" returntype="any">
    	<cfscript>
		var loc = {};
		loc.client = CreateObject("component", "LogClient").init(variables.instance.logKey, variables.instance.apiKey);
		loc.client.post(this.Event);
		
		return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Text" output="false" returntype="any">
    	<cfargument name="text" required="yes">
    	<cfscript>
			this.Event.Text = $AssignWithMacro(input=arguments.text, baseStr=this.Event.Text);
			this.Event.Text = $FormatWithArgs(field="text", format=this.Event.Text, args=arguments);
			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="AddText" output="false" returntype="any">
    	<cfargument name="text" required="yes">
    	<cfscript>
			arguments.text = $AssignWithMacro(input=arguments.text, baseStr=this.Event.Text);
			arguments.text = $FormatWithArgs(field="text", format=arguments.text, args=arguments);
			
			this.Event.Text &= arguments.text;
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Source" output="false" returntype="any">
    	<cfargument name="source" required="yes">
    	<cfscript>
			this.Event.Source = $AssignWithMacro(input=arguments.source, baseStr=this.Event.Source);
			this.Event.Source = $FormatWithArgs(field="source", format=this.Event.Source, args=arguments);
			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="User" output="false" returntype="any">
    	<cfargument name="user" required="yes">
    	<cfscript>
			this.Event.User = $AssignWithMacro(input=arguments.user, baseStr=this.Event.User);
			this.Event.User = $FormatWithArgs(field="user", format=this.Event.User, args=arguments);
			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Link" output="false" returntype="any">
    	<cfargument name="link" required="yes">
    	<cfscript>
			this.Event.Link = $AssignWithMacro(input=arguments.link, baseStr=this.Event.Link);
			this.Event.Link = $FormatWithArgs(field="link", format=this.Event.Link, args=arguments);
			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Data" output="false" returntype="any">
    	<cfargument name="data" required="yes">
    	<cfscript>
			this.Event.Data = $AssignWithMacro(input=arguments.data, baseStr=this.Event.Data);
			this.Event.Data = $FormatWithArgs(field="data", format=this.Event.Data, args=arguments);			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="AddData" output="false" returntype="any">
    	<cfargument name="data" required="yes">
    	<cfscript>
			arguments.data = $AssignWithMacro(input=arguments.data, baseStr=this.Event.Data);
			arguments.data = $FormatWithArgs(field="data", format=arguments.Data, args=arguments);	
			
			this.Event.Data &= arguments.data;
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Value" output="false" returntype="any">
    	<cfargument name="value" required="yes">
    	<cfscript>
			this.Event.Value = arguments.value;			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="ValueClear" output="false" returntype="any">
    	<cfscript>
			this.Event.Value = "";			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Geo" output="false">
    	<cfargument name="lat" required="yes">
    	<cfargument name="lng" required="yes">
    	<cfscript>
			this.Event.Geo = arguments.lat & "," & arguments.lng;			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="GeoIP" output="false" returntype="any">
    	<cfargument name="ip" required="yes" type="any">
    	<cfscript>
			this.Event.Geo = "ip:" & arguments.ip;			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="GeoClear" output="false">
    	<cfscript>
			this.Event.Geo = "";			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="Tags" output="false" returntype="any">
    	<cfargument name="tags" required="yes">
    	<cfscript>
			if (IsArray(arguments.tags))
			{
				this.Event.tags = ArrayToList(arguments.tags, " ");
			}
			else
			{
				this.Event.Tags = arguments.tags;
			}
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="AddTags" output="false" returntype="any">
    	<cfargument name="tags" required="yes">
    	<cfscript>
			if (IsArray(arguments.tags))
			{
				this.Event.tags &= ArrayToList(arguments.tags, " ");
			}
			else
			{
				this.Event.Tags &= " " & arguments.tags;
			}
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="TagsClear" output="false" returntype="any">
    	<cfscript>
			this.Event.Tags = "";			
			return this;
		</cfscript>
    </cffunction>
    
    <cffunction name="DataType" output="false" returntype="any">
    	<cfargument name="datatype" required="yes">
        <cfscript>	
			if (IsNumeric(arguments.datatype))
			{
				this.Event.DataType = arguments.datatype;
			}
			else
			{
				this.Event.DataType = $GetDataTypeValue(arguments.datatype);	
			}
			return this;
		</cfscript>
	</cffunction>
    
    <cffunction name="$GetDataTypeValue" output="false" access="private" returntype="any">
    	<cfargument name="type" required="no" default="html">
        <cfscript>
		switch (arguments.type)
		{
			case "plaintext":
				return constants.DataType.plaintext;
			case "html":
				return constants.DataType.html;
			default:
				$throw(type="Loggr.FluentEvent.invalidDataType", message="Invalid DataType: " & arguments.type);
		}		
        </cfscript>
    </cffunction>
    
    <cffunction name="$AssignWithMacro" output="false" access="private" returntype="any">
    	<cfargument name="input" required="yes" type="string">
        <cfargument name="baseStr" required="yes" type="string">
 		<cfscript>
		return Replace(arguments.input, "$$", arguments.baseStr, "all");
		</cfscript>
	</cffunction>
    
    <cffunction name="$FormatWithArgs" output="false" access="private" returntype="any">
    	<cfargument name="field" required="yes" type="string">
        <cfargument name="format" required="yes" type="string">
        <cfargument name="args" required="yes" type="struct">
    	<cfscript>
			var loc = {};
			
			if (StructKeyExists(arguments.args, arguments.field))
			{
				StructDelete(arguments.args, arguments.field);
			}
			
			for (loc.i in arguments.args)
			{
				arguments.format = Replace(arguments.format, "{" & (loc.i-1) & "}", arguments.args[ loc.i ], "all");	
			}
			return arguments.format;
		</cfscript>
    </cffunction>
    
    
</cfcomponent>