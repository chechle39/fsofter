$(function () {
    $("form[name='login-form']").validate({
        rules: {
            username: "required",
            password: {
                required: true,
                minlength: 6
            },
            newPassword: {
                required: true,
                minlength: 6
            },
            rePassword: {
                equalTo: "#newPassword",
                required: true,
                minlength: 6
            },
            email: {
                required: true,
                email: true
            }
        },
        // Specify validation error messages
        messages: {
            username: "Vui long nhap username",
            password: {
                required: "Vui long nhap password",
                minlength: "Password tren 6 ky tu"
            },
            newPassword: {
                required: "Vui long nhap password mới",
                minlength: "Password tren 6 ky tu"
            },
            rePassword: {
                equalTo: "Nhập lại mật khẩu không giống",
                required: "Vui long nhap xác nhận lại password",
                minlength: "Password tren 6 ky tu"
            },
            email: {
                required: "Vui long nhap email",
                email: "Vui lòng nhập đúng định dạng email"
            }
        },

        invalidHandler: function(e, validator){
            var errors = validator.errorList;
            if (errors) {
                errors.forEach(function (value) {
                    alert(value.message);
                })
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});
