package ;

import haxe.Json;
import haxe.io.Path;
import hx.widgets.PlatformInfo;
import hx.widgets.StandardPaths;
import sys.FileSystem;
import sys.io.File;


typedef AppSettings = {
    var apiKey: String;
    var stAddress: String;
    var usingCustomStAddress: Bool;
}

class AppConfigHandler {

    private final defaultConfiguration: AppSettings = {
        apiKey: "",
        stAddress: "",
        usingCustomStAddress: false
    }

    // TODO maybe I should do setFileLayout(XDG), see wxWidgets documentation.
    private final dataDirPath: String;
    private final configFilePath: String;

    private var configuration: AppSettings = null;

    public function new() {
        var platform:PlatformInfo  = new PlatformInfo();
        if (platform.isWindows) {
            // Expected to be: C:\Users\<username>\AppData\Local\<appinfo>
            this.dataDirPath = Path.join([new StandardPaths().userLocalDataDir]);
        } else {
            // Expected to be: value of $XDG_CONFIG_HOME/<appinfo>, usually $HOME/.config/<appinfo>
            this.dataDirPath = Path.join([new StandardPaths().userConfigDir, Main.APP_NAME]);
        }

        this.configFilePath = Path.join([dataDirPath, "config.json"]);
    }

    public function init() {
        // check if dirs & file exists
        if (!FileSystem.exists(dataDirPath)) {
            trace("Directory '" + dataDirPath + "' does not exist, creating...");
            FileSystem.createDirectory(dataDirPath);
        }

        if (!FileSystem.exists(configFilePath)) {
            trace("Configuration file does not exist (first run?), creating file '" + configFilePath + "'...");
            File.saveContent(configFilePath, Json.stringify(defaultConfiguration));
        }

        trace("Reading configuration from '" + configFilePath + "'.");

        this.configuration = Json.parse(File.getContent(configFilePath));
    }

    public function getApiKey(): String {
        return this.configuration.apiKey;
    }

    public function getSyncthingAddress(): String {
        if (this.configuration.usingCustomStAddress) {
            return Path.removeTrailingSlashes(this.configuration.stAddress);
        } else {
            return "http://localhost:8384";
        }
    }

    public function getConfig(): AppSettings {
        return this.configuration;
    }

    public function save(config: AppSettings): Void {
        File.saveContent(configFilePath, Json.stringify(config, "  "));
        this.configuration = config;
    }

}