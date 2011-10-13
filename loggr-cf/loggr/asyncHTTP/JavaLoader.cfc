<!--- Document Information -----------------------------------------------------

Title:      JavaLoader.cfc

Author:     Mark Mandel
Email:      mark@compoundtheory.com

Website:    http://www.compoundtheory.com

Purpose:    Utlitity class for loading Java Classes

Usage:      

Modification Log:

Name			Date			Description
================================================================================
Mark Mandel		08/05/2006		Created
Mark Mandel		22/06/2006		Added verification that the path exists

------------------------------------------------------------------------------->
<cfcomponent name="JavaLoader" hint="Loads External Java Classes, while providing access to ColdFusion classes">
	
<cfscript>
	instance = StructNew();
</cfscript>
	
<!------------------------------------------- PUBLIC ------------------------------------------->
<cffunction name="init" hint="Constructor" access="public" returntype="JavaLoader" output="false">
	<cfargument name="loadPaths" hint="An array of diretories, or paths to .jar files to load into the classloader" type="array" default="#ArrayNew(1)#" required="no">
	<cfargument name="parentClassLoader" hint="(Expert use only) The parent java.lang.ClassLoader to set when creating the URLClassLoader" type="any" default="#getClass().getClassLoader()#" required="false">
	
	<cfscript>
		var iterator = arguments.loadPaths.iterator();
		var Array = createObject("java", "java.lang.reflect.Array");
		var Class = createObject("java", "java.lang.Class");
		var URLs = Array.newInstance(Class.forName("java.net.URL"), JavaCast("int", ArrayLen(arguments.loadPaths)));
		var file = 0;
		var classLoader = 0;
		var counter = 0;
		
		while(iterator.hasNext())
		{
			file = createObject("java", "java.io.File").init(iterator.next());
			if(NOT file.exists())
			{
				throw("PathNotFoundException", "The path you have specified could not be found", file.getAbsolutePath() & " does not exist");
			}
			Array.set(URLs, JavaCast("int", counter), file.toURL());
			counter = counter + 1;			
		}

		//alternate approach to getting the system class loader
		//var Thread = createObject("java", "java.lang.Thread");
		//classLoader = createObject("java", "java.net.URLClassLoader").init(URLs, Thread.currentThread().getContextClassLoader());

		//pass in the system loader		
		classLoader = createObject("java", "java.net.URLClassLoader").init(URLs, arguments.parentClassLoader);
		setURLClassLoader(classLoader);

		return this;
	</cfscript>
</cffunction>

<cffunction name="create" hint="Retrieves a reference to the java class. To create a instance, you must run init() on this object" access="public" returntype="any" output="false">
	<cfargument name="className" hint="The name of the class to create" type="string" required="Yes">
	<cfscript>
		var class = getURLClassLoader().loadClass(arguments.className);
		
		return createObject("java", "coldfusion.runtime.java.JavaProxy").init(class);
	</cfscript>
</cffunction>

<cffunction name="getURLClassLoader" hint="Returns the java.net.URLClassLoader in case you need access to it" access="public" returntype="any" output="false">
	<cfreturn instance.ClassLoader />
</cffunction>

<cffunction name="getVersion" hint="Retrieves the version of the loader you are using" access="public" returntype="string" output="false">
	<cfreturn "0.2">
</cffunction>

<!------------------------------------------- PACKAGE ------------------------------------------->

<!------------------------------------------- PRIVATE ------------------------------------------->	

<cffunction name="setURLClassLoader" access="private" returntype="void" output="false">
	<cfargument name="ClassLoader" type="any" required="true">
	<cfset instance.ClassLoader = arguments.ClassLoader />
</cffunction>	

<cffunction name="throw" access="private" hint="Throws an Exception" output="false">
	<cfargument name="type" hint="The type of exception" type="string" required="Yes">
	<cfargument name="message" hint="The message to accompany the exception" type="string" required="Yes">
	<cfargument name="detail" type="string" hint="The detail message for the exception" required="No" default="">
		<cfthrow type="#arguments.type#" message="#arguments.message#" detail="#arguments.detail#">
</cffunction>

</cfcomponent>