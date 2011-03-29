## .Net Loggr Agent

Loggr agent to post events. Includes a fluent interface to posting events.

## Installation  

* Drop the binary in the /bin folder of your app 
* Add a section to your web.config or app.config

Sample lines for you *.config file
	<configuration>
	  <configSections>
		<sectionGroup name="loggr">
		  <section name="log" type="System.Configuration.NameValueSectionHandler"/>
		</sectionGroup>
	  </configSections>
	  
	  ...
	  
	  <loggr>
		<log>
		  <add key="logKey" value="#####"/>
		  <add key="apiKey" value="#####"/>
		</log>
	  </loggr>
	  
	  ...
	  
	</configuration>

## How To Use

Here's some sample code to get you started...

Post a .Net exception

	Loggr.Events.CreateFromException(ex)
		.Text("This was an error: $$")
		.Source("myapp")
		.AddTag("critical")
		.Post()

Post a simple event

	Loggr.Events.Create()
		.Text("This is a simple event")
		.Post()


## More Information

For more details, see http://loggr.net/docs




