
$(function () {
    //初始化Singalr
    var oSignalrInit = new SignalrInit();
    oSignalrInit.Init();

    //1.初始化Table
    var oTable = new TableInit();
    oTable.Init();

    //2.初始化Button的点击事件
    var oButtonInit = new ButtonInit();
    oButtonInit.Init();

    //3、初始化 toastr。将这个属性值设置为不同的值就能让提示信息显示在不同的位置，如toast-bottom-right表示下右、toast-bottom-center表示下中、toast-top-center表示上中等，更过位置信息请查看文档
    toastr.options.positionClass = 'toast-top-center';
    toastr.options.closeButton = true;   

});


var TableInit = function () {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $('#tb_svnfolders').bootstrapTable({
            url: '/SvnPathManagement/GetSVNFolders',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar_svnfolders',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）这里设置为false会显示不出记录
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: {},//oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 1000,                       //每页的记录行数（*）
            pageList: [],        //可供选择的每页的行数（*）//不做分页这里设置成[]就不显示选择每页显示多少了
            search: false,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            strictSearch: false,
            showColumns: false,                  //是否显示所有的列
            showRefresh: false,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: true,                //是否启用点击选中行
            height: 700,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "Id",                     //每一行的唯一标识，一般为主键列
            showToggle: false,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            columns: [{
                checkbox: true
            }, {
                field: 'Sort',
                title: '排序'
            }, {
                field: 'FolderName',
                title: '名称'
            }, {
                field: 'FolderPath',
                title: '路径'
            }]
            //}, {
            //    field: '',
            //    title: '操作',
            //    formatter: function (value, row, index) {
            //        return "<button  type='button' class='btn btn-default'>更新</button>";
            //      }
            //}]
        });
    };

    return oTableInit;
};


var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {
        $("#btn_svnupdate").click(function () {
            var arrselections = $("#tb_svnfolders").bootstrapTable('getSelections', function (row) { return row; });//返回一个数组
            if (arrselections.length <= 0) {
                toastr.warning('请选择有效数据');
                return;
            }

            $.ajax({
                type: "post",
                url: "/SVNOperation/Update",
                dataType: 'json',//服务端返回数据的格式，如果不对会跳到error
                data: { svnFoldersList: arrselections },//MVC的Action绑定数组类型的参数，这里一定要传一个数组而不能是字符串
                success: function (data, status) {
                    if (status == "success" && data.Result == 1) {
                        toastr.success(data.PromptMsg);
                        $("#tb_svnfolders").bootstrapTable('refresh');
                    }
                    else {
                        toastr.error('任务添加失败');
                    }
                },
                error: function () {
                    toastr.error('Error');
                },
                complete: function () {

                }

            });
        });
        $("#btn_folderopen").click(function () {
            var arrselections = $("#tb_svnfolders").bootstrapTable('getSelections', function (row) { return row; });
            if (arrselections.length <= 0) {
                toastr.warning('请选择有效数据');
                return;
            }

            for (i = 0; i < arrselections.length; i++) {
                var obj = openFolderOrFile(arrselections[i].FolderPath.replace(/\\/g, '\\\\'));
                if (!(typeof obj == "undefined" || obj == null || obj == ""))
                    toastr.error(obj);
            }
            
            
        });

    };

    return oInit;
};

var SignalrInit = function () {
    var oSignalrInit = new Object();
    oSignalrInit.Init = function () {
        //在js里面引用后台Hub的类时，启用的是骆驼命名法，所以类名的首字母得小写.
        var cmdMessageHub = $.connection.cMDMessageHub;
        //addNewMessageToPage名称要跟服务端发送消息方法定义的接口一致,在这里实现该接口，当调用服务端的发送信息的方法（可在客户端或服务端调用）时，就等于调用此方法
        cmdMessageHub.client.addNewMessageToPage = function (message) {
            $('#CMDMessage').append('<li>' + message + '</li>');
            //让滚动条一直在最下
            $(".pre-scrollable").scrollTop($(".pre-scrollable")[0].scrollHeight);
        }
        $.connection.hub.start().done(function () {
            //$('#sendmessage').click(function () {
            //    // Call the Send method on the hub. 
            //    chat.server.send($('#displayname').val(), $('#message').val());
            //    // Clear text box and reset focus for next comment. 
            //    $('#message').val('').focus();
            //});
        });


    };
    return oSignalrInit;
}