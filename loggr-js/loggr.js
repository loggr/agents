/*!
* Loggr JavaScript Library v1
* http://api.loggr.net/1/loggr.js
*
* Copyright 2011, Dave Weaver
* http://loggr.net
*
* USAGE
* ---------------------------------------------------------------------
* this library can read and post events
* 
* The first thing you need to do is get a reference to a log using the logkey
* and apikey which can be found thru loggr.net
*
* var log = Loggr.logs.get("YOUR-LOGKEY", "YOUR-APIKEY");
* 
* READING EVENTS
* ---------------------------------------------------------------------
* There are 3 methods for reading events: get(), query() and getData().
*
* get() returns a single event given an event id:
* 
*     log.events.get(id, function (e) {
*         alert(e.text);
*     }
* 
* query() executes a Loggr Query Language (LQL) statement and returns the results:
* 
*     log.events.query("GET events TAKE 10 SORT created DESC", function (es) {
*         alert(es.length);
*     }
* 
* getData() returns a the data for a given event id:
* 
*     log.events.getData(id, function (data) {
*         alert(data);
*     }
* 
* POSTING EVENTS
* ---------------------------------------------------------------------
* With a log reference you can create and post events using a fluent event wrapper:
* 
*     log.events.createEvent().text("this is text").post()
* 
*     log.events.createEvent()
*         .text("my first event")
*         .link("http://loggr.net")
*         .tags("tag1 tag2")
*         .source("jsfiddle")
*         .value(35.50)
*         .data("<b>user-agent:</b> {0}<br/><b>on:</b> {1}", navigator.userAgent, new Date())
*         .dataType(Loggr.dataType.html)
*         .geo(40.1203, -76.2944)
*         .post();
* 
* The text, link, source and data methods can work like the C sprintf function:
* 
*    log.events.createEvent().text("the date is {0} and the time is {1}", new Date().toDateString(), new Date().toTimeString());
* 
* Those methods will also replace $$ with the previous value:
* 
*     log.events.createEvent().text("foo").text("$$bar") --> will output "foobar" for the text
* 
* The tags method can accept an array of string, a space-delimited string, or multiple string arguments:
* 
*     .tags(new Array("tag1", "tag2"), "tag3")
*     .tags("tag1 tag2", "tag3")
* 
* The text, tags and data methods also have corresponding append methods, which append values.
* addText(), addTags(), addData() take the same arguments that their setters do.
* 
* When settings data, you can specify if the data is to be displayed as HTML or Plain Text using 
* the .dataType() method (plaintext is default)
*
*     .dataType(Loggr.dataType.html) or .dataType(Loggr.dataType.plaintext)
*
*/

Loggr = {};

Loggr.logFactory = function () {
    this.get = function (logKey, apiKey) {
        return new Loggr.log(logKey, apiKey);
    };
};

Loggr.eventFactory = function (log) {
    this.log = log;
    var base = this;
    this.query = function (lql, callback) {
        Loggr.jsonp.fetch("http://api.loggr.net/1/logs/" + base.log.logKey + "/query?query=" + encodeURIComponent(lql) + "&fmt=jsonp&apikey=" + base.log.apiKey + "&callback=?", function (data) {
            if (callback) {
                callback(data);
            }
        });
    };
    this.get = function (id, callback) {
        Loggr.jsonp.fetch("http://api.loggr.net/1/logs/" + base.log.logKey + "/events/" + id + "?fmt=jsonp&apikey=" + base.log.apiKey + "&callback=?", function (data) {
            if (callback) {
                callback(data);
            }
        });
    };
    this.getData = function (id, callback) {
        Loggr.jsonp.fetch("http://api.loggr.net/1/logs/" + base.log.logKey + "/events/" + id + "/data?fmt=jsonp&apikey=" + base.log.apiKey + "&callback=?", function (data) {
            if (callback) {
                callback(data);
            }
        });
    };
	this.createEvent = function () {
		return new Loggr.fluentEvent(base.log);
	};
};

Loggr.logs = new Loggr.logFactory();

Loggr.log = function (logKey, apiKey) {
    this.logKey = logKey;
    this.apiKey = apiKey;
    this.events = new Loggr.eventFactory(this);
};

Loggr.fluentEvent = function (log) {
    this.event = new Loggr.event();
    this.log = log;

    this.clear = function () {
        this.event = new Loggr.event();
        return this;
    };

    this.post = function () {
        var qs = "";
        if (this.event.text != null) qs += "text=" + encodeURIComponent(this.event.text);
        if (this.event.link != null) qs += "&link=" + encodeURIComponent(this.event.link);
        if (this.event.source != null) qs += "&source=" + encodeURIComponent(this.event.source);
        if (this.event.tags != null) qs += "&tags=" + encodeURIComponent(this.event.tags.join(" "));
        if (this.event.value != null) qs += "&value=" + encodeURIComponent(this.event.value);
        var dataType = "";
        if (this.event.dataType == Loggr.dataType.html) dataType = "@html\r\n";
        if (this.event.data != null) qs += "&data=" + dataType + encodeURIComponent(this.event.data);
        if (this.event.lat != null && this.event.lon != null) qs += "&lat=" + encodeURIComponent(this.event.lat) + "&lon=" + encodeURIComponent(this.event.lon);
        Loggr.jsonp.fetch("http://post.loggr.net/1/logs/" + this.log.logKey + "/events?" + qs + "&fmt=jsonp&apikey=" + this.log.apiKey + "&callback=?", function (data) { });
        return this;
    };

    this.text = function (text) {
        var formatted = text.replace("$$", this.event.text);
        for (var i = 1; i < arguments.length; i++) formatted = formatted.replace("{" + (i - 1) + "}", arguments[i]);
        this.event.text = formatted;
        return this;
    };

    this.addText = function (text) {
        var formatted = text.replace("$$", this.event.text);
        for (var i = 1; i < arguments.length; i++) formatted = formatted.replace("{" + (i - 1) + "}", arguments[i]);
        this.event.text += formatted;
        return this;
    };

    this.link = function (link) {
        var formatted = link.replace("$$", this.event.link);
        for (var i = 1; i < arguments.length; i++) formatted = formatted.replace("{" + (i - 1) + "}", arguments[i]);
        this.event.link = formatted;
        return this;
    };

    this.source = function (source) {
        var formatted = source.replace("$$", this.event.source);
        for (var i = 1; i < arguments.length; i++) formatted = formatted.replace("{" + (i - 1) + "}", arguments[i]);
        this.event.source = formatted;
        return this;
    };

    this.tags = function (tags) {
        var newTags = new Array();
        //handle all arguments passed in
        for (var i = 0; i < arguments.length; i++) {
            var arg = arguments[i];
            // if the input is an array, add values to our new tag array
            if (arg.constructor.toString().indexOf("Array") != -1) {
                for (var j = 0; j < tags.length; j++)
                    this._addRange(newTags, this._tokenizeAndFormatTags(tags[j]));
            } else {
                this._addRange(newTags, this._tokenizeAndFormatTags(arg));
            }
        }
        this.event.tags = newTags;
        return this;
    };

    this.addTags = function (tags) {
        var newTags = new Array();
        //handle all arguments passed in
        for (var i = 0; i < arguments.length; i++) {
            var arg = arguments[i];
            // if the input is an array, add values to our new tag array
            if (arg.constructor.toString().indexOf("Array") != -1) {
                for (var j = 0; j < tags.length; j++)
                    this._addRange(newTags, this._tokenizeAndFormatTags(tags[j]));
            } else {
                this._addRange(newTags, this._tokenizeAndFormatTags(arg));
            }
        }
        this._addRange(this.event.tags, newTags);
        return this;
    };

    this.value = function (value) {
        this.event.value = value;
        return this;
    };

    this.valueClear = function () {
        this.event.value = null;
        return this;
    };

    this.data = function (data) {
        var formatted = data.replace("$$", this.event.data);
        for (var i = 1; i < arguments.length; i++) formatted = formatted.replace("{" + (i - 1) + "}", arguments[i]);
        this.event.data = formatted;
        return this;
    };

    this.addData = function (data) {
        var formatted = data.replace("$$", this.event.data);
        for (var i = 1; i < arguments.length; i++) formatted = formatted.replace("{" + (i - 1) + "}", arguments[i]);
        this.event.data += formatted;
        return this;
    };

    this.dataType = function (dataType) {
        this.event.dataType = dataType;
        return this;
    };

    this.geo = function (lat, lon) {
        this.event.lat = lat;
        this.event.lon = lon;
        return this;
    };

    this._addRange = function (toArray, fromArray) {
        for (var j = 0; j < fromArray.length; j++) {
            toArray.push(fromArray[j]);
        }
    };

    this._tokenizeAndFormatTags = function (tags) {
        var results = new Array();
        var tokens = tags.split(" ");
        for (var i = 0; i < tokens.length; i++) {
            var token = tokens[i].replace(" ", "");
            var regexp = new RegExp('[^a-zA-Z0-9\\-]', 'gi');
            token = token.replace(regexp, "");
            if (token.length > 0) results.push(token);
        }
        return results;
    };
};

Loggr.dataType = { "html": 0, "plaintext": 1 };

Loggr.event = function () {
	this.text = null;
	this.link = null;
	this.source = null;
	this.tags = null;
	this.value = null;
	this.data = null;
	this.dataType = Loggr.dataType.plaintext;
	this.lat = null;
	this.lon = null;
};

Loggr.jsonp = {
    callbackCounter: 0,

    fetch: function(url, callback) {
        var fn = 'JSONPCallback_' + this.callbackCounter++;
        window[fn] = this.evalJSONP(callback);
        url = url.replace('=?', '=' + fn);
        var scriptTag = document.createElement('SCRIPT');
        scriptTag.src = url;
        document.getElementsByTagName('HEAD')[0].appendChild(scriptTag);
    },

    evalJSONP: function(callback) {
        var base = this;
		return function(data) {
			if (data) {
				for (var i=0; i<data.length; i++) {
					if (data[i].created) {
						data[i].created = base.parseDate(data[i].created);
					}
				}
			}
			callback(data);
        }
    },
	
	parseDate: function (dt) {
		var match = dt.match(/^\/Date\((\S+)\)\/$/);
		return new Date(parseInt(match[1]));
	}
};

