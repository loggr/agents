require 'loggr-rb'
require 'rails'

module Loggr
  class Railtie < Rails::Railtie

    initializer "loggr.middleware" do |app|

      config_file = File.join(Rails.root, "/config/loggr.yml")
      Loggr::Config.load config_file if File.exist?(config_file)
      # On Heroku config is loaded via the ENV so no need to load it from the file

      if Loggr::Config.should_send_to_api?
        Loggr.logger.info("Loading Loggr #{Loggr::VERSION} for #{Rails::VERSION::STRING}")
        app.config.middleware.use "Rack::RailsLoggr"
      end
    end
  end
end
