import json
import logging

from tornado import websocket

logger = logging.getLogger(__name__)


class GameSocket(websocket.WebSocketHandler):
    def open(self):
        logger.debug("Game WebSocket opened")
        self.application.ctxt.game = self
        self.actions = {
            "start-game": self.start_game,
            "load-level": self.load_level
        }

        if self.application.ctxt.is_ready():
            self.application.ctxt.send_both(json.dumps({
                "event": "ready"
            }))
        else:
            self.write_message(json.dumps({
                "event": "waiting-client"
            }))

    def on_message(self, message):
        data = json.loads(message)
        if "action" in data and data["action"] in self.actions:
            action = data["action"]
            self.actions[action](message)

    def on_close(self):
        logger.debug("WebSocket closed")

    def start_game(self, message):
        """ TODO: Start Game """
        pass

    def load_level(self, message):
        self.application.ctxt.client.write_message(json.dumps({
            "event": "load-level",
            "level": message['level']
        }))
