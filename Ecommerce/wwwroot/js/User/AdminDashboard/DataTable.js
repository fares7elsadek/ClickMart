var UserDataTable;
$(document).ready(function () {
    loadUserDataTable();
});

function loadUserDataTable() {
    UserDataTable = $('#UserDataTable').DataTable({
        "ajax": { url: '/Admin/User/getallusers' },
        "columns": [
            {
                data: null,
                render: function (data, type, row) {
                    return '<img src="/' + row.avatar + '" class="me-1 rounded-circle" style="height:40px;width:40px;"/> ' + row.firstName + ' ' + row.lastName;
                },
                width: "30%"
            },
            { data: "userName", width: "35%" },
            { data: "email", width: "35%" }
        ]
    });
}