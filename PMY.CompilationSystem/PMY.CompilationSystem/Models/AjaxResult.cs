﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMY.CompilationSystem.Models
{
    public class AjaxResult
    {
        public AjaxResult()
        { }

        public string PromptMsg { get; set; }
        public DoResult Result { get; set; }
        public object Tag { get; set; }
    }

    public enum DoResult
    {
        Failed = 0,
        Success = 1,
        OverTime = 2,
        NoAuthorization = 3,
        Other = 255
    }
}