package ;

import data.*;
import haxe.Http;
import haxe.Json;


class ApiClient {

    private var config: AppConfigHandler;

    public function new(config: AppConfigHandler) {
        this.config = config;
    }

    private static final PATH_PING = "/rest/system/ping";
    private static final PATH_VERSION = "/rest/system/version";
    private static final PATH_ERROR = "/rest/system/error";
    private static final PATH_FOLDERS = "/rest/config/folders";

    private static final HEADER_X_API_KEY = "X-API-Key";

    public function ping(
        onSuccess: (data: PingResponse) -> Void,
        onError: (msg: String) -> Void
    ) {
        makeRequest(config.getSyncthingAddress() + PATH_PING, config.getApiKey(), onSuccess, onError);
    }

    public function version(
        onSuccess: (data: VersionResponse) -> Void,
        onError: (msg: String) -> Void
    ) {
        makeRequest(config.getSyncthingAddress() + PATH_VERSION, config.getApiKey(), onSuccess, onError);
    }

    public function error(
        onSuccess: (data: ErrorResponse) -> Void,
        onError: (msg: String) -> Void
    ) {
        makeRequest(config.getSyncthingAddress() + PATH_ERROR, config.getApiKey(), onSuccess, onError);
    }

    public function folders(
        onSuccess: (data: FoldersResponse) -> Void,
        onError: (msg: String) -> Void
    ) {
        makeRequest(config.getSyncthingAddress() + PATH_FOLDERS, config.getApiKey(), onSuccess, onError);
    }

    public static function makeRequest<T>(
        url: String, 
        apiKey: String, 
        onSuccess: (data: T) -> Void,
        onError: (msg: String) -> Void = null
    ) {
        var req = new Http(url);
        req.addHeader(HEADER_X_API_KEY, apiKey);
        // trace("Making request to " + url);
        req.onData = function(data) {
            // trace("Response OK: " + data);
            var parsedData:T = Json.parse(data);
            onSuccess(parsedData);
        }

        req.onError = function(error) {
            trace(error);
            if (onError != null) {
                onError(error);
            }
        }

        req.request();
    }

}