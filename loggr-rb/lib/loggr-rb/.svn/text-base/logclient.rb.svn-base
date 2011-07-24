require 'loggr-rb/http'
require 'uri'

module Loggr
  class LogClient
    def post(e, async=true)
      logkey = ::Loggr::Config.log_key
	  if async == true
	    call_remote_async("post.loggr.net", "/1/logs/#{logkey}/events", create_params(e))
	  else
	    call_remote("post.loggr.net", "/1/logs/#{logkey}/events", create_params(e))
      end
    end

	def create_params(e)
      apikey = ::Loggr::Config.api_key
	  params = {"apikey" => apikey, "text" => e.text}
	  params = params.merge({"link" => e.link}) if !e.link.nil?
	  params = params.merge({"tags" => e.tags}) if !e.tags.nil?
	  params = params.merge({"source" => e.source}) if !e.source.nil?
	  params = params.merge({"geo" => e.geo}) if !e.geo.nil?
	  params = params.merge({"value" => e.value}) if !e.value.nil?
	  if e.datatype == DataType::HTML
	    params = params.merge({"data" => sprintf("@html\r\n%s", e.data)}) if !e.data.nil?
	  else
	    params = params.merge({"data" => e.data}) if !e.data.nil?
	  end
	  return params
	end

    def call_remote(host, path, params)
      http = Net::HTTP.new(host)
      req = Net::HTTP::Get.new(path)
      data = params.collect { |k, v| "#{k}=#{v}&" }.join
      http.start
      begin
        http.request_async(req, data)
		res = http.read_response(req)
      rescue Exception => e
        Loggr.logger.error('Problem notifying Loggr about the event')
        Loggr.logger.error(e)
      ensure
        http.finish
      end
      res.value # raise if error
    end

    def call_remote_async(host, path, params)
      http = Net::HTTP.new(host)
      req = Net::HTTP::Get.new(path)
      data = params.collect { |k, v| "#{k}=#{v}&" }.join
      http.start
      begin
        http.request_async(req, data)
      rescue Exception => e
        Loggr.logger.error('Problem notifying Loggr about the event')
        Loggr.logger.error(e)
      ensure
        http.finish
      end
    end
  end
end