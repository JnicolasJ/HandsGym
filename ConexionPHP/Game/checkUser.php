<?php
	include_once("dbConnection.php");
  $username  = $_POST["username"];
  $password = hash("sha256",$_POST["password"]);
  $sql = "SELECT * FROM usuarios WHERE username='$username' AND password='$password'";
  $result = $con->query($sql);
	while( $row = $result->fetch(PDO::FETCH_ASSOC) ) {
    #$books[] = $row; // appends each row to the array
		$data = array('done' => true, 'mensaje' => "Bienvenido $username", 'db_usuario' => $row['username'],
		 'db_email' => $row['email'],
		   'db_avatar'=> $row['avatar'],
			  'db_x'=> $row['x'],
				'db_y'=> $row['y'],
				'db_z'=> $row['z'],
				'db_puntaje' => $row['puntaje'],
				'db_ranking' => $row['ranking'],
				'db_penalizacion' => $row['penalizaciones'],
				'db_sessiones' => $row['sessiones'],
				'db_instructor'=> $row['codigo'] );
		Header('Content-Type: application/json');
		echo json_encode($data);
		exit();
	}

  if($result ->rowCount() == 0){
		$data = array('done' => false, 'mensaje' => "usuario y/o password incorrectos");
		Header('Content-Type: application/json');
		echo json_encode($data);
		exit();
  }
  echo "se ha conectado a la base correctamente";

?>
