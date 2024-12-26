document.addEventListener('DOMContentLoaded',  function ()
{
    const min = document.getElementById('min-price');
    if(min) { min.addEventListener('input', updatePriceValues); }
    
    const max = document.getElementById('max-price');
    if(max) { max.addEventListener('input', updatePriceValues); }

    function updatePriceValues() {
        const min = document.getElementById('min-price').value;
        const max = document.getElementById('max-price').value;

        document.getElementById('price_values').innerText = `${min} - ${max}`;
    }
    
    const sortSelect = document.getElementById('sort-options');
    const productContainer = document.querySelector('.cont_product');
    
    if (sortSelect) {
        sortSelect.addEventListener('change', () => {
            const sortOption = sortSelect.value;

            const products = Array.from(productContainer.querySelectorAll(".product_item"));

            products.sort((a, b) => {
                switch (sortOption) {
                    case 'asc': {
                        const priceA = parseFloat(a.querySelector('.product_extra p').textContent.replace(/[^\d]/g, ''));
                        const priceB = parseFloat(b.querySelector('.product_extra p').textContent.replace(/[^\d]/g, ''));
                        return priceA - priceB;
                    }
                    case 'desc': {
                        const priceA = parseFloat(a.querySelector('.product_extra p').textContent.replace(/[^\d]/g, ''));
                        const priceB = parseFloat(b.querySelector('.product_extra p').textContent.replace(/[^\d]/g, ''));
                        return priceB - priceA;
                    }
                    default: location.reload();
                }
            });
            products.forEach(product => productContainer.appendChild(product));
        });
    }
});


