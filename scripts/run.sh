#!/usr/bin/env bash

set -e
set -x

if [[ ! -f "SyncthingStatus.hxml" ]]; then
    echo "Script not ran from repository root. Aborting."
    exit 1
fi

./scripts/build.sh

if [[ -f "build/SyncthingStatus.exe" ]]; then
    ./build/SyncthingStatus.exe
else
    ./build/SyncthingStatus
fi
