<?php
	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}	
	$ownerID = $_POST["ownerID"];//?? how do I access
	$id = $_POST["id"];
	$isPublic = $_POST["isPublic"];
	$name = $_POST["name"];
	$health = $_POST["health"];
	$honeyStore = $_POST["honeyStore"];
	$queenProduction = $_POST["queenProduction"];
	$equipment = $_POST["equipment"];
	$profit = $_POST["profit"];

	if ($id == -1) {//new 	
	//FIXME insert hives id???
		$inserthivequery = "INSERT INTO hives (ownerID, isPublic, name, health, honeyStore, queenProduction, equipment, profit) VALUES (" . $ownerID . ", " . $isPublic . ", '" . $name . "', " . $health . ", " . $honeyStore . ", ". $queenProduction .", '". $equipment ."', ". $profit .");";
		$db->query($inserthivequery) or die("2: Insert hive query failed"); //error code #2 = insert hive query failed.
		echo("last inserted record has id: " . mysqli_insert_id());
	} else { //update existing hive
		$updatehivequery = "UPDATE hives
	SET ownerID=" . $ownerId . ", isPublic=" . $isPublic . ", name=" . $name . ", health=" . $health . ", honeyStore=" . $honeyStore . ", queenProduction=". $queenProduction .", equipment=". $equipment .", profit=". $profit .", id= ". $id . " WHERE id=". $id .";";
		$db->query($updatehivequery) or die("4: update hive query failed");
	}
	
	$db->close();
?>