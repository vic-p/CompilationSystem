using PMY.CompilationSystem.IService;
using PMY.CompilationSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMY.CompilationSystem.Service
{
    public class MenuService : IMenuService
    {
        public Dictionary<string, string> GetMenuByRight(UserRight userRight)
        {
            Dictionary<string, string> dicMenu = new Dictionary<string, string>();
            switch (userRight)
            {
                case UserRight.超级管理员:
                    dicMenu.Add("SVN操作", "/SVNOperation/Index");
                    dicMenu.Add("编译", "/ProjectOperation/Index");
                    dicMenu.Add("打包", "/Home/Index");
                    dicMenu.Add("打包记录", "/Home/Index");
                    dicMenu.Add("任务列表", "/TaskOperation/Index");
                    dicMenu.Add("后台管理", "/Background/Index");
                    break;
                case UserRight.普通用户:
                    dicMenu.Add("SVN操作", "/SVNOperation/Index");
                    dicMenu.Add("编译", "/ProjectOperation/Index");
                    dicMenu.Add("打包", "/Home/Index");
                    dicMenu.Add("打包记录", "/Home/Index");
                    dicMenu.Add("任务列表", "/TaskOperation/Index");
                    break;
                case UserRight.游客:
                    dicMenu.Add("打包记录", "/Home/Index");
                    dicMenu.Add("任务列表", "/TaskOperation/Index");
                    break;
            }

            return dicMenu;
        }
    }
}
