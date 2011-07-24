## Ruby Loggr Agent (loggr-rb)

This Gem/Plugin posts events and exception data to Loggr <http://loggr.net>. Includes a fluent interface to posting events.

Before installing this gem/plugin, be sure to sign up for a free Loggr account and log at <http://loggr.net>

## Installation
### Install in your Rails 3 app

1. Configure your Gemfile

	```
	gem 'loggr-rb'
	```

2. Run Bundler
	
	```
	bundle install
	```

3. Generate the Loggr config file (config/loggr.yml)

	*If you are using bundler:*

	```
	$ bundle exec loggr install YOUR-LOG-KEY YOUR-API-KEY
	```

	*If you are not using bundler:*

	```
	$ loggr install YOUR-LOG-KEY YOUR-API-KEY
	```

	You will find the LOG_KEY and API_KEY in the apps settings popup within Loggr.

4. Test everything is working

	*If you are using bundler:*

	```
	$ bundle exec loggr test
	```

	*If you are not using bundler:*

	```
	$ loggr test
	```

	You will see a test event in your Loggr log

### Install in your Rails 2 app

1. Install the loggr gem

	```
	$ gem install loggr-rb
	```

2. Configure your environment.rb file
	
	```
	config.gem 'loggr-rb'
	```

3. Generate the Loggr config file (config/loggr.yml)

	```
	$ loggr install YOUR-LOG-KEY YOUR-API-KEY
	```

	You will find the LOG_KEY and API_KEY in the apps settings popup within Loggr.

4. Delete the old Exceptional Plugin (if installed)

	```
	$ rm -rf vendor/plugins/loggr-rb
	```

5. Test everything is working

	```
	$ loggr test
	```

	You will see a test event in your Loggr log

### Install in your Rack app

1. Install the loggr gem

	```
	$ gem install loggr-rb
	```

2. Configure your config.ru file

	```
	require 'loggr-rb'
	use Rack::Loggr, LOG-KEY, API_KEY
	```

	You will find the LOG_KEY and API_KEY in the apps settings popup within Loggr.

3. Ensure Loggr for Rack is being loaded

	Check log/loggr.log log file

### Install in your Sinatra app

1. Install the loggr gem

	```
	$ gem install loggr-rb
	```

2. Configure your Sinatra app's environment

	```
	require 'loggr-rb'
	use Rack::Loggr, LOG-KEY, API_KEY
	```

	You will find the LOG_KEY and API_KEY in the apps settings popup within Loggr.

3. Ensure :raise_errors is set to true

	```
	set :raise_errors, true
	```

4. Ensure Loggr for Sinatra is being loaded

	Check log/loggr.log log file

### Install in your Ruby app

1. Install the loggr gem

	```
	$ gem install loggr-rb
	```

2. Generate the Loggr config file (config/loggr.yml)

	*If you are using bundler:*

	```
	$ bundle exec loggr install YOUR-LOG-KEY YOUR-API-KEY
	```

	*If you are not using bundler:*

	```
	$ loggr install YOUR-LOG-KEY YOUR-API-KEY
	```

	You will find the API_KEY in the apps settings screen within Exceptional.

3. Require the Loggr gem in your ruby code

	```
	require 'rubygems'
	require 'loggr-rb'
	```

4. Configure Loggr for your ruby app

	```
	Loggr::Config.load("config/loggr.yml")
	```

5. Use Loggr block to catch exceptions in your ruby code in different ways

	*If you are using bundler:*

	```
	$ bundle exec loggr test
	```

	*If you are not using bundler:*

	```
	$ loggr test
	```

	You will see a test event in your Loggr log

6. Use Loggr blocks to catch exceptions in your ruby code in different ways

	```
	Loggr.rescue do
	  # Exceptions inside this block will be reported to loggr.net
	end

	Loggr.rescue_and_reraise do
	  # Exceptions will be reported to loggr.net and then
	  # re-raised to your ruby code
	end
	```

## How To Use

Here's some sample code to get you started...

### Post events

Post a simple event

	Loggr::Events.create() \
		.text("This is a simple event") \
		.post()

A more complex example

	Loggr::Events.create() \
		.textf("More complex event: %1", counter) \
		.link("http://loggr.net") \
		.tags("tag1 tag2") \
		.source(current_user) \
		.value(35.50) \
		.dataf("<b>user-agent:</b> %s<br/><b>on:</b> %s", r.UA, today) \
		.datatype(Loggr::DataType.HTML) \
		.geo(40.1203, -76.2944) \
		.post()

### Exceptions

Easily post a Ruby exception

	Loggr::Events.create_from_exception(ex, request).post()

Or add your own details to the exception

	Loggr::Events.create_from_exception(ex, request) \
		.text("This was an error: $$") \
		.source("myapp") \
		.add_tags("critical") \
		.geo_ip("234.56.32.112") \
		.post()

## Extending the agent

It's pretty common to want to override the posting of the event to add things like default tags or other information. You can do that by providing a lambda to the create() call as shown here:

	```
	callback = lambda {|ev|
	  # use this callback to make sure source is set
	  # to the site's current user
      e.source(MyApp::context.current_user)
    }
	```

	Then use that lambda when posting an event

	```
	Loggr::Events.create(callback) \
      .text("deleted a commment") \
      .tags("comment delete") \
      .post()
	```

	It also works with exceptions.

	```
	Loggr::Events.create_from_exception(ex, request, callback).post()
	```

It may be helpful to provide a wrapper around Loggr::Events.create and Loggr::Events.create_with_exception that always includes your callback.

## More Information
For more information check out our docs site <http://docs.loggr.net>

