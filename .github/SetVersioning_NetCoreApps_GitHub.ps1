$buildYear = Get-Date -Format "yyyy"
$buildMonth = Get-Date -Format "MM"
$buildDay =  Get-Date -Format "dd"
$rev = $env:GITHUB_RUN_NUMBER
Write-Host "rev number is " $rev
$customBuildVersion = -join($buildYear, ".", $buildMonth, ".", $buildDay, ".", $rev)

#$sourcePath = '$(SourcePath)'
$sourcepath = $env:GITHUB_WORKSPACE
$releaseMajor = $env:Release.Major #Set from build 
$releaseQuarter = $env:Release.Quarter #Set from build 
$IPReleaseNumber = $env:Release.IPReleaseNumber #Set from build 
$sourceVersion = $env:GITHUB_SHA

#Set defaults if none are entered
#Set to current year if releaseMajor is empty
if([string]::IsNullOrWhitespace($releaseMajor)){
    $releaseMajor = $buildYear
}

#Set to current quarter if releaseQuarter is not set
if([string]::IsNullOrWhitespace($releaseQuarter)){
    $releaseQuarter = [Math]::Ceiling($buildMonth /3)
}

#Set IPReleaseNumber to XXX if not set 
if([string]::IsNullOrWhitespace($IPReleaseNumber)){
    $IPReleaseNumber = -join ($buildMonth,$buildDay)
}

$releaseName = -join($releaseMajor, "Q", $releaseQuarter, "IP", $IPReleaseNumber)
$releaseVersion = $releaseMajor + "." + $releaseQuarter + "." + $IPReleaseNumber
$assemblyVersion = $releaseMajor + "." + $releaseQuarter + "." + $IPReleaseNumber + ".*"
$productVersion = -join($customBuildVersion, "+", $sourceVersion)

Write-Host "Source path is $sourcePath"
Write-Host "releaseMajor is $releaseMajor for source path $sourcePath"
Write-Host "releaseQuarter is $releaseQuarter for source path $sourcePath"
Write-Host "IPReleaseNumber is $IPReleaseNumber for source path $sourcePath"

Write-Host "customBuildVersion is $customBuildVersion for source path $sourcePath"
Write-Host "releaseName is $releaseName for source path $sourcePath - sets AssemblyProductAttribute in AssemblyInfo"
Write-Host "releaseVersion is $releaseVersion for source path $sourcePath - Sets AssemblyFileVersionAttribute in AssemblyInfo"
Write-Host "assemblyVersion is $assemblyVersion for source path $sourcePath - set AssemblyVersionAttribute in AssemblyInfo"
Write-Host "sourceVersion is $sourceVersion for source path $sourcePath - sets AssemblyDescriptionAttribute in AssemblyInfo"
Write-Host "productVersion is $productVersion for source path $sourcePath - sets AssemblyInformationalVersionAttribute in AssemblyInfo"


$assemblyInfoCSharp = Get-ChildItem -Path $sourcePath -Include AssemblyInfo.cs -Recurse
ForEach ($file in $assemblyInfoCSharp)
{
    Write-Host "Updating file $file"
    $content = (Get-Content -Path $file.FullName)
    #AssemblyInformationalVersionAttribute --> Product Version in Windows Explorer
    $content = $content -replace '\[assembly:\sAssemblyInformationalVersionAttribute\("(.*)"\)\]', "[assembly: AssemblyInformationalVersionAttribute(`"$productVersion`")]"
    #AssemblyFileVersion --> File Version in Windows Explorer
    $content = $content -replace '\[assembly:\sAssemblyFileVersion\("(.*)"\)\]', "[assembly: AssemblyFileVersion(`"$releaseVersion`")]"
    #AssemblyVersion - 
    $content = $content -replace '\[assembly:\sAssemblyVersion\("(.*)"\)\]', "[assembly: AssemblyVersion(`"$assemblyVersion`")]"
    #AssemblyProduct --> Product Name in Windows Explorer
    $content = $content -replace '\[assembly:\sAssemblyProduct\("(.*)"\)\]', "[assembly: AssemblyProduct(`"$releaseName`")]"
    #AssemblyDescription - Comments in Windows Explorer
    $content = $content -replace '\[assembly:\sAssemblyDescription\("(.*)"\)\]', "[assembly: AssemblyDescription(`"$customBuildVersion`")]"
    $content | Out-File $file -Encoding utf8
}