﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageViewer.Views.DocumentViewer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/DocumentViewer/Index.cshtml")]
    public partial class _Views_DocumentViewer_Index_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_DocumentViewer_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<iframe");

WriteAttribute("src", Tuple.Create(" src=\"", 7), Tuple.Create("\"", 33)
            
            #line 1 "..\..\Views\DocumentViewer\Index.cshtml"
, Tuple.Create(Tuple.Create("", 13), Tuple.Create<System.Object, System.Int32>(ViewBag.Framesource
            
            #line default
            #line hidden
, 13), false)
);

WriteAttribute("width", Tuple.Create(" width=\"", 34), Tuple.Create("\"", 56)
            
            #line 1 "..\..\Views\DocumentViewer\Index.cshtml"
, Tuple.Create(Tuple.Create("", 42), Tuple.Create<System.Object, System.Int32>(ViewBag.Width
            
            #line default
            #line hidden
, 42), false)
);

WriteAttribute("height", Tuple.Create(" height=\"", 57), Tuple.Create("\"", 81)
            
            #line 1 "..\..\Views\DocumentViewer\Index.cshtml"
, Tuple.Create(Tuple.Create("", 66), Tuple.Create<System.Object, System.Int32>(ViewBag.Height
            
            #line default
            #line hidden
, 66), false)
);

WriteLiteral("></iframe>");

        }
    }
}
#pragma warning restore 1591
