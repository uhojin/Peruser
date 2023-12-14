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

        // Create and append a new element for the total listing count
        const countElement = document.createElement('p');
        countElement.textContent = `Total listings: ${listings.length}`;
        listingsContainer.appendChild(countElement);

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