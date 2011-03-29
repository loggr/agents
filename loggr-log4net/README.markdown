## Log4Net Loggr Agent

A log4net "appender" for logging to Loggr. You should be able to drop in the binaries and configure the appender and log4net will automatically log to your Loggr log.

## Installation  

* Drop the binaries in the /bin folder of your app 
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

* Config log4net to use the Appender
	<appender name="LoggrAppender" type="Loggr.Log4Net.Appender,loggr-log4net"></appender>

## How To Use

When properly configured you shouldn't have to change your logging code. This Appender installs the loggr-dotnet agent, so you can add a reference to that and make better use of the Loggr posting API.


## More Information

For more details, see http://loggr.net/docs




