#! /bin/bash
 
 dotnet pack -o ${PWD}/target -c Release src/DraftJs.Abstractions/DraftJs.Abstractions.csproj
 dotnet pack -o ${PWD}/target -c Release src/DraftJs.Exporter/DraftJs.Exporter.csproj
 dotnet pack -o ${PWD}/target -c Release src/DraftJs.Exporter.Html/DraftJs.Exporter.Html.csproj
