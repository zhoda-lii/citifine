// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let currentSortColumn = null; // Keep track of the current sorted column
let currentSortOrder = 'asc'; // Default to ascending order

document.querySelectorAll('.sortable').forEach(function (header) {
    header.addEventListener('click', function () {
        const column = this.getAttribute('data-column');
        const icon = this.querySelector('i');

        if (currentSortColumn === column) {
            // If the same column is clicked, toggle the sort order
            currentSortOrder = currentSortOrder === 'asc' ? 'desc' : 'asc';
        } else {
            // If a different column is clicked, reset to ascending order
            currentSortColumn = column;
            currentSortOrder = 'asc';
        }

        sortTableByColumn(column);
        updateSortIcons(icon);
    });
});

function sortTableByColumn(column) {
    const table = document.getElementById("violationTable");
    const rows = Array.from(table.querySelectorAll("tbody tr"));
    const columnIndex = getColumnIndex(column);

    const sortedRows = rows.sort(function (a, b) {
        const aText = a.cells[columnIndex].textContent.trim();
        const bText = b.cells[columnIndex].textContent.trim();

        if (column === 'Paid') {
            // Handling boolean sorting for "Paid"
            const aBool = aText.toLowerCase() === 'true'; // 'true' or 'false' from text
            const bBool = bText.toLowerCase() === 'true'; // 'true' or 'false' from text
            return currentSortOrder === 'asc' ? aBool - bBool : bBool - aBool;
        } else if (isNaN(aText) || isNaN(bText)) {
            // Sort as strings for non-numeric columns
            return currentSortOrder === 'asc' ? aText.localeCompare(bText) : bText.localeCompare(aText);
        } else {
            // Sort as numbers for numeric columns like FineAmount
            return currentSortOrder === 'asc' ? parseFloat(aText) - parseFloat(bText) : parseFloat(bText) - parseFloat(aText);
        }
    });

    // Clear the existing table rows
    const tbody = table.querySelector('tbody');
    tbody.innerHTML = '';

    // Append the sorted rows
    sortedRows.forEach(function (row) {
        tbody.appendChild(row);
    });
}

// Helper function to get the column index based on the column name
function getColumnIndex(column) {
    const columnMapping = {
        "ViolationId": 0,
        "LicensePlate": 1,
        "ViolationType": 2,
        "FineAmount": 3,
        "DateIssued": 4,
        "PaymentDeadline": 5,
        "Paid": 6
    };
    return columnMapping[column];
}

// Update the icons based on the current sort order
function updateSortIcons(icon) {
    // Reset all icons
    document.querySelectorAll('.sortable i').forEach(function (i) {
        i.classList.remove('fa-sort-up', 'fa-sort-down');
        i.classList.add('fa-sort');
    });

    // Set the correct icon for the sorted column
    if (currentSortOrder === 'asc') {
        icon.classList.remove('fa-sort');
        icon.classList.add('fa-sort-up');
    } else {
        icon.classList.remove('fa-sort');
        icon.classList.add('fa-sort-down');
    }
}
