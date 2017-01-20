import logging
import os
from tornado import ioloop, web, websocket

from server.socket import EchoWebSocket
from server.web import MainAppHandler

logger = logging.getLogger(__name__)


class Server:
    def __init__(self):
        logger.debug("Created Server")
        self.app = web.Application(
            handlers=[
                (r'/', MainAppHandler),
                (r'/ws', EchoWebSocket)
            ],
            template_path=os.path.join(os.path.dirname(__file__), "templates")
        )

    def run(self):
        logger.debug("Starting Server")
        self.app.listen(8888)
        ioloop.IOLoop.instance().start()
