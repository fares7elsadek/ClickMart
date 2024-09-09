var OrderDataTable;
$(document).ready(function () {
    loadOrderDataTable();
});

function loadOrderDataTable() {
    OrderDataTable = $('#OrderDataTable').DataTable({
        "ajax": { url: '/Admin/Order/GetAllOrders' },
        "columns": [
            { data: "name", "width": "30%" },
            { data: "displayOrder", "width": "30%" },
            { data: "displayOrder", "width": "30%" },
            { data: "displayOrder", "width": "30%" },
            { data: "displayOrder", "width": "30%" },
            {
                data: "id",
                "render": function (data) {
                    return `
                         <a class="btn btn-info me-2" href="/Admin/Category/Upsert/${data}">
                              <i class="bi bi-pencil-square"></i> Edit
                         </a>
                    `;
                },
                "width": "40%"
            }
        ]
    });
}


