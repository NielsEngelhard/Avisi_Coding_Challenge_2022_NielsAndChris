import os
os.system('cls')

from tkinter import *

GRID_SIZE = 60
TILE_SIZE = 15

TEST_MAP = [(1,6),(3,6),(3,3),(5,3),(5,2),(4,2),(4,1),(1,1),(1,3),(2,3),(2,5),(1,5),(1,6)]
REAL_MAP = [(5,5),(10,5),(10,7),(18,7),(18,3),(13,3),(13,-2),(10,-2),(10,-4),(4,-4),(4,-5),(6,-5),(6,-14),(12,-14),(12,-16),(10,-16),(10,-24),(1,-24),(1,-25),(-5,-25),(-5,-24),(-13,-24),(-13,-22),(-16,-22),(-16,-20),(-7,-20),(-7,-22),(0,-22),(0,-18),(2,-18),(2,-21),(9,-21),(9,-20),(6,-20),(6,-17),(1,-17),(1,-9),(2,-9),(2,-6),(-1,-6),(-1,-11),(-5,-11),(-5,-10),(-10,-10),(-10,-17),(-17,-17),(-17,-26),(-13,-26),(-13,-29),(-17,-29),(-17,-31),(-15,-31),(-15,-30),(-7,-30),(-7,-32),(-1,-32),(-1,-29),(-8,-29),(-8,-28),(-4,-28),(-4,-27),(-6,-27),(-6,-26),(-1,-26),(-1,-27),(1,-27),(1,-34),(-4,-34),(-4,-43),(-6,-43),(-6,-36),(-13,-36),(-13,-40),(-16,-40),(-16,-37),(-23,-37),(-23,-31),(-31,-31),(-31,-36),(-33,-36),(-33,-40),(-38,-40),(-38,-33),(-40,-33),(-40,-38),(-44,-38),(-44,-31),(-43,-31),(-43,-32),(-37,-32),(-37,-34),(-32,-34),(-32,-32),(-35,-32),(-35,-27),(-40,-27),(-40,-26),(-43,-26),(-43,-23),(-36,-23),(-36,-16),(-38,-16),(-38,-19),(-39,-19),(-39,-17),(-40,-17),(-40,-18),(-43,-18),(-43,-11),(-45,-11),(-45,-6),(-46,-6),(-46,-13),(-50,-13),(-50,-21),(-53,-21),(-53,-16),(-51,-16),(-51,-9),(-56,-9),(-56,-10),(-57,-10),(-57,-3),(-62,-3),(-62,-6),(-66,-6),(-66,-4),(-64,-4),(-64,1),(-60,1),(-60,10),(-58,10),(-58,12),(-50,12),(-50,13),(-44,13),(-44,4),(-46,4),(-46,9),(-49,9),(-49,5),(-58,5),(-58,1),(-49,1),(-49,-1),(-51,-1),(-51,-2),(-44,-2),(-44,-1),(-48,-1),(-48,0),(-44,0),(-44,2),(-52,2),(-52,3),(-43,3),(-43,2),(-42,2),(-42,0),(-36,0),(-36,-9),(-40,-9),(-40,-5),(-38,-5),(-38,-2),(-42,-2),(-42,-6),(-41,-6),(-41,-7),(-42,-7),(-42,-16),(-40,-16),(-40,-10),(-36,-10),(-36,-14),(-31,-14),(-31,-5),(-24,-5),(-24,0),(-17,0),(-17,-9),(-9,-9),(-9,-4),(-10,-4),(-10,4),(-14,4),(-14,10),(-23,10),(-23,2),(-24,2),(-24,5),(-30,5),(-30,4),(-37,4),(-37,8),(-33,8),(-33,9),(-34,9),(-34,16),(-32,16),(-32,13),(-26,13),(-26,15),(-27,15),(-27,19),(-19,19),(-19,12),(-15,12),(-15,13),(-10,13),(-10,8),(5,8),(5,5)]

robot_position = (2, 2)

root = Tk()
doolhof_canvas = None

def draw_tile(canvas: Canvas, x1, y1, x2, y2):
    line_color = "black"
    canvas.create_line(x1 * TILE_SIZE, y1 * TILE_SIZE, x2 * TILE_SIZE, y2 * TILE_SIZE, fill=line_color) # TOP

def draw_spaceship_map():
    
    root.title("Spaceship")
    root.geometry("1080x700")

    global doolhof_canvas
    doolhof_canvas = Canvas(root, scrollregion=(-1000, -650, 450, 300), bg="silver")

    doolhof_canvas.pack(side=LEFT, fill=BOTH, expand=1, padx=10, pady=10)

    used_map = REAL_MAP

    draw_map(doolhof_canvas, used_map)
    draw_robot(doolhof_canvas, robot_position)

    def on_keypress(event):
        key = event.char
        if (key == 'w' or key == 'a' or key == 's' or key == 'd'):

            global robot_position
            robot_position_list = list(robot_position)

            if (key == 'w'):
                robot_position_list[1] -= 1
            if (key == 'a'):
                robot_position_list[0] -= 1
            if (key == 's'):
                robot_position_list[1] += 1
            if (key == 'd'):
                robot_position_list[0] += 1

            robot_position = tuple(robot_position_list)
            
            doolhof_canvas.delete("all")
            draw_map(doolhof_canvas, used_map)
            draw_robot(doolhof_canvas, robot_position)

    doolhof_canvas.bind_all("<Key>", on_keypress)

def draw_robot(canvas, position):
    color = "aquamarine"
    canvas.create_rectangle(position[0] * TILE_SIZE, position[1] * TILE_SIZE , position[0] * TILE_SIZE + TILE_SIZE, position[1] * TILE_SIZE + TILE_SIZE, fill=color, outline="black")

def draw_map(canvas, used_map):
    for i in range(len(used_map)):
        if (i == len(used_map) - 1):
            continue
        else:
            x1 = used_map[i][0]
            y1 = used_map[i][1]
            x2 = used_map[i+1][0]
            y2 = used_map[i+1][1]
            draw_tile(canvas, x1, y1, x2, y2)

def run():
    root.mainloop()

draw_spaceship_map()
root.state('zoomed')
run()