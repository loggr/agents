## Javascript Loggr Agent

Loggr agent to post and read events. Includes a fluent interface for posting events.

## Installation  

* Link to the loggr.min.js file from your web page 

	<script type="text/javascript" src="http://api.loggr.net/1/loggr.min.js"></script>

## How To Use

This library can read and post events

The first thing you need to do is get a reference to a log using the logkey
and apikey which can be found thru loggr.net

    var log = Loggr.logs.get("YOUR-LOGKEY", "YOUR-APIKEY");

### Reading Events
There are 3 methods for reading events: get(), query() and getData().
You can try this out here: [http://jsfiddle.net/xkNjs/](http://jsfiddle.net/ez4aH/)

get() returns a single event given an event id:

    log.events.get(id, function (e) {
        alert(e.text);
    }

query() executes a Loggr Query Language (LQL) statement and returns the results:

    log.events.query("GET events TAKE 10 SORT created DESC", function (es) {
        alert(es.length);
    }

getData() returns a the data for a given event id:

    log.events.getData(id, function (data) {
        alert(data);
    }

### Posting Events
With a log reference you can create and post events using a fluent event wrapper.
You can try this out here: [http://jsfiddle.net/Ssjwq/](http://jsfiddle.net/3AqTP/)

    log.events.createEvent().text("this is text").post()

    log.events.createEvent()
        .text("my first event")
        .link("http://loggr.net")
        .tags("tag1 tag2")
        .source("jsfiddle")
        .value(35.50)
        .data("<b>user-agent:</b> {0}<br/><b>on:</b> {1}", navigator.userAgent, new Date())
        .dataType(Loggr.dataType.html)
        .geo(40.1203, -76.2944)
        .post();

The text, link, source and data methods can work like the C sprintf function:

    log.events.createEvent().text("the date is {0} and the time is {1}", new Date().toDateString(), new Date().toTimeString());

Those methods will also replace $$ with the previous value:

    log.events.createEvent().text("foo").text("$$bar") --> will output "foobar" for the text

The tags method can accept an array of string, a space-delimited string, or multiple string arguments:

    .tags(new Array("tag1", "tag2"), "tag3")
    .tags("tag1 tag2", "tag3")

The text, tags and data methods also have corresponding append methods, which append values.
addText(), addTags(), addData() take the same arguments that their setters do.

When settings data, you can specify if the data is to be displayed as HTML or Plain Text using 
the .dataType() method (plaintext is default)

    .dataType(Loggr.dataType.html) or .dataType(Loggr.dataType.plaintext)

## More Information

For more details, see http://docs.loggr.net




