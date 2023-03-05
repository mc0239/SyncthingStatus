package ;

import Util.getAboutString;
import Util.openFolder;
import Util.openUrl;
import data.FoldersResponse.Folder;
import haxe.ds.ArraySort;
import hx.AppTaskBarIconWrapper;
import hx.widgets.*;

enum TaskBarIconState {
    Unknown;
    Ok;
    NoResponse;
    BadResponse;
    HasErrors;
    // Sync;
}

class TaskBarIconHandler {

    private final defaultBitmap = Bitmap.fromHaxeResource("default.ico");
    private final notifyBitmap = Bitmap.fromHaxeResource("notify.ico");
    // private final syncBitmap = Bitmap.fromHaxeResource("sync.ico");
    private final thinkBitmap = Bitmap.fromHaxeResource("think.ico");

    private final app: App;
    private final settingsFrame: Frame;
    private final config: AppConfigHandler;

    private var icon: AppTaskBarIconWrapper;

    private var menu: Menu;
    private var submenuFolders: Menu;
    private var menuItemAbout: MenuItem;
    private var menuItemAbout2: MenuItem;
    private var menuItemOpenWeb: MenuItem;
    private var menuItemOpenFolder: MenuItem;
    private var menuItemSettings: MenuItem;
    private var menuItemExit: MenuItem;

    private var folders: Array<Folder>;

    public function new(app: App, settingsFrame: Frame, config: AppConfigHandler) {
        this.app = app;
        this.settingsFrame = settingsFrame;
        this.config = config;
        init();
    }

    private function init() {
        icon = new AppTaskBarIconWrapper();
        setState(Unknown);
        initMenu();
        initEventHandlers();
    }

    private function initMenu() {
        menu = new Menu();
        icon.setPopupMenu(menu);

        menuItemAbout = new MenuItem(menu, getAboutString());
        menuItemAbout2 = new MenuItem(menu, "Syncthing ...");
        menuItemOpenWeb = new MenuItem(menu, "Open Web GUI", null, Id.OPEN_WEB);
        submenuFolders = new Menu();
        menuItemSettings = new MenuItem(menu, "Settings", StandardId.PREFERENCES);
        menuItemExit = new MenuItem(menu, "Exit", StandardId.EXIT);
        menu.appendItem(menuItemAbout);
        menu.appendItem(menuItemAbout2);
        menu.appendSeparator();
        menu.appendItem(menuItemOpenWeb);
        menuItemOpenFolder = menu.appendSubMenu(submenuFolders, "Open folder");
        menu.appendItem(menuItemSettings);
        menu.appendItem(menuItemExit);

        // enable can only be set after item is appended to menu
        menuItemAbout.enable = false;
        menuItemAbout2.enable = false;
        menuItemOpenFolder.enable = false;
    }

    private function initEventHandlers() {
        // event bindings for icon
        icon.bind(EventType.TASKBAR_LEFT_DCLICK, (event) -> {
            openUrl(config.getSyncthingAddress());
        });

        // TODO: I had to add e.skip(false); to all events otherwise they were executed twice (why?)

        // event bindings for menu
        menu.bind(EventType.MENU, (e) -> {
            e.skip(false);
            openUrl(config.getSyncthingAddress());
        }, Id.OPEN_WEB);

        menu.bind(EventType.MENU, (e) -> {
            e.skip(false);
            settingsFrame.show();
        }, StandardId.PREFERENCES);

        menu.bind(EventType.MENU, (e) -> {
            e.skip(false);
            trace("Exiting...");
            icon.removeIcon();
            app.exit(); // TODO segfault (but does it matter at this point?)
        }, StandardId.EXIT);

        menu.bind(EventType.MENU, (e) -> {
            if (e.id >= Id.OPEN_FOLDER_START && e.id <= Id.OPEN_FOLDER_END) {
                e.skip(false);
                var idx = e.id - Id.OPEN_FOLDER_START;
                if (idx < this.folders.length) {
                    var folder = this.folders[idx];
                    openFolder(folder.path);
                }
            }
        }, -1);
    }

    public function setState(state: TaskBarIconState) {
        switch (state) {
            case Unknown: {
                icon.setBitmap(thinkBitmap, "Syncthing: ...");
            }
            case Ok: {
                icon.setBitmap(defaultBitmap, "Syncthing: OK");
            }
            case NoResponse: {
                icon.setBitmap(thinkBitmap, "Syncthing: No response");
            }
            case BadResponse: {
                icon.setBitmap(notifyBitmap, "Syncthing: Bad response");
            }
            case HasErrors: {
                icon.setBitmap(notifyBitmap, "Syncthing: Reporting errors");
            }
        }
    }

    public function setVersion(version: String) {
        menuItemAbout2.label = "Syncthing " + version;
    }

    public function setFolders(folders: Array<Folder>) {
        if (equalFolders(this.folders, folders)) {
            // same folders as before, skip the rest of the function.
            return;
        }
        this.folders = folders;

        menu.destroyItem(menuItemOpenFolder);

        submenuFolders = new Menu();
        menuItemOpenFolder = menu.insertSubMenu(4, -1, submenuFolders, "Open folder");
        menuItemOpenFolder.enable = folders.length > 0;

        // append new items
        for (i in 0...folders.length) {
            if (Id.OPEN_FOLDER_START + i > Id.OPEN_FOLDER_END) {
                trace("That's a lot of folders!");
                break;
            }
            var folder = folders[i];
            submenuFolders.append(Id.OPEN_FOLDER_START + i, folder.label);
        }
    }

    private function equalFolders(folders1: Array<Folder>, folders2: Array<Folder>): Bool {
        if (folders1 == null || folders2 == null || folders1.length != folders2.length) {
            return false;
        }

        var folderHash = function(folder: Folder) {
            return folder.id + folder.path + folder.label;
        }

        var folderSort = function(f1: Folder, f2: Folder): Int {
            return f1.id > f2.id ? 1 : -1;
        }

        ArraySort.sort(folders1, folderSort);
        ArraySort.sort(folders2, folderSort);
        
        return folders1.map(folderHash).join(";") == folders2.map(folderHash).join(";");
    }

}