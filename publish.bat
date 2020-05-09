dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true -o "Publish/win-x64"
dotnet publish -r win-x86 -c Release /p:PublishSingleFile=true -o "Publish/win-x86"
dotnet publish -r linux-x64 -c Release /p:PublishSingleFile=true -o "Publish/linux-x64"
./zip.bat