document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('apply-filter').addEventListener('click', applyPriceFilter);
    
    const categoryLinks = document.querySelectorAll('.cat_menu a');
    
    categoryLinks.forEach((categoryLink) => {
        categoryLink.addEventListener('click', (e) => {
            e.preventDefault();
            
            categoryLinks.forEach((link) => link.classList.remove('active-category'));
            
            e.target.classList.add('active-category');
            
            const categoryId = e.target.getAttribute('data-category-id');
            const minPrice = document.getElementById('min-price').value;
            const maxPrice = document.getElementById('max-price').value;

            const filterData = { categoryId: categoryId, minPrice: minPrice, maxPrice: maxPrice };

            console.log('Фильтр по категории:', filterData);
            
            fetch('/Catalogue/Filter', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(filterData),
            })
                .then((response) => {
                    if (!response.ok) {throw new Error('Ошибка при фильтрации данных');}
                    return response.json();
                })
                .then((data) => {
                    console.log('Результаты фильтрации по категории:', data);
                    dataDisplay(data);
                })
                .catch((error) => {console.error('Ошибка:', error);});
        });
    });
   
    function applyPriceFilter() {
        const categoryId = document.getElementById('categoryId').value;
        const minPrice = document.getElementById('min-price').value;
        const maxPrice = document.getElementById('max-price').value;

        const filterData = { categoryId: categoryId, minPrice: minPrice, maxPrice: maxPrice };

        console.log('Отправляемые данные:', filterData);

        fetch('/Catalogue/Filter', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(filterData),
        })
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Ошибка при фильтрации данных');
                }
                return response.json();
            })
            .then((data) => {
                console.log('Результаты фильтрации:', data);
                dataDisplay(data);
            })
            .catch((error) => {
                console.error('Ошибка:', error);
            });
    }


    function dataDisplay(data) {
        const productList = document.querySelector('.cont_product');
        productList.innerHTML = '';

        if (!data || data.length === 0) {
            const noProductMessage = `<h2>По данному фильтру нет товаров</h2>`;
            productList.innerHTML += noProductMessage;
        } else {
            data.forEach((product) => {
                const productItem = `
                    <div class="cont_product">
                        <div class="product_item">
                            <div class="product_main">
                                <h2>${product.name}</h2>
                                <a href="/Catalogue/ProductPage/${product.id}" class="product-link">
                                    <img src="${product.imagePath}" class="product_img" alt=""/>
                                </a>
                            </div>
                            <div class="product_extra">
                                <p style="font-size: 25px">${product.price} &#8381;</p>
                                <p style="color: ${product.quantity > 0 ? "green" : "red"}">
                                    ${product.quantity > 0 ? "Есть в наличии" : "Нет в наличии"}
                                </p>
                            </div>
                            <input asp-for="@item.Id" value="${product.id}" style="display: none"/>
                            <input asp-for="@item.CategoryId" value="${product.categoryId}" style="display: none"/>
                        </div>
                    </div>`;
                productList.innerHTML += productItem;
            });
        }
    }
});