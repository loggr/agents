package net.loggr;

import java.net.URL;
import java.util.concurrent.Callable;

public class AsyncWebRequest implements Callable<AsyncWebResponse> {
	private URL url;

    public AsyncWebRequest(URL url) {
        this.url = url;
    }

    @Override
    public AsyncWebResponse call() throws Exception {
        return new AsyncWebResponse(url.openStream());
    }
}
