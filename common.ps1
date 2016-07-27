
# For Prebuild command, use:
#   powershell -ExecutionPolicy ByPass "&'$(ProjectDir)buildevents.ps1'" -Function Prebuild -ConfigurationName '$(ConfigurationName)' -OutDir '$(OutDir)' -DevEnvDir '$(DevEnvDir)' -PlatformName '$(PlatformName)' -ProjectDir '$(ProjectDir)' -ProjectPath '$(ProjectPath)' -ProjectName '$(ProjectName)' -ProjectFileName '$(ProjectFileName)' -ProjectExt '$(ProjectExt)' -SolutionDir '$(SolutionDir)' -SolutionPath '$(SolutionPath)' -SolutionName '$(SolutionName)' -SolutionFileName '$(SolutionFileName)' -SolutionExt '$(SolutionExt)' -TargetDir '$(TargetDir)' -TargetPath '$(TargetPath)' -TargetName '$(TargetName)' -TargetFileName '$(TargetFileName)' -TargetExt '$(TargetExt)'
 
# For Postbuild command, use:
#   powershell -ExecutionPolicy ByPass "&'$(ProjectDir)buildevents.ps1'" -Function Postbuild -ConfigurationName '$(ConfigurationName)' -OutDir '$(OutDir)' -DevEnvDir '$(DevEnvDir)' -PlatformName '$(PlatformName)' -ProjectDir '$(ProjectDir)' -ProjectPath '$(ProjectPath)' -ProjectName '$(ProjectName)' -ProjectFileName '$(ProjectFileName)' -ProjectExt '$(ProjectExt)' -SolutionDir '$(SolutionDir)' -SolutionPath '$(SolutionPath)' -SolutionName '$(SolutionName)' -SolutionFileName '$(SolutionFileName)' -SolutionExt '$(SolutionExt)' -TargetDir '$(TargetDir)' -TargetPath '$(TargetPath)' -TargetName '$(TargetName)' -TargetFileName '$(TargetFileName)' -TargetExt '$(TargetExt)'
 
param (
    [string]$ConfigurationName, # The name of the current project configuration, for example, "Debug".
    [string]$OutDir, # Path to the output file directory, relative to the project directory. This resolves to the value for the Output Directory property. It includes the trailing backslash '\'.
    [string]$DevEnvDir, # The installation directory of Visual Studio (defined with drive and path); includes the trailing backslash '\'.
    [string]$PlatformName, # The name of the currently targeted platform. For example, "AnyCPU".
    [string]$ProjectDir, # The directory of the project (defined with drive and path); includes the trailing backslash '\'.
    [string]$ProjectPath, # The absolute path name of the project (defined with drive, path, base name, and file extension).
    [string]$ProjectName, # The base name of the project.
    [string]$ProjectFileName, # The file name of the project (defined with base name and file extension).
    [string]$ProjectExt, # The file extension of the project. It includes the '.' before the file extension.
    [string]$SolutionDir, # The directory of the solution (defined with drive and path); includes the trailing backslash '\'.
    [string]$SolutionPath, # The absolute path name of the solution (defined with drive, path, base name, and file extension).
    [string]$SolutionName, # The base name of the solution.
    [string]$SolutionFileName, # The file name of the solution (defined with base name and file extension).
    [string]$SolutionExt, # The file extension of the solution. It includes the '.' before the file extension.
    [string]$TargetDir, # The directory of the primary output file for the build (defined with drive and path). It includes the trailing backslash '\'.
    [string]$TargetPath, # The absolute path name of the primary output file for the build (defined with drive, path, base name, and file extension).
    [string]$TargetName, # The base name of the primary output file for the build.
    [string]$TargetFileName, # The file name of the primary output file for the build (defined as base name and file extension).
    [string]$TargetExt, # The file extension of the primary output file for the build. It includes the '.' before the file extension.
 
    [string]$Function # Name of the function to run
)

$extraDir = "$($TargetDir)extra"
$nugetOutDir = "$extraDir\nuget"

function say($toSay)
{
	Write-Host $toSay
}

function Remove-Dir($dirName)
{
	say "Removing dir: $dirName"
	if (Test-Path -Path $dirName) {
		rm $dirName -Recurse -Force
		say "OK, Removed $dirName"
	}
	else
	{
		say "OK, $dirName did not exist"
	}
}

function Create-Dir($dirName)
{
	say "Creating dir: $dirName"
    mkdir $dirName -Force
	say "OK"
}

function Remove-ExtraDir()
{
	Remove-Dir $extraDir
}

function Create-ExtraDir()
{
	Create-Dir $extraDir
}

function Get-AssemblyVersion
{
	param([string] $assemblyPath)
	[System.Reflection.Assembly]::LoadFrom($assemblyPath).GetName().Version.ToString()
}

function Get-AutoSemVer($assemblyVersion, $configuration) {
    $versionParts = $assemblyVersion.Split(".")
    $mainVersionParts = $versionParts[0 .. 2]

    $firstSubpatchIndex = $mainVersionParts.Length
    $lastSubpatchIndex = $versionParts.Length - 1

    $subpatchVersionParts = @()
    if ($firstSubpatchIndex -le $lastSubpatchIndex) {
        $subpatchVersionParts = $versionParts[$firstSubpatchIndex .. $lastSubpatchIndex]
    }

    $mainVersion = [string]::Join(".", $mainVersionParts)
    $subpatchVersion = [string]::Join(".", $subpatchVersionParts)

    if ($configuration -eq "release") {
        "$mainVersion.$subpatchVersion"
    }
    else {
        "$mainVersion-$configuration-$subpatchVersion"
    }
}

function Pack-NuGet()
{
	[string]$packageAssemblyVersion = Get-AssemblyVersion $TargetPath

	$packageVersion = Get-AutoSemVer $packageAssemblyVersion $ConfigurationName
		
	Create-Dir $nugetOutDir

	say "Packing NuGet for $ProjectPath"
	nuget pack $ProjectPath -Version $packageVersion -OutputDirectory $nugetOutDir
	say "OK, NuGet package is in $nugetOutDir"
}

# ------------------------------
# AVOID EDITING BELOW THIS POINT
# ------------------------------

$_buildEventsFile = "$($ProjectDir)buildevents.ps1"
say "--- Loading project build events from $_buildEventsFile ---"
. $_buildEventsFile
say "--- Done loading project build events from $_buildEventsFile ---"

function Run-BuildEventFunction($buildEventFunctionName)
{
    say "--- Build event '$buildEventFunctionName' started ---"
    Try
    {
        & $buildEventFunctionName
        say "--- Build event '$buildEventFunctionName' completed normally ---"
    }
    Catch
    {
        say "--- Build event '$buildEventFunctionName' failed with exception ---"
        throw
    }
}
 
# Call the event indicated by $Function
Run-BuildEventFunction $Function