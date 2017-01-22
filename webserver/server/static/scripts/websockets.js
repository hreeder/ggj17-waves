var callbacks = {};
var actionCallbacks = {};

function startWebsocket() {
    window.websocket = new WebSocket("ws://localhost:8888/ws/client");

    window.websocket.onmessage = function(msg) {
        var data = JSON.parse(msg.data);
        console.log(data);

        if (data.event === "action") {
            console.log("action callback");
            console.log("level: " + data.level);
            var chain = actionCallbacks[data.level];
            if(typeof chain == 'undefined') return; // no callbacks for this event
            console.log("dispatching action callback for " + data.level);
            console.log(chain);
            for(var i = 0; i < chain.length; i++){
              chain[i](data);
            }
        } else {
            var chain = callbacks[data.event];
            if(typeof chain == 'undefined') return; // no callbacks for this event
            for(var i = 0; i < chain.length; i++){
              chain[i](data);
            }
        }
    };
}

function addWebsocketCallback(event, callback) {
    callbacks[event] = callbacks[event] || [];
    callbacks[event].push(callback);
    return this;// chainable
}

function addActionCallback(level, callback) {
    console.log("adding callback for " + level);
    actionCallbacks[level] = actionCallbacks[level] || [];
    actionCallbacks[level].push(callback);
    return this;// chainable
}