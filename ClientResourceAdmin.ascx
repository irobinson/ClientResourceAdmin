<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ClientResourceAdmin.ascx.cs" Inherits="ClientResourceAdmin.ClientResourceAdmin" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm dnnClear clientResourceAdmin">

    <asp:Label runat="server" id="IntroLabel"/>

    <fieldset>
	    <div class="dnnFormItem">
		    <dnn:Label runat="server" ResourceKey="VersionLabel"/>
            <asp:Label runat="server" ID="Version" />
		</div>
        <div class="dnnFormItem" runat="server" id="LoggerRow">
		    <dnn:Label runat="server" ResourceKey="LoggerLabel"/>
            <asp:Label runat="server" ID="Logger" />
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

<asp:LinkButton runat="server" ID="SaveButton" ResourceKey="SaveButton" CssClass="dnnPrimaryAction" />
<asp:LinkButton runat="server" ID="IncrementVersionButton" ResourceKey="IncrementVersionButton" CssClass="dnnSecondaryAction" />

<script type="text/javascript">
    (function ($) {
        $('#<%= SaveButton.ClientID %>').dnnConfirm({
            text: '<%= LocalizeString("SaveButton.Confirm") %>',
            yesText: '<%= Localization.GetString("Yes.Text", Localization.SharedResourceFile) %>',
            noText: '<%= Localization.GetString("No.Text", Localization.SharedResourceFile) %>',
            title: '<%= Localization.GetString("Confirm.Text", Localization.SharedResourceFile) %>'
        });
        $('#<%= IncrementVersionButton.ClientID %>').dnnConfirm({
            text: '<%= LocalizeString("IncrementVersionButton.Confirm") %>',
            yesText: '<%= Localization.GetString("Yes.Text", Localization.SharedResourceFile) %>',
            noText: '<%= Localization.GetString("No.Text", Localization.SharedResourceFile) %>',
            title: '<%= Localization.GetString("Confirm.Text", Localization.SharedResourceFile) %>'
        });
    })(jQuery);    
</script>