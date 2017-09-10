// create datatable, apply for table id 'Table'
var datatable = $('#Table').DataTable({
    "language": {
        "lengthMenu": 'Display <select>' +
        '<option value="5">5</option>' +
        '<option value="10">10</option>' +
        '<option value="20">20</option>' +
        '<option value="30">30</option>' +
        '<option value="40">40</option>' +
        '<option value="50">50</option>' +
        '<option value="-1">All</option>' +
        '</select> records',
        // Notify when not found result record
        "zeroRecords": "Không tìm thấy!",
        "info": "",
        "infoEmpty": "",
        "infoFiltered": "",
        "search": "",
        "paginate": {
            "next": '<i class="fa fa-angle-right" aria-hidden="true"></i>',
            "previous": '<i class="fa fa-angle-left" aria-hidden="true"></i>'
        }
    },
    "sDom": 'tipr',
});

// Event: click advanced button
$('#AddvancedButton').on('click', function () {
    //
    // Call function to clear all input fields
    ClearConditionSearch();
    var click = $(this);
    //
    // Check 'data-click' value of this button
    if (click.data('click') == "0") {
        $('#basesearch').addClass('hidden');
        $('#advancedsearch').removeClass('hidden');
        click.data('click', '1');
    }
    else {
        $('#advancedsearch').addClass('hidden');
        $('#basesearch').removeClass('hidden');
        click.data('click', '0');
    }
});

// Function: Advanced search
function SearchAdvanced() {
    var form = $('#SearchAdvancedForm');
    //
    // Set value to input name 'delete'
    var deletecheck = form.find('[name=delete]');
    if ($('#check').is(':checked')) { //Check radio id 'check'
        deletecheck.val(true);
    }
    else {
        deletecheck.val(false);
    }
    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: form.serialize(), //get all variable of form to ajax
    })
        .done(function (results) {
            // Clear datatable data
            $('#Table').DataTable().clear().draw();
            // forearch to add row into datatable
            $.each(results.data, function (idx, result) {
                var html = '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-grouptableid="' + result.GroupTableID + '" data-description="' + result.Des + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                    + '<a href="javascript:;" style="margin-left: 4px;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>';
                if (results.delete == true) {
                    html = '<a href="javascript:;" class="_recover btn btn-primary btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#RecoverModal" title="Phục hồi"><i class="fa fa-reply-all" aria-hidden="true"></i></i></a>';
                }
                else { }
                // add row into datatable
                var rowNode = datatable.row.add([
                    result.ID,
                    result.Name,
                    result.GroupTableName,
                    result.OrderCount,
                    result.Des,
                    html,
                ]).draw(false).node();
                // Add attribute for this row <tr>
                $(rowNode)
                    .attr('id', 'item-' + result.ID);
            })
        })
        .fail(function (data) {
            alert('Tải dữ liệu thất bại! Vui lòng thử lại.');
        })
}
// Function: clear all input of search and get data
function ClearConditionSearch() {
    // clear value of condition
    $('#search').val('');
    datatable.search('').draw();
    $('#NameSearch').val('');
    $('#GroupTableIDSearch').val('0');
    // get option radio
    var value;
    if ($('#check').is(':checked')) {
        value = true;
    }
    else {
        value = false;
    }

    // send to server
    $.ajax({
        url: '/Table/SearchCondition/")',
        data: { 'delete': value },
        type: 'post',
        timeout: 10000,
    })
        .done(function (results) {

            $('#Table').DataTable().clear().draw();

            $.each(results.data, function (idx, result) {
                var html = '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-grouptableid="' + result.GroupTableID + '" data-description="' + result.Des + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                    + '<a href="javascript:;" style="margin-left: 4px;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>';
                if (results.delete == true) {
                    html = '<a href="javascript:;" class="_recover btn btn-primary btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#RecoverModal" title="Phục hồi"><i class="fa fa-reply-all" aria-hidden="true"></i></i></a>';
                }
                else {}
                var rowNode = datatable.row.add([
                    result.ID,
                    result.Name,
                    result.GroupTableName,
                    result.OrderCount,
                    result.Des,
                    html,
                ]).draw(false).node();
                // Add attribute for this row <tr>
                $(rowNode)
                    .attr('id', 'item-' + result.ID);
            });
        })
        .fail(function (data) {
            alert('Tải dữ liệu thất bại! Vui lòng thử lại.');
        })
}

// Event: input search textbox id 'NameSearch'
$('#NameSearch').on('input', function () {
    SearchAdvanced();
})

// Event: change select option id 'GroupTableIDSearch'
$('#GroupTableIDSearch').on('change', function () {
    SearchAdvanced();
})

// Function: validate add, edit
function validate(element) {
    element.validate({
        rules: {
            Name: "required",
            'GroupTableID': { min: 1 },
        },
        messages: {
            Name: "Vui lòng nhập tên.",
            'GroupTableID': "Vui lòng chọn nhóm bàn.",
        },
        errorElement: 'span',
        errorClass: 'help-block',
        highlight: function (elements) {
            $(elements).closest('.form-group').removeClass('has-success');
            $(elements).closest('.form-group').addClass('has-error');
        },
        success: function (label) {
            $(label).closest('.form-group').removeClass('has-error');
            $(label).closest('.form-group').addClass('has-success');
            label.remove();
        }
    });
}

// Event: combobox to show number record of page
$('#lengthpage').on('change', function () {
    var s = $(this).val();
    datatable.page.len(s).draw(false);
});

// Event: input to search base
$("#search").keyup(function () {
    datatable.search(this.value).draw();
});

// Event: change radio name 'option' to view, search mode
$('[name=option]').on('change', function () {
    ClearConditionSearch();
});

// Event: click 'Thêm' button
$('#AddButton').on('click', function () {
    $('#AddForm')[0].reset();
    $('#AddModal').modal('show');
});

// Event: click 'Lưu' button of form id 'AddForm'
$('#AddForm .savebutton').on('click', function () {
    var form = $('#AddForm');

    // validate input of form
    validate(form);

    form.validate();
    if (form.valid() == false) {
        return;
    };
    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: form.serialize(),
        timeout: 10000,
    })
        .done(function (result) {
            if ($('#check').is(':checked')) {
                $('#AddModal').modal('hide');
                return;
            }
            else {}
            // Create new row
            var rowNode = datatable.row.add([
                result.ID,
                result.Name,
                result.GroupTableName,
                result.OrderCount,
                result.Des,
                '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-grouptableid="' + result.GroupTableID + '" data-description="' + result.Des + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                + '<a href="javascript:;" style="margin-left: 4px;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>',
            ]).draw(false).node();

            // Add attribute for this row <tr>
            $(rowNode)
                .attr('id', 'item-' + result.ID);

            // Hide popup add
            $('#AddModal').modal('hide');
        })
        .fail(function (data) {
            alert('Thêm thất bại! Vui lòng kiểm tra lại.');
        })
});

// Event: click 'Sửa' button
$('#BodyTable').on('click', '._edit', function () {
    var form = $('#EditForm');

    // Fill data to form
    form.find('[name=ID]').val($(this).data('id'));
    form.find('[name=Name]').val($(this).data('name'));
    form.find('[name=GroupTableID]').val($(this).data('grouptableid'));
    form.find('[name=Description]').val($(this).data('description'));

    $('#EditModal').modal('show');
});

// Event: click "Lưu" button of form id 'EditForm'
$('#EditForm .savebutton').on('click', function () {
    var form = $('#EditForm');

    validate(form);
    form.validate();
    if (form.valid() == false) {
        return;
    };

    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: form.serialize(),
        timeout: 10000,
    })
        .done(function (data) {
            // get value of input of form
            var id = form.find('[name=ID]').val();
            var name = form.find('[name=Name]').val();
            var grouptableid = form.find('[name=GroupTableID] option:selected');
            var description = form.find('[name=Description]').val();
            var table = $('#BodyTable');

            // set data to button "Sửa"
            var element = table.find('#item-' + id).find('._edit');
            element.data('id', id);
            element.data('name', name);
            element.data('grouptableid', grouptableid.val());
            element.data('description', description);

            // add row into datatable
            var row = datatable.row('#item-' + id);
            datatable.cell(row, 1).data(name).draw(false);
            datatable.cell(row, 2).data(grouptableid.text()).draw(false);
            datatable.cell(row, 4).data(description).draw(false);

            $('#EditModal').modal('hide');
        })
        .fail(function (data) {
            alert('Cập nhật thất bại! Vui lòng kiểm tra lại.');
        })
});

// Event: click 'Xóa' button
$('#BodyTable').on('click', '._delete', function () {
    var form = $('#DeleteForm');
    // set ID to form input name 'ID'
    form.find('[name=ID]').val($(this).data('id'));
    $('#DeleteModal').css('display', 'block');
});

// Event: click 'Đồng ý' button of form id 'DeleteForm'
$('#DeleteForm .savebutton').on('click', function () {
    var form = $('#DeleteForm');

    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: form.serialize(),
    })
        .done(function (data) {
            if (data == false) {
                alert('Xóa thất bại! Vui lòng kiểm tra lại.');
                return;
            }
            else{}

            // Remove row
            datatable.row('#item-' + form.find('[name=ID]').val()).remove().draw(false);
        })
        .fail(function (data) {
            alert('Xóa thất bại! Vui lòng kiểm tra lại.');
        })
});

// Event: click 'Phục hồi' button
$('#BodyTable').on('click', '._recover', function () {
    var form = $('#RecoverForm');

    // set ID to form input name 'ID'
    form.find('[name=ID]').val($(this).data('id'));
    $('#RecoverModal').css('display', 'block');
});

// Event: click 'Đồng ý' button of form id 'RecoverForm'
$('#RecoverForm .savebutton').on('click', function () {
    var form = $('#RecoverForm');

    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: form.serialize(),
    })
        .done(function (data) {
            if (data == false) {
                alert('Phục hồi thất bại! Vui lòng kiểm tra lại');
                return;
            }
            // Remove row
            datatable.row('#item-' + form.find('[name=ID]').val()).remove().draw(false);
        })
        .fail(function (data) {
            alert('Phục hồi thất bại! Vui lòng kiểm tra lại.');
        })
});