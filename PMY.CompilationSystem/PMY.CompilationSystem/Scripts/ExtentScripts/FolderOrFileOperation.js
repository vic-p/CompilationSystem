//(function () {
//    var openFolderOrFile = function(path) {
//        try {
//            var obj = new ActiveXObject("wscript.shell");
//            if (obj) {
//                //if (filename.indexOf("http://") == -1)
//                //    obj.CurrentDirectory = filename.substring(0, filename.lastIndexOf("\\") + 1);
//                obj.Run("\"" + path + "\"", 1, false);
//                obj = null;
//            }
//            return "";
//        } catch (e) {
//            return "请确定是否存在该盘符或文件" + path;
//        }
//    };
//    window.openFolderOrFile = openFolderOrFile;
   
//}())

function openFolderOrFile(path) {
    try {
        var obj = new ActiveXObject("wscript.shell");
        if (obj) {
            //if (filename.indexOf("http://") == -1)
            //    obj.CurrentDirectory = filename.substring(0, filename.lastIndexOf("\\") + 1);
            obj.Run("\"" + path + "\"", 1, false);
            obj = null;
        }
        return "";
    } catch (e) {
        return "请确定是否存在该盘符或文件" + path;
    }
}

function selectFolder() {
    try {
        //Shell.BrowseForFolder(Hwnd, Title,Options, [RootFolder])
        var filePath;
        var objSrc = new ActiveXObject("Shell.Application").BrowseForFolder(0, '请选择保存路径', 0, '');
        if (objSrc != null) {
            filePath = objSrc.Items().Item().Path;
            if (filePath.charAt(0) == ':') {
                alert('请选择文件夹.');
                return;
            }
            return filePath;
        }
    } catch (e) {
        alert(e + '请设置IE，Internet选项－安全－自定义级别－将ActiveX控件和插件前3个选项设置为启用，然后再尝试。');
        return;
    }
}

function SaveAs() {
    var fd = new ActiveXObject("MSComDlg.CommonDialog");
    //fd.Filter = "Microsoft Office Excel(*.xlsx)|*.xsl";
    fd.FilterIndex = 2;

    // 必须设置MaxFileSize. 否则出错
    fd.MaxFileSize = 128;

    // 显示对话框
    fd.ShowOpen();
}
