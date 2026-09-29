@echo off
dotnet publish -c Release -r win-x64
iscc setup.iss
pause