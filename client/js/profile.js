// Retrieve the user ID from local storage
var userID = localStorage.getItem('userID');
const deleteButton = document.getElementById('delete-button');


document.addEventListener('DOMContentLoaded', function () {
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
        profileInfo.innerHTML = `<h2><strong>Name:</strong> ${user.name}</h2>
                                 <h2><strong>Email:</strong> ${user.email}</h2>`;
    }
});

function deleteUser(userId) {
    fetch("http://localhost:5000/api/users/delete/" + userID, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
    })
    .then(response => {
        console.log('Full Response:', response);
        return response.json();
    })
    .then(data => {
        if (data) {
            localStorage.clear("userID");
            window.location.href = "login.html";
        } else {
            alert("Failed to delete user. Please check your input.");
        }
    })
    .catch((error) => {
        console.error('Error:', error);
    });
}

deleteButton.addEventListener('click', () => {
    deleteUser(userID);
});