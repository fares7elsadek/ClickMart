var ProductDataTable;
$(document).ready(function () {
    loadProductDataTable();
})
function loadProductDataTable() {
    dataTable = $('#ProductDataTable').DataTable({
        select: false,
        ordering: false,
        searching: false,
        paging: false,
        scrollY: 200,
        "ajax": { url: '/Admin/Product/getallproducts' },
        "columns": [
            { data: "title", "width": "50%" },
            { data: "category.name", "width": "50%" },
        ]
    });
}

