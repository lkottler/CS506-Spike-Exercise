<?php
	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}	
	$owner = $_POST["ownerId"];//?? how do I access


	$isPublic = $_POST["isPublic"];//
	$name = $_POST["name"];
	$health = $_POST["health"];
	$honeyStore = $_POST["honeyStore"];
	$queenProduction = $_POST["queenProduction"];
	$equipment = $_POST["equipment"];
	$profit = $_POST["profit"];

	$inserthivequery = "INSERT INTO hives (ownerId, isPublic, name, health, honeyStore, queenProduction, equipment, profit) VALUES ('" . $ownerId . "', '" . $isPublic . "', '" . $name . "', '" . $health . "', '" . $honeyStore . ", ". $queenProduction .", ". $equipment .", ". $profit ."');";
	$db->query($inserthivequery) or die("2: Insert hive query failed"); //error code #4 = insert user query failed.

	echo("0");

	$db->close();
?>