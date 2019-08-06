$(function () {
    var oSearchTime = new SearchTimeInit();
    oSearchTime.Init();

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
        $('#tb_task').bootstrapTable({
            url: '/TaskOperation/GetTaskList',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            //toolbar: '#',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）这里设置为false会显示不出记录
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 15,                       //每页的记录行数（*）
            pageList: [10,15,20],        //可供选择的每页的行数（*）//不做分页这里设置成[]就不显示选择每页显示多少了
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
                field: 'Id',
                title: '编号'
            }, {
                field: 'TaskType',
                title: '任务类型',
                formatter: function (value, row, index) {
                    switch (value) {
                        case 1:
                            return "更新";
                        case 2:
                            return "编译";
                        case 3:
                            return "编译并发布";
                        case 4:
                            return "打包";
                        default:
                            return value;
                    }
                }
            }, {
                field: 'TaskStatus',
                title: '任务状态',
                formatter: function (value, row, index) {
                    switch (value) {
                        case 1:
                            return "未处理";
                        case 2:
                            return "处理中";
                        case 3:
                            return "已完成";
                        case 4:
                            return "出现错误";
                        case 5:
                            return "已取消";
                        default:
                            return value;
                    }
                }
            }, {
                field: 'ActionPath',
                title: '路径'
            }, {
                field: 'CreateTime',
                title: '生成时间',
                formatter: function (value, row, index) {
                    if (value) {
                        return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }, {
                field: 'Creator',
                title: '创建者'
            }, {
                field: 'FinishTime',
                title: '完成时间',
                formatter: function (value, row, index) {
                    if (value) {
                        return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-MM-dd HH:mm:ss");
                    }
                }
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

    //得到查询的参数
    oTableInit.queryParams = function (params) {
        var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的          
            offset: params.offset,  //页码
            limit: params.limit,   //页面大小
            createTime: $("#txt_search_datetime").val(),

        };
        return temp;
    };

    return oTableInit;
};

var ButtonInit = function () {
    var oInit = new Object();
    oInit.Init = function () {
        $("#btn_query").click(function () {
            $("#tb_task").bootstrapTable('refresh');
        });
    }
    return oInit;
}

var SearchTimeInit = function () {
    var oSearchTimeInit = new Object();
    oSearchTimeInit.Init = function () {
        $("#txt_search_datetime").val(new Date().pattern("yyyy-MM-dd"));
        $("#txt_search_datetime").datetimepicker({
            language: 'zh-CN',
            format: 'yyyy-mm-dd',
            minView: 'month',
            initialDate: new Date(),
            autoclose: true,
            todayBtn: true

        })
    }

    return oSearchTimeInit;
}