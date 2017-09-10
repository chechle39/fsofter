/// <reference path="../jquery.validate.min.js" />

//khoi tạo trang hiện tại mặc định vừa load là 1.
var pageIndex = 1;

//hàm convert date json sang date.
function convertDateJson(date) {
    var value = new Date
            (
                 parseInt(date.replace(/(^.*\()|([+-].*$)/g, ''))
            );
    var dat = value.getFullYear() + "-" + ("0" + value.getMonth()).slice(-2) + "-" + ("0" + value.getDay()).slice(-2);
    return dat;
}

//ham appenddata vào table.
function pagination_LoadTable(data, tableHolder) {
    var row = "";
    data.forEach(function (item) {
        row += "<tr><td></td>";
        row += "<td>" + item.ID + "</td>";
        row += "<td>" + item.Name + "</td>";
        row += "<td>" + (item.CreatedDate != null ? convertDateJson(item.CreatedDate) : null) + "</td>";
        row += "<td>" + item.Inventory + "</td>";
        row += "<td>" + item.UnitPrice + "</td>";
        row += "<td>" + (item.IsDelete == true ? 'Đã xóa' : 'Chưa xóa') + "</td>";
        row += "<td>" + item.Description + "</td>";
        row += "<td>" + item.CategoryName + "</td>";
        row += "<td>";
        row += "<a class='btn btn-info btn-xs btnEditMaterial' data-id='" + item.ID + "' title='Chỉnh sửa'><i class='fa fa-pencil'></i></a>";
        row += "<a class='btn btn-danger btn-xs' data-id='" + item.ID + "' title='Xóa'><i class='fa fa-trash-o'></i></a>";
        row += "</td>";
    });

    tableHolder.empty();
    tableHolder.append(row);
}

var material = {
    //hàm khỏi tạo, gọi hàm load là các sự kiện.
    init: function () {
        material.loadData();
        material.registerEvent();
    },

    //hàm viết các sự kiện
    registerEvent: function () {
        //kiểm tra hợp lệ của form thêm
        $('#frmMaterialLogInfo').validate({
            rules: {
                Name: {
                    required: true
                },
                CategoryID: {
                    required: true
                },
                UnitPrice: {
                    number: true,
                    digits: true
                },
                Inventory: {
                    number: true,
                    digits: true
                }
            },
            messages: {
                Name: {
                    required: "Bạn phải nhập tên nguyên liệu",
                },
                CategoryID: {
                    required: "Bạn phải chọn loại nguyên liệu",
                },
                UnitPrice: {
                    number: "Đơn giá phải là số",
                    digits: "Đơn giá phải lớn hơn không"
                },
                Inventory: {
                    number: "Số lượng tồn phải là số",
                    digits: "Đơn giá phải lớn hơn không"
                }
            }
        });

        //sự kiện click dấu + trong giao diện
        $("#btnAddNew").click(function () {
            $('#SubmitFrmMaterial').text("Thêm");
            $("#frmMaterialLogInfo")[0].reset();
            $("#MyModal").modal();
        });

        //sự kiến click icon xóa ở mỗi record
        $('.btn-danger').click(function () {
            var id = $(this).data('id');
            $('#btn-deleteMaterial').data('id', id)
            $("#DeleteConfirmation").modal("show");
        });

        //sự kiện nhấn enter ở textbox search
        $("#text-search").off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                material.loadData(true);
            }
        });

        //sử kiện chọn số record hiện thị ở trang.
        $("#comboPageSize").off('change').on('change', function (e) {
            $('#text-search').val("");
            material.loadData(true);
        });

        //sự kiện chọn radio button trình trạng hiển thị.
        $('input[name=optradio]').off('click').on('click', function () {
            $('#text-search').val("");
            material.loadData(true);
        });

        //sự kiện button edit ở mỗi record.
        $('.btnEditMaterial').off('click').on('click', function () {
            $("#frmMaterialLogInfo")[0].reset();
            var id = parseInt($(this).data('id'));
            $.ajax({
                type: 'GET',
                url: '/Material/GetDetailEdit',
                dataType: 'JSON',
                data: { id: id },
                success: function (response) {
                    if (response.status) {
                        var material = response.material;
                        $('#txtName').val(material.Name);
                        $('#slCategory').val(material.CategoryID);
                        $('#txtUnitPrice').val(material.UnitPrice);
                        $('#dtCreatedDate').val(material.CreatedDate != null ? convertDateJson(material.CreatedDate) : "");
                        $('#txtInventory').val(material.Inventory);
                        $('#txtDescription').val(material.Description);
                        $('#hiddenID').val(material.ID);
                    }
                }
            })
            $('#SubmitFrmMaterial').text("Sửa");
            $('#infoModal').modal('show');
        });

        //sự kiện Thêm or Sửa ở form InfoMaterial
        $("#SubmitFrmMaterial").off('click').on('click', function () {
            if ($('#frmMaterialLogInfo').valid()) {
                material.submitFromSaveData();
            }
        });

        //sự kiến click xác nhận xóa ở popup confirm.
        $("#btn-deleteMaterial").off('click').on('click', function () {
            var id = $(this).data('id');
            material.deleteMaterial(id);
        });
    },

    //hàm thực hiện gọi controller để thêm hoặc sửa data
    submitFromSaveData: function () {
        var name = $('#txtName').val();
        var categoryID = $('#slCategory').val();
        var unitPrice = $('#txtUnitPrice').val();
        var createdDate = $('#dtCreatedDate').val();
        var inventory = $('#txtInventory').val();
        var description = $('#txtDescription').val();
        var id = parseInt($('#hiddenID').val());
        var mate = {
            Name: name,
            CategoryID: categoryID,
            UnitPrice: unitPrice,
            CreatedDate: createdDate,
            Inventory: inventory,
            Description: description,
            ID: id
        }

        $.ajax({
            url: '/Material/SubmitForm',
            data: {
                strMaterial: JSON.stringify(mate)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    if (response.action) {
                        //var dialog = bootbox.dialog({
                        //    message: 'Thêm thành công',
                        //    closeButton: false
                        //});
                    }
                    else {
                        //var dialog = bootbox.dialog({
                        //    message: 'Sua thanh cong',
                        //    closeButton: false
                        //});
                    }
                    $('#infoModal').modal('hide');

                    material.loadData(true);
                }
                else {
                    bootbox.alert(response.message);
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    },

    //hàm gọi controller delete để xóa 1 record có ID = id
    deleteMaterial: function (id) {
        $.ajax({
            type: 'GET',
            url: '/Material/Delete',
            dataType: 'JSON',
            data: { id: id },
            success: function (response) {
                if (response.status) {
                    //var dialog = bootbox.dialog({
                    //    message: 'Xóa thành công',
                    //    closeButton: false
                    //});
                }
                else {
                    alert(response.message);
                }
                $('#DeleteConfirmation').modal('hide');

                material.loadData(true);
            }
        })
    },

    //ham reset các field trong form.
    resetForm: function () {
        $('#txtName').val("");
        $('#slCategory').reset();
        $('#txtUnitPrice').val("");
        $('#dtCreatedDate').val();
        $('#txtInventory').val();
        $('#txtDescription').val();
        $('#hiddenID').val();
    },

    //hàm load danh sách theo trình trạng, số lượng record, trang hiện tại.
    loadData: function (changePageSize) {
        var keyword = $('#text-search').val();
        var status = $('input[name=optradio]:checked').val();
        var pageSize = $('#comboPageSize').val();
        $.ajax({
            url: '/Material/LoadData',
            type: 'GET',
            data: {
                keyword: keyword,
                status: status,
                page: pageIndex,
                pageSize: pageSize
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var data = response.data;
                    var html = '';
                    var bodyTable = $('#bodyTable');

                    pagination_LoadTable(data, bodyTable);

                    material.paging(response.total, function () {
                        material.loadData();
                    }, changePageSize);
                    material.registerEvent();
                }
            },
            error: function () {
                alert("error");
            }
        });
    },

    //hàm phân trang, callback hàm loadData
    paging: function (totalRow, callback, changePageSize) {
        var pageSize = $('#comboPageSize').val();
        var totalPage = Math.ceil(totalRow / pageSize);

        //Unbind pagination if it existed or click change pagesize
        if ($('#pagination a').length === 0 || changePageSize === true) {
            $('#pagination').empty();
            $('#pagination').removeData("twbs-pagination");
            $('#pagination').unbind("page");
        }

        $('#pagination').twbsPagination({
            totalPages: totalPage,
            first: "Đầu",
            next: ">",
            last: "Cuối",
            prev: "<",
            visiblePages: 3,
            hideOnlyOnePage: true,
            onPageClick: function (event, page) {
                pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}

//gọi phương thức khởi tạo để load và chạy được các sự kiện javascript.
material.init();