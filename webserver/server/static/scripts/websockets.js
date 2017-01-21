var callbacks = {};
var actionCallbacks = {};

function startWebsocket() {
    window.websocket = new WebSocket("ws://localhost:8888/ws/client");

    window.websocket.onmessage = function(msg) {
        var data = JSON.parse(msg.data);
        console.log(data);

        if (data.event === "action") {
            var chain = actionCallbacks[data.level];
            if(typeof chain == 'undefined') return; // no callbacks for this event
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
    actionCallbacks[event] = actionCallbacks[event] || [];
    actionCallbacks[event].push(callback);
    return this;// chainable
}