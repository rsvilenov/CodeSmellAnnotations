param($installPath, $toolsPath, $package, $project)

if($project.Object.SupportsPackageDependencyResolution)
{
    if($project.Object.SupportsPackageDependencyResolution())
    {
        # Do not install analyzers via install.ps1, instead let the project system handle it.
        return
    }
}

$analyzerPath = join-path $toolsPath "analyzers\cs"
$analyzerFilePath = join-path $analyzerPath "CodeSmellAnnotations.dll"

$project.Object.AnalyzerReferences.Add("$analyzerFilePath")