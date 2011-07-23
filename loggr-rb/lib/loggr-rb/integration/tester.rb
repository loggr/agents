module Loggr
  module Integration
    class LoggrTestException <StandardError;
    end

    def self.test
	  Loggr::Events.create().text("Test event").tags("test").source("loggr-rb-#{Loggr::VERSION}").post()
	  puts "Test Event sent. Please login to http://loggr.net to see it!"
    end
  end
end


