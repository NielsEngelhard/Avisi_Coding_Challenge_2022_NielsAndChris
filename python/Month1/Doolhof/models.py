import string


class Tile:

    def __init__(self, x, y, open_dirs, locked_dirs, item):
        self.x = x
        self.y = y
        self.open_dirs = open_dirs
        self.locked_dirs = locked_dirs
        self.item = item
        self.n_visited = 0

    def __str__(self):
        print(f"[x: {self.x}, y: {self.y}, open directions: {self.open_dirs}, locked directions: {self.locked_dirs}, item: {self.item}, times visited: {self.n_visited}]")