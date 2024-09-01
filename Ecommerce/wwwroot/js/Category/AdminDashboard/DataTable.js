var CategoryDataTable;
$(document).ready(function () {
    loadCategoryDataTable();
})
function loadCategoryDataTable() {
    CategoryDataTable = $('#CategoryDataTable').DataTable({
        select: false,
        ordering: false,
        searching: false,
        paging: false,
        scrollY: 200,
        "ajax": { url: '/Admin/Category/GetAllCategories' },
        "columns": [
            { data: "name", "width": "100%" },
        ]
    });
}

