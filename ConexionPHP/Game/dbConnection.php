<?php
  $host   = "mysql:host=localhost;dbname=handsgym";
	$user   = "unity";
	$pass   = "unity";
	$option = array(PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8");
	try{
		$con = new PDO($host,$user, $pass, $option);
		$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	}catch(PDOException $e){ echo $e.getMessage(); }
?>
