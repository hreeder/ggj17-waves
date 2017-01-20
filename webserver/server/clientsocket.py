import json

from tornado import websocket


class EchoWebSocket(websocket.WebSocketHandler):
    actions = {
        "DEBUG-reset": self.debug_reset
    }

    def open(self):
        print("WebSocket opened")
        self.application.client = self

    def on_message(self, message):
        data = json.loads(message)
        if "action" in data and data["action"] in self.actions:
            action = data["action"]
            self.actions[action](message)

    def on_close(self):
        print("WebSocket closed")

    def debug_reset(self):
        self.application.client = None
        self.close()
