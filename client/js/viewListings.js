// Fetch the products from the API
fetch('http://localhost:5000/api/listings', {
    method: 'GET',
    headers: {
        'Content-Type': 'application/json'
    }})
    .then(response => response.json())
    .then(products => {
        // Display the products on the webpage
        const productsContainer = document.getElementById('products');
        products.forEach(product => {
            const productElement = document.createElement('div');
            var descriptionLength = productsContainer.classList.contains('grid') ? 50 : 140;
            var description = product.description.length > descriptionLength ? product.description.substring(0, descriptionLength) + '...' : product.description;
        
            productElement.innerHTML = `
                <h3><a href="listingDetails.html?id=${product.id}">${product.title}</a></h3>
                <p>${description}</p>
                <p>Price: ${product.price} Points</p>
            `;
            productsContainer.appendChild(productElement);
        });
    })
    .catch(error => {
        console.error('Error fetching products:', error);
    });

document.getElementById('searchForm').addEventListener('submit', function(event) {
    event.preventDefault();

    var searchQuery = document.getElementById('searchInput').value;
    if (searchQuery.length == 0) {
        return;
    } 

    // Fetch the products from the API
    fetch(`http://localhost:5000/api/listings/search/${searchQuery}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }})
        .then(response => response.json())
        .then(products => {
            // Display the products on the webpage
            const productsContainer = document.getElementById('products');
            productsContainer.innerHTML = '';
            products.forEach(product => {
                const productElement = document.createElement('div');
                productElement.innerHTML = `
                    <h3><a href="listingDetails.html?id=${product.id}">${product.title}</a></h3>
                    <p>${product.description}</p>
                    <p>Price: ${product.price} Points</p>
                `;
                productsContainer.appendChild(productElement);
            });
        })
        .catch(error => {
            console.error('Error fetching products:', error);
        });
});

document.getElementById('listViewBtn').addEventListener('click', function() {
    // Save the selected view to localStorage
    localStorage.setItem('view', 'list');
    // Remove the 'grid' class from the products container
    document.getElementById('products').classList.remove('grid');
    location.reload();
});

document.getElementById('gridViewBtn').addEventListener('click', function() {
    // Save the selected view to localStorage
    localStorage.setItem('view', 'grid');
    // Add the 'grid' class to the products container
    document.getElementById('products').classList.add('grid');
    location.reload();
});

var view = localStorage.getItem('view');
if (view === 'grid') {
    // If the selected view is 'grid', add the 'grid' class to the products container
    document.getElementById('products').classList.add('grid');
} else if (view === 'list') {
    // If the selected view is 'list', remove the 'grid' class from the products container
    document.getElementById('products').classList.remove('grid');
}