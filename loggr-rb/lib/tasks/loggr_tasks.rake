namespace :loggr do
  desc 'Send a test event to Loggr.'
  task :test => :environment do
    unless Loggr::Config.api_key.blank?
      puts "Sending test exception to Loggr."
      require "loggr-rb/integration/tester"
      Loggr::Integration.test
      puts "Done."
    end
  end
end