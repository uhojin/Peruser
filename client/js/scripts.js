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
            productElement.innerHTML = `
                <h3>${product.title}</h3>
                <p>${product.description}</p>
                <p>Price: ${product.price} Points</p>
            `;
            productsContainer.appendChild(productElement);
        });
    })
    .catch(error => {
        console.error('Error fetching products:', error);
    });