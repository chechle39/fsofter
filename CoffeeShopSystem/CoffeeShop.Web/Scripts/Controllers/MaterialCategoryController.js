var pageIndex = 1;
function convertDateJson(date) {
    var value = new Date
            (
                 parseInt(date.replace(/(^.*\()|([+-].*$)/g, ''))
            );
    var dat = value.getFullYear() + "-" + ("0" + value.getMonth()).slice(-2) + "-" + ("0" + value.getDay()).slice(-2);
    return dat;
}
function pagination_LoadTable(data, tableHolder) {
    var row = "";
    data.forEach(function (item) {
        row += "<tr><td></td>";
        row += "<td>" + item.ID + "</td>";
        row += "<td>" + item.Name + "</td>";
        row += "<td>" + (item.CreatedDate != null ? convertDateJson(item.CreatedDate) : null) + "</td>";
        row += "<td>" + item.Description + "</td>";
        row += "<td>" + (item.IsDelete == true ? 'Đã xóa' : 'Chưa xóa') + "</td>";
        row += "<td>";
        row += "<a class='btn btn-info btn-xs btnEditMaterialCategory' data-id='" + item.ID + "' title='Chỉnh sửa'><i class='fa fa-pencil'></i></a>";
        row += "<a class='btn btn-danger btn-xs' data-id='" + item.ID + "' title='Xóa'><i class='fa fa-trash-o'></i></a>";
        row += "</td>";
    });

    tableHolder.empty();
    tableHolder.append(row);
}
var materialCategory = {
    init: function () {
        materialCategory.loadData();
        materialCategory.registerEvent();
    },
    registerEvent: function () {
        $("#btnAddNew").click(function () {
            $('#SubmitFrmMaterialCategory').text("Thêm");
            $("#frmMaterialCategoryLogInfo")[0].reset();
            $("#MyModal").modal();
        });

        //event click delete
        $('.btn-danger').click(function () {
            var id = $(this).data('id');
            $('#btn-deleteMaterialCategory').data('id', id)
            $("#DeleteConfirmation").modal("show");
        });
        $("#text-search").off('keypress').on('keypress', function (e) {
            if (e.which == 13) {
                materialCategory.loadData(true);
            }
        });

        $("#comboPageSize").off('change').on('change', function (e) {
            $('#text-search').val("");
            materialCategory.loadData(true);
        });

        $('input[name=optradio]').off('click').on('click', function () {
            $('#text-search').val("");
            materialCategory.loadData(true);
        });

        $('.btnEditMaterialCategory').off('click').on('click', function () {
            $("#frmMaterialCategoryLogInfo")[0].reset();
            var id = parseInt($(this).data('id'));
            alert(id);
            $.ajax({
                type: 'GET',
                url: '/MaterialCategory/GetDetailEdit',
                dataType: 'JSON',
                data: { id: id },
                success: function (response) {
                    if (response.status) {
                        var materialCategory = response.materialCategory;
                        $('#txtName').val(materialCategory.Name);
                        $('#dtCreatedDate').val(materialCategory.CreatedDate != null ? convertDateJson(materialCategory.CreatedDate) : "");
                        $('#txtDescription').val(materialCategory.Description);
                    }
                }
            })
            $('#SubmitFrmMaterialCategory').text("Sửa");
            $('#infoModal').modal('show');
        });

        $("#SubmitFrmMaterialCategory").off('click').on('click', function () {
            //if ($('#frmMaterialCategoryLogInfo').valid()) {
            materialCategory.submitFromSaveData();
            //}
        });

        $("#btn-deleteMaterialCategory").off('click').on('click', function () {
            var id = $(this).data('id');
            materialCategory.deleteMaterialCategory(id);
        });
    },
    submitFromSaveData: function () {
        var name = $('#txtName').val();
        var description = $('#txtDescription').val();
        var createdDate = $('#dtCreatedDate').val();
        var id = parseInt($('#hiddenID').val());
        var materialCategory = {
            Name: name,
            CategoryID: categoryID,
            UnitPrice: unitPrice,
            CreatedDate: createdDate,
            Inventory: inventory,
            Description: description,
            ID: id
        }

        $.ajax({
            url: '/MaterialCategory/SubmitForm',
            data: {
                strMaterialCategory: JSON.stringify(materialCategory)
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    if (response.action) {

                    }
                    else {
                    }
                    $('#infoModal').modal('hide');
                    dialog.modal('hide');
                    materialCategory.loadData(true);

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

    deleteMaterial: function (id) {
        $.ajax({
            type: 'GET',
            url: '/MaterialCategory/Delete',
            dataType: 'JSON',
            data: { id: id },
            success: function (response) {
                if (response.status) {
                }
                else {
                    alert(response.message);

                }
                $('#DeleteConfirmation').modal('hide');
                dialog.modal('hide');
                materialCategory.loadData(true);
            }
        })
    },
    resetForm: function () {
        $('#txtName').val("");
        $('#txtDescription').val();
        $('#dtCreatedDate').val();
        $('#hiddenID').val();
    },
    loadData: function (changePageSize) {
        var keyword = $('#text-search').val();
        var status = $('input[name=optradio]:checked').val();
        var pageSize = $('#comboPageSize').val();
        $.ajax({
            url: '/MaterialCategory/LoadData',
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

                    materialCategory.paging(response.total, function () {
                        materialCategory.loadData();
                    }, changePageSize);
                    materialCategory.registerEvent();
                }
            },
            error: function () {
                alert("error");
            }
        });
    },
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
            next: "Tiếp",
            last: "Cuối",
            prev: "Trước",
            visiblePages: 3,
            hideOnlyOnePage: true,
            onPageClick: function (event, page) {
                pageIndex = page;
                setTimeout(callback, 200);
            }
        });
    }
}
materialCategory.init();