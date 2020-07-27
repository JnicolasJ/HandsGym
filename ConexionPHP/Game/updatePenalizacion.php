<?php
	include_once("dbConnection.php");
  $username  = $_POST["username"];
  $penalizacion =  $_POST["penalizacion"];
  $sql = "UPDATE usuarios SET penalizaciones = $penalizacion WHERE username='$username'";
  $result = $con->query($sql);

		exit();

?>
