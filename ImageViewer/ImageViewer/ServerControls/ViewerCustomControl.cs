using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImageViewer
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ViewerCustomControl runat=server></{0}:ViewerCustomControl>")]
    public class ViewerCustomControl : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }
            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.WriteLine("<link href='Scripts/pdfjs-dist/viewer.css' rel='stylesheet' />");
            output.Write("<div id='app'><div id='toolbar'><button type='button' id='prev'>Previous</button><button type='button' id='next'>Next</button>&nbsp; &nbsp;<span>Page: <span id='page_num'></span>/ <span id='page_count'></span></span><input type='file' multiple name='openFile' id='openFile' accept='text/plain, application/pdf, image/*' style='display:none' /><button type='button' id='btnOpenFile'>Open File</button></div><div id='viewport'><canvas id='the-canvas' style='border: 1px solid black; direction: ltr;'></canvas></div></div>");
            
            output.Write("<script src='https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.3.200/pdf.min.js'></script>");
            output.Write("<script src='Scripts/pdfjs-dist/viewer.js'></script>");
        }
    }
}
