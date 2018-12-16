#! /bin/bash
set -e

NUGET_API_KEY=$1
if [ -z "$NUGET_API_KEY" ]; then
    echo Please, write nuget api key
    read NUGET_API_KEY
fi

./bump_version.sh

./pack.sh

VERSION=$(sed -r "s/<Version>[0-9]+.[0-9]+.[0-9]+</Version>/${VERSION}/" ./src/Version.props)

dotnet nuget push ./Target/DraftJSExporter.${VERSION}.nupkg -k $NUGET_API_KEY
