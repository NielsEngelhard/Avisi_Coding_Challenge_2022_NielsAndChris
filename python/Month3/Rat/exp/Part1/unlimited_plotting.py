import requests
import json
import datetime
import time
import matplotlib.pyplot as plt
from datetime import timedelta
import datetime as dt
import numpy as np

def avisi_str_to_datetime(avisi_str):
    dt_pattern = pattern = '%Y-%m-%dT%H:%M:%S.%fZ' # pattern of how api passes date times. The Z at the end indicates UTC time
    return datetime.datetime.strptime(avisi_str, dt_pattern)

# express date time as a floating number
def datetime_to_float(d):
    return d.timestamp()

# Plot 1 iteration
# Put coordinates of the new big data to 2d array
def get_data_x_position_by_file_name(file_name):
    big_data_x_coordinates = []

    with open(file_name, 'r') as file:
        lines = file.readlines()

        for line in lines:
            coordinate = []
            json_line = json.loads(line)

            datetime_obj = avisi_str_to_datetime(json_line['timestamp'])
            datetime_as_float = datetime_to_float(datetime_obj)

            coordinate.append(datetime_as_float)
            coordinate.append(json_line['x_position'])
            big_data_x_coordinates.append(coordinate)

    return big_data_x_coordinates

def get_data_y_position_by_file_name(file_name):
    big_data_x_coordinates = []

    with open(file_name, 'r') as file:
        lines = file.readlines()

        for line in lines:
            coordinate = []
            json_line = json.loads(line)

            datetime_obj = avisi_str_to_datetime(json_line['timestamp'])
            datetime_as_float = datetime_to_float(datetime_obj)

            coordinate.append(datetime_as_float)
            coordinate.append(json_line['y_position'])
            big_data_x_coordinates.append(coordinate)

    return big_data_x_coordinates

single_run_x_data = get_data_x_position_by_file_name(r"C:\Users\niels\source\repos\Avisi_Coding_Challenge_2022_NielsAndChris\python\Month3\Rat\two_rat_loops.txt")

x_axis_array = []
y_axis_array = []

for line in single_run_x_data:
    x_axis_array.append(line[0])
    y_axis_array.append(line[1])

x_axis = np.array(x_axis_array) # the time stamp
y_axis = np.array(y_axis_array) # the x_position

x=range(len(x_axis))

fig, ax = plt.subplots(1, 1)
ax.set_xticks(x) # set tick positions
# Labels are formated as integers:
ax.set_xticklabels(["{:d}".format(int(v)) for v in x_axis]) 
ax.plot(x, y_axis)  

fig.canvas.draw() # actually draw figure
plt.show() # enter GUI loop (for non-interactive interpreters)