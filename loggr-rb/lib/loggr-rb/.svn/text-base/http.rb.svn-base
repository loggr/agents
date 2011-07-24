require 'net/http'

module Net
  class HTTP < Protocol
    # pasted first half of HTTP.request that writes the request to the server,
    # does not return HTTPResponse and does not take a block
    def request_async(req, body = nil)
      if proxy_user()
        unless use_ssl?
          req.proxy_basic_auth proxy_user(), proxy_pass()
        end
      end

      req.set_body_internal body
      begin_transport req
      req.exec @socket, @curr_http_version, edit_path(req.path)
    end

    # second half of HTTP.request that yields or returns the response
    def read_response(req, body = nil, &block)  # :yield: +response+
      begin
        res = HTTPResponse.read_new(@socket)
      end while res.kind_of?(HTTPContinue)
      res.reading_body(@socket, req.response_body_permitted?) {
        yield res if block_given?
      }
      end_transport req, res

      res
    end
  end
end