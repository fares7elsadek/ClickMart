﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#datatable').DataTable({
        "ajax": { url: '/Admin/Product/getallproducts' },
        "columns": [
            { data: "title", "width": "30%" },
            { data: "price", "width": "10%" },
            { data: "category.name", "width": "15%" },
            {
                data: "published",
                "render": function (data) {
                    return data ? '<span class="badge bg-success">Active</span>' : '<span class="badge bg-danger">Not Active</span>';
                },
                "width": "10%"
            },
            {
                data: "id",
                "render": function (data) {
                    return `
                         <a class="btn btn-info me-2" href="/Admin/Product/Upsert/${data}">
                              <i class="bi bi-pencil-square"></i> Edit
                         </a>
                         <a class="btn btn-info me-2" href="/Admin/Coupons/AddToProduct?productId=${data}">
                              <i class="bi bi-pencil-square"></i> Add Coupon
                         </a>
                        <a onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-danger">
                            <i class="bi bi-trash"></i> Delete
                        </a>
                        
                    `;
                },
               
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
                        dataTable.ajax.reload();
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
