<?php

	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}

	$owner = $_POST["ownerID"];

	$result = $db->query("SELECT * FROM hives WHERE ownerID=" . $owner . ";");
	while($row = $result->fetch_assoc()){
		echo $row["isPublic"]   . "\t" . $row["name"] . "\t" . $row["health"]  . "\t" . 
		     $row["honeyStore"] . "\t" . $row["queenProduction"] . "\t" . 
			 $row["equipment"]  . "\t" . $row["profit"] . "\t" . $row["id"] . "\n";
		}
	$db->close();
?>