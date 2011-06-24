## .Net Loggr Agent

Loggr agent to post events. Includes a fluent interface to posting events.

**NOTICE: This agent requires the full version of the .Net Framework, client profiles will not work**

## Installation  

* Drop the binary in the /bin folder of your app 
* Add lines to your web.config or app.config

Sample lines for your *.config file

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

## Extending the agent

It's pretty common to want to override the Post() method of the fluent event to add things like default tags or other information. It's pretty easy to extend this agent to do just that.

    class MyFluentEvent : FluentEventBase<MyFluentEvent>
    {
        public override MyFluentEvent Post(bool Async)
        {
            // make sure we append our server name as a tag
            this.AddTags(System.Environment.MachineName);
            
            // now just post it
            return base.Post(Async);
        }
    }
	
To use this new fluent event wrapper with the example above...

	Loggr.Events.Create<MyFluentEvent>()
		.Text("This is a simple event")
		.Post()


## More Information

For more details, see http://loggr.net/docs




