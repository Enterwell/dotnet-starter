param (
    [string]$oldName = "Acme",
    [string]$newName = "NewAppName",
    [string]$solutionRoot = ".\"
)

# Recursively get all files in the solution directory
$allFiles = Get-ChildItem -Path $solutionRoot -Recurse

# Iterate through each file and replace occurrences of the old name
foreach ($file in $allFiles) {
    # Skip directories
    if ($file.PSIsContainer) {
        continue
    }

    # Read the content of the file
    $content = Get-Content $file.FullName -Raw

    # Replace occurrences of the old name with the new name
    $newContent = $content -replace $oldName, $newName

    # Write the updated content back to the file
    Set-Content -Path $file.FullName -Value $newContent
}

# Rename files and directories containing the old name
Get-ChildItem -Path $solutionRoot -Recurse | ForEach-Object {
    $newPath = $_.FullName -replace $oldName, $newName
    Move-Item -Path $_.FullName -Destination $newPath -ErrorAction SilentlyContinue
}
