Client Resource Admin
=====================

This is an administrative module for DNN 6.1 that provides a user interface for key settings for managing CSS/JS resources. It allows a DNN SuperUser to update their web.config through the UI instead of editing the web.config by hand.

Installation
------------
* Download the latest installable module package from the [downloads][downloads] page.
* Both Install and Source are installable through the DNN UI, Source just includes the C# code-behind.

Usage
-----
* Because of the sensitive nature of the functionality, the module is restricted to use by a DNN super-user.
* The module features quite a bit of helpful information about how/why to configure the settings right on the admin page. Make sure to read through the information before saving new changes.
* Caution: clicking save or increment version in the module will update your web.config and restart your website. Please plan accordingly.
* Additionally, each time you save changes or increment the version, that information will be written to the DNN Event Viewer so you can keep track of what changes have been made on your site.

Issues
------
If you encounter any bugs or would like to request an enhancement, please [create an issue][issues].

Blog posts
----------
* [Enhancements for working with JavaScript and CSS Files in DNN 6.1][crm]
* [DNN 6.1 JS/CSS File Combination Potential Gotchas][crmpg]

Documentation
-------------
* [DNN Client Resource Management API][dnncrmwiki]
* [Client Dependency Framework][cdfwiki]

[crm]: http://www.dotnetnuke.com/Resources/Blogs/EntryId/3191/Enhancements-for-working-with-JavaScript-and-CSS-files-in-DNN-6-1.aspx
[crmpg]: http://www.dotnetnuke.com/Resources/Blogs/EntryId/3207/DNN-6-1-JS-CSS-File-Combination-Potential-Gotchas.aspx
[dnncrmwiki]: http://www.dotnetnuke.com/Resources/Wiki/Page/Client-Resource-Management-API.aspx
[cdfwiki]: http://clientdependency.codeplex.com/documentation
[downloads]: https://github.com/irobinson/ClientResourceAdmin/downloads
[issues]: https://github.com/irobinson/ClientResourceAdmin/issues?sort=created&direction=desc&state=open