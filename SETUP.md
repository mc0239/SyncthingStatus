# Setup

Here's a short guide on how I set-up Syncthing on my Windows machines.

## Setting up Syncthing

Download "Base Syncthing": https://syncthing.net/downloads/ and put it somewhere you won't (re)move it later on.

Then follow the guide on "Starting Syncthing Automatically", section "Using startup folder":

https://docs.syncthing.net/users/autostart.html#run-at-user-log-on-using-the-startup-folder

Congratulations, Syncthing will run on next user logon.

## Setting up SyncthingStatus

Download latest release exe from GitHub releases:

https://github.com/mc0239/SyncthingStatus/releases

Just like you did it for the Base Syncthing, put the exe file somewhere (I just put it into the same folder next to Syncthing).

Note: SyncthingStatus requires .NET Core. Upon first run, you might get a prompt to install it.

Just like for Syncthing, also create a shortcut in startup folder.

Congratulations, SyncthingStatus will run on next user logon.

Once running the SyncthingStatus, don't forget to setup the API Key for it to work properly.

