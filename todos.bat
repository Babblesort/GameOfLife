@echo off
REM Slash command to find all TODOs in the codebase
REM Usage: todos [path]

setlocal enabledelayedexpansion

set "SCRIPT_DIR=%~dp0"
set "PROJECT_ROOT=%SCRIPT_DIR:~0,-1%"
set "TARGET_PATH=%1"
if "%TARGET_PATH%"=="" set "TARGET_PATH=."

REM Build the TodoCommand if not already built
if not exist "%PROJECT_ROOT%\TodoCommand\bin\Release\net10.0\todos.exe" (
    if not exist "%PROJECT_ROOT%\TodoCommand\bin\Debug\net10.0\todos.exe" (
        echo Building TodoCommand...
        dotnet build "%PROJECT_ROOT%\TodoCommand\TodoCommand.csproj" -c Release > nul 2>&1
    )
)

REM Run the command
dotnet run --project "%PROJECT_ROOT%\TodoCommand\TodoCommand.csproj" -- "%TARGET_PATH%"
