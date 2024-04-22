$(document).ready(OnPageLoad);

function OnPageLoad() {
    BindDataTable('#employeeTable');
}

function BindDataTable(selector) {
    if (!$(selector).length) {
        return;
    }

    let dtConfig = {
        "columnDefs": [
            {
                "targets": 0,
                "visible": false // ID column should be hidden
            },
            {
                "targets": [1, 2, 3, 4, 5], "render": function (data, type, row, meta) {
                    // Contenteditable attribute added for inline editing
                    return `<div contenteditable="true" data-column="${meta.col}">${data}</div>`;
                }
            }
        ]
    };
    let tableEl = $(selector),
        dtMounted = tableEl.DataTable(dtConfig);

    tableEl.on('focusout', 'div[contenteditable]', function () {
        let el = $(this),
            oldValue = dtMounted.cell(this.parentElement).data(),
            newValue = el.text();

        if (newValue != oldValue) {
            let columnIndex = el.data('column'),
                columnHeader = $(selector + ' thead th').eq(columnIndex - 1),
                rowData = dtMounted.row(this.parentElement.parentElement).data();

            let id = Number(rowData[0]),
                name = columnHeader.data('column'),
                value = newValue;

            if (id && name) {
                UpdateRecordFieldInDB(id, name, value, function () {
                    el.text(oldValue);
                });
            }
        }
    });

    tableEl.on('click', 'tbody button.delete-record-btn', function () {
        let row = dtMounted.row($(this).parents('tr'));
        if (row) {
            let rowData = row.data(),
                recordId = Number(rowData[0]);

            if (recordId) {
                DeleteRecordFromDB(recordId, function () {
                    row.remove().draw();
                });
            }
        }
    });
}

function UpdateRecordFieldInDB(id, name, value, failCallback) {
    $.ajax({
        url: '/Employee/UpdateRecord',
        method: 'POST',
        data: { id: id, fieldName: name, fieldValue: value },
        success: function (response) {
            if (response.success) {
                toastr.success(response.message);
            } else {
                toastr.error(response.message);

                if (typeof failCallback === 'function') {
                    failCallback();
                }
            }
        },
        error: function (error) {
            toastr.error('Update failed: ' + error.responseText);
        }
    });
}

function DeleteRecordFromDB(id, successCallback) {
    $.ajax({
        url: '/Employee/DeleteRecord',
        method: 'POST',
        data: { id: id },
        success: function (response) {
            if (response.success) {
                toastr.success(response.message);

                if (typeof successCallback === 'function') {
                    successCallback();
                }
            } else {
                toastr.error(response.message);
            }
        },
        error: function (error) {
            toastr.error('Delete failed: ' + error.responseText);
        }
    });
}