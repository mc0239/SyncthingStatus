# Development

## Requirements

SyncthingStatus has been successfuly built on Windows 10, Linux Mint 21, OpenSUSE Leap 15.4 with the following:

- Haxe 4.3.0
- Haxelib 4.1.0
  - hxcpp 4.3.2
  - [my hxWidgets fork](https://github.com/mcebular/hxWidgets)
- wxWidgets 3.2.2.1

Windows-specific:

- Visual Studio 2022
- "Desktop Development with C++" Visual Studio Installer workload
- any Windows specific requirements listed in [wxWidgets Windows installation manual](https://github.com/wxWidgets/wxWidgets/blob/master/docs/msw/install.md)
- any Windows specific requirements listed in [hxWidgets](https://github.com/haxeui/hxWidgets/blob/master/README.md)

Linux-specific:

- any Linux & GTK specific requirements listed in [wxWidgets GTK installation manual](https://github.com/wxWidgets/wxWidgets/blob/master/docs/gtk/install.md)
- any Linux specific requirements listed in [hxWidgets](https://github.com/haxeui/hxWidgets/blob/master/README.md)

## Environment setup

Clone this repo and hxWidgets fork:

```sh
git clone https://github.com/mcebular/SyncthingStatus.git
git clone https://github.com/mcebular/hxWidgets.git
```

In the directory where SyncthingStatus is cloned, create a local haxelib repository and install dependencies:

```sh
cd SyncthingStatus
haxelib newrepo
haxelib install hxWidgets
haxelib dev hxWidgets <path to hxWidgets dir>
```

## Build & run

To build an executable, run:

```sh
haxe SyncthingStatus.hxml
```

If there aren't any errors, it will produce a runnable `Main` (or `Main.exe` on Windows) in `build` directory.

Hint: On Windows, make sure to specifically use "Developer Command Prompt for VS 2022" (and not e.g. cmd.exe), and to run `vcvarsall.bat` beforehand, as noted by the [hxWidgets README](https://github.com/haxeui/hxWidgets/blob/master/README.md).


## Appendix I.: Installing wxWidgets on Linux

Installing wxWidgets from source on Linux can generally be done with the following series of commands:

```sh
# Get wxWidgets source
git clone --recurse-submodules https://github.com/wxWidgets/wxWidgets.git
cd wxWidgets
git checkout <tag>

# Configure and build wxWidgets
mkdir wx_build
cd wx_build
../configure --with-opengl --disable-shared
make -j<no. of cores>
make install

# Check if installed successfully
wx-config --version
```
