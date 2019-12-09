#! /bin/bash
set -e

NUGET_API_KEY=$1
if [ -z "$NUGET_API_KEY" ]; then
    echo Please, write nuget api key
    read NUGET_API_KEY
fi

./bump_version.sh

./pack.sh

VERSION=$(grep "<Version>" ./src/Version.props | sed -r "s#.*<Version>([0-9]+.[0-9]+.[0-9]+)</Version>.*#\1#")

dotnet nuget push ./target/DraftJsExporter.Abstractions.${VERSION}.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_API_KEY
dotnet nuget push ./target/DraftJsExporter.Exporter.${VERSION}.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_API_KEY
dotnet nuget push ./target/DraftJsExporter.Exporter.Html.${VERSION}.nupkg -s https://api.nuget.org/v3/index.json -k $NUGET_API_KEY
