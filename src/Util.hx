package ;


function getAboutString(): String {
    return Main.APP_NAME + " " + Main.APP_VERSION;
}

function openUrl(url: String): Void {
    switch (Sys.systemName()) {
        case "Linux", "BSD": {
            Sys.command("xdg-open", [url]);
        }
        case "Windows": {
            // TODO verify that this works
            Sys.command("start", [url]);
        }
        default: {
            trace("openUrl failed: unsupported system '" + Sys.systemName() + "'");
        }
    }
}

function openFolder(path: String): Void {
    switch (Sys.systemName()) {
        case "Linux", "BSD": {
            Sys.command("xdg-open", [path]);
        }
        case "Windows": {
            // TODO verify that this works
            Sys.command("start", [path]);
        }
        default: {
            trace("openFolder failed: unsupported system '" + Sys.systemName() + "'");
        }
    }
}