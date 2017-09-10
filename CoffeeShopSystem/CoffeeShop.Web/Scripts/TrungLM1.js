//<!-- GetPromotionById-->

$('.EditItem').on('click', function () {

    var id = $(this).data('proid');
    $('#ProIDText').val(id);

    var form = $('#GetEditForm');

    $.ajax({
        url: form.attr('action'),
        type: form.attr('method'),

        dataType: 'json',
        data: form.serialize(),
        timeout: 30 * 1000,

    })
         .done(function (result) {

             $('#EIDtext').val(result.ID);
             $('#EShoptext').val(result.ShopID);
             $('#Enametext').val(result.Name);

             // Gán date
             var date = new Date(parseInt(result.StartDate.replace('/Date(', '')));
             var month = date.getMonth() + 1;
             if (month < 10) month = '0' + month;
             var day = date.getDate();
             if (day < 10) month = '0' + day;
             var year = date.getFullYear();
             var StartDate = year + "-" + month + "-" + day;
             $('#EStartDate').val(StartDate);

             // Gán date
             var date = new Date(parseInt(result.EndDate.replace('/Date(', '')));
             var month = date.getMonth() + 1;
             if (month < 10) month = '0' + month;
             var day = date.getDate();
             if (day < 10) month = '0' + day;
             var year = date.getFullYear();
             var EndDate = year + "-" + month + "-" + day;
             $('#EEndDate').val(EndDate);
             $('#Epurchasenumber').val(result.BasePurchase);
             $('#Ediscountnumer').val(result.Discount);
             $('#Ediscriptiontextarea').val(result.Description);

         })

         .fail(function (jqXHR, status, error) {
             console.log(jqXHR);
             alert('Load thất bại!');

         });

})

//<!-- AddModal -->

$('#AddModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var recipient = button.data('whatever') // Extract info from data-* attributes
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this)
    modal.find('.modal-title').text('New message to ' + recipient)
    modal.find('.modal-body input').val(recipient)
})

//<!-- EditModal -->

$('#EditModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var recipient = button.data('whatever') // Extract info from data-* attributes
 
    var modal = $(this)
    modal.find('.modal-title').text('New message to ' + recipient)
    modal.find('.modal-body input').val(recipient)
})

//<!-- DeleteModal -->

$('#DeleteModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var recipient = button.data('whatever') // Extract info from data-* attributes
    
    var modal = $(this)
    modal.find('.modal-title').text('New message to ' + recipient)
    modal.find('.modal-body input').val(recipient)
})


//<!-- RecoveryModal -->

$('#RecoveryModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var recipient = button.data('whatever') // Extract info from data-* attributes
   
    var modal = $(this)
    modal.find('.modal-title').text('New message to ' + recipient)
    modal.find('.modal-body input').val(recipient)
})

//<!-- Validate AddForm -->

$('#AddForm').validate({
    rules: {
        Name: {
            required: true,
        },
        StartDate: {
            required: true,
        },        
        EndDate: {
            required: true,
        },

        BasePurchase: {
            required: true,
        },


        Discount: {
            required: true,
        },

    },

    messages: {
        Name: {
            required: 'Vui lòng nhập tên',
        },
        StartDate: {
            required: 'Please enter Start Date',
        },
        EndDate: {
            required: 'Please enter End Date',
        },

        BasePurchase: {
            required: 'Please enter BasePurchase',
        },

        Discount: {
            required: 'Please enter Discount',
        },
    },

    errorElement: "span",
    errorClass: "help-block",

    highlight: function (elements) {
        $(elements).closest('.form-group').removeClass('has-success');
        $(elements).closest('.form-group').addClass('has-error');

    },
    success: function (label) {
        $(label).closest('.form-group')
        .removeClass('has-error');

        $(label).closest('.form-group')
       .addClass('has-success');

        label.remove();

    },
});



//<!-- Validate EditForm -->

$('#EditForm').validate({
    rules: {
        Name: {
            required: true,
        },
        StartDate: {
            required: true,
        },

        EndDate: {
            required: true,
        },

        BasePurchase: {
            required: true,
        },


        Discount: {
            required: true,
        },

    },

    messages: {
        Name: {
            required: 'Vui lòng nhập tên',
        },
        StartDate: {
            required: 'Please enter Start Date',
        },
        EndDate: {
            required: 'Please enter End Date',
        },

        BasePurchase: {
            required: 'Please enter BasePurchase',
        },

        Discount: {
            required: 'Please enter Discount',
        },
    },

    errorElement: "span",
    errorClass: "help-block",

    highlight: function (elements) {
        $(elements).closest('.form-group').removeClass('has-success');
        $(elements).closest('.form-group').addClass('has-error');

    },
    success: function (label) {
        $(label).closest('.form-group')
        .removeClass('has-error');

        $(label).closest('.form-group')
       .addClass('has-success');

        label.remove();

    },
});


//<!-- Submit Add -->

$(document).ready(function () {
    $('#submitAdd').on('click', function () {

        //// Check validate
        var form = $('#AddForm');
        form.validate();

        if (form.valid() == true) {
            $('#AddForm').submit();
            $('#AddModal').modal('hide');
        }

        else {
        }
    })
})

//<!-- Ajax: Submit Edit-->

$(document).ready(function () {
    $('#submitEdit').on('click', function () {
        // Check validate
        var form = $('#EditForm');
        form.validate();
        if (form.valid() == true) {
            //Call ajax
            $.ajax({
                url: form.attr('action'),
                type: form.attr('method'),

                dataType: 'json',
                data: form.serialize(),
                timeout: 30 * 1000,

            })
                .done(function (n) {
                    // update this row in table
                    var id = form.find('[name=ID]').val();//?form > result
                    var name = form.find('[name=Name]').val();
                    var basepurchase = form.find('[name=BasePurchase]').val();//?val();
                    var discount = form.find('[name=Discount]').val();
                    var startdate = form.find('[name=StartDate]').val();
                    var enddate = form.find('[name=EndDate]').val();
                    var description = form.find('[name=Description]').val();

                    var table = $('#BodyTable');
                    table.find('#ID-' + id).html(id);
                    table.find('#Name-' + id).html(name);
                    table.find('#BasePurchase-' + id).html(basepurchase);
                    table.find('#Discount-' + id).html(discount);
                    table.find('#StartDate-' + id).html(startdate);
                    table.find('#EndDate-' + id).html(enddate);
                    table.find('#Description-' + id).html(description);

                    $('#EditModal').modal('hide');
                })

             .fail(function (jqXHR, status, error) {
                 console.log(jqXHR);
                 alert('Cập nhật thất bại!');
             });
        }
        else { }
    })
})

//<!-- Ajax: Submit Delete-->

$('.DeleteItem').on('click', function () {
    //show Modal
    var iddel = $(this).data('proid');

    //Submit delete
    $('#submitDelete').on('click', function () {
        //Call Ajax
        $.ajax({
            url: 'Promotion/Delete/',
            type: 'post',
            data: {'idToDelete': iddel},
            //dataType: 'json',
        })

         .done(function (result) {
             if (result == false) {
                 alert('Không được xóa khuyến mãi đang trong hóa đơn !!!');

                 $('#DeleteModal').modal('hide');
             }
             else {
                 $('#DeleteModal').modal('hide');

                 // Remove this row in table
                 $('#BodyTable #item-' + iddel).remove();
             }

         })

         .fail(function (jqXHR, status, error) {
             console.log(jqXHR);
             alert('Xóa thất bại!');
         });

    })
})

//<!-- Ajax: Submit Recovery-->

$('.RecoveryItem').on('click', function () {
    //show Modal
    var idrecovery = $(this).data('proid');

    //Submit
    $('#submitRecovery').on('click', function () {
        //Call Ajax
        $.ajax({
            url: '/Promotion/Recovery/',
            type: 'post',
            data: { 'idToRecovery': idrecovery },
            //dataType: 'json',
        })

         .done(function (result) {
             $('#RecoveryModal').modal('hide');

             // Remove this row in table
             $('#BodyTable #item-' + idrecovery).remove();

         })

         .fail(function (jqXHR, status, error) {
             console.log(jqXHR);
             alert('Phục hồi thất bại!');
         });

    })
})

//<!-- Sort -->

function sortTable(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("listTable");
    switching = true;
    //Set the sorting direction to ascending:
    dir = "asc";
    /*Make a loop that will continue until
    no switching has been done:*/
    while (switching) {
        //start by saying: no switching is done:
        switching = false;
        rows = table.getElementsByTagName("TR");
        /*Loop through all table rows (except the
        first, which contains table headers):*/
        for (i = 1; i < (rows.length - 1); i++) {
            //start by saying there should be no switching:
            shouldSwitch = false;
            /*Get the two elements you want to compare,
            one from current row and one from the next:*/
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            /*check if the two rows should switch place,
            based on the direction, asc or desc:*/
            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch= true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                    //if so, mark as a switch and break the loop:
                    shouldSwitch= true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            /*If a switch has been marked, make the switch
            and mark that a switch has been done:*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            //Each time a switch is done, increase this count by 1:
            switchcount ++;
        } else {
            /*If no switching has been done AND the direction is "asc",
            set the direction to "desc" and run the while loop again.*/
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}


//<!-- Load list by radio-->

$('#RadioForm [name=optradio]').change(function () {
    var select = $(this).val();
    location.href = "/Promotion/LoadByCondition?select=" + select;
})

//<!-- Option search button-->

        $('#OptionSearch').on('click', function () {
            var click = $(this);
            if (click.data('option') == "0")
            {
                $('#AdvancedSearch').removeClass('hidden');
                click.data('option', '1');
            }
            else {
                $('#SearchForm').addClass('hidden');
                click.data('option', '0');
            }
        })