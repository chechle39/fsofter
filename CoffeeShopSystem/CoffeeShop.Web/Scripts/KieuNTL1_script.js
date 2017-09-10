function isNullOrEmpty(val) {
    if (typeof val !== 'undefined') {
        if (val.length > 0) {
            return false;
        }
    }

    return true;
}

function highLightErr(obj) {
    if (typeof obj !== 'undefined') {
        obj.css('border', '1px solid indianred');
    }
}

function deHighLightErr(obj) {
    if (typeof obj !== 'undefined') {
        obj.css('border', '1px solid #ccc');
    }
}

function convertJsonDate(date) {
    var value = new Date(parseInt(date.replace(/(^.*\()|([+-].*$)/g, '')));
    var dat = +value.getFullYear() + "-" + ("0" + (value.getMonth() + 1)).slice(-2) + "-" + ("0" + value.getDate()).slice(-2);
    return dat;
}

function pagination_LoadTable(data, tableHolder) {
    var row = "";
    data.forEach(function (item) {
        row += "<tr><td></td>";
        row += "<td>" + item.ID + "</td>";
        row += "<td>" + (item.CreatedDate == null ? "" : convertJsonDate(item.CreatedDate)) + "</td>";
        row += "<td>" + item.MateName + "</td>";
        row += "<td>" + item.Inventory + "</td>";
        row += "<td>" + item.UnitPrice + "</td>";
        row += "<td>" + item.EmpName + "</td>";
        row += "<td>" + (item.Type == 1 ? "Sửa" : "Thêm") + "</td>";
        row += "<td>" + item.Description + "</td>";
        row += "<td>";
        row += "<a href='#' class='btn btn-info btn-xs editItem' title='Chỉnh sửa' data-id='" + item.ID + "'><i class='fa fa-pencil'></i></a>";
        row += "<a href='#' class='btn btn-danger btn-xs deleteItem' title='Xóa' data-id='" + item.ID + "'><i class='fa fa-trash-o'></i></a>";
        row += "</td>";
    });

    tableHolder.empty();
    tableHolder.append(row);
}

function pagination_LoadPagingBox(holder, currentPage, totalPages) {
    var pagiLi = "";

    if (currentPage > 3 && currentPage <= totalPages) {
        pagiLi += "<li><a href='javascript:;' data-page='1'>Đầu tiên</a></li>";
    }

    var i = (currentPage - 2 > 0 ? currentPage - 2 : 1);
    for (; i <= currentPage + 2 && i <= totalPages; i++) {
        pagiLi += "<li class=' " + (i == currentPage ? 'active' : '') + " '><a href='javascript:;' data-page='" + i + "'>" + i + "</a></li>";
    }

    if (currentPage < totalPages - 2) {
        pagiLi += "<li><a href='javascript:;' data-page='" + totalPages + "'>Cuối cùng</a></li>";
    }

    holder.empty();
    holder.append(pagiLi);
}

$(document).ready(function () {

    $('#btnAddNew').on('click', function (event) {
        $('#formInfoType').val(0);
    });

    $('#btnBasicSearch').on('click', function (event) {
        event.preventDefault();
        var keyword = $('#txtKeyword').val();
        var rowPerPage = $('#slRowPerPage').val();

        if (keyword) {
            $.ajax({
                type: "get"
                , url: "/MaterialLog/SearchByName"
                , data: { keyword: keyword, rowPerPage: rowPerPage }
                , context: this
            }).done(function (data) {
                var table = $('#logContent');
                var pagiHolder = $('#logPaging ul');
                var pagiLi = "";
                var totalPages = parseInt(data.totalItems) * 1.0 / rowPerPage;
                if (totalPages > Math.trunc(totalPages)) {
                    totalPages = Math.trunc(totalPages) + 1;
                }
                console.log(data);
                pagination_LoadTable(data.items, table);
                pagination_LoadPagingBox(pagiHolder, 1, totalPages);
            });
        }
    });

    $('#btnAdvanceSearch').on('click', function (event) {
        $('#advanceSearchBox').fadeToggle('medium', 'linear');
    });

    $('#frmMaterialLogBasicSearch').on('submit', function (event) {
        event.preventDefault();
        var fromDate = $('#dtFromDate');
        var toDate = $('#dtToDate');
        var rs = true;

        if (fromDate.val() == "") {
            highLightErr(fromDate);
            rs = false;
        } else {
            deHighLightErr(fromDate);
        }

        if (toDate.val() == "") {
            highLightErr(toDate);
            rs = false;
        } else {
            var from = Date.parse(fromDate.val());
            var to = Date.parse(toDate.val());

            if (from > to) {
                var holder = $('#popupMes');
                holder.find('.modal-body').append("<p>From date must less than or equal to To date</p>");
                holder.modal();
                highLightErr(fromDate);
                highLightErr(toDate);
            } else {
                deHighLightErr(fromDate);
                deHighLightErr(toDate);
            }
        }

        return rs;
    });

    $('#frmMaterialLogInfo').on('submit', function (event) {
        event.preventDefault();

        var mate = $('#slCategory');
        var date = $('#dtDate');
        var inventory = $('#txtInventory');
        var unitPrice = $('#txtUnitPrice');
        var errHolder = $('#errMessageHolder');
        var type = $('#slType');
        var errMes = "";
        var rs = true;
        var url = "";
        var data = $(this).serialize() + "&IsDelete=false&EmployeeID=1";

        errHolder.empty();

        if ($('#formInfoType').val() == 0) {
            url = "/MaterialLog/Create";
        } else if ($('#formInfoType').val() == 1) {
            url = "/MaterialLog/Edit";
            data = data + "&ID=" + $('#txtID').val();
        }
        console.log(data);
        if (mate.val() == null) {
            errMes += "Chưa chọn nguyên vật liệu</br>";
            rs = false;
            highLightErr(mate);
        } else {
            deHighLightErr(mate);
        }

        if (date.val() == "") {
            errMes += "Ngày nhập không hợp lệ</br>";
            rs = false;
            highLightErr(date);
        } else {
            deHighLightErr(date);
        }

        if (type.val() == null) {
            errMes += "Chưa chọn loại log ghi</br>";
            rs = false;
            highLightErr(type);
        } else {
            deHighLightErr(type);
        }

        if (isNullOrEmpty(inventory.val()) || inventory.val() < 0) {
            errMes += "Số lượng tồn phải là số dương</br>";
            rs = false;
            highLightErr(inventory);
        } else {
            deHighLightErr(inventory);
        }

        if (isNullOrEmpty(unitPrice.val()) || unitPrice.val() < 0) {
            errMes += "Đơn giá phải là số dương</br>";
            rs = false;
            highLightErr(unitPrice);
        } else {
            deHighLightErr(unitPrice);
        }

        if (rs == true) {
            console.log($(this).serialize());
            $.ajax({
                type: "post"
                , url: url
                , data: data
                , context: this
            }).done(function (data) {
                if (data == "") {
                    $('#infoModal').modal('hide');
                } else {
                    errMes += data;
                    errHolder.empty();
                    errHolder.append(errMes);
                    errHolder.css('color', 'red');
                    errHolder.show();
                }
            });
        } else {
            errHolder.empty();
            errHolder.append(errMes);
            errHolder.css('color', 'red');
            errHolder.show();
        }
    });

    $('#logPaging').on('click', 'ul li', function () {
        var currentPage = $(this).find('a').data('page');
        var rowPerPage = $('#slRowPerPage').val();

        $.ajax({
            url: "/MaterialLog/Paging"
            , method: "GET"
            , data: { inRowPerPage: rowPerPage, currentPage: currentPage }
            , context: this
        }).done(function (data) {
            var table = $('#logContent');
            var pagiHolder = $('#logPaging ul');
            var pagiLi = "";
            var totalPages = parseInt(data.totalItems) * 1.0 / rowPerPage;
            if (totalPages > Math.trunc(totalPages)) {
                totalPages = Math.trunc(totalPages) + 1;
            }

            pagination_LoadTable(data.items, table);
            pagination_LoadPagingBox(pagiHolder, currentPage, totalPages);
        });
    });

    $('#frmGetByStatus [name=optradio]').on('change', function (event) {
        event.preventDefault();
        var status = $(this).val();
        var rowPerPage = $('#slRowPerPage').val();

        $.ajax({
            type: "get"
            , url: "/MaterialLog/GetByStatus"
            , data: { status: status, inRowPerPage: rowPerPage }
            , context: this
        }).done(function (data) {
            var table = $('#logContent');
            var pagiHolder = $('#logPaging ul');
            var pagiLi = "";
            var totalPages = parseInt(data.totalItems) * 1.0 / rowPerPage;
            if (totalPages > Math.trunc(totalPages)) {
                totalPages = Math.trunc(totalPages) + 1;
            }

            pagination_LoadTable(data.items, table);
            pagination_LoadPagingBox(pagiHolder, 1, totalPages);
        });
    });

    $('#logContent').on('click', 'a.editItem', function (event) {
        var id = $(this).data('id');

        $.ajax({
            type: "get"
            , url: "/MaterialLog/Detail"
            , data: { id: id }
            , context: this
        }).done(function (data) {
            if (data != "") {
                var popup = $('#infoModal');
                $('#formInfoType').val(1);
                popup.find('[name=MaterialID]').val(data.MaterialID);
                popup.find('[name=CreatedDate]').val(convertJsonDate(data.CreatedDate));
                popup.find('[name=Inventory]').val(data.Inventory);
                popup.find('[name=UnitPrice]').val(data.UnitPrice);
                popup.find('[name=Description]').val(data.Description);
                popup.find('[name=Type]').val(data.Type + ""); console.log(data.Type);
                $('#txtID').val(id);
                popup.modal('show');
            } else {
                var mesBox = $('#messageBox');
                mesBox.empty();
                mesBox.append("<p>Không tìm thấy item này, vui lòng thử lại</p>");
                mesBox.modal('show');
            }
        });
    });

    $('#logContent').on('click', 'a.deleteItem', function (event) {
        var id = $(this).data('id');
        var confirmMes = $('#DeleteConfirmation');
        confirmMes.find('.id').val(id);
        confirmMes.modal('show');
    });

    $('#btnDeleteItem').on('click', function (event) {
        var id = $('#DeleteConfirmation .id').val();

        $.ajax({
            type: "post"
            , url: "/MaterialLog/Delete"
            , data: { id: id }
            , context: this
        }).done(function (data) {
            if (data == "") {
                $('#DeleteConfirmation').modal('hide');
            } else {
                var mesBox = $('#messageBox');
                mesBox.empty();
                mesBox.append("<p>Có lỗi xảy ra, vui lòng thử lại</p>");
                mesBox.modal('show');
            }
        });
    });

    $('#btnAdvanceSearch').on('click', function (event) {
        $('#advanceSearchBox').toggleClass('hide');
    });

    $('#frmAdvanceSearch').on('submit', function (event) {
        event.preventDefault();

        var from = $(this).find('[name=FromDate]');
        var to = $(this).find('[name=ToDate]');
        var rowPerPage = $('#slRowPerPage').val();

        if (from.val() > to.val()) {
            highLightErr(from);
            highLightErr(to);
        } else {
            deHighLightErr(from);
            deHighLightErr(to);

            var str = $(this).serialize();
            str = str + "&RowPerPage=" + rowPerPage;
            console.log(str);
            $.ajax({
                type: "get"
                , url: "/MaterialLog/Search"
                , data: str
                , context: this
            }).done(function (data) {
                if (data != "") {
                    var table = $('#logContent');
                    var pagiHolder = $('#logPaging ul');
                    var pagiLi = "";
                    var totalPages = parseInt(data.totalItems) * 1.0 / rowPerPage;
                    if (totalPages > Math.trunc(totalPages)) {
                        totalPages = Math.trunc(totalPages) + 1;
                    }

                    pagination_LoadTable(data.items, table);
                    pagination_LoadPagingBox(pagiHolder, 1, totalPages); rt
                }
            });
        }
    });
});