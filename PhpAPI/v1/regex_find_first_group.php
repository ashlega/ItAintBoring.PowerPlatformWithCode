<?
		function myErrorHandler($errno, $errstr, $errfile, $errline) {
			header("HTTP/1.1 500 Internal Server Error");
			echo $errstr;//html for 500 page
		}

		// Set user-defined error handler function
		set_error_handler("myErrorHandler");
		
		$data = json_decode(file_get_contents('php://input'), true);
		$pattern = $data['pattern'];
		$value = $data['value'];
		preg_match($pattern, $value, $matches);
		if(count($matches) > 1)
		{
		    $result["data"] = $matches[1];
		}
		else {
    		$result["data"] = null;
		}
		$json_response = json_encode($result);
		
		header("Content-Type:application/json");
		echo $json_response;
?>