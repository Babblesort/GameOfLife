#!/bin/bash
set -e

# Add Homebrew .NET 10 to PATH if present
if [ -d "/opt/homebrew/opt/dotnet@10/libexec" ]; then
    export DOTNET_ROOT="/opt/homebrew/opt/dotnet@10/libexec"
    export PATH="$DOTNET_ROOT:$PATH"
fi

cd "$(dirname "$0")"
dotnet run --project UI --configuration Release
