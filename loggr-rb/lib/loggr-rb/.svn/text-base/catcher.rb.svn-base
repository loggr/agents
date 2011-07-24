module Loggr
  class Catcher
    class << self
      def handle_with_controller(exception, controller=nil, request=nil)
        if Config.should_send_to_api?
		  Loggr::Events.create_from_exception(exception, request).post()
        end
      end
      
      def handle_with_rack(exception, environment, request) 
        if Config.should_send_to_api?
		  Loggr::Events.create_from_exception(exception, request).post()
        end
      end

      def handle(exception, request=nil)
        if Config.should_send_to_api?
          Loggr::Events.create_from_exception(exception, request).post()
        end
      end
    end
  end
end
