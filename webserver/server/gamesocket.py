from tornado import websocket


class EchoWebSocket(websocket.WebSocketHandler):
    def open(self):
        print("WebSocket opened")
        self.application.game = self

    def on_message(self, message):
        self.write_message(u"You said: " + message)

    def on_close(self):
        print("WebSocket closed")
