package ;

import Util.getAboutString;
import hx.widgets.*;
import hx.widgets.styles.HyperlinkCtrlStyle;


class SettingsFrame extends Frame {

    private final config: AppConfigHandler;

    public function new(parent: Window, config: AppConfigHandler) {
        super(parent, Main.APP_NAME);
        this.config = config;
    }

    public function init() {
        initUi();

        // When clicking X to close window, don't actually close it, just hide it.
        this.bind(EventType.CLOSE_WINDOW, (event: Event) -> {
            event.skip(false);
            this.hide();
        });
    }

    private function initUi() {
        var sizer = new BoxSizer(Orientation.VERTICAL);

        var labelApiKey = new StaticText(this, "API Key");
        sizer.add(labelApiKey, 0, Direction.TOP | Direction.LEFT | Direction.RIGHT, 10);

        sizer.addSpacer(2);

        var inputApiKey = new TextCtrl(this, config.getConfig().apiKey);
        sizer.add(inputApiKey, 0, Stretch.EXPAND | Direction.LEFT | Direction.RIGHT, 10);

        sizer.addSpacer(14);

        var checkBoxCustomStAddress = new CheckBox(this, "Use custom Syncthing address");
        checkBoxCustomStAddress.value = config.getConfig().usingCustomStAddress;
        sizer.add(checkBoxCustomStAddress, 0, Direction.LEFT | Direction.RIGHT, 10);

        sizer.addSpacer(2);
        
        var inputStAddress = new TextCtrl(this, config.getConfig().stAddress);
        inputStAddress.hint = "http://localhost:8384";
        inputStAddress.enabled = checkBoxCustomStAddress.value;
        sizer.add(inputStAddress, 0, Stretch.EXPAND | Direction.LEFT | Direction.RIGHT, 10);

        sizer.addSpacer(14);

        var buttonSave = new Button(this, "Save settings");
        sizer.add(buttonSave, 0, Direction.LEFT | Direction.RIGHT | Defs.ALIGN_RIGHT, 10);

        sizer.addSpacer(28);

        var labelAbout = new StaticText(this, getAboutString());
        sizer.add(labelAbout, 0, Direction.LEFT | Direction.RIGHT | Defs.ALIGN_RIGHT, 10);

        var labelHomepage = new HyperlinkCtrl(this, "GitHub page", "https://github.com/mcebular/SyncthingStatus", HyperlinkCtrlStyle.ALIGN_LEFT | HyperlinkCtrlStyle.CONTEXTMENU);
        sizer.add(labelHomepage, 0, Direction.LEFT | Direction.RIGHT | Defs.ALIGN_RIGHT, 10);

        this.setSizerAndFit(sizer);
        this.resize(400, 200);

        // event bindings

        checkBoxCustomStAddress.bind(EventType.CHECKBOX, (e) -> {
            inputStAddress.enabled = checkBoxCustomStAddress.value;
        });

        var timerButtonSaveResetLabel = new Timer(this, -1, false, Id.SAVE_SETTINGS_BUTTON_RESET);
        this.bind(EventType.TIMER, (e) -> {
            e.skip(false);
            buttonSave.label = "Save settings";
        }, Id.SAVE_SETTINGS_BUTTON_RESET);
        buttonSave.bind(EventType.BUTTON, (e) -> {
            trace("Saving settings...");
            config.save({
                apiKey: inputApiKey.value,
                usingCustomStAddress: checkBoxCustomStAddress.value,
                stAddress: inputStAddress.value
            });
            buttonSave.label = "Saved!";
            timerButtonSaveResetLabel.start(2000, true);
        });
    }

}