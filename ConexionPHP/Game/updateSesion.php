<?php
	include_once("dbConnection.php");
  $username  = $_POST["username"];
  $sesion =  $_POST["sesion"];
  $sql = "UPDATE usuarios SET sessiones = $sesion WHERE username='$username'";
  $result = $con->query($sql);

		exit();

?>
