$(document).ready(function () {
    $("#MyModal").on('hidden.bs.modal', function () {
        $('#lblValidateName').css({ display: 'none' })
        $('#txtName').css({ 'border-color': '' });
        $('#txtName').val().trim();

        $('#btnCloseAddNewCity').css({ display: 'none' });
        $('#btnAddNewCity').css({ display: 'block' });

        $('#divAddNewCity').css({ display: 'none' });
        $('#txtName').prop('disabled', false);
        $('#txtDescription').prop('disabled', false);
        $('#txtNameCity').prop('disabled', false);
        $('#txtIsDelete').prop('disabled', false);
        $('#btnAdd').attr('disabled', false);
        $('#btnCancel').attr('disabled', false);

        $('#lblValidateNameCity').css({ display: 'none' })
        $('#txtInsertNameCity').css({ 'border': '' });
        $('#txtInsertNameCity').val('');
        $('#txtInsertDescriptionCity').val('');
    });
    //
    $('#txtIsDeleteOption').change(function () {
        var key = $('#txtIsDeleteOption').val();
        table.column(3).search(key).draw();
        if (key == "false") {
            $('.btnDelete').css("display", "");
            $('.btnRestore').css("display", "none");
            $('.btnEdit').css("display", "");
        } else {
            $('.btnDelete').css("display", "none");
            $('.btnEdit').css("display", "none");
            $('.btnRestore').css("display", "");
        }
    });

    function HasValue(val) {
        return val.replace(/\s+/, '').length;
    }

    $('#lengthpage').on('change', function () {
        var s = $(this).val();
        table.page.len(s).draw(false);
    });

    $("#search").keyup(function () {
        table.search(this.value).draw();
    });
    let checkBeforeInsert_Update = false;
    //validate txtName
    $('#txtName').focusout(function () {
        var name = $('#txtName').val();
        var reg = /[^0-9]/;
        if (HasValue(name) == false) {
            $('#lblValidateName').css({ display: 'block' })
            $('#lblValidateName').html("Tên không được để trống.");
            $('#txtName').css({ 'border-color': 'red' });
            $('#txtName').focus();
            checkBeforeInsert_Update = false;
        } else {
            if (reg.test(name)) {
                $('#lblValidateName').css({ display: 'none' })
                $('#txtName').css({ 'border-color': '' });
                $('#txtName').val().trim();
                checkBeforeInsert_Update = true;
            } else {
                $('#lblValidateName').css({ display: 'block' })
                $('#lblValidateName').html("Tên thành phố - tỉnh thành không hợp lệ.");
                $('#txtName').css({ 'border-color': 'red' });
                $('#txtName').focus();
                checkBeforeInsert_Update = false;
            }
        }
    });

    //======================start datatables===========================
    let table = $('#myTable').DataTable({
        language: {
            lengthMenu: "Hiện _MENU_ kết quả trên 1 trang",
            info: '',
            infoEmpty: "Không tìm thấy kết quả nào",
            infoFiltered: '',
            search: "_INPUT_",
            searchPlaceholder: "Tìm kiếm...",
            paginate: {
                'previous': '<span class="">&laquo;</span>',
                'next': '<span class="">&raquo;</span>'
            }
        },
        "ajax": {
            "url": '/City/LoadData/',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
                { className: 'ID', data: "ID", autoWidth: true, visible: false },
                { className: 'Name', data: "Name", autoWidth: true },
                { className: 'Description', data: "Description", autoWidth: true },
                { className: 'IsDelete', data: "IsDelete", autoWidth: true, visible: false },
                {
                    "targets": -1,
                    "data": null,
                    "defaultContent": '<a href="javascript:;" class="btn btn-info btn-xs btnEdit" title="Chỉnh sửa"><i class="fa fa-pencil"></i></a>' +
                                    '<a href="javascript:;" class="btn btn-danger btn-xs btnDelete" title="Xóa"><i class="fa fa-trash-o"></i></a>' +
                                    '<a href="javascript:;" class="btn btn-warning btn-xs btnRestore" title="Phục hồi" style="display:none"><i class="fa fa-refresh"></i></a>'
                }
        ],
        "columnDefs": [
                   { "display": 'block', "targets": [0, 3] }
        ],
        "sDom": 'tipr',
    });
    table.column(3).search("false").draw();
    table.page.len(5).draw(false);
    //======================end datatables===========================

    //event click paging datatable
    $('#myTable').on('draw.dt', function () {
        var key = $('#txtIsDeleteOption').val();
        if (key == "false") {
            $('.btnDelete').css("display", "");
            $('.btnRestore').css("display", "none");
            $('.btnEdit').css("display", "");
        } else {
            $('.btnDelete').css("display", "none");
            $('.btnEdit').css("display", "none");
            $('.btnRestore').css("display", "");
        }
    });


    let checkClickAddNew = false;
    //event click Thêm mới
    $("#btnAddNew").click(function () {
        checkClickAddNew = true;
        $("#form")[0].reset();
        $("#MyModal").modal();
        $('#ModalTitle').html("THÊM THÀNH PHỐ - TỈNH THÀNH");
    });

    //event click button 'Sua' in list city
    let idUpdate;
    let isDeleteUpdate;
    $('#myTable tbody').on('click', '.btnEdit', function () {
        checkClickAddNew = false;
        $("#form")[0].reset();
        $("#MyModal").modal();
        $('#ModalTitle').html("SỬA THÀNH PHỐ - TỈNH THÀNH");

        var row = $(this).closest("tr");
        idUpdate = table.row($(this).parents('tr')).data()["ID"];
        isDeleteUpdate = table.row($(this).parents('tr')).data()["IsDelete"];
        var name = row.find(".Name").html();
        var description = row.find(".Description").html();;

        $('#txtName').val(name);
        $('#txtDescription').val(description);;
    });

    //event click button Thêm in popup
    $('#btnAdd').click(function () {
        if ($('#txtName').val() == "") {
            checkBeforeInsert_Update = false;
            $('#lblValidateName').css({ display: 'block' })
            $('#lblValidateName').html("Tên không được để trống.");
            $('#txtName').css({ 'border-color': 'red' });
            $('#txtName').focus();
        } else {
            var reg = /[^0-9]/;
            if (reg.test($('#txtName').val())) {
                $('#lblValidateName').css({ display: 'none' })
                $('#txtName').css({ 'border-color': '' });
                $('#txtName').val().trim();
                checkBeforeInsert_Update = true;
            } else {
                $('#lblValidateName').css({ display: 'block' })
                $('#lblValidateName').html("Tên thành phố - tỉnh thành không hợp lệ.");
                $('#txtName').css({ 'border-color': 'red' });
                $('#txtName').focus();
                checkBeforeInsert_Update = false;
            }
        }
        if (checkBeforeInsert_Update == true) {
            //checkClickAddNew == true => insert
            if (checkClickAddNew == true) {
                var name = $('#txtName').val();
                var description = $('#txtDescription').val();

                var city = new Object();
                city.Name = name;
                city.Description = description;
                city.IsDelete = false;

                $.ajax({
                    type: "POST",
                    url: '/City/Insert/',
                    data: JSON.stringify(city),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (rs) {
                        if (rs != 0 && rs != -1) {
                            $("#MyModal").modal("hide");
                            EventSuccess(true);
                            table.ajax.reload(null, false);
                        } else {
                            if (rs == -1) {
                                $('#lblValidateName').css({ display: 'block' })
                                $('#lblValidateName').html("Tên thành phố - tỉnh thành đã tồn tại.");
                                $('#txtName').css({ 'border-color': 'red' });
                                $('#txtName').focus();
                                EventSuccess(false);
                            } else {
                                $("#MyModal").modal("hide");
                                EventSuccess(false);
                            }
                        }
                    }
                });

            }//checkClickAddNew == false => update
            else {
                var name = $('#txtName').val();
                var description = $('#txtDescription').val();

                var city = new Object();
                city.ID = idUpdate;
                city.Name = name;
                city.Description = description;
                city.IsDelete = isDeleteUpdate;

                $.ajax({
                    type: "POST",
                    url: '/City/Update/',
                    data: JSON.stringify(city),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (rs) {
                        if (rs == 1) {
                            $("#MyModal").modal("hide");
                            EventSuccess(true);
                            table.ajax.reload(null, false);
                        } else {
                            if (rs == -1) {
                                $('#lblValidateName').css({ display: 'block' })
                                $('#lblValidateName').html("Tên thành phố - tỉnh thành đã tồn tại.");
                                $('#txtName').css({ 'border-color': 'red' });
                                $('#txtName').focus();
                                EventSuccess(false);
                            } else {
                                $("#MyModal").modal("hide");
                                EventSuccess(false);
                            }
                        }
                    }
                });
            }
        }
    });

    //event click delete
    let idDelete;
    $('#myTable tbody').on('click', '.btnDelete', function () {
        $("#DeleteConfirmation").modal("show");
        var row = $(this).closest("tr");
        idDelete = table.row($(this).parents('tr')).data()["ID"];
        var name = row.find(".Name").html();
        $('#lblName').html(name);
    });

    $('#btnConfirmDelete').click(function () {
        $.ajax({
            type: "POST",
            url: '/City/Delete/',
            data: '{id: ' + idDelete + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (rs) {
                if (rs != 0) {
                    EventSuccess('Đã xóa');
                    table.ajax.reload(null, false);
                } else {
                    EventError('Lỗi!');
                }
            }

        });
    });

    //event click delete
    $('#myTable tbody').on('click', '.btnRestore', function () {
        var id = table.row($(this).parents('tr')).data()["ID"];
        $.ajax({
            type: "POST",
            url: '/City/Restore/',
            data: '{id: ' + id + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (rs) {
                if (rs != 0) {
                    table.ajax.reload(null, false);
                    table.column(3).search("false").draw();
                    $('#txtIsDeleteOption').val("false");
                    EventSuccess(true);
                } else {
                    EventSuccess(false);
                }
            }
        });
    });
    function EventSuccess(status) {
        if (status) {
            $('#StatusSuccess').addClass('fa-check-circle-o');
            $('#StatusSuccess').removeClass('fa-times');
            $('#StatusSuccess').html('Thành công');
            $('#StatusSuccess').show('slow').delay(1500).hide('slow');
            $('#StatusSuccess').css({ display: 'block' });
            $('#divStatusSuccess').addClass('isa_success');
            $('#divStatusSuccess').removeClass('isa_error');

        } else {
            $('#StatusSuccess').addClass('fa-times');
            $('#StatusSuccess').removeClass('fa-check-circle-o');
            $('#StatusSuccess').html('Thất bại');
            $('#StatusSuccess').show('slow').delay(1500).hide('slow');
            $('#StatusSuccess').css({ display: 'block' });
            $('#divStatusSuccess').addClass('isa_error');
            $('#divStatusSuccess').removeClass('isa_success');
        }
    }
});