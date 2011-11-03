namespace ClientResourceAdmin
{
    using System;
    using System.Text;
    using System.Xml;
    using System.Xml.XPath;
    using DotNetNuke.Application;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Log.EventLog;
    using DotNetNuke.UI.Skins.Controls;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    public partial class ClientResourceAdmin : PortalModuleBase
    {
        protected override void OnInit(EventArgs e)
        {
            this.SaveButton.Click += Save;
            this.IncrementVersionButton.Click += IncrementVersion;
            this.Load += PageLoad;
        }

        protected void PageLoad(object sender, EventArgs e)
        {
            ValidateSuperUser();

            if (!this.Page.IsPostBack)
            {
                UpdateView();
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            ValidateSuperUser();

            try
            {
                var doc = new XmlDocument();
                var xml = GetNewConfig();
                doc.LoadXml(xml);
                Application app = DotNetNukeContext.Current.Application;
                var merge = new DotNetNuke.Services.Installer.XmlMerge(doc, Globals.FormatVersion(app.Version), app.Description);
                merge.UpdateConfigs();

                ShowSuccessMsg();
                LogHostAlertToEventViewer(LocalizeString("ConfigurationUpdate.Title"), string.Format(LocalizeString("ConfigurationUpdate.Message"), GetFormValuesAsString()));
            }
            catch (Exception ex)
            {
                ShowErrorAndLogException(ex);
            }
        }

        private string GetFormValuesAsString()
        {
            var values = new StringBuilder();
            values.Append("Enable Composite files: " + this.EnableCompositeFiles.Checked.ToString().ToLower() + ", ");
            values.Append("Minify CSS: " + this.MinifyCss.Checked.ToString().ToLower() + ", ");
            values.Append("Minify JS: " + this.MinifyJs.Checked.ToString().ToLower() + ", ");
            values.Append("Persist Files:" + this.PersistFiles.Checked.ToString().ToLower() + ", ");
            values.Append("Url Type: " + this.UrlTypeList.SelectedValue + ".");
            return values.ToString();
        }

        protected void IncrementVersion(object sender, EventArgs e)
        {
            try
            {
                ClientResourceManager.UpdateVersion();
                LogHostAlertToEventViewer(LocalizeString("VersionIncremented.Title"), LocalizeString("VersionIncremented.Message"));
                ShowSuccessMsg();
            }
            catch (Exception ex)
            {
                ShowErrorAndLogException(ex);
            }
        }

        private string GetNewConfig()
        {
            string xml = Localization.GetString("Basic.Text", this.LocalResourceFile);
            xml = string.Format(xml,
                this.Version.Text, // {0}
                this.EnableCompositeFiles.Checked.ToString().ToLower(), // {1}
                this.MinifyCss.Checked.ToString().ToLower(), // {2}
                this.MinifyJs.Checked.ToString().ToLower(), // {3}
                this.PersistFiles.Checked.ToString().ToLower(), // {4}
                this.UrlTypeList.SelectedValue, // {5}
                this.Logger.Text // {6}
                );
            return xml;
        }

        private void UpdateView()
        {
            XPathNavigator config = Config.Load().CreateNavigator();
            XPathNavigator clientDependencyNode = config.SelectSingleNode("/configuration/clientDependency");

            if (clientDependencyNode != null)
            {
                XPathNavigator fileProcessingProvider = config.SelectSingleNode("configuration/clientDependency/compositeFiles/fileProcessingProviders/add");
                XPathNavigator fileRegistrationProvider = config.SelectSingleNode("configuration/clientDependency/fileRegistration/providers/add");
                
                int version = XmlUtils.GetAttributeValueAsInteger(clientDependencyNode, "version", 0);
                string loggerType = XmlUtils.GetAttributeValue(clientDependencyNode, "loggerType");

                bool compositeFiles = XmlUtils.GetAttributeValueAsBoolean(fileRegistrationProvider, "enableCompositeFiles", true);
                bool minifyCss = XmlUtils.GetAttributeValueAsBoolean(fileProcessingProvider, "enableCssMinify", false);
                bool minifyJs = XmlUtils.GetAttributeValueAsBoolean(fileProcessingProvider, "enableJsMinify", true);
                bool persistFiles = XmlUtils.GetAttributeValueAsBoolean(fileProcessingProvider, "persistFiles", true);
                string urlType = XmlUtils.GetAttributeValue(fileProcessingProvider, "urlType");

                this.Version.Text = version.ToString();
                this.EnableCompositeFiles.Checked = compositeFiles;
                this.MinifyCss.Checked = minifyCss;
                this.MinifyJs.Checked = minifyJs;
                this.PersistFiles.Checked = persistFiles;
                this.UrlTypeList.SelectedValue = urlType;
                this.Logger.Text = loggerType;

                this.LoggerRow.Visible = !string.IsNullOrEmpty(loggerType);
            }
        }

        private void LogHostAlertToEventViewer(string title, string message)
        {
            new EventLogController().AddLog(title, message, this.PortalSettings, this.UserId, EventLogController.EventLogType.HOST_ALERT);
        }

        private void ShowSuccessMsg()
        {
            var message = string.Format(Localization.GetString("Success.Text", this.LocalResourceFile), Globals.NavigateURL());
            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, message, ModuleMessage.ModuleMessageType.GreenSuccess);
        }

        private void ShowErrorAndLogException(Exception ex)
        {
            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, Localization.GetString("Error.Text", this.LocalResourceFile), ModuleMessage.ModuleMessageType.RedError);
            Exceptions.LogException(ex);
        }

        private void ValidateSuperUser()
        {
            if (!UserInfo.IsSuperUser)
            {
                Response.Redirect(Globals.NavigateURL("Access Denied"), true);
            }
        }
    }
}