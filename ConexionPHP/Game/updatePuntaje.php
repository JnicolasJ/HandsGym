<?php
	include_once("dbConnection.php");
  $username  = $_POST["username"];
  $puntaje =  $_POST["puntaje"];
  $sql = "UPDATE usuarios SET puntaje = $puntaje WHERE username='$username'";
  $result = $con->query($sql);

		exit();

?>
