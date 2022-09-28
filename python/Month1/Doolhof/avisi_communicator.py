from urllib import request
import requests
import json
from models import Tile

class Api:

    def __init__(self):
        self.endpoint = "https://acc.avisilabs.nl/api/maze"
        self.api_key = "e41b0ea1-6693-403e-9b87-a2ebbc7284cb"


    def reset_maze(self):
        path = "/reset"
        return self.post_request(path)


    def get_current_pos(self) -> Tile:
        path = "/position"
        response = json.loads(self.get_request(path).text)
        return Tile(response['position']['x'], response['position']['y'], response['openDirections'], response['lockedDirections'], response['item'])


    def move(self, direction):
        path = f"/position/move/{direction}"
        response = self.post_request(path)
        if (response.status_code == 200):
            return True
        else:
            return False


    def pick_up_key(self, key):
        path = f"/inventory/loot/key/{key}"
        return json.loads(self.post_request(path).text)


    def get_inventory(self):
        path = f"/inventory"
        return json.loads(self.get_request(path).text)


    def unlock_door(self, key, door):
        path = f"/inventory/use/key/{key}/{door['direction']}"
        return self.post_request(path)

    
    def loot_fuse(self):
        path = "/inventory/loot/fuse"
        response = json.loads(self.post_request(path).text)
        print(response['fuse'])
        self.insert_fuse()
        return response


    def insert_fuse(self):
        path = "/fuseholder"
        response = json.loads(self.post_request(path).text)
        print(response['flag'])
        return response['flag']

    def post_request(self, path):
        URL = self.endpoint + path + f"?secret={self.api_key}"
        response = requests.post(url=URL)
        try:
            return response
        except:
            pass


    def get_request(self, path):
        URL = self.endpoint + path + f"?secret={self.api_key}"
        response = requests.get(url=URL)
        try:
            return response
        except:
            pass