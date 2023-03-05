#!/usr/bin/env bash

set -e
set -x

if [[ ! -f "SyncthingStatus.hxml" ]]; then
    echo "Script not ran from repository root. Aborting."
    exit 1
fi

haxe SyncthingStatus.hxml

if [[ -f "build/Main.exe" ]]; then
    mv "build/Main.exe" "build/SyncthingStatus.exe"
else
    mv "build/Main" "build/SyncthingStatus"
fi
