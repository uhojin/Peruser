document.getElementById('loginForm').addEventListener('submit', function(event) {
    event.preventDefault();

    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value;

    fetch('http://localhost:5000/api/users/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            username: username,
            password: password
        }),
    })
    .then(response => response.json())
    .then(data => {
        // Store the user ID in local storage
        console.log(data.userID);
        localStorage.setItem('userID', data.userID);
        // Redirect to index.html
        window.location.href = "index.html";
    })
    .catch((error) => {
        console.error('Error:', error);
    });
});