var dataTable;

$(document).ready(function () {
    var productId = $('#productId').val();
    loadDataTable(productId);
})
function loadDataTable(productId) {
    dataTable = $('#datatable').DataTable({
        select: false,
        ordering: false,
        searching: false,
        paging: false,
        scrollY: 300,
        "ajax": { url: '/Admin/Attributes/GetAllAttributes/'+productId },
        "columns": [
            { data: "attributeName", "width": "10%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                         <a class="btn btn-info me-2" href="/Admin/Attributes/Edit/${data}?productId=${productId}">
                              <i class="bi bi-pencil-square"></i> Edit
                         </a>
                        <a onClick=Delete('/Admin/Attributes/Delete/${data}') class="btn btn-danger">
                            <i class="bi bi-trash"></i> Delete
                        </a>

                    `
                }
            },
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