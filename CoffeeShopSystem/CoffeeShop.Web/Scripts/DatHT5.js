//Khai báo sử dụng data table

var mainTable = $('#mainTable').DataTable({
    "language": {
        "lengthMenu": 'Hiển thị _MENU_' ,
        "zeroRecords": "Không tìm thấy!",
        "info": "",
        "infoEmpty": "",
        "infoFiltered": "",
        "search": "",
        "searchPlaceholder": "Từ khóa",
        "paginate": {
            "next": '<i class="fa fa-angle-right" aria-hidden="true"></i>',
            "previous": '<i class="fa fa-angle-left" aria-hidden="true"></i>'
        },
    },
    "sDom": 'tipr',
});
$('#lengthpage').on('change', function () {
    var s = $(this).val();
    mainTable.page.len(s).draw(false);
});

$("#baseSearch").keyup(function () {
    mainTable.search(this.value).draw();
});


//AddForm validation
//Thêm validation UniqueUser

jQuery.validator.addMethod("UniqueUser", function (value, element, elementID) {
    var url = '/ShopUser/IsUsernameUnique/';
    var ret = false;
    var id = $(elementID).val();
    $.ajax({
        type: 'POST',
        url: url,
        data: { 'input': value, 'userID': id },
        async: false,
        success: function (data) {
            ret = data;
            console.log(data);
        }
    });
    if (ret == "True") {
        return true;
    }
    if (ret == "False") {
        return false;
    }
}, "Username đã tồn tại");

//Thêm validation UniqueEmail
jQuery.validator.addMethod("UniqueEmail", function (value, element, elementID) {
    var url = '/ShopUser/IsEmailUnique/';
    var ret = false;
    var id = $(elementID).val();
    $.ajax({
        type: 'POST',
        url: url,
        data: { 'input': value, 'userID': id },
        async: false,
        success: function (data) {
            //alert(data);
            ret = data;
            console.log(data);
        }
    });
    if (ret == "True") {
        return true;
    }
    if (ret == "False") {
        return false;
    }
}, "Email đã tồn tại");

// Rule for combobox
jQuery.validator.addMethod("ValueNotEquals", function (value, element, comparator) {
    return comparator !== value;
}, "Giá trị không được bằng comparator");

//Validate AddForm
$('#AddForm').validate({
    //Định dạng thông báo lỗi
    highlight: function (element) {
        $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-clock',

    //Các trường cần validate và nội dung validate
    rules: {
        Name: {
            required: true,
        },
        Username: {
            required: true,
            minlength: 5,
            UniqueUser: '#AddShopUserIDTextbox',
        },
        Email: {
            required: true,
            email: true,
            UniqueEmail: '#AddShopUserIDTextbox',
        },
        Password: {
            required: true,
        },
        RePassword: {
            required: true,
            equalTo: '#PasswordTextbox'
        },
        role: {
            ValueNotEquals: "0",
            required: true,
        },
        City: {
            required: true,
            ValueNotEquals: "0",
        },
        District: {
            required: true,
            ValueNotEquals: "0",
        },
        WardID: {
            required: true,
            ValueNotEquals: "0",
        },
        BirthDay: {
            required: true,
        },
        Sex: {
            ValueNotEquals: "0",
            required: true
        },
    },

    //Error messages
    messages: {
        Name: {
            required: "Vui lòng nhập họ tên",
        },
        Username: {
            required: "Vui lòng nhập username",
            minlength: "Username tối thiểu 5 kí tự",
            UniqueUser: "Username đã tồn tại"
        },
        Email: {
            required: "Vui lòng nhập email",
            email: "Email không hợp lệ",
            UniqueEmail: "Email đã tồn tại"
        },
        Password: {
            required: "Vui lòng nhập mật khẩu",
        },
        RePassword: {
            required: "Vui lòng nhập lại mật khẩu",
            equalTo: "Sai mật khẩu nhập lại"
        },
        role: {
            ValueNotEquals: "Vui lòng chọn vị trí",
            required: "Vui lòng chọn vị trí",
        },
        City: {
            required: "Vui lòng chọn thành phố",
            ValueNotEquals: "Vui lòng chọn thành phố",
        },
        District: {
            required: "Vui lòng chọn phường",
            ValueNotEquals: "Vui lòng chọn phường",
        },
        WardID: {
            required: "Vui lòng chọn quận",
            ValueNotEquals: "Vui lòng chọn quận",
        },
        BirthDay: {
            required: "Vui lòng chọn ngày sinh"
        },
        Sex: {
            ValueNotEquals: "Vui lòng chọn giới tính",
            required: "Vui lòng chọn giới tính",
        },
    }
});

//EditForm validation
$('#EditForm').validate({
    //Định dạng thông báo lỗi
    highlight: function (element) {
        $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-clock',

    //Các trường cần validate và nội dung validate
    rules: {
        Name: {
            required: true,
        },
        Username: {
            required: true,
            minlength: 5,
            UniqueUser: '#EditShopUserIDTextbox',
        },
        Email: {
            required: true,
            email: true,
            UniqueEmail: '#EditShopUserIDTextbox',
        },
        Password: {
            required: true,
        },
        RePassword: {
            required: true,
            equalTo: '#EditPasswordTextbox'
        },
        role: {
            required: true,
        },
        WardID: {
            required: true,
        },
        BirthDay: {
            required: true,
        },
        Sex: {
            required: true
        },
    },

    //Error messages
    messages: {
        Name: {
            required: "Vui lòng nhập họ tên",
        },
        Username: {
            required: "Vui lòng nhập username",
            minlength: "Username tối thiểu 5 kí tự",
            UniqueUser: "Username đã tồn tại"
        },
        Email: {
            required: "Vui lòng nhập email",
            email: "Email không hợp lệ",
            UniqueEmail: "Email đã tồn tại"
        },
        Password: {
            required: "Vui lòng nhập mật khẩu",
        },
        RePassword: {
            required: "Vui lòng nhập lại mật khẩu",
            equalTo: "Sai mật khẩu nhập lại"
        },
        role: {
            required: "Vui lòng chọn vị trí",
        },
        WardID: {
            required: "Vui lòng chọn quận",
        },
        BirthDay: {
            required: "Vui lòng chọn ngày sinh"
        },
        Sex: {
            required: "Vui lòng chọn giới tính",
        },
    }
});

//#AddForm combobox change
//City combobox change
//Load lại District theo cityID tương ứng
$("#CityList").change(function () {
    var selectedCity = this.value;
    $('#DistrictList').empty();
    $('#WardList').empty();
    $.ajax({
        url: '/ShopUser/DistrictListByCityID/',
        type: 'GET',
        datatype: 'json',
        data: { 'cityID': selectedCity },
    })
    .done(function (result) {
        $('#DistrictList').append(new Option('Quận', 0));
        $.each(result, function (index, data) {
            $('#DistrictList').append(new Option(data.Name, data.ID));
        });
    })
    .fail(function (data) {
        alert('Load district fail');
    });
});

//District combobox change
//Load lại Ward theo districtID tương ứng
$("#DistrictList").change(function () {
    var selectedDistrict = this.value;
    $('#WardList').empty();
    $.ajax({
        url: '/ShopUser/WardListByDistrictID/',
        type: 'GET',
        datatype: 'json',
        data: { 'districtID': selectedDistrict },
    })
    .done(function (result) {
        $('#WardList').append(new Option('Phường', 0));
        $.each(result, function (index, data) {
            $('#WardList').append(new Option(data.Name, data.ID));
        });
    })
    .fail(function (data) {
        alert('Load wardfail');
    });
});

//#EditForm combobox change
//City combobox change
//Load lại District theo cityID tương ứng
$("#EditCityList").change(function () {
    var selectedCity = this.value;
    $('#EditDistrictList').empty();
    $('#EditWardList').empty();
    $.ajax({
        url: '/ShopUser/DistrictListByCityID/',
        type: 'GET',
        datatype: 'json',
        data: { 'cityID': selectedCity },
    })
    .done(function (result) {
        $('#EditDistrictList').append(new Option('Quận', 0));
        $.each(result, function (index, data) {
            $('#EditDistrictList').append(new Option(data.Name, data.ID));
        });
    })
    .fail(function (data) {
        alert('Load district fail');
    });
});

//District combobox change
//Load lại Ward theo districtID tương ứng
$("#EditDistrictList").change(function () {
    var selectedDistrict = this.value;
    $('#EditWardList').empty();
    $.ajax({
        url: '/ShopUser/WardListByDistrictID/',
        type: 'GET',
        datatype: 'json',
        data: { 'districtID': selectedDistrict },
    })
    .done(function (result) {
        $('#EditWardList').append(new Option('Phường', 0));
        $.each(result, function (index, data) {
            $('#EditWardList').append(new Option(data.Name, data.ID));
        });
    })
    .fail(function (data) {
        alert('Load city fail');
    });
});

//Click submit #AddForm
$('#SubmitAdd').on('click', function () {

    //Check field validation
    var addform = $("#AddForm");
    addform.validate();
    if (addform.valid() == true) {
        $.ajax({
            url: '/ShopUser/Add/',
            type: 'POST',
            datatype: 'json',
            data: $('#AddForm').serialize(),
        })
        .done(function (result) {

            //Sử dụng phương thức add của datatable, add row vào table
            var rownode = mainTable.row.add([
                    result.ID,
                    result.Name,
                    result.Username,
                    result.Email,
                    result.DetailAddress + ' - ' + result.Ward + ' - ' + result.District + ' - ' + result.City,
                    '<a class="btn btn-info btn-xs edit" data-shopuserid="' + result.ID + '"><i class="fa fa-pencil"></i></a>'
                    + '<a class="btn btn-danger btn-xs delete" data-id="' + result.ID + '"><i class="fa fa-trash"></i></a>',
            ]).draw(false).node();
            $(rownode).attr('id', 'tr-' + result.ID);
            $(rownode).find('td:eq(5)')
                .attr('align', 'center');
            $('#AddForm')[0].reset();
            $('#AddModal').modal('hide');
            alert("Thêm thành công");
        })
        .fail(function (data) {
            $('#AddModal').modal('hide');
            alert('Thêm thất bại');
        });
    }
});

//Nhấn nút xóa trên table
$('#tbody').on('click', '.delete', function () {
    $('#DeleteConfirmation').modal('show');
    var btn = $(this);
    var id = btn.data('id');
    $('#btnConfirmDelete').data('id', id);
});

//Nhấn nút xác nhận xóa trên pop-up
$('#btnConfirmDelete').on('click', function () {
    var btn = $(this);
    var id = btn.data('id');
    $.ajax({
        url: '/ShopUser/Delete/',
        type: 'POST',
        data: { 'shopUserID': id },
    })
    .done(function (data) {
        var trid = "#tr-" + id;
        $(trid).addClass("selected");
        var rows = mainTable
            .rows('.selected')
            .remove()
            .draw(false);
        $('#DeleteConfirmation').modal('hide');
        alert('Xóa thành công');

    })
    .fail(function (data) {
        $('#DeleteConfirmation').modal('hide');
        alert('Xóa thất bại');
    });
});

//Nhấn nút edit trên table

$('#tbody').on('click', '.edit', function () {
    $('#EditModal').modal('show');
    var btn = $(this);
    var id = btn.data('shopuserid');
    var selectedCity = 0;
    var selectedDistrict = 0;
    var selectedWard = 0;
    $('#EditDistrictList').empty();
    $('#EditWardList').empty();
    $.ajax({
        url: '/ShopUser/Detail/',
        type: 'GET',
        dataType: 'json',
        data: { 'shopUserID': id },
    })
    .done(function (data) {

        //Gán selected value vào City, Ward, DIstrict combobox trên #EditForm
        selectedCity = data.City;
        selectedDistrict = data.District;
        selectedWard = data.Ward;
        {
            $.ajax({
                url: '/ShopUser/DistrictListByCityID/',
                type: 'GET',
                datatype: 'json',
                data: { 'cityID': selectedCity },
            })
            .done(function (result) {
                $('#EditDistrictList').append(new Option('Quận', 0));
                $.each(result, function (index, data) {
                    $('#EditDistrictList').append(new Option(data.Name, data.ID));
                });
                $('#EditDistrictList').val(selectedDistrict);
            })
            .fail(function (data) {
                alert('Set disitrct fail');
            });
        }
        //
        {
            $.ajax({
                url: '/ShopUser/WardListByDistrictID/',
                type: 'GET',
                datatype: 'json',
                data: { 'districtID': selectedDistrict },
            })
            .done(function (result) {
                $('#EditWardList').append(new Option('Phường', 0));
                $.each(result, function (index, data) {
                    $('#EditWardList').append(new Option(data.Name, data.ID));
                });
                $('#EditWardList').val(selectedWard);
            })
            .fail(function (data) {
                alert('Set ward fail');
            });
        }
        
        //Gán value vào control trên #EditForm
        $('#EditUserIDTextbox').val(data.UserID);
        $('#EditShopUserIDTextbox').val(data.ShopUserID);
        $('#EditNameTextbox').val(data.Name);
        $('#EditUsernameTextbox').val(data.Username);
        $('#EditEmailTextbox').val(data.Email);
        $('#EditPasswordTextbox').val(data.Password);
        $('#EditRole').val(data.RoleID);
        $('#EditRoleDescriptionTextbox').val(data.RoleDescription);
        $('#EditCityList').val(data.City);
        $('#EditDetailAddressTextbox').val(data.DetailAddress);
        $('#EditBirthDayTextbox').val(data.BirthDay);
        $('#EditGenderList').val(data.Sex);
        $('#EditDescriptionTextbox').val(data.UserDescription);
    })
    .fail(function (data) {
        alert('Lấy detail thất bại');
    });

});

//Click submit EditForm
$('#SubmitEdit').on('click', function () {

    //Check field validation
    var editform = $("#EditForm");
    editform.validate();
    if (editform.valid() == true) {
        $.ajax({
            url: '/ShopUser/Edit/',
            type: 'POST',
            data: $('#EditForm').serialize(),
        })
        .done(function (result) {
            //Cập naht65 lại row tương ứng với row vừa chọn
            var row = mainTable.row('#tr-' + result.ID);
            mainTable.cell(row, 1).data(result.Name).draw(false);
            mainTable.cell(row, 2).data(result.Username).draw(false);
            mainTable.cell(row, 3).data(result.Email).draw(false);
            mainTable.cell(row, 4).data(result.DetailAddress + ' - ' + result.Ward + ' - ' + result.District + ' - ' + result.City).draw(false);

            $('#EditForm')[0].reset();
            $('#EditModal').modal('hide');
            alert("Sửa thành công");
        })
        .fail(function (data) {
            $('#EditModal').modal('hide');
            alert('Sửa thất bại');
        });
    }

});

//Xem trang theo IsDelete= true/false
$('input:radio[name="viewOption"]').change(function () {
    var radio = $(this);

    $("#baseSearch").val('');
    mainTable.search('').draw(false);

    //Xem danh sách đã xóa
    if (radio.val() == 'true') {
        $.ajax({
            url: '/ShopUser/ViewDeleted/',
            type: 'GET',
            data: '',
        })
        .done(function (result) {
            $('#mainTable').DataTable().clear().draw();
            $.each(result, function (index, data) {
                var rownode = mainTable.row.add([
                        data.ID,
                        data.Name,
                        data.Username,
                        data.Email,
                        data.DetailAddress + ' - ' + data.Ward + ' - ' + data.District + ' - ' + data.City,
                        '<a class="btn btn-info btn-xs recover" title="Phục hồi" data-id="' + data.ID + '"><i class="fa fa-reply-all"></i></a>',
                ]).draw(false).node();
                $(rownode).attr('id', 'tr-' + data.ID);
                $(rownode).find('td:eq(5)')
                    .attr('align', 'center');
            });
        })
        .fail(function (data) {
            alert('Load fail');
        });
    }

    //Xem danh sách đang quản lý
    else if (radio.val() == 'false') {
        $.ajax({
            url: '/ShopUser/ViewCurrent/',
            type: 'GET',
            data: '',
        })
        .done(function (result) {
            $('#mainTable').DataTable().clear().draw();
            $.each(result, function (index, data) {
                var rownode = mainTable.row.add([
                        data.ID,
                        data.Name,
                        data.Username,
                        data.Email,
                        data.DetailAddress + ' - ' + data.Ward + ' - ' + data.District + ' - ' + data.City,
                        '<a class="btn btn-info btn-xs edit" data-shopuserid="' + data.ID + '"><i class="fa fa-pencil"></i></a>'
                        + '<a class="btn btn-danger btn-xs delete" data-id="' + data.ID + '"><i class="fa fa-trash"></i></a>',
                ]).draw(false).node();
                $(rownode).attr('id', 'tr-' + data.ID);
                $(rownode).find('td:eq(5)')
                    .attr('align', 'center');
            });
        })
        .fail(function (data) {
            alert('Load fail');
        });
    }

    //Nhấn nút phục hồi trên table
    $('#tbody').on('click', '.recover', function () {
        $('#RecoverConfirmation').modal('show');
        var btn = $(this);
        var id = btn.data('id');
        $('#btnConfirmRecover').data('id', id);
    });

    //Nhấn nút xác nhận phục hồi trên pop-up
    $('#btnConfirmRecover').on('click', function () {
        var btn = $(this);
        var id = btn.data('id');
        $.ajax({
            url: '/ShopUser/Recover/',
            type: 'POST',
            data: { 'shopUserID': id },
        })
        .done(function (data) {
            var trid = "#tr-" + id;
            $(trid).addClass("selected");
            var rows = mainTable
                .rows('.selected')
                .remove()
                .draw(false);
            $('#RecoverConfirmation').modal('hide');
            alert('Phục hồi thành công');

        })
        .fail(function (data) {
            $('#RecoverConfirmation').modal('hide');
            alert('Xóa thất bại');
        });
    });
});