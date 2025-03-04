<?php

require 'ConnectionSettings.php';

$userID = $_POST["userid"];

// Check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT ID, itemid FROM usersitems WHERE userid = '" . $userID . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
    $rows = array();
  // output data of each row
  while($row = $result->fetch_assoc()) 
  {
    $rows[] = $row;
  }
  echo json_encode($rows);
}
else 
{
  echo "0";
}
$conn->close();

?>