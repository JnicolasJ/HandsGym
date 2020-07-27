<?php
	include_once("dbConnection.php");
  $username  = $_POST["username"];
  $avatar =  $_POST["avatar"];
  $sql = "UPDATE usuarios SET avatar = '$avatar' WHERE username='$username'";
  $result = $con->query($sql);

		exit();

?>
