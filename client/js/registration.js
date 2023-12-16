document.getElementById('registrationForm').addEventListener('submit', function(event) {
    event.preventDefault();

    var email = document.getElementById('email').value;
    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value;

    fetch('http://localhost:5000/api/users/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            email: email,
            name: username,
            password: password
        }),
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            // Registration successful
            //alert("Registration successful!");
            // Redirect to login page
            window.location.href = "login.html";
        } else {
            // Registration failed (username already exists)
            alert("Username already exists or email is invalid. Please check your input.");
        }
    })
    .catch((error) => {
        console.error('Error:', error);
    });
});
