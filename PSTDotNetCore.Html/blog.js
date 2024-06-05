const tblBlog = "blogs";
let blogId = null;

getBlogTable();
//testConfirmMessage();
//testConfirmMessage2();
//createBlog();
//updateBlog("608bd5fb-f289-4e4e-995f-191d146375c3", "title-1", "author-1", "content-1");
//deleteBlog("608bd5fb-f289-4e4e-995f-191d146375c3");

function readBlog() {
    let lst = getBlogs();
    console.log(lst);
}

function editBlog(id) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    console.log(items);

    if (items.length == 0) {
        console.log("no data found.");
        errorMessage("no data found.");
        return;
    }

    let item = items[0];

    blogId = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();
}

function createBlog(title, author, content) {
    const fields = ['#txtTitle', '#txtAuthor', '#txtContent'];
    const isEmpty = fields.some(field => $(field).val().trim() === '');
    if (isEmpty) {
        warningMessage("Please fill in all required fields.");
        return;
    }

    let lst = getBlogs();

    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };

    lst.push(requestModel);

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Saving Sucessful.");
    clearControl();
}

function updateBlog(id, title, author, content) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    console.log(items);

    if (items.length == 0) {
        console.log("no data found.");
        errorMessage("no data found.");
        return;
    }

    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Updating Successful.");
    clearControl();

}

function deleteBlog(id) {
    confirmMessage("Are you sure want to delete?").then(
        function (value) {
            let lst = getBlogs();

            const items = lst.filter(x => x.id === id);
            if (items.length == 0) {
                console.log("no data found");
                return;
            }

            lst = lst.filter(x => x.id !== id);
            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog);

            successMessage("Deleting Succssfull.");
            getBlogTable();
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

function getBlogs() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}

$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();

    if (blogId === null) {
        createBlog(title, author, content);
    } else {
        updateBlog(blogId, title, author, content);
        blogId = null;
    }

    getBlogTable();

});

$('#btnCancel').click(function () {

    clearControl();
});

function clearControl() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function getBlogTable() {
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr>
        `;
        htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);
    Notiflix.Block.dots('#tableId');

    setTimeout(() => {
        Notiflix.Block.remove('#tableId');
    }, 3000);
}