param (
    [string]$oldName = "Acme",
    [string]$newName = "NewAppName",
    [string]$solutionRoot = ".\"
)

# Iterate through each file
foreach ($file in Get-ChildItem -Path $solutionRoot -Recurse) {
    # Skip directories
    if ($file.PSIsContainer) {
        continue
    }

    # Read the content of the file
    $content = Get-Content $file.FullName -Raw

    # Replace occurrences of the old name with the new name (must be lowercase in case of docker compose file)
    $newContent = $content -replace $oldName, $(If($file.FullName -match "docker-compose") { $newName.ToLower() } Else { $newName })

    # Write the updated content back to the file
    Set-Content -Path $file.FullName -Value $newContent -NoNewline
}

# Rename files and directories containing the old name
Get-ChildItem -Path $solutionRoot -Recurse | ForEach-Object {
    $newPath = $_.FullName -replace $oldName, $newName
    Move-Item -Path $_.FullName -Destination $newPath -ErrorAction SilentlyContinue
}

# Rename the solution file with the new name (just for the initial name)
$solutionFilePath = Join-Path -Path $solutionRoot -ChildPath "DotnetStarter.sln"
if (Test-Path $solutionFilePath) {
    $newSolutionFilePath = $solutionFilePath -replace "DotnetStarter.sln", "$newName.sln"
    Move-Item -Path $solutionFilePath -Destination $newSolutionFilePath -ErrorAction SilentlyContinue
}
