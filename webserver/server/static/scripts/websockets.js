function startWebsocket() {
    window.websocket = new WebSocket("ws://localhost:8888/ws/client");

    window.websocket.onmessage = function(msg) {
        console.log(JSON.parse(msg.data));
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
