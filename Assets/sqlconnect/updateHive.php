<?php
	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}	
	$owner = $_POST["ownerId"];//?? how do I access
	$id = $_POST["id"];

	$isPublic = $_POST["isPublic"];
	$name = $_POST["name"];
	$health = $_POST["health"];
	$honeyStore = $_POST["honeyStore"];
	$queenProduction = $_POST["queenProduction"];
	$equipment = $_POST["equipment"];
	$profit = $_POST["profit"];

	if (id == -1) {//new 	
	//FIXME insert hives id???
		$inserthivequery = "INSERT INTO hives (ownerId, isPublic, name, health, honeyStore, queenProduction, equipment, profit) VALUES ('" . $ownerId . "', '" . $isPublic . "', '" . $name . "', '" . $health . "', '" . $honeyStore . ", ". $queenProduction .", ". $equipment .", ". $profit ."');";
		$db->query($inserthivequery) or die("2: Insert hive query failed"); //error code #4 = insert user query failed.
		
		$fetchIdquery = "Select MAX(id) from hives";
		$db->query($fetchIdquery) or die("3: fetch new hive id failed");
		echo("new Id:" + id);
	} else { //update existing hive
		$updatehivequery = "UPDATE beekeepers.hives
	SET ownerID=" . $ownerId . ", isPublic=" . $isPublic . ", name=" . $name . ", health=" . $health . ", honeyStore=" . $honeyStore . ", queenProduction=". $queenProduction .", equipment=". $equipment .", profit=". $profit .", id= ". $id .
	"WHERE id=". $id .";";
		$db->query($updatehivequery) or die("3: update hive query failed");
	}
	

	

	$db->close();
?>