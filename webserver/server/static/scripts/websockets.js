var callbacks = {};

function startWebsocket() {
    window.websocket = new WebSocket("ws://localhost:8888/ws/client");

    window.websocket.onmessage = function(msg) {
        var data = JSON.parse(msg.data);
        console.log(data);

        var chain = callbacks[data.event];
        if(typeof chain == 'undefined') return; // no callbacks for this event
        for(var i = 0; i < chain.length; i++){
          chain[i](data);
        }
    };

    // Tell the server this is client 1 (swap for client 2 of course)
    //    websocket.send(JSON.stringify({
    //      id: "client1"
    //    }));
    //
    //    // Tell the server we want to send something to the other client
    //    websocket.send(JSON.stringify({
    //      to: "client2",
    //      data: "foo"
    //    }));
}

function addWebsocketCallback(event, callback) {
    callbacks[event] = callbacks[event] || [];
    callbacks[event].push(callback);
    return this;// chainable
}