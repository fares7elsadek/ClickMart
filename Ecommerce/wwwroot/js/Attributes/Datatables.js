var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#datatable').DataTable({
        select: false,
        ordering: false,
        searching: false,
        paging: false,
        scrollY: 300,
        "ajax": { url: '/Admin/Attributes/GetAllAttributes' },
        "columns": [
            { data: "attributeName", "width": "40%" },
            { data: "id", "width":"40%" },
            //{
            //    data: "id",
            //    "render": function (data) {
            //        return `
            //             <a class="btn btn-info me-2" href="/Admin/Product/Upsert/${data}">
            //                  <i class="bi bi-pencil-square"></i> Edit
            //             </a>
            //            <a onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-danger">
            //                <i class="bi bi-trash"></i> Delete
            //            </a>

            //        `
            //    }
            //},
        ]



    });
}