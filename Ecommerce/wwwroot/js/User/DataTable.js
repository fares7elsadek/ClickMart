var AllUserDataTable;
$(document).ready(function () {
    loadAllUserDataTable();
});

function loadAllUserDataTable() {
    UserDataTable = $('#AllUserDataTable').DataTable({
        "ajax": { url: '/Admin/User/getallusers' },
        "columns": [
            {
                data: null,
                render: function (data, type, row) {
                    return '<img src="/' + row.avatar + '" class="me-1 rounded-circle" style="height:40px;width:40px;"/> ' + row.firstName + ' ' + row.lastName;
                },
                width: "20%"
            },
            { data: "userName", width: "15%" },
            { data: "email", width: "15%" },
            { data: "phoneNumber", width: "15%" },
            { data: "role", width: "10%" },
            {
                data: "email",
                "render": function (data) {
                    return `
                         <a class="btn btn-info me-2" href="/Admin/User/Edit/${data}">
                              <i class="bi bi-pencil-square"></i> Edit
                         </a>
                        <a onClick=Delete('${data}') class="btn btn-danger">
                            <i class="bi bi-trash"></i> Delete
                        </a>
                    `;
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(email) {
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
                url: '/Admin/User/Delete', // URL without email
                type: "POST", // Use POST method
                headers: {
                    "RequestVerificationToken": token
                },
                data: { email: email }, // Send email in the request body
                success: function (data) {
                    if (data.success) {
                        AllUserDataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function (err) {
                    toastr.error("An error occurred while trying to delete the user.");
                }
            });
        }
    });
}
