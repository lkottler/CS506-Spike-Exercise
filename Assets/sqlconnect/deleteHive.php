<?php
	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}	
	$owner = $_POST["ownerId"];
	$id = $_POST["id"];

	$deletehivequery = "DELETE FROM hives
						WHERE id = ". $id .";";
	
	$db->query($deletehivequery) or die("2: Delete hive query failed"); //error code #2 = insert user query failed.

	$db->close();
?>