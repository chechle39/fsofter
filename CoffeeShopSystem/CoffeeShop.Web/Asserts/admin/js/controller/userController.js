var user = {
    init: function () {
        user.ok();
    },
    ok: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "Json",
                type: "POST",
                success: function (r) {
                    console.log(r);
                    if (r.status == true) {
                        btn.text('Kích hoạt');
                    } else {
                        btn.text('Khóa');
                    }
                }
            });
        });
    }
}
user.init();