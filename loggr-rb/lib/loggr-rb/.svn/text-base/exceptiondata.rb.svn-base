require 'digest/md5'

module Loggr
  class ExceptionData
    def self.format_exception(ex, request=nil)
	  res = ""

	  # basic info
      res = res + sprintf("<b>Exception</b>: %s<br />", ex.message)
      res = res + sprintf("<b>Type</b>: %s<br />", ex.class)
      res = res + sprintf("<b>Machine</b>: %s<br />", get_hostname)
      res = res + sprintf("<b>Language</b>: ruby %s<br />", language_version_string)
      res = res + "<br />"

	  # web details
	  if !request.nil?
	    res = res + sprintf("<b>Request URL</b>: %s<br />", (request.respond_to?(:url) ? request.url : "#{request.protocol}#{request.host}#{request.request_uri}"))
	    res = res + sprintf("<b>User</b>: %s<br />", get_username)
	    res = res + sprintf("<b>User host address</b>: %s<br />", (request.respond_to?(:remote_ip) ? request.remote_ip : request.ip))
	    res = res + sprintf("<b>Request Method</b>: %s<br />", request.request_method.to_s)
	    res = res + sprintf("<b>User Agent</b>: %s<br />", request.env['HTTP_USER_AGENT'] || '')
	    res = res + sprintf("<b>Referer</b>: %s<br />", request.env['HTTP_REFERER'] || '')
	    res = res + sprintf("<b>Script Name</b>: %s<br />", request.env['SCRIPT_NAME'] || '')
	    res = res + "<br />"
	  end

	  # stack
      res = res + "<b>Stack Trace</b><br />"
	  res = res + (ex.backtrace || []).join("<br/>")

	  return res
	end

    def self.get_hostname
      require 'socket' unless defined?(Socket)
      Socket.gethostname
    rescue
      'UNKNOWN'
    end

    def self.language_version_string
      "#{RUBY_VERSION rescue '?.?.?'} p#{RUBY_PATCHLEVEL rescue '???'} #{RUBY_RELEASE_DATE rescue '????-??-??'} #{RUBY_PLATFORM rescue '????'}"
    end

    def self.get_username
      ENV['LOGNAME'] || ENV['USER'] || ENV['USERNAME'] || ENV['APACHE_RUN_USER'] || 'UNKNOWN'
    end
  end
end