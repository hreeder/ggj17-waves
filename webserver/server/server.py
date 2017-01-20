import logging
import os
from tornado import ioloop, web, websocket

from server.gamesocket import EchoWebSocket
from server.web import MainAppHandler

logger = logging.getLogger(__name__)


class Server:
    def __init__(self):
        logger.debug("Created Server")
        self._port = 8888
        self.app = web.Application(
            handlers=[
                (r'/', MainAppHandler),
                (r'/ws/client', EchoWebSocket),
                (r'/ws/game', EchoWebSocket)
            ],
            template_path=os.path.join(os.path.dirname(__file__), "templates")
        )
        self.game = None
        self.client = None

    def run(self):
        logger.debug("Starting Server, listening on 0.0.0.0:{port}".format(port=self._port))
        self.app.listen(self._port)
        ioloop.IOLoop.instance().start()
