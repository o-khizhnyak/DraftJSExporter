#! /bin/bash
set -e
VERSION=$1
echo last version is $(git --no-pager tag -l "v..*" --sort=v:refname | tac | head -1)

BRANCH=$(git rev-parse --abbrev-ref HEAD)
if [ "${BRANCH}" != "master" ]; then
    echo Only master branch are supported
    exit 2
fi

if [ ! -z "$(git status --short)" ]; then
    echo Unstaged or uncommited changes detected
    exit 3
fi


if [ -z "$VERSION" ]; then
    echo Please, write new version of application
    read VERSION
fi

if [ -z "$VERSION" ]; then
    echo Version udefined
    exit 1;
fi


sed -i -r "s/<Version>[0-9]+.[0-9]+.[0-9]+</Version>/<Version>${VERSION}</Version>/" ./src/Version.props

git add ./src/Version.props

git commit -m "release: new version ${VERSION}" -m "" -m "META: version:${VERSION}"

git tag -a "v${VERSION}" -m ''

git push origin --tags