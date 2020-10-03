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

	$username = preg_replace("/[^A-Za-z0-9 ]/", '', $username);
	$address = preg_replace("/\r?\n|\r/", '', $address);
	$contact = preg_replace("/\r?\n|\r/", '', $contact);
	$equipment = preg_replace("/\r?\n|\r/", '', $equipment);

	$updateQuery = "UPDATE users SET address='" . $address . "', contact='" . $contact . "', equipment='" . $equipment . "' WHERE username='" . $username . "';";
	$db->query($updateQuery) or die("2: update user query failed");
	$db->close();
?>