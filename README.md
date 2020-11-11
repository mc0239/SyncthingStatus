# SyncthingStatus

Tray menu icon as a quick Syncthing status check & a shortcut to web GUI and opening synced folders in File Explorer.

## Requirements

- .NET Core 3 Runtime (Windows 10 will automatically prompt you to download it when you try to run SyncthingStatus).
- An already set up and running Syncthing instance. Refer to [SETUP](https://github.com/mc0239/SyncthingStatus/blob/master/SETUP.md) for an example of how to set it up.

## Building

### Release build

To create a release executable (*win-x64* target example):

1. In Visual Studio 2019, go to _Build_ -> _Publish SyncthingStatus_.
2. Create a new Publish Profile and set up the following:
    - Deployment mode: *Framework-dependant*
    - Target runtime: *win-x64*
    - Open *File publish options* and check *Produce single file*.
3. This build will generate a single executable file in *publish* folder (not *win-x64*).
