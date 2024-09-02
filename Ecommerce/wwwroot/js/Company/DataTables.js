var CompanyDataTable;
$(document).ready(function () {
    loadCompanyDataTable();
});

function loadCompanyDataTable() {
    CompanyDataTable = $('#CompanyDataTable').DataTable({
        "ajax": { url: '/Admin/Company/getallcompanies' },
        "columns": [
            { data: "name", "width": "15%" },
            { data: "streetAddress", "width": "15%" },
            { data: "city", "width": "15%" },
            { data: "state", "width": "15%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                         <a class="btn btn-info me-2" href="/Admin/Company/Upsert/${data}">
                              <i class="bi bi-pencil-square"></i> Edit
                         </a>
                        <a onClick=Delete('/Admin/Company/Delete/${data}') class="btn btn-danger">
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
                        CompanyDataTable.ajax.reload();
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
