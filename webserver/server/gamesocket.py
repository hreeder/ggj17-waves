import json
import logging

from tornado import websocket

logger = logging.getLogger(__name__)


class GameSocket(websocket.WebSocketHandler):
    def open(self):
        logger.debug("Game WebSocket opened")
        self.application.ctxt.game = self
        self.events = {
            "start-game": self.start_game,
            "load-level": self.load_level,
            "action": self.action
        }

        if self.application.ctxt.is_ready():
            self.application.ctxt.send_both(json.dumps({
                "event": "ready"
            }))
        else:
            self.write_message(json.dumps({
                "event": "waiting-client"
            }))

    def check_origin(self, origin):
        return True

    def on_message(self, message):
        data = json.loads(message)
        logger.debug(data)
        if "event" in data and data["event"] in self.events:
            event = data["event"]
            self.events[event](data)

    def on_close(self):
        logger.debug("WebSocket closed")

    def start_game(self, message):
        self.application.ctxt.client.write_message(json.dumps({
            "event": "start-game"
        }))

    def load_level(self, message):
        self.application.ctxt.client.write_message(message)

    def action(self, message):
        self.application.ctxt.client.write_message(message)
