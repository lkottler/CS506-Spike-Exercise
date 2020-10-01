<?php
	$db = mysqli_connect('34.67.175.21', 'root', 'virtualcommencement', 'beekeepers');

	if (mysqli_connect_errno())
	{
		echo "1: Connection failed"; //error code #1 = failed to connect to db
		exit();
	}

	$userQuery = $db->query("SELECT * FROM users ORDER by id");
	while ($row = $userQuery->fetch_array(MYSQLI_ASSOC)){
		echo $row["id"] . "\t" . $row["username"] . "\t" . $row["contact"] . "\t" . $row["address"] . "\n";
	}

	$db->close();
?>