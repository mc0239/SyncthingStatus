package ;

import hx.widgets.*;

class StatusCheckTimer {

    private static final TIMER_INTERVAL = 1000 * 5;

    private final app: App;
    private final client: ApiClient;
    private final iconHandler: TaskBarIconHandler;

    private final timer: Timer;

    public function new(app: App, parent: Window, apiClient: ApiClient, iconHandler: TaskBarIconHandler) {
        this.app = app;
        this.client = apiClient;
        this.iconHandler = iconHandler;

        // TODO wxTimer requires EvtHandler, not sure why is hx Timer limited to Window.
        this.timer = new Timer(parent);

        parent.bind(EventType.TIMER, (event) -> {   
            execute(); 
        });
    }

    public function start() {
        execute();
        this.timer.start(TIMER_INTERVAL, false);
    }

    public function execute() {
        var handleAsBadResponse = function(error: String) {
            iconHandler.setState(BadResponse);
        }


        client.ping((data) -> {
            client.version((data) -> {
                iconHandler.setVersion(data.version);

                client.error((data) -> {
                    if (data.errors != null && data.errors.length > 0) {
                        iconHandler.setState(HasErrors);
                    }

                    client.folders((data) -> {
                        iconHandler.setState(Ok);
                        iconHandler.setFolders(data);
                    }, handleAsBadResponse);

                }, handleAsBadResponse);
                
            }, handleAsBadResponse);

        }, (error) -> {
            if (StringTools.startsWith(error, "Http Error #403")) {
                iconHandler.setState(BadResponse);
            } else {
                iconHandler.setState(NoResponse);
            }
        });
    }

}