<%@ Page Title='About' Language='C#' AutoEventWireup='true' MasterPageFile="~/Site.Master" CodeBehind='About.aspx.cs' Inherits='ImageViewer.WebR.About' %>
<%@ Register Assembly="ImageViewer" Namespace="ImageViewer" TagPrefix="cc" %>

<asp:Content ID='BodyContent' ContentPlaceHolderID='MainContent' runat='server'>
    <cc:DocumentViewer runat="server" Width="80%" Height="500" FilePath="~/Scripts/pdf.js/web/compressed.tracemonkey-pldi-09.pdf" />
</asp:Content>