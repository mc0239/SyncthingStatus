package ;

import Util.getAboutString;
import hx.widgets.*;
import hx.widgets.styles.HyperlinkCtrlStyle;
import hx.widgets.styles.MessageDialogStyle;
import wx.widgets.Direction;


class Main {

    public static final APP_NAME = "SyncthingStatus";
    public static final APP_VERSION = "v1.0";

    public static function main() {
        trace(getAboutString());
        var app = new App();
        app.init();
        app.name = APP_NAME;

        var configHandler = new AppConfigHandler();
        var settingsFrame:SettingsFrame = new SettingsFrame(null, configHandler);

        try {
            configHandler.init();
            settingsFrame.init();
        } catch (exception) {
            MessageDialog.showMessageBox("An exception occured when starting Syncthing Status:\n" + exception.message, APP_NAME + " could not start", MessageDialogStyle.OK | MessageDialogStyle.ICON_EXCLAMATION, settingsFrame);
            return;
        }

        var apiClient = new ApiClient(configHandler);
        var iconHandler = new TaskBarIconHandler(app, settingsFrame, configHandler);
        
        var statusCheckTimer = new StatusCheckTimer(app, settingsFrame, apiClient, iconHandler);
        statusCheckTimer.start();

        app.run();
        app.exit();
    }
}
