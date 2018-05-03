@echo off

powershell "./build.ps1 -Target Default"
exit /b %errorlevel%