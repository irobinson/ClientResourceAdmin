namespace ClientResourceAdmin
{
    using System;
    using System.Xml;
    using System.Xml.XPath;
    using DotNetNuke.Application;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.Skins.Controls;

    public partial class ClientResourceAdmin : PortalModuleBase
    {
        protected override void OnInit(EventArgs e)
        {
            this.SaveButton.Click += Save;
            this.Load += PageLoad;
        }

        protected void PageLoad(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                SetCurrentSettings();
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
                var xml = UpdateValues();
                doc.LoadXml(xml);
                Application app = DotNetNukeContext.Current.Application;
                var merge = new DotNetNuke.Services.Installer.XmlMerge(doc, Globals.FormatVersion(app.Version), app.Description);
                merge.UpdateConfigs();

                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, Localization.GetString("Success.Text", this.LocalResourceFile), ModuleMessage.ModuleMessageType.GreenSuccess);
            }
            catch (Exception ex)
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, Localization.GetString("Error.Text", this.LocalResourceFile), ModuleMessage.ModuleMessageType.RedError);
                Exceptions.LogException(ex);
            }
        }

        private string UpdateValues()
        {
            string xml = Localization.GetString("Basic.Text", this.LocalResourceFile);
            xml = string.Format(xml,
                this.EnableCompositeFiles.Checked.ToString().ToLower(), // {0}
                this.MinifyCss.Checked.ToString().ToLower(), // {1}
                this.MinifyJs.Checked.ToString().ToLower(), // {2}
                this.PersistFiles.Checked.ToString().ToLower(), // {3}
                this.UrlTypeList.SelectedValue
                );
            return xml;
        }

        private void ValidateSuperUser()
        {
            if (!UserInfo.IsSuperUser)
            {
                Response.Redirect(Globals.NavigateURL("Access Denied"), true);
            }
        }

        public void SetCurrentSettings()
        {
            XPathNavigator config = Config.Load().CreateNavigator();
            XPathNavigator clientDependencyNode = config.SelectSingleNode("/configuration/clientDependency");

            if (clientDependencyNode != null)
            {
                XPathNavigator fileProcessingProvider = config.SelectSingleNode("configuration/clientDependency/compositeFiles/fileProcessingProviders/add");
                XPathNavigator fileRegistrationProvider = config.SelectSingleNode("configuration/clientDependency/fileRegistration/providers/add");
                
                int version = XmlUtils.GetAttributeValueAsInteger(clientDependencyNode, "version", 0);

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
            }
        }
    }
}