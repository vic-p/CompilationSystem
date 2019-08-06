
$(function () {

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
        $('#tb_projectclass').bootstrapTable({
            url: '/ProjectClassManagement/GetProjectClass',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar_projectclass',                //工具按钮用哪个容器
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
                field: 'ClassName',
                title: '项目分类名称'
            },{
                field: 'CreateTime',
                title: '创建时间',
                formatter: function (value, row, index) {
                    return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-MM-dd HH:mm:ss");
                }
            }, ]
        });
    };

    return oTableInit;
};


var ButtonInit = function () {
    var oInit = new Object();
    var postdata = {};

    oInit.Init = function () {

        $("#btn_addprojectclass").click(function () {
            $("#projectclassModalLabel").text("新增");
            $("#txt_Id").val("");
            $("#txt_projectclassName").val("");
            postdata.Id = "";
            $('#projectclassModal').modal()
        });

        $("#btn_editprojectclass").click(function () {
            var arrselections = $("#tb_projectclass").bootstrapTable('getSelections', function (row) { return row; });
            if (arrselections.length > 1) {
                toastr.warning('只能选择一行进行编辑');

                return;
            }
            if (arrselections.length <= 0) {
                toastr.warning('请选择有效数据');

                return;
            }
            $("#projectclassModalLabel").text("编辑");
            $("#txt_Id").val(arrselections[0].Id);
            $("#txt_projectclassName").val(arrselections[0].ClassName);

            postdata.Id = arrselections[0].Id;
            $('#projectclassModal').modal();
        });

        $("#btn_deleteprojectclass").click(function () {
            var arrselections = $("#tb_projectclass").bootstrapTable('getSelections', function (row) { return row; });
            if (arrselections.length <= 0) {
                toastr.warning('请选择有效数据');
                return;
            }

            Ewin.confirm({ message: "确认要删除选择的数据吗？" }).on(function (e) {
                if (!e) {
                    return;
                }
                $.ajax({
                    type: "post",
                    url: "/ProjectClassManagement/Delete",
                    dataType: 'json',//服务端返回数据的格式，如果不对会跳到error
                    data: { projectclassList: arrselections },
                    success: function (data, status) {
                        if (status == "success" && data.Result == 1) {
                            toastr.success('提交数据成功');
                            $("#tb_projectclass").bootstrapTable('refresh');
                        }
                        else {
                            toastr.error('提交数据失败');
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

        $("#btn_submitprojectclass").click(function () {
            postdata.Id = $("#txt_Id").val();
            postdata.ClassName = $("#txt_projectclassName").val();
            $.ajax({
                type: "post",
                url: "/ProjectClassManagement/AddOrEdit",
                dataType: 'json',
                data: { projectclass: postdata },
                success: function (data, status) {
                    if (status == "success" && data.Result == 1) {
                        toastr.success('提交数据成功');
                        $("#tb_projectclass").bootstrapTable('refresh');
                    }
                    else {
                        toastr.success('提交数据失败');
                    }
                },
                error: function () {
                    toastr.error('Error');
                },
                complete: function () {

                }

            });
        });

    };

    return oInit;
};