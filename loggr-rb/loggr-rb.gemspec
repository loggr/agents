# -*- encoding: utf-8 -*-
require File.expand_path('../lib/loggr-rb/version', __FILE__)

Gem::Specification.new do |gem|
  gem.name = %q{loggr-rb}
  gem.version = Loggr::VERSION
  gem.authors = ["Loggr.net"]
  gem.summary = %q{ loggr.net is a hosted service for logging events in your Ruby/Rails/Rack apps }
  gem.description = %q{loggr-rb is the Ruby gem for communicating with http://loggr.net (hosted logging service).}
  gem.email = %q{info@loggr.net}
  gem.files =  Dir['lib/**/*'] + Dir['rails/**/*'] + Dir['tasks/**/*'] + Dir['*.rb'] + ["loggr-rb.gemspec"]
  gem.homepage = %q{http://loggr.net/}
  gem.require_paths = ["lib"]
  gem.executables << 'loggr'
  gem.requirements << ""
  gem.add_dependency 'rack'
end
