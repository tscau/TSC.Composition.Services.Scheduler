namespace TSC.Composition.Services.Scheduler.Config
{
    /// <summary>
    /// Class to hold the Assembly version details
    /// </summary>
    public class AssemblyVersionDetails
    {
        /// <summary>
        /// The Assembly version. Set by the build system to be the release number. Corresponds to the AssemblyVersion attribute in the AssemblyInfo.cs file, 
        /// and the actual assembly version when looking at the .NET DLL Metadata
        /// e.g. 20:4:430:17292
        /// </summary>
        public string ReleaseBuild { get; set; }

        /// <summary>
        /// The AssemblyFile version. Set by the build system to be the release number
        /// Corresponds to the AssemblyFileVersion attribute int he AssemblyInfo.cs
        /// e.g. 20.4.430.0
        /// </summary>
        public string ReleaseVersion { get; set; }

        /// <summary>
        /// Set by the build system to be the build number of the application. Represents the actual build that produced the compil;ed applicaiton, and can be used to view the TFS build that produced the packages
        /// Corresponds to the AssemblyInformationVersion attribute in teh AssemblyInfo.cs
        /// e.g. 2020.11.10.1
        /// </summary>
        public string BuildNumber { get; set; }

        /// <summary>
        /// Set by the build system to indicate the name of the release that the compiled package belongs to
        /// Corresponds to the AssemblyProduct attribute in the AssemblyInfo.cs file
        /// e.g. 20Q4IP430
        /// </summary>
        public string ReleaseName { get; set; }
    }
}
