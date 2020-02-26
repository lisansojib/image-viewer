<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ImageViewer.Web.WebForm1" %>
<%@ Register Assembly="ImageViewer" Namespace="ImageViewer" TagPrefix="imgViewer" %>

<!DOCTYPE html>

<html dir='ltr' mozdisallowselectionprint>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1, maximum-scale=1'>
    <meta name='google' content='notranslate'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <title>PDF.js viewer</title>
    <link rel='stylesheet' href='Scripts/pdfjs-dist/viewer.css'>
    <link rel='resource' type='application/l10n' href='Scripts/pdfjs-dist/locale/locale.properties'>
    <script src='Scripts/pdfjs-dist/pdf.js'></script>
    <script src='Scripts/pdfjs-dist/viewer.js'></script>
</head>
<body tabindex='1' class='loadingInProgress'>
    <imgViewer:WebCustomControl runat='server' Text='' />
</body>
</html>
