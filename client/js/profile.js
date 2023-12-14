document.addEventListener('DOMContentLoaded', function () {
    // Retrieve the user ID from local storage
    var userID = localStorage.getItem('userID');

    // Fetch user details using the user ID
    fetch('http://localhost:5000/api/users/' + userID)
        .then(response => response.json())
        .then(data => {
            // Render user details on the profile page
            renderProfile(data);
        })
        .catch((error) => {
            console.error('Error:', error);
        });

    // Function to render user details on the profile page
    function renderProfile(user) {
        var profileInfo = document.getElementById('profile-info');
        profileInfo.innerHTML = `
            <p><strong>Name:</strong> ${user.name}</p>
            <p><strong>Email:</strong> ${user.email}</p>
            <!-- Add more details as needed -->
        `;
    }
});
