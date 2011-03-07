## PHP Loggr Agent

Loggr agent to post events. Includes a fluent interface to posting events.

## Installation  

* Just copy the loggr.php file to your PHP application
* Reference the file using 'require_once'

## How To Use

Here's some sample code to get you started...

	<?php 
		require_once 'loggr.php';
		
		// creating class for using fluent syntax
		$loggr = new Loggr("myfirstlog", "db961642e48e48e4ab00ef60c90fa29e");
		
		// create a simple event
		$loggr->Events->Create()
			->Text("Simple fluent event")
			->Post();
		
		// more complex event
		$world = "World";
		$loggr->Events->Create()
			->TextF("hello%s", $world)
			->Tags("tag1 tag2 tag3")
			->Link("http://google.com")
			->Source("dave")
			->Data("foobar")
			->Value(3)
			->Geo(-14.456, 73.6879)
			->Post();
			
		// trace a variable
		$var = "TEST VAR";
		$loggr->Events->CreateFromVariable($var)
			->Text("Tracing TEST VAR")
			->Post();
		
		// trace an exception
		try {
			$error = 'Always throw this error';
			throw new Exception($error);
		} catch (Exception $e) {
			$loggr->Events->CreateFromException($e)
				->Text("Exception")
				->Post();
		}
		
		// alternatively you can use a non-fluent syntax
		$client = new LogClient("myfirstlog", "db961642e48e48e4ab00ef60c90fa29e");
		
		// create a simple event
		$ev = new Event();
		$ev->Text = "Simple non-fluent event";
		$client->Post($ev);	

	?>

## More Information

For more details, see http://loggr.net/docs


