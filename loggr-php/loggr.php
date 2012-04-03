<?php

class Loggr
{
	public $Events;
	
	function __construct($logKey, $apiKey)
	{
		$this->Events = new Events($logKey, $apiKey);
	}
	
	public function trapExceptions()
	{
		set_error_handler(array($this, "errorHandler"));
		set_exception_handler(array($this, "exceptionHandler"));		
	}
	
	public function errorHandler($code, $message, $file, $line)
	{
		if ($code == E_STRICT && $this->reportESTRICT === false) return;
					
		ob_start();
		var_dump(debug_backtrace());
		$stack = str_replace("\n", "<br>", ob_get_clean());
		
		$data = "@html\r\n";
		$data .= "<b>MESSAGE:</b> " . $message . "<br>";
		$data .= "<b>FILE:</b> " . $file . ", " . $line . "<br>";
		$data .= "<b>CODE:</b> " . $code . "<br>";
		$data .= "<br><b>STACK TRACE:</b> " . $stack;
		
		$this->Events->Create()
			->Text($message)
			->Tags("error")
			->Data($data)
			->Post();
	}
	
	public function exceptionHandler($exception)
	{
		ob_start();
		var_dump($exception->getTrace());
		$stack = str_replace("\n", "<br>", ob_get_clean());
		
		$data = "@html\r\n";
		$data .= "<b>MESSAGE:</b> " . $exception->getMessage() . "<br>";
		$data .= "<b>FILE:</b> " . $exception->getFile() . ", " . $exception->getLine() . "<br>";
		$data .= "<b>CODE:</b> " . get_class($exception) . "<br>";
		$data .= "<br><b>STACK TRACE:</b> " . $stack;
		
		$this->Events->Create()
			->Text($exception->getMessage())
			->Tags("error exception")
			->Data($data)
			->Post();
	}
}

class Events
{
	private $_logKey;
	private $_apiKey;

	function __construct($logKey, $apiKey)
	{
		$this->_logKey = $logKey;
		$this->_apiKey = $apiKey;
	}
	
	public function Create()
	{
		return new FluentEvent($this->_logKey, $this->_apiKey);
	}

	public function CreateFromException($exception)
	{
		ob_start();
		var_dump($exception->getTrace(), 5);
		$stack = str_replace("\t", "----", str_replace("\n", "<br>", ob_get_clean()));
		
		$data = "<b>MESSAGE:</b> " . $exception->getMessage() . "<br>";
		$data .= "<b>FILE:</b> " . $exception->getFile() . ", " . $exception->getLine() . "<br>";
		$data .= "<b>CODE:</b> " . get_class($exception) . "<br>";
		$data .= "<br><b>BACK TRACE:</b> " . backtrace();
	
		return $this->Create()
			->Text($exception->getMessage())
			->Tags("error " . get_class($exception))
			->Data($data)
			->DataType(DataType::html);
	}

	public function CreateFromVariable($var)
	{
		ob_start();
		var_dump($var);
		$trace = str_replace("\t", "----", str_replace("\n", "<br>", ob_get_clean()));
		
		$data = "<pre>" . $trace . "</pre>";
	
		return $this->Create()
			->Data($data)
			->DataType(DataType::html);
	}	
}

class FluentEvent
{
	public $Event;
	
	private $_logKey;
	private $_apiKey;

	function __construct($logKey, $apiKey)
	{
		$this->_logKey = $logKey;
		$this->_apiKey = $apiKey;
		$this->Event = new Event();
	}

	public function Post()
	{
		$client = new LogClient($this->_logKey, $this->_apiKey);
		$client->Post($this->Event);
		return $this;
	}

	public function Text($text)
	{
		$this->Event->Text = $this->AssignWithMacro($text, $this->Event->Text);
		return $this;
	}

	public function TextF()
	{
		$args = func_get_args();
	    return $this->Text(vsprintf(array_shift($args), array_values($args)));
	}
	
	public function AddText($text)
	{
		$this->Event->Text .= $this->AssignWithMacro($text, $this->Event->Text);
		return $this;
	}

	public function AddTextF()
	{
		$args = func_get_args();
	    return $this->AddText(vsprintf(array_shift($args), array_values($args)));
	}
	
	public function Source($source)
	{
		$this->Event->Source = $this->AssignWithMacro($source, $this->Event->Source);
		return $this;
	}

	public function SourceF()
	{
		$args = func_get_args();
	    return $this->Source(vsprintf(array_shift($args), array_values($args)));
	}
	
	public function User($user)
	{
		$this->Event->User = $this->AssignWithMacro($user, $this->Event->User);
		return $this;
	}

	public function UserF()
	{
		$args = func_get_args();
	    return $this->User(vsprintf(array_shift($args), array_values($args)));
	}

	public function Link($link)
	{
		$this->Event->Link = $this->AssignWithMacro($link, $this->Event->Link);
		return $this;
	}

	public function LinkF()
	{
		$args = func_get_args();
	    return $this->Link(vsprintf(array_shift($args), array_values($args)));
	}
	
	public function Data($data)
	{
		$this->Event->Data = $this->AssignWithMacro($data, $this->Event->Data);
		return $this;
	}

	public function DataF()
	{
		$args = func_get_args();
	    return $this->Data(vsprintf(array_shift($args), array_values($args)));
	}
	
	public function AddData($data)
	{
		$this->Event->Data .= $this->AssignWithMacro($data, $this->Event->Data);
		return $this;
	}

	public function AddDataF()
	{
		$args = func_get_args();
	    return $this->AddData(vsprintf(array_shift($args), array_values($args)));
	}
	
	public function Value($value)
	{
		$this->Event->Value = $value;
		return $this;
	}

	public function ValueClear()
	{
		$this->Event->Value = "";
		return $this;
	}

	public function Tags($tags)
	{
		$this->Event->Tags = $tags;
		return $this;
	}

	public function AddTags($tags)
	{
		$this->Event->Tags .= " " . $tags;
		return $this;
	}

	public function Geo($lat, $lon)
	{
		$this->Event->Latitude = $lat;
		$this->Event->Longitude = $lon;
		return $this;
	}

	public function DataType($datatype)
	{
		$this->Event->DataType = $datatype;
		return $this;
	}

	private function AssignWithMacro($input, $baseStr)
	{
		return str_replace("$$", $baseStr, $input);
	}
}

class DataType
{
    const html = 0;
    const plaintext = 1;
}

class Event
{
	public $Text;
	public $Source;
	public $User;
	public $Link;
	public $Data;
	public $Value;
	public $Tags;
	public $Latitude;
	public $Longitude;
	public $DataType = DataType::plaintext;
}

class LogClient
{
	private $_logKey;
	private $_apiKey;

	function __construct($logKey, $apiKey)
	{
		$this->_logKey = $logKey;
		$this->_apiKey = $apiKey;
	}

	public function Post($event)
	{
		// format data
		$qs = $this->CreateQuerystring($event);
		$data = "apikey=" . $this->_apiKey . "&" . $qs;
		
		// write without waiting for a response
		$fp = fsockopen('post.loggr.net', 80, $errno, $errstr, 30);	
		$out = "POST /1/logs/".$this->_logKey."/events HTTP/1.1\r\n";
		$out.= "Host: "."post.loggr.net"."\r\n";
		$out.= "Content-Type: application/x-www-form-urlencoded\r\n";
		$out.= "Content-Length: ".strlen($data)."\r\n";
		$out.= "Connection: Close\r\n\r\n";
		if (isset($data)) $out.= $data;
	
		fwrite($fp, $out);
		fclose($fp);
	}
	
	public function CreateQuerystring($event)
	{
		$res = "";
		$res .= "text=" . urlencode($event->Text);
		if (isset($event->Source)) $res .= "&source=" . urlencode($event->Source);
		if (isset($event->User)) $res .= "&user=" . urlencode($event->User);
		if (isset($event->Link)) $res .= "&link=" . urlencode($event->Link);
		if (isset($event->Value)) $res .= "&value=" . urlencode($event->Value);
		if (isset($event->Tags)) $res .= "&tags=" . urlencode($event->Tags);
		if (isset($event->Latitude)) $res .= "&lat=" . urlencode($event->Latitude);
		if (isset($event->Longitude)) $res .= "&lon=" . urlencode($event->Longitude);
		
		if (isset($event->Data))
		{
			if ($event->DataType == DataType::html)
				$res .= "&data=" . "@html\r\n" . urlencode($event->Data);
			else
				$res .= "&data=" . urlencode($event->Data);
		}
		
		return $res;
	}
}

function backtrace()
{
    $output = "<div style='text-align: left; font-family: monospace;'>\n";
    $backtrace = debug_backtrace();
    
    $defaults = array(
            'class' => '',
            'type' => '',
            'function' => '',
            'line' => '',
            'file' => ''
        );

    foreach ($backtrace as $bt) {
        $args = '';
        foreach ($bt['args'] as $a) {
            if (!empty($args)) {
                $args .= ', ';
            }
            switch (gettype($a)) {
            case 'integer':
            case 'double':
                $args .= $a;
                break;
            case 'string':
                $a = htmlspecialchars(substr($a, 0, 64)).((strlen($a) > 64) ? '...' : '');
                $args .= "\"$a\"";
                break;
            case 'array':
                $args .= 'Array('.count($a).')';
                break;
            case 'object':
                $args .= 'Object('.get_class($a).')';
                break;
            case 'resource':
                $args .= 'Resource('.strstr($a, '#').')';
                break;
            case 'boolean':
                $args .= $a ? 'True' : 'False';
                break;
            case 'NULL':
                $args .= 'Null';
                break;
            default:
                $args .= 'Unknown';
            }
        }
                
        
        $bt += $defaults;
        
        $output .= "<br />\n";
        $output .= "<b>file:</b> {$bt['line']} - {$bt['file']}<br />\n";
        $output .= "<b>call:</b> {$bt['class']}{$bt['type']}{$bt['function']}($args)<br />\n";
    }
    $output .= "</div>\n";
    return $output;
}
?>