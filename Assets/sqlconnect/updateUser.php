<?php
	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}

	$username = $_POST["username"];
	$address = $_POST["address"];
	$contact = $_POST["contact"];
	$equipment = $_POST["equipment"];

	$updateQuery = "UPDATE users SET address='" . $address . "', contact='" . $contact . "', equipment='" . $equipment . "'WHERE username='" . $username . "';";
	$db->query($updateQuery) or die("2: update user query failed");
	$db->close();
?>