namespace ClientResourceAdmin
{
    using System.Collections.Generic;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Icons;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Modules.Definitions;
    using DotNetNuke.Entities.Tabs;
    using DotNetNuke.Services.Installer.Packages;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Upgrade;

    public class DnnFeatureController : IUpgradeable
    {
        private const string ResourceFileRelativePath = "~/DesktopModules/ClientResourceAdmin/App_LocalResources/DnnFeatureController.cs.resx";
        private const string ModuleName = "ClientResourceAdmin";

        public string UpgradeModule(string version)
        {
            PackageInfo package = PackageController.GetPackageByName(ModuleName);
            IDictionary<int, TabInfo> moduleTabs = new TabController().GetTabsByPackageID(-1, package.PackageID, false);

            if (moduleTabs.Count > 0)
                return string.Empty;

            AddClientResourceAdminHostPage();

            return Localization.GetString("SuccessMessage", ResourceFileRelativePath);
        }

        private static void AddClientResourceAdminHostPage()
        {
            DesktopModuleInfo desktopModule = DesktopModuleController.GetDesktopModuleByModuleName(ModuleName, Null.NullInteger);
            ModuleDefinitionInfo moduleDefinition = desktopModule.ModuleDefinitions[ModuleName];

            string configIconFileLarge = IconController.IconURL("Configuration", "32x32");
            TabInfo hostPage = Upgrade.AddHostPage(Localization.GetString("PageName", ResourceFileRelativePath),
                                                   Localization.GetString("PageDescription", ResourceFileRelativePath),
                                                   IconController.IconURL("Configuration", "16x16"),
                                                   configIconFileLarge, true);

            Upgrade.AddModuleToPage(hostPage, moduleDefinition.ModuleDefID, Localization.GetString("ModuleTitle", ResourceFileRelativePath), configIconFileLarge, true);
        }
    }
}