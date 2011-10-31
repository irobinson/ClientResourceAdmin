<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ClientResourceAdmin.ascx.cs" Inherits="ClientResourceAdmin.ClientResourceAdmin" %>

CDF Version: <asp:Label runat="server" ID="Version" /> <br/>
Enable composite files: <asp:CheckBox runat="server" ID="EnableCompositeFiles" /><br/>
Minify CSS: <asp:CheckBox runat="server" ID="MinifyCss" /><br/>
Minify JS: <asp:CheckBox runat="server" ID="MinifyJs" /><br/>
Persist files: <asp:CheckBox runat="server" ID="PersistFiles" /><br/>
Url type:
<asp:DropDownList runat="server" ID="UrlTypeList">
    <asp:ListItem Selected="True">MappedId</asp:ListItem>
    <asp:ListItem>Base64QueryStrings</asp:ListItem>
    <asp:ListItem>Base64Paths</asp:ListItem>
</asp:DropDownList><br/>

<asp:LinkButton runat="server" ID="SaveButton" >Save</asp:LinkButton>
