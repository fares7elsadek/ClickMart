var CategoryDataTable;
$(document).ready(function () {
    loadCategoryDataTable();
});

function loadCategoryDataTable() {
    CategoryDataTable = $('#CategoryDataTable').DataTable({
        "ajax": { url: '/Admin/Category/GetAllCategories' },
        "columns": [
            { data: "name", "width": "30%" },
            { data: "displayOrder", "width": "30%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                         <a class="btn btn-info me-2" href="/Admin/Category/Upsert/${data}">
                              <i class="bi bi-pencil-square"></i> Edit
                         </a>
                        <a onClick=Delete('/Admin/Category/Delete/${data}') class="btn btn-danger">
                            <i class="bi bi-trash"></i> Delete
                        </a>
                    `;
                },
                "width": "40%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                url: url,
                type: "DELETE",
                headers: {
                    "RequestVerificationToken": token
                },
                success: function (data) {
                    if (data.success) {
                        CategoryDataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (err) {
                    toastr.error("An error occurred while trying to delete the product.");
                }
            });
        }
    });
}
