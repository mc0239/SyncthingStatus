#!/usr/bin/env bash

set -e
set -x

if [[ ! -f "SyncthingStatus.hxml" ]]; then
    echo "Script not ran from repository root. Aborting."
    exit 1
fi

if [[ -d "build/" ]]; then
    rm -vrf "build"
fi
