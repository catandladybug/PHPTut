<?php

require 'ConnectionSettings.php';

$itemID = $_POST["itemid"];
$userID = $_POST["userid"];
$ID = $_POST["ID"];

// Check connection
if ($conn->connect_error) 
{
  die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT price FROM items WHERE ID = '" . $itemID . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
    $itemPrice = $result->fetch_assoc()["price"];

    $sql2 = "DELETE FROM usersitems WHERE ID = '" . $ID ."'";

    $result2 = $conn->query($sql2);

    if($result2)
    {
        $sql3 = "UPDATE `users` SET `coins` = coins + '" . $itemPrice . "' WHERE `id` = '" . $userID . "'";
        $conn->query($sql3);
    }
    else
    {
        echo "error: could not delete item";
    }
}
else 
{
  echo "0";
}
$conn->close();

?>