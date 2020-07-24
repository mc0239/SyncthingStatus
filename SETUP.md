# Setup

Here follows a short guide on how I set-up Syncthing & SyncthingStatus on my Windows machines.

## Setting up Syncthing

Download "Base Syncthing" from [Syncthing's downloads page](https://syncthing.net/downloads/). Store downloaded binary somewhere you won't (re)move it later on (For example, `D:\Programs\Syncthing`).

Then, make sure that Syncthing runs on Windows startup. Syncthing docs have an [article that describes a few different approaches](https://docs.syncthing.net/users/autostart.html#run-at-user-log-on-using-the-startup-folder) how to set this up.

I prefer to do it with the Startup folder:

1. Open up the *Run...* window (Win + R)
2. Type in `shell:startup` and click OK. <br> ![](https://raw.githubusercontent.com/mc0239/SyncthingStatus/master/img/run_shell_startup.png)
3. An Explorer window will pop up with the Startup folder. Here, do the *Right click* > *New* > *Shortcut*.
4. When prompted for the location of the item, browse and select `Syncthing.exe`. Before clicking *Next*, add `-no-console -no-browser` to the end (this will make Syncthing run in background, without console window or opening the browser). <br> ![](https://raw.githubusercontent.com/mc0239/SyncthingStatus/master/img/add_shortcut_1.png)
5. Complete the shortcut creation process. Congrats, Syncthing will now automatically run when you log in.

## Setting up SyncthingStatus

Download latest release executable from [GitHub releases](https://github.com/mc0239/SyncthingStatus/releases) (look under Assets).

On first run, you might be prompted by Windows that you need to install .Net Core. Click Yes. A website with download options will pop up.

![](https://raw.githubusercontent.com/mc0239/SyncthingStatus/master/img/net_core_prompt.png)

On the webpage, look under the *Desktop Runtime* section and download the installer. Run the installer and let it do it's thing.

![](https://raw.githubusercontent.com/mc0239/SyncthingStatus/master/img/net_core_download.png).

The steps to make SyncthingStatus run on startup are practically the same as for the Syncthing itself (create a shortcut in the Startup folder, see instructions above).

Once SyncthingStatus is running, don't forget to setup the API Key in settings so it can connect to Syncthing successfuly.

