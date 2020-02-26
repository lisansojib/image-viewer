<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ImageViewer.Web._Default" %>
<%@ Register Assembly="ImageViewer" Namespace="ImageViewer" TagPrefix="cc" %>

<asp:Content ID='BodyContent' ContentPlaceHolderID='MainContent' runat='server'>
    <cc:DocumentViewer runat="server" Width="100%" Height="500" FilePath="~/Scripts/pdf.js/web/compressed.tracemonkey-pldi-09.pdf" />
</asp:Content>