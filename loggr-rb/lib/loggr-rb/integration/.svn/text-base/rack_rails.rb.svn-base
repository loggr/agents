require 'rubygems'
require 'rack'

module Rack  
  class RailsLoggr    

    def initialize(app)
      @app = app
    end    
    
    def call(env)
      begin
        body = @app.call(env)
      rescue Exception => e
        ::Loggr::Catcher.handle_with_controller(e,env['action_controller.instance'], Rack::Request.new(env))
        raise
      end

      if env['rack.exception']
        ::Loggr::Catcher.handle_with_controller(env['rack.exception'],env['action_controller.instance'], Rack::Request.new(env))
      end

      body
    end      
  end
end
