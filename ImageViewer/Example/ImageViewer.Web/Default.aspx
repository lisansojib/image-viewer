<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ImageViewer.Web._Default" %>
<%@ Register Assembly="ImageViewer" Namespace="ImageViewer" TagPrefix="imgViewer" %>

<asp:Content ID='BodyContent' ContentPlaceHolderID='MainContent' runat='server'>
    <imgViewer:ViewerCustomControl runat='server' Text='' />

    <%--<div id='app'><div id='toolbar'><button type='button' id='prev'>Previous</button><button type='button' id='next'>Next</button>&nbsp; &nbsp;<span>Page: <span id='page_num'></span>/ <span id='page_count'></span></span></div><div id='viewport'><canvas id='the-canvas' style='border: 1px solid black; direction: ltr;'></canvas></div></div>--%>
</asp:Content>