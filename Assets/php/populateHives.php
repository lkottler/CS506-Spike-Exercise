<?php

	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}

	$owner = $_POST["ownerID"];

	$hiveQuery = $db->query("SELECT * FROM hives WHERE ownerID='" . $owner ."';");
	$row = $hiveQuery->fetch_array(MYSQLI_ASSOC)
	echo $row["isPublic"]   . "\t" . $row["name"] . "\t" . $row["health"]  . "\t" . 
	     $row["honeyStore"] . "\t" . $row["queenProduction"] . "\t" . 
		 $row["equipment"]  . "\t" . $row["profit"] . "\t" . $row["id"] . "\n";

	$db->close();
?>