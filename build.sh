# Remove out folder
rm -rf ./out

# Build project
dotnet publish --runtime ubuntu.16.04-x64 -c Release -o out

# TODO: add multi-platform builds