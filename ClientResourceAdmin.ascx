<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ClientResourceAdmin.ascx.cs" Inherits="ClientResourceAdmin.ClientResourceAdmin" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<p><dnn:Label runat="server" ResourceKey="Intro.Text" /></p>

<div class="dnnForm dnnClear">
    <fieldset>
	    <div class="dnnFormItem">
		    <dnn:Label runat="server" ResourceKey="VersionLabel"/>
            <asp:Label runat="server" ID="Version" />
		</div>
		<div class="dnnFormItem">
            <dnn:Label runat="server" ResourceKey="CompositeFilesLabel"/>
            <asp:CheckBox runat="server" ID="EnableCompositeFiles" />
		</div>
		<div class="dnnFormItem">
            <dnn:Label runat="server" ResourceKey="MinifyCssLabel"/>
            <asp:CheckBox runat="server" ID="MinifyCss" />            
		</div>
		<div class="dnnFormItem">
        <dnn:Label runat="server" ResourceKey="MinifyJsLabel"/>
            <asp:CheckBox runat="server" ID="MinifyJs" />
		</div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ResourceKey="PersistFilesLabel"/>
            <asp:CheckBox runat="server" ID="PersistFiles" />
		</div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ResourceKey="UrlTypeLabel"/>
            <asp:DropDownList runat="server" ID="UrlTypeList">
                <asp:ListItem Selected="True">MappedId</asp:ListItem>
                <asp:ListItem>Base64QueryStrings</asp:ListItem>
            </asp:DropDownList>
        </div>
	</fieldset>
</div>

<asp:LinkButton runat="server" ID="SaveButton" ResourceKey="SaveButton" CssClass="dnnPrimaryAction"></asp:LinkButton>