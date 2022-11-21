import requests
import json
import datetime

class RatLocationResponse:
    x_position: float
    y_position: float
    timestamp: str
        
    def print_resp(self):
        print("x: " + str(self.x_position) + " y: " + str(self.y_position) + " timestamp: " + timestamp)
    

def get_current_rat_location():
    response = requests.get('https://acc.avisilabs.nl/api/rat/position?secret=e41b0ea1-6693-403e-9b87-a2ebbc7284cb')
    js_rs = response.json
    json_as_string = json.dumps(js_rs())
    json_obj = json.loads(json_as_string)
    return rat_response_to_object(json_obj)

def rat_response_to_object(rat_response):
    object_to_return = RatLocationResponse()
    
    object_to_return.x_position = rat_response['position']['x']
    object_to_return.y_position = rat_response['position']['y']
    object_to_return.timestamp = rat_response['timestamp']
    
    return object_to_return
    
import matplotlib.pyplot as plt

def avisi_str_to_datetime(avisi_str):
    dt_pattern = pattern = '%Y-%m-%dT%H:%M:%S.%fZ' # pattern of how api passes date times. The Z at the end indicates UTC time
    return datetime.datetime.strptime(avisi_str, dt_pattern)
#0: x= 239.4471767 y=428.8943535time=2022-11-19T14:56:36.588155Z
#1: x= 239.3359039 y=428.6718078time=2022-11-19T14:56:37.678678Z
coordinates = [
    (avisi_str_to_datetime("2022-11-19T14:56:36.588155Z"), 239.4471767),
    (avisi_str_to_datetime("2022-11-19T14:56:37.678678Z"), 239.3359039)
]

x, y = zip(*coordinates)

plt.plot(*zip(*coordinates))