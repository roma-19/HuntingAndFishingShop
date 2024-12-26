document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.btn-add').forEach(button => {
        button.addEventListener('click', async (event) => {
            const productId = event.currentTarget.dataset.productId;
            const price = parseFloat(button.dataset.productPrice);
            const imagePath = button.dataset.productImage;
            const quantity = 1;

            if (!productId) {
                console.error('Product ID is missing.');
                alert('Не удалось определить идентификатор товара.');
                return;
            }

            try {
                const response = await fetch('/Cart/AddToCart', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ productId, quantity, price, imagePath }),
                });
            } catch (error) {
                console.error('Error adding to cart:', error);
            }
        });
    });
    
    document.querySelectorAll('.remove-item-btn').forEach(button => {
        button.addEventListener('click', async (event) => {
            const cartItemId = event.currentTarget.dataset.cartItemId;
            if (cartItemId) {
                await removeFromCart(cartItemId);
            } else {
                console.error('Cart item ID not found!');
            }
        });
    });

    async function removeFromCart(cartItemId) {
        try {
            const response = await fetch('/Cart/RemoveFromCart', {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ cartItemId }),
            });

            if (response.ok) {
                location.reload();
            } else {
                const error = await response.json();
                alert(`Ошибка: ${error.message}`);
            }
        } catch (error) {
            console.error('Error removing item from cart:', error);
        }
    }


    // document.querySelectorAll('.quantity-input').forEach(input => {
    //     input.addEventListener('change', async (event) => {
    //         const cartItemId = event.target.id.split('-')[1];
    //         const newQuantity = event.target.value;
    //
    //         if (newQuantity <= 0) {
    //             alert('Количество должно быть больше нуля.');
    //             event.target.value = 1;
    //             return;
    //         }
    //
    //         try {
    //             const response = await fetch('/Cart/UpdateQuantity', {
    //                 method: 'POST',
    //                 headers: {
    //                     'Content-Type': 'application/json'
    //                 },
    //                 body: JSON.stringify({
    //                     cartItemId: cartItemId,
    //                     newQuantity: newQuantity
    //                 })
    //             });
    //
    //             if (response.ok) {
    //                 alert('Количество обновлено.');
    //                 location.reload();
    //             } else {
    //                 const errorData = await response.json();
    //                 alert(`Ошибка: ${errorData.message}`);
    //             }
    //         } catch (error) {
    //             console.error('Error updating quantity:', error);
    //         }
    //     });
    // });

});
