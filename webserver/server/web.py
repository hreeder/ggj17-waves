from tornado import web


class MainAppHandler(web.RequestHandler):
    def get(self):
        self.render("index.html")
