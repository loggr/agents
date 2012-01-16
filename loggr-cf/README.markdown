## ColdFusion Loggr Agent

Loggr agent to post events. Includes a fluent interface to posting events.

## Installation  

* Copy the loggr directory to a location accessible by your application
* Reference the CFC's using the CreateObject function	

## How To Use

Here's some sample code to get you started...

	<!--- creating instance for using fluent syntax --->
	<cfset Loggr = CreateObject("component", "loggr.Loggr").init(LOG_KEY, API_KEY)>
	
	<!--- create a simple event --->
	<cfset Loggr.Events.Create()
				.Text("Simple fluent event")
				.Post()>
	
	<!--- more complex event --->
	<cfset world = "World">
	<cfset Loggr.Events.Create()
			 	.Text("Hello, {1}. Welcome to {2}!", world, "Loggr")
			 	.Tags("tag1 tag2 tag3")
			 	.Link("http://loggr.net")
			 	.Source("neal")
			 	.Data("foobar")
			 	.Value(3)
			 	.Geo(39.963701,-76.7274)
			 	.Post()>
	
	<!--- trace an exception --->
	<cftry>
		<cfthrow type="MyApp.Exception" message="Test Exception">
		
		<cfcatch type="any">
			<cfset Loggr.Events.CreateFromException(cfcatch).Text("Exception").Post()>
		</cfcatch>
	</cftry>
	
	<!--- alternatively you can use a non-fluent syntax --->
	<cfset LogClient = CreateObject("component", "loggr.LogClient").init(LOG_KEY, API_KEY)>
	
	<!--- create a simple event --->
	<cfset ev = CreateObject("component", "loggr.Event")>
	<cfset ev.Text = "Simple non-fluent event">
	<cfset LogClient.Post(ev)>

## More Information
For more information check out our docs site <http://docs.loggr.net>
