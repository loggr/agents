<!--- Document Information -----------------------------------------------------

Title:      asyncHTTP.cfc

Author:     Mark Mandel
Email:      mark@compoundtheory.com

Website:    http://www.compoundtheory.com

Purpose:    Asynchronously calls a cfml page

Usage:

Modification Log:

Name			Date			Description
================================================================================
Mark Mandel		14/07/2006		Created

------------------------------------------------------------------------------->
<cfcomponent name="AsyncHTTP" hint="Asyncronously calls a cfml page">

<cfscript>
	instance = StructNew();
</cfscript>

<!------------------------------------------- PUBLIC ------------------------------------------->

<cffunction name="init" hint="Constructor" access="public" returntype="AsyncHTTP" output="false">
	<cfscript>
		setJavaLoader(initJavaLoader());
		setAsyncHTTP(getJavaLoader().create("com.compoundtheory.asyncHTTP.AsyncHTTP").init());

		return this;
	</cfscript>
</cffunction>

<cffunction name="get" hint="Does a asyncronous GET HTTP request" access="public" returntype="void" output="false">
	<cfargument name="url" hint="The url to do a get request on, eg. http://server.com/file/dog.cfm?query=1234" type="string" required="Yes">
	<cfscript>
		var URLObject = createObject("java", "java.net.URL").init(arguments.url);

		getAsyncHTTP().get(URLObject);
	</cfscript>
</cffunction>

<cffunction name="post" hint="Does a asyncronous POST HTTP request" access="public" returntype="void" output="false">
	<cfargument name="url" hint="The url to do a get request on, eg. http://server.com/file/dog.cfm?query=1234" type="string" required="Yes">
	<cfargument name="formData" hint="Structure of form, value pairs" type="any" required="no" default="#StructNew()#">
	<cfscript>
		var URLObject = createObject("java", "java.net.URL").init(arguments.url);
		var HashtableObject = CreateObject("java","java.util.Hashtable");
		
		cleanFormData(arguments.formData);
		
		HashtableObject.init(formData);
		
		getAsyncHTTP().post(URLObject, HashtableObject);
	</cfscript>
</cffunction>

<cffunction name="getVersion" hint="Returns the version" access="public" returntype="string" output="false">
	<cfreturn "0.2">
</cffunction>
<!------------------------------------------- PACKAGE ------------------------------------------->

<!------------------------------------------- PRIVATE ------------------------------------------->

<cffunction name="initJavaLoader" hint="Initialises loading in JAR files" access="private" returntype="JavaLoader" output="false">
	<cfscript>
		var path = getDirectoryFromPath(getMetaData(this).path) & "/lib";
		var qJars = 0;

		var jarArray = ArrayNew(1);
		var aJars = ArrayNew(1);
		var libName = 0;
	</cfscript>

	<cfdirectory action="list" name="qJars" directory="#path#" filter="*.jar" sort="name desc"/>

	<cfloop query="qJars">
		<cfscript>
			libName = ListGetAt(name, 1, "-");
			//let's not use the lib's that have the same name, but a lower datestamp
			if(NOT jarArray.contains(libName))
			{
				ArrayAppend(aJars, path & "/" & name);
				ArrayAppend(jarArray, libName);
			}
		</cfscript>
	</cfloop>

	<cfscript>
		return createObject("component", "JavaLoader").init(aJars);
	</cfscript>
</cffunction>

<cffunction name="cleanFormData" hint="Casts all form data to strings" access="private" returntype="void" output="false">
	<cfargument name="formData" hint="The form data to clean" type="struct" required="Yes">
	<cfscript>
		var iterator = StructKeyArray(arguments.formData).iterator();
		var key = 0;

		while(iterator.hasNext())
		{
			key = iterator.next();
			arguments.formData[key] = JavaCast("string", arguments.formData[key]);
		}
	</cfscript>
</cffunction>

<cffunction name="getJavaLoader" access="private" returntype="JavaLoader" output="false">
	<cfreturn variables.JavaLoader />
</cffunction>

<cffunction name="setJavaLoader" access="private" returntype="void" output="false">
	<cfargument name="JavaLoader" type="JavaLoader" required="true">
	<cfset variables.JavaLoader = arguments.JavaLoader />
</cffunction>

<cffunction name="getAsyncHTTP" access="private" returntype="any" output="false">
	<cfreturn variables.AsyncHTTP />
</cffunction>

<cffunction name="setAsyncHTTP" access="private" returntype="void" output="false">
	<cfargument name="AsyncHTTP" type="any" required="true">
	<cfset variables.AsyncHTTP = arguments.AsyncHTTP />
</cffunction>

</cfcomponent>