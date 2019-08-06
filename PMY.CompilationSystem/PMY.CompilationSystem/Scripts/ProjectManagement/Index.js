
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
        $('#tb_projects').bootstrapTable({
            url: '/ProjectManagement/Getprojects',         //请求后台的URL（*）
            method: 'get',                      //请求方式（*）
            toolbar: '#toolbar_projects',                //工具按钮用哪个容器
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
                field: 'ProjectClassName',
                title: '项目分类'
            }, {
                field: 'ProjectName',
                title: '项目名称'
            }, {
                field: 'ProjectPath',
                title: '项目路径'
            }, {
                field: 'PublishPath',
                title: '发布路径'
            }, {
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
        $.get("/ProjectClassManagement/GetProjectClassOption", function (data) {
            $("#txt_ProjectClass").html(data.Tag);
        })
        //$.ajax({
        //    type: "post",
        //    url: "/SvnPathManagement/GetProjectClassOption",
        //    dataType: 'json',//服务端返回数据的格式，如果不对会跳到error
        //    data: {},
        //    success: function (data, status) {
        //        if (status == "success" && data.Result == 1) {
        //            $("#txt_ProjectClass").html(data.Tag);
        //        }
        //    },
        //    error: function () {
        //        toastr.error('Error');
        //    },
        //    complete: function () {
        //    }
        //});

        $("#btn_addproject").click(function () {
            $("#projectModalLabel").text("新增");
            $("#txt_Id").val("");
            $("#txt_ProjectClass").val("");
            $("#txt_ProjectName").val("");
            $("#txt_ProjectPath").val("");
            $("#txt_PublishPath").val("");
            $("#txt_Sort").val("");
            postdata.Id = "";
            $('#projectModal').modal()
        });

        $("#btn_editproject").click(function () {
            var arrselections = $("#tb_projects").bootstrapTable('getSelections', function (row) { return row; });
            if (arrselections.length > 1) {
                toastr.warning('只能选择一行进行编辑');

                return;
            }
            if (arrselections.length <= 0) {
                toastr.warning('请选择有效数据');

                return;
            }
            $("#projectModalLabel").text("编辑");
            $("#txt_Id").val(arrselections[0].Id);
            $("#txt_ProjectClass").val(arrselections[0].ProjectClassName);
            $("#txt_ProjectName").val(arrselections[0].ProjectName);
            $("#txt_ProjectPath").val(arrselections[0].ProjectPath);
            $("#txt_PublishPath").val(arrselections[0].PublishPath);
            $("#txt_Sort").val(arrselections[0].Sort);

            postdata.Id = arrselections[0].Id;
            $('#projectModal').modal();
        });

        $("#btn_deleteproject").click(function () {
            var arrselections = $("#tb_projects").bootstrapTable('getSelections', function (row) { return row; });
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
                    url: "/ProjectManagement/Delete",
                    dataType: 'json',//服务端返回数据的格式，如果不对会跳到error
                    data: { projectsList:arrselections},
                    success: function (data, status) {
                        if (status == "success" && data.Result == 1) {
                            toastr.success('提交数据成功');
                            $("#tb_projects").bootstrapTable('refresh');
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

        $("#btn_PublishPath").click(function () {
            var folderPath = selectFolder();
            $("#txt_PublishPath").val(folderPath);
        })

        $("#btn_ProjectPath").click(function () {
            var selectfile = document.getElementById("selectfile");
            selectfile.click();
            var fileName = selectfile.value;
            $("#txt_ProjectPath").val(fileName);
        })

        $("#btn_submitproject").click(function () {
            postdata.Id = $("#txt_Id").val();
            postdata.ProjectClassName = $("#txt_ProjectClass").val();
            postdata.ProjectName = $("#txt_ProjectName").val();
            postdata.ProjectPath = $("#txt_ProjectPath").val();
            postdata.PublishPath = $("#txt_PublishPath").val();
            postdata.Sort = $("#txt_Sort").val();
            $.ajax({
                type: "post",
                url: "/ProjectManagement/AddOrEdit",
                dataType: 'json',
                data: { projects: postdata },
                success: function (data, status) {
                    if (status == "success" && data.Result == 1) {
                        toastr.success('提交数据成功');
                        $("#tb_projects").bootstrapTable('refresh');
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