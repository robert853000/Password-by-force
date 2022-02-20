<?php
session_start();
setcookie("Test", rand(10000, 99999), 0, "/");
setcookie("Test2", rand(10000, 99999), 0, "/");

$_passwd = "";
$_user = "";

class Users
{
    public $password;
    public $username;
}


if (isset($_GET["password"])) {
    $_passwd = $_GET["password"];
}

if (isset($_POST["password"])) {
    $_passwd = $_POST["password"];
}
if (isset($_GET["username"])) {
    $_user = $_GET["username"];
}

if (isset($_POST["username"])) {
    $_user = $_POST["username"];
}


$people = array();


$person1 = new Users();
$person1->password = "password1";
$person1->username = "David";
array_push($people, $person1);


$person2 = new Users();
$person2->password = "18";
$person2->username = "Anna";
array_push($people, $person2);

$person3 = new Users();
$person3->password = "aa12345678";
$person3->username = "Daniel";
array_push($people, $person3);

$person3 = new Users();
$person3->password = "qwertyuiop";
$person3->username = "Maria";
array_push($people, $person3);

function loginUser($username, $pwd)
{
    global $people;
    foreach ($people as $person) {
        $checkPwd = $person->username == $username && $person->password == $pwd;
        if ($checkPwd === true) {
            return true;
        }
    }
    return false;
}


if (loginUser($_user, $_passwd)) {
    echo "Your PASSWORD is correct";
} else {
    echo "Failure. Invalid login or password";
}
