// function getProductCard() {
//     const lst = getProducts();
//     let htmlCards = '';
//     lst.forEach(item => {
//         const htmlCard = `
//         <div class="col-md-4 mb-4">
//         <div class="card" style="width: 18rem;">
//         <img class="card-img-top" src="${item.image}" alt="product image">
//         <div class="card-body">
//             <h5 class="card-title">${item.name}</h5>
//             <p class="card-text">MMK ${item.price}</p>
//             <a href="#" class="btn btn-secondary add-to-cart">
//     <i class="bi bi-cart-plus"></i> Add to Cart
// </a>
//         </div>
//         </div>
//         </div>
//         `;
//         htmlCards += htmlCard;
//     });

//     $('#productContainer').html(htmlCards);
// }

// $(document).ready(function () {
//     let cartCount = 0;
//     getProductCard();
//     $('#productContainer').on('click', '.add-to-cart', function (event) {
//         event.preventDefault();
//         cartCount++;
//         $('#cartBadge').text(cartCount);
//     });
// });

function getProductCard() {
    const lst = getProducts();
    let htmlCards = '';
    lst.forEach(item => {
        const htmlCard = `
            <div class="col-md-4 mb-4">
                <div class="card" style="width: 18rem;">
                    <img class="card-img-top" src="${item.image}" alt="product image">
                    <div class="card-body">
                        <h5 class="card-title">${item.name}</h5>
                        <p class="card-text">MMK ${item.price}</p>
                        <a href="#" class="btn btn-secondary add-to-cart" data-id="${item.id}" data-name="${item.name}" data-price="${item.price}">
                            <i class="bi bi-cart-plus"></i> Add to Cart
                        </a>
                    </div>
                </div>
            </div>
            `;
        htmlCards += htmlCard;
    });

    $('#productContainer').html(htmlCards);
}

$(document).ready(function () {
    let cart = [];

    getProductCard();

    $('#productContainer').on('click', '.add-to-cart', function (event) {
        event.preventDefault();
        const id = $(this).data('id');
        const name = $(this).data('name');
        const price = $(this).data('price');

        const existingProduct = cart.find(product => product.id === id);
        if (existingProduct) {
            existingProduct.qty++;
        } else {
            cart.push({
                id,
                name,
                price,
                qty: 1
            });
        }

        $('#cartBadge').text(cart.reduce((acc, product) => acc + product.qty, 0));
    });

    $('#cartBadgeContainer').on('click', function (event) {
        event.preventDefault();
        if (cart.length === 0) {
            alert("No items in cart.");
            return;
        }

        let cartDetails = '<table class="table"><thead><tr><th>Name</th><th>Price</th><th>Quantity</th><th>Amount</th></tr></thead><tbody>';
        let totalAmount = 0;

        cart.forEach((product, index) => {
            const amount = product.price * product.qty;
            totalAmount += amount;
            cartDetails += `
                    <tr>
                        <td>${product.name}</td>
                        <td>MMK ${product.price}</td>
                        <td><input type="number" min="0" class="form-control qty-input" data-index="${index}" value="${product.qty}"></td>
                        <td class="amount" data-index="${index}">MMK ${amount}</td>
                    </tr>
                `;
        });

        cartDetails += `
                <tr>
                    <td colspan="3"><strong>Total Amount</strong></td>
                    <td id="totalAmount"><strong>MMK ${totalAmount}</strong></td>
                </tr>
                </tbody></table>`;

        $('#cartDetails').html(cartDetails);
        $('#cartModal').modal('show');

        $('.qty-input').on('input', function () {
            const index = $(this).data('index');
            const newQty = parseInt($(this).val());
            if (newQty >= 1) {
                cart[index].qty = newQty;
                updateAmounts();
            }
        });
    });

    $('#updateCart').on('click', function () {
        cart = cart.filter(product => product.qty > 0);

        updateCartBadge();
        $('#cartModal').modal('hide');
    });

    function updateAmounts() {
        let totalAmount = 0;

        cart.forEach((product, index) => {
            const amount = product.price * product.qty;
            totalAmount += amount;
            $(`.amount[data-index="${index}"]`).text(`MMK ${amount}`);
        });

        $('#totalAmount').html(`<strong>MMK ${totalAmount}</strong>`);
    }

    function updateCartBadge() {
        const totalQty = cart.reduce((acc, product) => acc + product.qty, 0);
        $('#cartBadge').text(totalQty);
    }
});