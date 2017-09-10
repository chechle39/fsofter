
$(document).ready(function () {
    var datatable = $('#listTable').DataTable({
        "language": {
            lengthMenu: "Hiện _MENU_ kết quả trên 1 trang",
            info: "",
            infoEmpty: "Không tìm thấy kết quả nào",
            infoFiltered: "(Trên _MAX_ kết quả)",
            search: "_INPUT_",
            searchPlaceholder: "Tìm kiếm...",
            paginate: {
                'previous': '<span class="">&laquo;</span>',
                'next': '<span class="">&raquo;</span>'
            }
        },
        "sDom": 'tipr',
        "columnDefs": [
    { "orderable": false, "targets": [7,8] }
    ],

    });
    //advanced search
    $('#AddvancedButton').on('click', function () {
        var click = $(this);
        if (click.data('click') == "0") {
            $('#frmAdvancedSearch').removeClass('hidden');
            click.data('click', '1');
        }
        else {
            $('#frmAdvancedSearch').addClass('hidden');
            click.data('click', '0');
        }
    });

    //select show record
    $('#lengthpage').on('change', function () {
        var s = $(this).val();
        datatable.page.len(s).draw(false);
    });

    //search cơ bản
    $("#search2").keyup(function () {
        datatable.search(this.value).draw();
        // datatable.column('2').search(this.value).draw();
    });

    //View detail order
    $("#bodyOrder").on('click', '.btnDetail', function () {
        var id = $(this).data('id');

        $.ajax({
            //'@Url.Action("GetListOrderProductByOrderID", "Order")',
            url: '/Order/GetListOrderProductByOrderID/)',
            type: 'post',
            data: { 'id': id },
            dataType: 'json',
            timeout: 30 * 1000,

        })
                    .done(function (data) {
                        $('#Detailbody').empty();
                        $.each(data, function (idx, item) {
                            var html =
                                '<tr>' +
                                    '<td>' + item.name + '</td>' +
                                    '<td>' + item.price + '</td>' +
                                    '<td>' + item.quantity + '</td>' +
                                    '<td>' + item.money + '</td>' +
                                '</tr>';
                            $('#Detailbody').append(html);
                        });
                        $('#modalDetail').modal('show');
                    })
                    .fail(function (jqXHR, status, error) {
                        console.log(jqXHR);
                        alert('fail');
                    });
    });


    //EditOrder

    $('#bodyOrder').on('click', '.btnEdit', function () {
        var id = $(this).data('id');

        $('#modalEdit [name=txtID]').val($(this).data('id'));
        $('#modalEdit [name=txtName]').val($(this).data('user'));
        $('#modalEdit [name=txtPromotion').val($(this).data('promo'));
        $('#modalEdit [name=txtTable]').val($(this).data('table'));
        $('#modalEdit [name=txtSetDate]').val($(this).data('date'));
        $('#modalEdit [name=txtStatus]').val($(this).data('status'));
        $('#modalEdit [name=lbMonney]').html($(this).data('money'));


        $.ajax({
            //'@Url.Action("GetListOrderProductByOrderID", "Order")',
            url: '/Order/GetListOrderProductByOrderID/)',
            type: 'post',
            data: { 'id': id },
            dataType: 'json',
            //   timeout: 30 * 1000,

        })
                .done(function (data) {
                    $('#tbEdit').empty();
                    $.each(data, function (idx, item) {
                        var html =
                            '<tr class="tr-' + item.id + '">' +
                                '<td>' + item.name + '</td>' +
                                '<td id="eprice-' + item.id + '" data-eprice="' + item.price + '">' + item.price + '</td>' +
                                '<td>' + '<input type="number" min="0" id="eq-' + item.id + '"value="' + item.quantity + '"class="form-control txtEQuantityPro"/> ' + '</td>' +
                                '<td id="eamount' + item.id + '">' + item.money + '</td>' +
                                '<td>' + '<a href="javascript:;" data-id="' + item.id + '" class="btn btn-primary btn-xs btnEProEdit" title="Sửa"><i class="fa fa-pencil"></i></a>'
                                 + '<a href="javascript:;" data-id="' + item.id + '" class="btn btn-danger btn-xs btnEProRemove" title="Xóa"><i class="fa fa-trash-o"></i></a></td>'
                        '</tr>';
                        $('#tbEdit').append(html);
                    });

                })
                .fail(function (jqXHR, status, error) {
                    console.log(jqXHR);
                    alert('fail');
                });
        $('#modalEdit').modal('show');


        //Set into session
        $.ajax({
            //'@Url.Action("SetOrderToSession", "Order")',
            url: '/Order/SetOrderToSession/',
            type: 'post',
            data: { 'id': id },
            dataType: 'json',
            // timeout: 30 * 1000,

        })
         .done(function (data) {
         })
          .fail(function (jqXHR, status, error) {
              console.log(jqXHR);
              alert('fail');
          });
    });


    //Update OrderProduct in Session
    $('#tbEdit').on('click', '.btnEProEdit', function () {

        var id = $(this).data('id');
        var quantity = $('#tbEdit #eq-' + id).val();

        $.ajax({
            // '@Url.Action("UpdateOrderProduct", "Order")',
            url: '/Order/UpdateOrderProduct/',
            type: 'post',
            data: { 'id': id, 'q': quantity },
            dataType: 'json',
            // timeout: 30 * 1000,
        })
        .done(function (data) {

            //Update data on modal
            $('#tbEdit #eq-' + id).val(quantity);
            $('#tbEdit #eprice-' + id).data('eprice');
            $('#tbEdit #eamount' + id).html($('#tbEdit #eprice-' + id).data('eprice') * quantity)
            $('#modalEdit [name=lbMonney]').html(data);

        })
         .fail(function (jqXHR, status, error) {
             console.log(jqXHR);
             alert('fail');
         });

    });


    //delete orderproduct from session
    $('#tbEdit').on('click', '.btnEProRemove', function () {

        $('#confirmDelete').modal('show');
    });

    $('#btnConfirmDelete').on('click', function () {
        var id = $('#tbEdit .btnEProRemove').data('id');

        $.ajax({
            //'@Url.Action("RemoveOrderProduct", "Order")',
            url: '/Order/RemoveOrderProduct/',
            type: 'post',
            data: { 'id': id },
            dataType: 'json',
            // timeout: 30 * 1000,
        })
        .done(function (data) {

            $('#confirmDelete').modal('hide');
            $('#tbEdit .tr-' + id).remove();
            $('#modalEdit [name=lbMonney]').html(data);

        })
         .fail(function (jqXHR, status, error) {
             console.log(jqXHR);
             alert('fail');
         });
    });



    //Update orderproduct into database when user click button "Lưu"

    $('#btnSaveChanges').on('click', function () {

        var id = $('#modalEdit [name=txtID]').val();
        var status = $('#modalEdit [name=txtStatus]').val();
        var tableID = $('#modalEdit [name=txtTable]').val();

        $.ajax({
            //'@Url.Action("UpdateOrder", "Order")',
            url: '/Order/UpdateOrder/',
            type: 'post',
            data: { 'id': id, 'tableID': tableID, 'status': status },
            dataType: 'json',
            // timeout: 30 * 1000,
        })
        .done(function (data) {

            var id = $('#modalEdit [name=txtID]').val();

            //Update data (of form to row selecting) to datatable
            var row = datatable.row('.trbody-' + id);
            datatable.cell(row, 5).data($('#modalEdit [name=lbMonney]').html()).draw(false);
            datatable.cell(row, 1).data($('#modalEdit [name=txtTable] option:selected').text()).draw(false);
            datatable.cell(row, 6).data($('#modalEdit [name=txtStatus] option:selected').text()).draw(false);

            $('#modalEdit').modal('hide');

        })
         .fail(function (jqXHR, status, error) {
             console.log(jqXHR);
             alert('fail');
         });
    });

    //Search bu Id or table name or customer name
    $('#txtSearch').on('input', function () {
        var keyword = $(this).val();
        var status;
        var boolStatus;

        $.ajax({
            //'@Url.Action("Search", "Order")'
            url: '/Order/Search/',
            type: 'post',
            data: { 'keyword': keyword },
            dataType: 'json',
            // timeout: 30 * 1000,
        })
        .done(function (data) {

            $('#bodyOrder').empty();
            var html = '';

            if (data == 0) {
                html = '<h4>Không có kết quả tìm kiếm</h4>';
            }
            else {
                $.each(data, function (idx, item) {

                    //convert to Date form json
                    var currentTime = new Date(parseInt(item.setDate.replace('/Date(', '')));
                    var month = currentTime.getMonth() + 1;
                    var day = currentTime.getDate();
                    var year = currentTime.getFullYear();
                    var date = day + "-" + month + "-" + year;

                    if (item.status == false) {
                        status = "Chưa thanh toán";
                        boolStatus = "False";
                    }
                    else if (item.status == true) {
                        status = "Đã thanh toán";
                        boolStatus = "True";
                    }

                    //show data table
                    html +=
                         '<tr class="trbody-' + item.id + '">' +
                                '<td>' + item.id + '</td>' +
                                '<td id="bodyTableName-' + item.id + '">' + item.tableName + '</td>' +
                                '<td>' + item.userName + '</td>' +
                                '<td>' + item.promoName + '</td>' +
                                '<td>' + date + '</td>' +
                                '<td id="bodyTotalMoney-' + item.id + '">' + item.totalMoney + '</td>' +
                                '<td id="bodyStatus-' + item.id + '">' + status + '</td>' +
                                '<td>' +
                                 ' <a href="javascript:;" data-id="' + item.id + '" class="btn btn-primary btn-xs btnDetail" title="Xem chi tiết">' +
                                     '<i class="fa fa-folder"></i>' +
                                 '</a>' +
                                 '</td>' +
                              '<td>' +
                                  '<a href="javascript:;" data-id="' + item.id + '"data-table="' + item.tableID + '" data-promo="' + item.promoName + '" data-user="' + item.userName + '" data-date="' + date + '" data-status="' + boolStatus + '" data-money="' + item.totalMoney + '"class="btn btn-info btn-xs btnEdit" title="Chỉnh sửa"><i class="fa fa-pencil"></i></a>' +
                              '</td>' +
                         '</tr>';
                })
            }
            $('#bodyOrder').html(html);

        })
         .fail(function (jqXHR, status, error) {
             console.log(jqXHR);
             alert('fail');
         });
    });

    //Filter by order status
    $('#frmView [name=optradio]').change(function () {
        var status = $('input[name=optradio]:checked', '#frmView').val();
        $.ajax({
            //@Url.Action("FilterStatus", "Order")'
            url: '/Order/FilterStatus/',
            type: 'post',
            data: { 'status': status },
            dataType: 'json',
            // timeout: 30 * 1000,
        })
        .done(function (data) {
            var html1 = '';
            var html2 = '';
            $('#listTable').DataTable().clear().draw();
            $.each(data, function (idx, item) {

                var currentTime = new Date(parseInt(item.setDate.replace('/Date(', '')));
                var month = currentTime.getMonth() + 1;
                var day = currentTime.getDate();
                var year = currentTime.getFullYear();
                var date = day + "/" + month + "/" + year;

                if (item.status == false) {
                    status = "Chưa thanh toán";
                    boolStatus = "False";
                }
                else if (item.status == true) {
                    status = "Đã thanh toán";
                    boolStatus = "True";
                }
                //show data table
                html1 = '<a href="javascript:;" data-id="' + item.id + '" class="btn btn-primary btn-xs btnDetail" title="Xem chi tiết">' +
                                 '<i class="fa fa-folder"></i>' +
                             '</a>';
                html2 = '<a href="javascript:;" data-id="' + item.id + '"data-table="' + item.tableID + '" data-promo="' + item.promoName + '" data-user="' + item.userName + '" data-date="' + date + '" data-status="' + boolStatus + '" data-money="' + item.totalMoney + '"class="btn btn-info btn-xs btnEdit" title="Chỉnh sửa"><i class="fa fa-pencil"></i></a>';

                var rowNode = datatable.row.add([
                    item.id,
                    item.tableName,
                    item.userName,
                    item.promoName,
                    date,
                    item.totalMoney,
                    status,
                    html1,
                    html2,
                ]).draw(false).node();

                //Add attribute for this row <tr>
                $(rowNode)
                    .addClass('trbody-' + item.id);
            })
        })
         .fail(function (jqXHR, status, error) {
             console.log(jqXHR);
             alert('fail');
         });
    });


    //advanced search by date and table name

    $('#advancedSearch').on('click', function () {

        $.ajax({
            //'@Url.Action("AdvancedSearch", "Order")',
            url: '/Order/AdvancedSearch/',
            type: 'post',
            data: { 'customerName': $('#txtSearch2').val(), 'aSFromDate': $('#aSFromDate').val(), 'aSToDate': $('#aSToDate').val(), 'aSTableName': $('#aSTableName').val() },
            dataType: 'json',
            // timeout: 30 * 1000,


        })
            .done(function (data) {
                var status = '';
                var html1 = '';
                var html2 = '';
                $('#listTable').DataTable().clear().draw();
                $.each(data, function (idx, item) {

                    var currentTime = new Date(parseInt(item.setDate.replace('/Date(', '')));
                    var month = currentTime.getMonth() + 1;
                    var day = currentTime.getDate();
                    var year = currentTime.getFullYear();
                    var date = day + "/" + month + "/" + year;

                    if (item.status == false) {
                        status = "Chưa thanh toán";
                        boolStatus = "False";
                    }
                    else if (item.status == true) {
                        status = "Đã thanh toán";
                        boolStatus = "True";
                    }
                    //show data table
                    html1 = '<a href="javascript:;" data-id="' + item.id + '" class="btn btn-primary btn-xs btnDetail" title="Xem chi tiết">' +
                                     '<i class="fa fa-folder"></i>' +
                                 '</a>';
                    html2 = '<a href="javascript:;" data-id="' + item.id + '"data-table="' + item.tableID + '" data-promo="' + item.promoName + '" data-user="' + item.userName + '" data-date="' + date + '" data-status="' + boolStatus + '" data-money="' + item.totalMoney + '"class="btn btn-info btn-xs btnEdit" title="Chỉnh sửa"><i class="fa fa-pencil"></i></a>';

                    var rowNode = datatable.row.add([
                       item.id,
                        item.tableName,
                        item.userName,
                        item.promoName,
                        date,
                        item.totalMoney,
                        status,
                        html1,
                        html2,
                    ]).draw(false).node();
                    //Add attribute for this row <tr>
                    $(rowNode)
                        .addClass('trbody-' + item.id);
                })
            })
        .fail(function () {
            alert('fail');
        });
    });
});
