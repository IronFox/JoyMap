del /Q release.zip
rmdir /Q /S .\JoyMap
del /Q .\JoyMap
mkdir .\JoyMap
copy /Y ..\bin\Release\net10.0-windows .\JoyMap

powershell Compress-Archive .\JoyMap .\release.zip