<?php

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "unitybackendtut";

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT password, id FROM users WHERE username = '" . $loginUser . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
  // output data of each row
  while($row = $result->fetch_assoc()) 
  {
    if($row["password"] == $loginPass)
    {
        echo $row["id"];

        
    }
    else
    {
        echo "Wrong Credentials.";
    }
  }
} 
else 
{
  echo "Username does not exist.";
}
$conn->close();

?>