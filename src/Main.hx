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

        var frame:Frame = new Frame(null, APP_NAME);

        var configHandler = new AppConfigHandler();
        try {
            configHandler.init();
        } catch (exception) {
            MessageDialog.showMessageBox("An exception occured when starting Syncthing Status:\n" + exception.message, APP_NAME + " could not start", MessageDialogStyle.OK | MessageDialogStyle.ICON_EXCLAMATION, frame);
            return;
        }

        // TODO move frame UI stuff to a separate file

        frame.bind(EventType.CLOSE_WINDOW, (event: Event) -> {
            event.skip(false);
            frame.hide();
        });

        var sizer = new BoxSizer(Orientation.VERTICAL);

        var labelApiKey = new StaticText(frame, "API Key");
        sizer.add(labelApiKey, 0, Direction.TOP | Direction.LEFT | Direction.RIGHT, 10);

        sizer.addSpacer(2);

        var inputApiKey = new TextCtrl(frame, configHandler.getConfig().apiKey);
        sizer.add(inputApiKey, 0, Stretch.EXPAND | Direction.LEFT | Direction.RIGHT, 10);

        sizer.addSpacer(14);

        var checkBoxCustomStAddress = new CheckBox(frame, "Use custom Syncthing address");
        checkBoxCustomStAddress.value = configHandler.getConfig().usingCustomStAddress;
        sizer.add(checkBoxCustomStAddress, 0, Direction.LEFT | Direction.RIGHT, 10);

        sizer.addSpacer(2);
        
        var inputStAddress = new TextCtrl(frame, configHandler.getConfig().stAddress);
        inputStAddress.hint = "http://localhost:8384";
        inputStAddress.enabled = false;
        sizer.add(inputStAddress, 0, Stretch.EXPAND | Direction.LEFT | Direction.RIGHT, 10);

        sizer.addSpacer(14);

        var buttonSave = new Button(frame, "Save settings");
        sizer.add(buttonSave, 0, Direction.LEFT | Direction.RIGHT | Defs.ALIGN_RIGHT, 10);

        sizer.addSpacer(28);

        var labelAbout = new StaticText(frame, getAboutString());
        sizer.add(labelAbout, 0, Direction.LEFT | Direction.RIGHT | Defs.ALIGN_RIGHT, 10);

        var labelHomepage = new HyperlinkCtrl(frame, "GitHub page", "https://github.com/mcebular/SyncthingStatus", HyperlinkCtrlStyle.ALIGN_LEFT | HyperlinkCtrlStyle.CONTEXTMENU);
        sizer.add(labelHomepage, 0, Direction.LEFT | Direction.RIGHT | Defs.ALIGN_RIGHT, 10);

        frame.setSizerAndFit(sizer);
        frame.resize(400, 200);

        checkBoxCustomStAddress.bind(EventType.CHECKBOX, (e) -> {
            inputStAddress.enabled = checkBoxCustomStAddress.value;
        });


        var timerButtonSaveResetLabel = new Timer(frame, -1, false, Id.SAVE_SETTINGS_BUTTON_RESET);
        frame.bind(EventType.TIMER, (e) -> {
            e.skip(false);
            buttonSave.label = "Save settings";
        }, Id.SAVE_SETTINGS_BUTTON_RESET);
        buttonSave.bind(EventType.BUTTON, (e) -> {
            trace("Saving settings...");
            configHandler.save({
                apiKey: inputApiKey.value,
                usingCustomStAddress: checkBoxCustomStAddress.value,
                stAddress: inputStAddress.value
            });
            buttonSave.label = "Saved!";
            timerButtonSaveResetLabel.start(2000, true);
        });
        
        var apiClient = new ApiClient(configHandler);
        var iconHandler = new TaskBarIconHandler(app, frame, configHandler);
        
        var statusCheckTimer = new StatusCheckTimer(app, frame, apiClient, iconHandler);
        statusCheckTimer.start();

        app.run();
        app.exit();
    }
}
