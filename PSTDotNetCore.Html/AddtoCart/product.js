getProductTable();
getProductCard();

function readProduct() {
    let lst = getProducts();
    console.log(lst);
}

function editProduct(id) {
    let lst = getProducts();

    const items = lst.filter(x => x.id === id);
    console.log(items);

    if (items.length == 0) {
        console.log("no data found.");
        errorMessage("no data found.");
        return;
    }

    let item = items[0];

    productId = item.id;
    $('#txtName').val(item.name);
    $('#txtPrice').val(item.price);
    $('#txtName').focus();
}

function createProduct(name, price, image) {
    const fields = ['#txtName', '#txtPrice'];
    const isEmpty = fields.some(field => $(field).val().trim() === '');
    if (isEmpty) {
        warningMessage("Please fill in all required fields.");
        return;
    }

    let lst = getProducts();

    const requestModel = {
        id: uuidv4(),
        name: name,
        price: price,
        image: image
    };

    lst.push(requestModel);

    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(tblProduct, jsonProduct);

    successMessage("Saving Successful.");
    clearControl();
}

function updateProduct(id, name, price, image) {
    let lst = getProducts();

    const items = lst.filter(x => x.id === id);
    console.log(items);

    if (items.length == 0) {
        console.log("no data found.");
        errorMessage("no data found.");
        return;
    }

    const item = items[0];
    item.name = name;
    item.price = price;

    if (image !== null) {
        item.image = image;
    }

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(tblProduct, jsonProduct);

    successMessage("Updating Successful.");
    clearControl();
}

function deleteProduct(id) {
    confirmMessage("Are you sure want to delete?").then(
        function (value) {
            let lst = getProducts();

            const items = lst.filter(x => x.id === id);
            if (items.length == 0) {
                console.log("no data found");
                return;
            }

            lst = lst.filter(x => x.id !== id);
            const jsonProduct = JSON.stringify(lst);
            localStorage.setItem(tblProduct, jsonProduct);

            successMessage("Deleting Succssfull.");
            getProductTable();
        }
    );
}

function confirmMessage(message) {
    // let confirmMessageResult = new Promise(function (success, error) {
    //     Swal.fire({
    //         title: "Confirm",
    //         text: message,
    //         icon: "warning",
    //         showCancelButton: true,
    //         confirmButtonText: "Yes"
    //     }).then((result) => {
    //         if (result.isConfirmed) {
    //             success();
    //         } else {
    //             error();
    //         }
    //     });
    // });
    // return confirmMessageResult;

    let confirmMessageResult = new Promise(function (success, error) {
        Notiflix.Confirm.show(
            'Confirm',
            message,
            'Yes',
            'No',
            function okCb() {
                success();
            },
            function cancelCb() {
                error();
            }
        );
    });
    return confirmMessageResult;
}

$('#btnSave').click(function () {
    const name = $('#txtName').val();
    const price = $('#txtPrice').val();

    const file = $('#txtImage')[0].files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const image = e.target.result;

            if (productId === null) {
                createProduct(name, price, image);
            } else {
                updateProduct(productId, name, price, image);
                productId = null;
            }

            getProductTable();
        };
        reader.readAsDataURL(file);
    } else {
        const image = null;

        if (productId === null) {
            createProduct(name, price, image);
        } else {
            updateProduct(productId, name, price, image);
            productId = null;
        }

        getProductTable();
    }
});

$('#btnCancel').click(function () {

    clearControl();
});

function clearControl() {
    $('#txtName').val('');
    $('#txtPrice').val('');
    $('#txtImage').val('');
    $('#txtName').focus();
}

function getProductTable() {
    const lst = getProducts();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="editProduct('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteProduct('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.name}</td>
            <td>${item.price}</td>
            <td><img src="${item.image}" alt = "Product Image" width = "50"></td>
        </tr>
        `;
        htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);
    Notiflix.Block.dots('#dtproduct');

    setTimeout(() => {
        Notiflix.Block.remove('#dtproduct');
    }, 2000);
}