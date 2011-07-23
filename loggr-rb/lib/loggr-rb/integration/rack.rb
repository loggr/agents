require 'rubygems'
require 'rack'

module Rack  
  class Loggr    

    def initialize(app, api_key = nil)
      @app = app
      if api_key.nil?
        loggr_config = "config/loggr.yml"
        ::Loggr::Config.load(loggr_config)
      else
        ::Loggr.configure(api_key)
        ::Loggr::Config.enabled = true
        ::Loggr.logger.info "Enabling Loggr for Rack"
      end
    end    
    
    def call(env)
      begin
        status, headers, body =  @app.call(env)
      rescue Exception => e
        ::Loggr::Catcher.handle_with_rack(e,env, Rack::Request.new(env))
        raise(e)
      end
    end
  end
end
