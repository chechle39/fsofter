$(document).ready(function () {

    //create datatable, apply for table id 'Table' 
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

            //Notify when not found result record
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
        //   "dom": 'l<"top">rt<"bottom"p><"clear">',
    });
    //datatable.column(3).search("false").draw();


    // click advanced search button
    // toggle form for search fields
    $('#AdvancedSearchButton').on('click', function () {
        $('#advancedsearch').fadeToggle();
    });

    //combobox to show number record of page
    $('#lengthpage').on('change', function () {
        var s = $(this).val();
        datatable.page.len(s).draw(false);
    });

    //input to search base
    $("#search").keyup(function () {
        datatable.search(this.value).draw();
    });

    //Advanced search
    function SearchAdvanced() {
        var form = $('#SearchAdvancedForm');

        //Set value to input named 'delete'
        //var deletecheck = form.find('[name=delete]');
        //if ($('#check').is(':checked')) {//Check radio id 'check'
        //    deletecheck.val(true);
        //}
        //else {
        //    deletecheck.val(false);
        //}

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),//get all variable of form to ajax
        })
        .done(function (results) {
            //Clear datatable data
            $('#Table').DataTable().clear().draw();

            //add row into datatable
            $.each(results.items, function (idx, result) {

                // action html
                var action = '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-grouptableid="' + result.GroupTableID + '" data-description="' + result.Des + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                    + '<a href="javascript:;" style="margin-left: 4px;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>';

                if (results.isDelete == true) {
                    html = '<a href="javascript:;" class="_recover btn btn-primary btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#RecoverModal" title="Phục hồi"><i class="fa fa-reply-all" aria-hidden="true"></i></i></a>';
                }

                // add fields to datarow
                var rowNode = datatable.row.add([
                    result.ID,
                    result.CreatedDate,
                    result.MaterialName,
                    result.Inventory,
                    result.UnitPrice,
                    result.EmployeeName,
                    result.Type,
                    result.Description,
                    action,
                ]).draw(false).node();

                //Add attribute for this row <tr>
                $(rowNode).attr('id', 'item-' + result.ID);
            });
        })
        .fail(function (data) {
            alert('Tải dữ liệu thất bại! Vui lòng thử lại.');
        })
    }

    function ChangeView() {

        //get option radio
        var value = $('#viewOption [name=option]:checked').val();
        $('#SearchAdvancedForm name[IsDelete]').val(value);

        //send to server
        $.ajax({
            url: '/MaterialLog/Search',
            data: "Type=-1&IsDelete="+value, //$('#SearchAdvancedForm').serialize(),
            type: 'get'
        })
        .done(function (results) {

            // clear current data in datatable
            $('#Table').DataTable().clear().draw();

            $.each(results, function (idx, result) {

                // action html
                var action = '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-grouptableid="' + result.GroupTableID + '" data-description="' + result.Des + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                    + '<a href="javascript:;" style="margin-left: 4px;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>';

                if (results.isDelete == true) {
                    html = '<a href="javascript:;" class="_recover btn btn-primary btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#RecoverModal" title="Phục hồi"><i class="fa fa-reply-all" aria-hidden="true"></i></i></a>';
                }

                // add fields to datarow
                var rowNode = datatable.row.add([
                    result.ID,
                    result.CreatedDate,
                    result.MaterialName,
                    result.Inventory,
                    result.UnitPrice,
                    result.EmployeeName,
                    result.Type,
                    result.Description,
                    action,
                ]).draw(false).node();

                //Add attribute for this row <tr>
                $(rowNode)
                    .attr('id', 'item-' + result.ID);
            });

        })
        .fail(function (data) {
            alert('Tải dữ liệu thất bại! Vui lòng thử lại.');
        });
    }

    //Option view, search
    $('[name=option]').on('change', function () {
        ChangeView();
    });

    // search in advance
    $('#btnAdvancedSearch').on('click', function () {
        console.log('test');
        //send fields to server
        $.ajax({
            url: '/MaterialLog/Search',
            data: $('#SearchAdvancedForm').serialize(),
            type: 'get'
        })
        .done(function (results) {

            // clear current data in datatable
            $('#Table').DataTable().clear().draw();

            $.each(results, function (idx, result) {

                // action html
                var action = '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-grouptableid="' + result.GroupTableID + '" data-description="' + result.Des + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                    + '<a href="javascript:;" style="margin-left: 4px;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>';

                if (results.isDelete == true) {
                    html = '<a href="javascript:;" class="_recover btn btn-primary btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#RecoverModal" title="Phục hồi"><i class="fa fa-reply-all" aria-hidden="true"></i></i></a>';
                }

                // add fields to datarow
                var rowNode = datatable.row.add([
                    result.ID,
                    result.CreatedDate,
                    result.MaterialName,
                    result.Inventory,
                    result.UnitPrice,
                    result.EmployeeName,
                    result.Type,
                    result.Description,
                    action,
                ]).draw(false).node();

                //Add attribute for this row <tr>
                $(rowNode)
                    .attr('id', 'item-' + result.ID);
            });

        })
        .fail(function (data) {
            alert('Tải dữ liệu thất bại! Vui lòng thử lại.');
        });
    });

    //validate add
    function validate(element) {
        element.validate({
            rules: {
                CreatedDate: {
                    required: true
                },
                Inventory: {
                    required: true,
                    min: 1
                },
                UnitPrice: {
                    required: true,
                    min: 1
                }
            },
            messages: {
                CreatedDate: {
                    required: "Vui lòng nhập ngày ghi",
                },
                Inventory: {
                    required: "Vui lòng nhập số lượng tồn",
                    min: "Số lượng phải lớn hơn 0"
                },
                UnitPrice: {
                    required: "Vui lòng nhập đơn giá",
                    min: "Đơn giá phải lớn hơn 0"
                }
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

    //Add
    $('#AddButton').on('click', function () {
        // clear validate format
        //var validator = $("#AddForm").validate();
        //validator.resetForm();

        $('#AddForm')[0].reset();
        $('#AddModal').modal('show');
    });

    $('#AddForm .savebutton').on('click', function () {

        var form = $('#AddForm');

        //validate input of form
        validate(form);

        form.validate();
        if (form.valid() == false) {
            return;
        };

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize()
        })
        .done(function (result) {

            if (result) {
                //Create new row
                var rowNode = datatable.row.add([
                    result.ID,
                    result.CreatedDate,
                    result.MaterialName,
                    result.Inventory,
                    result.UnitPrice,
                    result.EmployeeName,
                    result.Type,
                    result.Description,
                    '<a href="javascript:;" class="_edit btn btn-info btn-xs" data-id="' + result.ID + '" data-name="' + result.Name + '" data-grouptableid="' + result.GroupTableID + '" data-description="' + result.Des + '" title="Sửa"><i class="fa fa-pencil"></i></a>'
                    + '<a href="javascript:;" style="margin-left: 4px;" class="_delete btn btn-danger btn-xs" data-id="' + result.ID + '" data-toggle="modal" data-target="#DeleteModal" title="Xóa"><i class="fa fa-trash-o"></i></a>',
                ]).draw(false).node();

                //Add attribute for this row <tr>
                $(rowNode)
                    .attr('id', 'item-' + result.ID);

                //Hide popup add
                $('#AddModal').modal('hide');

            } else {
                alert('Thêm thất bại! Vui lòng kiểm tra lại.');
            }
        })
        .fail(function (data) {
            alert('Thêm thất bại! Vui lòng kiểm tra lại.');
        })
    });

    // convert json date
    function convertJsonDate(data) {
        var date = new Date(data);
        var dateString = '';
        dateString += date.getFullYear() + '-';
        dateString += ('0' + (1 + date.getMonth())).slice(-2) + '-';
        dateString += ('0' + date.getDate()).slice(-2);
        return dateString;
    }

    //Edit
    $('#BodyTable').on('click', '._edit', function () {
        //Fill data to form
        var popup = $('#EditForm');
        var id = $(this).data('id');
        var isDelete = false;

        if ($('#check').is(':checked') == true) {
            isDelete = true;
        }
        
        $.ajax({
            type: "get"
            , url: "/MaterialLog/Detail"
            , data: { id: id }
        }).done(function (data) {
            console.log(convertJsonDate(data.CreatedDate));
            if (data != null) {
                popup.find('[name=ID]').val(data.ID);
                popup.find('[name=MaterialID]').val(data.MaterialID);
                popup.find('[name=CreatedDate]').val(convertJsonDate(data.CreatedDate));
                popup.find('[name=Inventory]').val(data.Inventory);
                popup.find('[name=UnitPrice]').val(data.UnitPrice);
                popup.find('[name=Description]').val(data.Description);
                popup.find('[name=Type]').val(data.Type + "");
                popup.find('[name=IsDelete]').val(isDelete);
                popup.modal('show');
            } else {
                alert('Có lỗi xảy ra, vui lòng thử lại!');
            }
        });

        $('#EditModal').modal('show');
    });

    //"Lưu" click
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
            data: form.serialize()
        })
        .done(function (data) {
            console.log(data);
            if (data) {
                var table = $('#BodyTable');

                //add row into datatable
                var row = datatable.row('#item-' + data.ID);
                datatable.cell(row, 0).data(data.ID).draw(false);
                datatable.cell(row, 1).data(data.CreatedDate).draw(false);
                datatable.cell(row, 2).data(data.MaterialName).draw(false);
                datatable.cell(row, 3).data(data.Inventory).draw(false);
                datatable.cell(row, 4).data(data.UnitPrice).draw(false);
                datatable.cell(row, 5).data(data.EmployeeName).draw(false);
                datatable.cell(row, 6).data(data.Type).draw(false);
                datatable.cell(row, 7).data(data.Description).draw(false);

                $('#EditModal').modal('hide');
            } else {
                alert('Cập nhật thất bại! Vui lòng kiểm tra lại.');
            }
        })
        .fail(function (data) {
            alert('Cập nhật thất bại! Vui lòng kiểm tra lại.');
        })
    });

    //Delete
    $('#BodyTable').on('click', '._delete', function () {
        var form = $('#DeleteForm');
        //set ID to form input name 'ID'
        form.find('[name=ID]').val($(this).data('id'));
        $('#DeleteModal').css('display', 'block');
    });


    $('#DeleteForm .savebutton').on('click', function () {
        var form = $('#DeleteForm');
        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
        })
        .done(function (data) {
            if (data) {
                //Remove row
                datatable.row('#item-' + form.find('[name=ID]').val()).remove().draw(false);
            } else {
                alert('Xóa thất bại! Vui lòng kiểm tra lại.');
            }
        })
        .fail(function (data) {
            alert('Xóa thất bại! Vui lòng kiểm tra lại.');
        })
    });


    /*
    //Recover
    $('#BodyTable').on('click', '._recover', function () {
        var form = $('#RecoverForm');
        //set ID to form input name 'ID'
        form.find('[name=ID]').val($(this).data('id'));
        $('#RecoverModal').css('display', 'block');
    });


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
                //Remove row
                datatable.row('#item-' + form.find('[name=ID]').val()).remove().draw(false);
            })
            .fail(function (data) {
                alert('Phục hồi thất bại! Vui lòng kiểm tra lại.');
            })
    });
    */
});

