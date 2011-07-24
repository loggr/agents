begin
  class Delayed::Job
    def log_exception_with_loggr(e)
      Loggr.handle(e, "Delayed::Job #{self.name}")
      log_exception_without_loggr(e)
      Loggr.context.clear!
    end
    alias_method_chain :log_exception, :loggr
    Loggr.logger.info "DJ integration enabled"
  end
rescue
end