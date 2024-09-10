var OrderDataTable;
$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const status = urlParams.get('status');
    if(status != "")
        loadOrderDataTable(status);
    else
        loadOrderDataTable();
});

function loadOrderDataTable(status = "All") {
    OrderDataTable = $('#OrderDataTable').DataTable({
        "ajax": { url: '/Admin/Order/GetAllOrders/?status=' + status },
        "columns": [
            {
                data: null,
                render: function (data, type, row) {
                    return '<img src="/' + row.user.avatar + '" class="me-1 rounded-circle" style="height:40px;width:40px;"/> ' + row.user.userName;
                },
                width: "20%"
            },
            { data: "user.email", "width": "20%" },
            {
                data: "orderStatus",
                render: function (data, type, row) {
                    let badgeClass = "";
                    switch (data) {
                        case "Delivered":
                            badgeClass = "success";
                            break;
                        case "Cancelled":
                            badgeClass = "danger";
                            break;
                        case "Approved":
                            badgeClass = "success";
                            break;
                        case "Shipped":
                            badgeClass = "primary";
                            break;
                        case "In Process":
                            badgeClass = "warning";
                            break;
                        case "Pending":
                            badgeClass = "secondary";
                            break;
                        default:
                            badgeClass = "danger";
                    }

                    return `<span class="badge bg-${badgeClass} p-2">${data}</span>`;
                },
                width: "20%"
            },
            { data: "orderTotal", "width": "20%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                         <a class="btn btn-info me-2" href="/Admin/Order/Details/${data}">
                              <i class="bi bi-pencil-square"></i> Details
                         </a>
                    `;
                },
            }
        ]
    });
}
