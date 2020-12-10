using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("TSAC.Composition.Services.Scheduler")]
[assembly: AssemblyDescription("DCS.Composition.Services.Scheduler .NET Core 3.1")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ATO")]
//Set by the build system/ABDO to indicate which release this is for. e.g. 20Q4IP430
[assembly: AssemblyProduct("LOCALRELEASE")] //Set from Build --> use build parameter set from ABDO
[assembly: AssemblyCopyright("Copyright © TSC 2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]


// Setting ComVisible to false makes the types in this assembly not visible
// to COM components. If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("CA7543D7-0F0F-4B48-9398-2712098E9324")]


// Version information for an assembly consists of the following four values:
//
// Major Version
// Minor Version
// Build Number --> perhaps sxxet this to the commit hash from git???
// Revision
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("20.4.430.*")]
[assembly: AssemblyVersion("1.1.1.*")] //Set from the build using the build number from the build
// Following is set by the release pipeline
[assembly: AssemblyFileVersion("1.1.1")]
// Following is set by the build definition. For database projects, this is set manually or by the database project build to correspond to the DBCC version that it is for
// This will be retrieved from a file that is generated when a merge from a branch happens -- will take the name of the branch we aree merging from
[assembly: AssemblyInformationalVersionAttribute("LOCALREL")]