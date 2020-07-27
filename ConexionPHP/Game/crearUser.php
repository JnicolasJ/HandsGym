<?php
	include_once("dbConnection.php");
  $username  = $_POST["username"];
  $email  = $_POST["email"];
  $password = hash("sha256",$_POST["password"]);
	$codigo  = $_POST["codigo"];
  $sql = "SELECT username FROM usuarios WHERE username='$username'";
  $result = $con->query($sql);

  if($result ->rowCount() > 0){
    $data = array('done' => false, 'mensaje' => "Error nombre de usuario ya existe");
    Header('Content-Type: application/json');
    echo json_encode($data);
    exit();
  }
  else
  {
    $sql = "SELECT email FROM usuarios WHERE email='$email'";
    $result = $con->query($sql);
    if($result ->rowCount() > 0){
      $data = array('done' => false, 'mensaje' => "Error email ya esta registrado");
      Header('Content-Type: application/json');
      echo json_encode($data);
      exit();
    }
    else
    {
        $sql = "INSERT INTO usuarios SET username='$username', email='$email', password='$password', codigo='$codigo'";
				$result = $con->query($sql);
        if($result ->rowCount() > 0){
          $data = array('done' => true, 'mensaje' => "Se ha creado el usuario");
          Header('Content-Type: application/json');
          echo json_encode($data);
          exit();
        }
    }
  }

  echo "se ha conectado a la base correctamente";
?>
