$projectName = "Savedrake v1.2.3" # Replace with your actual project name
$packages = Get-Package -Project $projectName
foreach ($package in $packages) {
    Uninstall-Package -Id $package.Id -Project $projectName -Force
}