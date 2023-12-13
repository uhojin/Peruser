// Get the user ID from local storage
var userID = localStorage.getItem('userID');

// Fetch the user's listings from the API
fetch(`http://localhost:5000/api/listings/${userID}`, {
    method: 'GET',
    headers: {
        'Content-Type': 'application/json'
    }})
    .then(response => response.json())
    .then(listings => {
        // Display the listings on the webpage
        const listingsContainer = document.getElementById('products');
        listings.forEach(listing => {
            const listingElement = document.createElement('div');
            listingElement.innerHTML = `
                <h3>${listing.title}</h3>
                <p>${listing.description}</p>
                <p>Price: ${listing.price} Points</p>
            `;
            listingsContainer.appendChild(listingElement);
        });
    })
    .catch(error => {
        console.error('Error fetching listings:', error);
    });