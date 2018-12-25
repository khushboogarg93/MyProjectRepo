$(document).on('click', '#btnCreateAccount', saveStudent);
$(document).on('click', '#btnLoginAccount', loginStudent);

function Validate(event) {
    //alert("hi");
}

function saveStudent() {
    let firstName = $('#txtFirstname').val();
    let lastName = $('#txtLastname').val();
    let email = $('#txtEmail').val();
    let password = $('#txtPassword').val();

    let studentObj = {
        'firstName': firstName,
        'lastName': lastName,
        'email': email,
        'password': password
    }
    authDataService.saveStudent(studentObj);
}

function bindStudentDetails(response) {
    if (response.operationStatus == "SUCCESS") {
        alert("Reistration successfull!!")
        window.location.href = "https://localhost:5001/TrackAbout/About";
    }
    else {
        alert("Failed Registration!")
    }
}

function loginStudent() {
    let email = $('#txtLoginEmail').val();
    let password = $('#txtLoginPassword').val();

    let studentLoginObj = {
        'email': email,
        'password': password
    }
    authDataService.loginStudent(studentLoginObj);
}

function bindLoginStudentDetails(response) {
    if (response.operationStatus == "SUCCESS") {
        alert("Login successfull!!")
        window.location.href = "https://localhost:5001/TrackAbout/About";
    }
    else {
        alert("Invalid Login Attempt!")
    }
}