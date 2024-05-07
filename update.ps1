$projectName = "Savedrake v1.2.3" # Replace with your actual project name

# Get the list of all packages in the project
$packages = Get-Package -ProjectName $projectName

# Loop through each package and update it
foreach ($package in $packages) {
    Update-Package -Id $package.Id -ProjectName $projectName
}
