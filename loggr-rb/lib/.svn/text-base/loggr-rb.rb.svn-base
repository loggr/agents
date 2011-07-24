$:.unshift File.dirname(__FILE__)

require 'loggr-rb/catcher'
require 'loggr-rb/logfactory'
require 'loggr-rb/config'
require 'loggr-rb/exceptiondata'
require 'loggr-rb/integration/rack'    
require 'loggr-rb/integration/rack_rails'
require 'loggr-rb/logclient'
require 'loggr-rb/events'

require 'loggr-rb/version' if !defined?(Loggr::VERSION)
require 'loggr-rb/railtie' if defined?(Rails::Railtie)

module Loggr
  PROTOCOL_VERSION = 5
  CLIENT_NAME = 'loggr-rb-gem'
  ENVIRONMENT_FILTER = []

  def self.logger
    ::Loggr::LogFactory.logger
  end

  def self.configure(api_key)
    Loggr::Config.api_key = api_key
  end

  def self.handle(exception, request=nil)
    Loggr::Catcher.handle(exception, request)
  end
  
  def self.rescue(name=nil, context=nil, &block)
    begin
      self.context(context) unless context.nil?
      block.call
    rescue Exception => e
      Loggr::Catcher.handle(e,name)
    ensure
      self.clear!
    end
  end

  def self.rescue_and_reraise(name=nil, context=nil, &block)
    begin
      self.context(context) unless context.nil?
      block.call
    rescue Exception => e
      Loggr::Catcher.handle(e,name)
      raise(e)
    ensure
      self.clear!      
    end
  end

  def self.clear!
    Thread.current[:loggr_context] = nil
  end

  def self.context(hash = {})
    Thread.current[:loggr_context] ||= {}
    Thread.current[:loggr_context].merge!(hash)
    self
  end
end