cmd.exe /c IF EXIST *-Release.zip (del /S *-Release.zip)
echo "Test"
for /d %%X in (Publish/*) do "C:\Program Files\7-Zip\7z.exe" a -tzip "%%X-Release.zip" "Publish/%%X/*"