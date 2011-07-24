if defined? Sinatra::Request
  error do
    Loggr::Catcher.handle_with_rack(request.env['sinatra.error'], request.env, request)
    raise request.env['sinatra.error']
  end
end