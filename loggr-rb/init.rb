require 'loggr-rb'

# If old plugin still installed then we don't want to install this one.
# In production environments we should continue to work as before, but in development/test we should
# advise how to correct the problem and exit
if (defined?(Loggr::VERSION::STRING) rescue nil) && %w(development test).include?(RAILS_ENV)
  message = %Q(
  ***********************************************************************
  You seem to still have an old version of the Loggr plugin installed.
  Remove it from /vendor/plugins and try again.
  ***********************************************************************
  )
  puts message
  exit -1
else
  begin
    if (Rails::VERSION::MAJOR < 3)    
      Loggr::Config.load(File.join(RAILS_ROOT, "/config/loggr.yml"))
      if Loggr::Config.should_send_to_api?
        Loggr.logger.info("Loading Loggr #{Loggr::VERSION} for #{Rails::VERSION::STRING}")      
        require File.join('loggr-rb', 'integration', 'rails')    
        require File.join('loggr-rb', 'integration', 'dj') if defined?(Delayed::Job)
      end
    else
      Loggr::Config.load(File.join(Rails.root, "/config/loggr.yml"))
      
      if Loggr::Config.should_send_to_api?
        Loggr.logger.info("Loading Loggr #{Loggr::VERSION} for #{Rails::VERSION::STRING}")      
        Rails.configuration.middleware.use "Rack::RailsLoggr"
        require File.join('loggr-rb', 'integration', 'dj') if defined?(Delayed::Job)
      end      
    end
  rescue => e
    STDERR.puts "Problem starting Loggr Plugin. Your app will run as normal. #{e.message}"
    Loggr.logger.error(e.message)
    Loggr.logger.error(e.backtrace)
  end
end
