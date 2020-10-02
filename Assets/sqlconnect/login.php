<?php
	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}

	$username = $_POST["name"];
	$password = $_POST["password"];

	$namecheckquery = "SELECT username, salt, hash, address, contact FROM users WHERE username='" . $username . "';";
	$namecheck = $db->query($namecheckquery) or die("2: Name check query failed"); //error code #2 = name check query failed
	if (mysqli_num_rows($namecheck) != 1)
	{
		echo "5: no user with name, or more than one."; //error code #5 = username either 0 or more than one times in db
		exit();
	}

	//get login info from query
	$existinginfo = mysqli_fetch_assoc($namecheck);
	$salt = $existinginfo["salt"];
	$hash = $existinginfo["hash"];

	$loginhash = crypt($password, $salt);
	if ($hash != $loginhash){
		echo "6: incorrect password"; //error code #6 = wrong password
		exit();
	}

	echo "0\t" . $existinginfo["address"] . "\t" . $existinginfo["contact"];

?>