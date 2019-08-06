$(function () {
    //初始化 toastr。将这个属性值设置为不同的值就能让提示信息显示在不同的位置，如toast-bottom-right表示下右、toast-bottom-center表示下中、toast-top-center表示上中等，更过位置信息请查看文档
    toastr.options.positionClass = 'toast-top-center';
    toastr.options.closeButton = true;

    $("#btnLogin").click(function () {
        $.ajax({
            type: "post",
            url: "/Login/UserLogin",
            dataType: 'json',//服务端返回数据的格式，如果不对会跳到error
            data: { "LoginCode": $("#LoginCode").val(), "LoginPwd": $("#LoginPwd").val() },
            cache:false,
            success: function (data, status) {
                if (status == "success" && data.Result == 1) {
                    window.location.href = "/SVNOperation/Index"
                }
                else {
                    $("#errorMsg").css("display", "block");
                    $("#errorMsg").text(data.PromptMsg);
                    toastr.error(data.PromptMsg);
                }
            },
            error: function () {
                toastr.error('Error');
            },
            complete: function () {
            }

        });
    });

    $("#visitorLogin").click(function () {
        $.ajax({
            type: "post",
            url: "/Login/VisitorLogin",
            dataType: 'json',//服务端返回数据的格式，如果不对会跳到error
            data: { },
            cache: false,
            success: function (data, status) {
                if (status == "success" && data.Result == 1) {
                    window.location.href = "/Home/Index"
                }
                else {
                    $("#errorMsg").css("display", "block");
                    $("#errorMsg").text(data.PromptMsg);
                    toastr.error(data.PromptMsg);
                }
            },
            error: function () {
                toastr.error('Error');
            },
            complete: function () {
            }

        });
    });

});