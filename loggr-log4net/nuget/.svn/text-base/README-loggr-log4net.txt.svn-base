Please note that in order to complete the installation of loggr-log4net you will have to do the following:

1. Configure log4net to use the provided appender, as shown below in web.config:

<configuration>
  ...
  <configSections>
    ...
    <section name="log4net" type="System.Configuration.IgnoreSectionHandler" />
  </configSections>
  ...
  <log4net>
    ...
    <appender name="LoggrAppender" type="Loggr.Log4Net.Appender,loggr-log4net"></appender>
	...
    <root>
      <level value="ALL" />
      <appender-ref ref="LoggrAppender" />
    </root>    
  </log4net>
  ...
</configuration>

2. Make sure log4net is using the web.config for its configuration, or adapt the settings accordingly
3. Make sure you enter the logKey and apiKey from your Loggr log in the appropriate config file.

NOTE: If you're using .NET 4, make sure you are using the full .NET 4 Profile
