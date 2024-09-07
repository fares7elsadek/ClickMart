var CouponsDataTable;
$(document).ready(function () {
    loadCouponsDataTable();
});

function loadCouponsDataTable() {
    CouponsDataTable = $('#CouponsDataTable').DataTable({
        "ajax": { url: '/Admin/Coupons/GetAllCoupons' },
        "columns": [
            { data: "code", "width": "10%" },
            { data: "discountValue", "width": "10%" },
            { data: "timesUsed", "width": "10%" },
            { data: "maxUsage", "width": "10%" },
            { data: "couponStartDate", "width": "10%" },
            { data: "couponEndDate", "width": "10%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                         <a class="btn btn-info me-2" href="/Admin/Coupons/Upsert/${data}">
                              <i class="bi bi-pencil-square"></i> Edit
                         </a>
                        <a onClick=Delete('/Admin/Coupons/Delete/${data}') class="btn btn-danger">
                            <i class="bi bi-trash"></i> Delete
                        </a>
                    `;
                },
                /*"width": "30%"*/
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
                        CouponsDataTable.ajax.reload();
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
