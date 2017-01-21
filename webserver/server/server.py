import logging
import os
from tornado import ioloop, web, websocket

from server.clientsocket import ClientSocket
from server.gamesocket import GameSocket
from server.web import MainAppHandler

logger = logging.getLogger(__name__)


class Server:
    def __init__(self):
        logger.debug("Created Server")
        self._port = 8888
        self.app = web.Application(
            handlers=[
                (r'/', MainAppHandler),
                (r'/ws/client', ClientSocket),
                (r'/ws/game', GameSocket),
                (r'/static/(.*)', web.StaticFileHandler, {'path': os.path.join(os.path.dirname(__file__), "static")})
            ],
            template_path=os.path.join(os.path.dirname(__file__), "templates")
        )
        self.app.ctxt = self
        self.game = None
        self.client = None

    def is_ready(self):
        return bool(self.game) and bool(self.client)

    def send_both(self, message):
        if self.game and self.client:
            self.game.write_message(message)
            self.client.write_message(message)

    def run(self):
        logger.debug("Starting Server, listening on 0.0.0.0:{port}".format(port=self._port))
        self.app.listen(self._port)
        ioloop.IOLoop.instance().start()
