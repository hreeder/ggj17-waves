#!/usr/bin/env python
import logging

from server import Server

logging.basicConfig(level=logging.DEBUG)

server = Server()
server.run()
