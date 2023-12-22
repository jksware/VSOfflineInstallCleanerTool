# VS Offline Install Cleaner Tool

VS Offline Install Cleaner Tool is a command line tool for cleaning the Visual Studio 2022 offline installer folder from old or unreferenced packages.

## Who Is "VS Offline Install Cleaner Tool" for?

VS Offline Install Cleaner Tool is a tool made for developers using [Visual Studio 2022 offline downloader] (https://docs.microsoft.com/en-us/visualstudio/install/create-an-offline-installation-of-visual-studio), who notice that the Offline Installer folder size describes a monotonically non-decreasing function after updating the installer packages. 

Just run the executable in the "visual studio offline installer" location and it will help you delete all unreferenced or old packages and save disk space*. 

The main reason for this issue is that "VS Offline installer" does not delete old packages in the update process, and the cleaning tool provided by the offline layout setup tool does not remove every dangling package or dependency!
 

## How to Use this App?

* Download the application from the Releases tab or compile it manually.

* Place the downloaded application in the same folder of the "vs offline installer". Alternatively, pass as first command line argument the path to the "vs offline installer" folder.

* Run the console application and read the results.


## How does it work?

The tool compiles a list of all packages referenced in the "Catalog.json" file inside the "vs offline installer" folder and then compares it with the existing packages in the "vs offline installer" folder, given the general structure of subfolders for the Visual Studio 2022 local layout. All of the unreferenced folders -- those that are not listed as a package in Catalog.json -- are moved to a folder called "ToBeRemoved" inside the "vs offline installer" folder. 

After the process is finished, the user can manually delete the "ToBeRemoved" folder.

*The application does not delete old packages, but only moves them to a folder, leaving the user the option to manually delete the folder. 