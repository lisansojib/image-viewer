<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ImageViewer.Web._Default" %>
<%@ Register Assembly="ImageViewer" Namespace="ImageViewer" TagPrefix="cc" %>

<asp:Content ID='BodyContent' ContentPlaceHolderID='MainContent' runat='server'>
    <cc:DocumentViewer runat="server" Width="100%" Height="500" 
        FilePath="~/Scripts/pdf.js/web/compressed.tracemonkey-pldi-09.pdf" 
        IsRemote="true"
        IsMinified="true"
        ScaleSelect="1.5"
        EnableFullScreenButton="true"
        EnableOpenButton="true"
        EnablePrintButton="false"
        EnableDownloadButton="true"
        EnableRotateButton="false"
        EnableFindButton="true"
        EnableViewScaleCombo="true"
        EnableZoomInZoomOutButton="true"
        EnableDisplayThumbnailList="true"
        EnableTextSelectionTool="true"
        ShowDocumentProperties="true"
        />
</asp:Content>