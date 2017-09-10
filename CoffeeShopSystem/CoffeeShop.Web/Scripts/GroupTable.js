// Create and apply datatable for table id 'Table'
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

// Event: change select option id 'lengthpage'
$('#lengthpage').on('change', function () {
    var s = $(this).val();

    datatable.page.len(s).draw(false);
});

// Event: input search textbox id 'search'
$("#search").keyup(function () {
    datatable.column('1').search(this.value).draw();
});

// Event: click 'Chuyển tùy chọn tìm kiếm' button
$('#AddvancedButton').on('click', function () {
    ClearConditionSearch();
    var click = $(this);

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

// Function: advanced search
function SearchAdvanced() {
    var form = $('#SearchAdvancedForm');
    var deletecheck = form.find('[name=delete]');
    if ($('#check').is(':checked')) {
        deletecheck.val(true);
    }
    else {
        deletecheck.val(false);
    }
    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: form.serialize(),
    })
        .done(function (results) {
            $('#Table').DataTable().clear().draw();

            $.each(results.data, function (idx, result) {
                var html = '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-surcharge="' + result.Surcharge + '" data-description="' + result.Description + '" data-shopid="' + result.ShopID + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                    + '<a href="javascript:;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>';
                if (results.delete == true) {
                    html = '<a href="javascript:;" class="_recover btn btn-primary btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#RecoverModal" title="Phục hồi"><i class="fa fa-reply-all" aria-hidden="true"></i></i></a>';
                }
                else {}

                // Add row to datatable
                var rowNode = datatable.row.add([
                    result.ID,
                    result.Name,
                    result.Surcharge,
                    result.TableCount,
                    result.Description,
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

// Function: clear all input fields to search
function ClearConditionSearch() {
    $('#search').val('');
    datatable.search('').draw();
    $('#NameSearch').val('');
    $('#FromSearch').val('0');
    $('#ToSearch').val('0');

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
        url: '/GroupTable/SearchCondition/',
        data: { 'delete': value },
        type: 'post',
        timeout: 10000,
    })
        .done(function (results) {
            $('#Table').DataTable().clear().draw();
            $.each(results.data, function (idx, result) {
                var html = '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-surcharge="' + result.Surcharge + '" data-description="' + result.Description + '" data-shopid="' + result.ShopID + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                    + '<a href="javascript:;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>';
                if (results.delete == true) {
                    html = '<a href="javascript:;" class="_recover btn btn-primary btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#RecoverModal" title="Phục hồi"><i class="fa fa-reply-all" aria-hidden="true"></i></i></a>';
                }
                else {}

                // Add row to datatable
                var rowNode = datatable.row.add([
                    result.ID,
                    result.Name,
                    result.Surcharge,
                    result.TableCount,
                    result.Description,
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

// Event: input search textbox id 'FromSearch'
$('#FromSearch').on('input', function () {
    SearchAdvanced();
})

// Event: input search textbox id 'ToSearch'
$('#ToSearch').on('input', function () {
    SearchAdvanced();
})

// Event: change select option id 'option' to change view mode
$('[name=option]').on('change', function () {
    ClearConditionSearch()
});

// Function: validate add, edit
function validate(element) {
    element.validate({
        rules: {
            Name: "required",
            Surcharge: { min: 0 },
        },
        messages: {
            Name: "Vui lòng nhập tên.",
            Surcharge: { min: 'Vui lòng nhập phụ thu lớn hơn 0' },
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

// Event: click 'Thêm' button
$('#AddButton').on('click', function () {
    $('#AddForm')[0].reset();
    $('#AddModal').modal('show');
});

// Event: click 'Lưu' button of form id 'AddForm'
$('#AddForm .savebutton').on('click', function () {
    var form = $('#AddForm');

    validate(form);

    form.validate();
    if (form.valid() == false) {
        return;
    };
    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),
        data: form.serialize(),
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
                result.Surcharge,
                result.TableCount,
                result.Description,
                '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-surcharge="' + result.Surcharge + '" data-description="' + result.Description + '" data-shopid="' + result.ShopID + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                + '<a href="javascript:;" style="margin-left: 4px;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>',
            ]).draw(false).node();

            // Add attribute for this row <tr>
            $(rowNode).attr('id', 'item-' + result.ID);
            $('#AddModal').modal('hide');
        })
        .fail(function (data) {
            alert('Thêm thất bại! Vui lòng kiểm tra lại.');
        })
});

// Event: click 'Sửa' button
$('#BodyTable').on('click', '._edit', function () {
    var form = $('#EditForm');

    form.find('[name=ID]').val($(this).data('id'));
    form.find('[name=Name]').val($(this).data('name'));
    form.find('[name=Surcharge]').val($(this).data('surcharge'));
    form.find('[name=Description]').val($(this).data('description'));
    form.find('[name=ShopID]').val($(this).data('shopid'));

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
            var id = form.find('[name=ID]').val();
            var name = form.find('[name=Name]').val();
            var surcharge = form.find('[name=Surcharge]').val();
            var description = form.find('[name=Description]').val();
            var shopid = form.find('[name=ShopID]').val();

            var table = $('#BodyTable');

            // Set data to a element
            var element = table.find('#item-' + id).find('._edit');
            element.data('id', id);
            element.data('name', name);
            element.data('surcharge', surcharge);
            element.data('description', description);
            element.data('shopid', shopid);

            // Update data (of form to row selecting) to datatable
            var row = datatable.row('#item-' + id);
            datatable.cell(row, 1).data(name).draw(false);
            datatable.cell(row, 2).data(surcharge).draw(false);
            datatable.cell(row, 4).data(description).draw(false);

            $('#EditModal').modal('hide');
        })
        .fail(function (data) {
            alert('Cập nhật thất bại! Vui lòng kiểm tra lại.');
        })
});

// Event: click 'Xóa' button
$('#BodyTable').on('click', '._delete', function () {
    // Set ID to input of form
    var form = $('#DeleteForm');

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

            // Remove row
            datatable.row('#item-' + form.find('[name=ID]').val()).remove().draw(false);

        })
        .fail(function (data) {
            alert('Xóa thất bại! Vui lòng kiểm tra lại.');
        })
});

// Event: click 'Phục hồi' button
$('#BodyTable').on('click', '._recover', function () {

    // Set ID to input of form
    var form = $('#RecoverForm');
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
                alert('Phục hồi thất bại! Vui lòng kiểm tra lại.');
                return;
            }

            //Remove row
            datatable.row('#item-' + form.find('[name=ID]').val()).remove().draw(false);
        })
        .fail(function (data) {
            alert('Phục hồi thất bại! Vui lòng kiểm tra lại');
        })
});