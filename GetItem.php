<?php

require 'ConnectionSettings.php';

//$loginUser = $_POST["loginUser"];
//$loginPass = $_POST["loginPass"];\
$itemID = $_POST["itemid"];

// Check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT name, description, price FROM items WHERE ID = '" . $itemID . "'";
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