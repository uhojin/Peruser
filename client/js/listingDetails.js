// Get the listing ID from the URL
var urlParams = new URLSearchParams(window.location.search);
var id = urlParams.get('id');

// Fetch the listing details from the API
fetch(`http://localhost:5000/api/listings/${id}`, {
    method: 'GET',
    headers: {
        'Content-Type': 'application/json'
    }
})
    .then(response => response.json())
    .then(listing => {
        // Display the listing details on the webpage
        const listingContainer = document.getElementById('listing');
        var imgTag = listing.imgUrl ? `<img src="${listing.imgUrl}" alt="${listing.title}" class="listing-image">` : '';

        // Get the user ID from local storage
        var userId = localStorage.getItem('userID');

        // Add Edit and Delete buttons if the user is the owner of the listing
        var buttons = userId === listing.ownerId ? '<div id="buttonContainer"><button id="editBtn">Edit</button><button id="deleteBtn">Delete</button></div>' : '<div id="buttonContainer"><button id="buyBtn">Buy</button></div>';
        console.log(`${listing.ownerId}`)

        listingContainer.innerHTML = `
            ${imgTag}
            <h3>${listing.title}</h3>
            <p>${listing.description}</p>
            <p>Price: ${listing.price} Points</p>
            ${buttons}
        `;
        // Add event listeners to the buttons
        var editBtn = document.getElementById('editBtn');
        var deleteBtn = document.getElementById('deleteBtn');
        var buyBtn = document.getElementById('buyBtn');

        if (editBtn) {
            editBtn.addEventListener('click', function () {
                // Redirect to the edit page with the listing ID in the URL
                window.location.href = `editListing.html?id=${listing.id}`;
            });
        }

        if (deleteBtn) {
            deleteBtn.addEventListener('click', function () {
                fetch(`http://localhost:5000/api/listings/${listing.id}`, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Error deleting listing');
                        }
                        // Redirect to the all listings page
                        window.location.href = 'index.html';
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });
            });
        }

        if (buyBtn) {
            buyBtn.addEventListener('click', function () {
                var buyerId = localStorage.getItem('userID');
                fetch(`http://localhost:5000/api/listings/purchase/${listing.id}`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(buyerId)
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Error purchasing listing');
                        }
                        // Redirect to the all listings page
                        window.location.href = 'index.html';
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });
            });
        }
    })
    .catch(error => {
        console.error('Error fetching listing:', error);
    });

