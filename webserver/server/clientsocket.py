import json
import logging

from tornado import websocket

logger = logging.getLogger(__name__)


class ClientSocket(websocket.WebSocketHandler):
    def open(self):
        logger.debug("Client WebSocket opened")
        self.application.ctxt.client = self
        self.events = {
            "DEBUG-reset": self.debug_reset
        }

        if self.application.ctxt.is_ready():
            self.application.ctxt.send_both(json.dumps({
                "event": "ready"
            }))
        else:
            self.write_message(json.dumps({
                "event": "waiting-game"
            }))

    def on_message(self, message):
        data = json.loads(message)
        if "event" in data and data["event"] in self.events:
            event = data["event"]
            self.events[event](message)

    def on_close(self):
        logger.debug("WebSocket closed")

    def debug_reset(self):
        self.application.ctxt.client = None
        self.close()
