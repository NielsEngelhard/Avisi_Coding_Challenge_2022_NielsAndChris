import random
from tkinter import *
from tkinter import ttk
from avisi_communicator import Api
from models import Tile

GRID_SIZE = 100
TILE_SIZE = 25

root = Tk()
API = Api()
tile_map = [[Tile for i in range(GRID_SIZE)] for j in range(GRID_SIZE)]
doolhof_canvas = None
prev_dir = ""
all_locked_doors = []


def draw_tile(canvas: Canvas, x, y, open_directions = [], n_visited = 0, is_current_position = False, is_key = False, is_locked_door = False, is_navigable = False):
    box_color = ""
    top_line_color = "black"
    bottom_line_color = "black"
    left_line_color = "black"
    right_line_color = "black"

    if (n_visited == 1):
        box_color = "cornflowerblue"
    elif (n_visited > 1):
        box_color = "midnightblue"

    if (is_current_position):
        box_color = "lightcoral"

    if (is_key):
        box_color = "gold"

    if (is_locked_door):
        box_color = "orange"

    if (is_navigable):
        box_color = "limegreen"

    if (n_visited > 0):
        if (open_directions.__contains__('UP')):
            top_line_color = box_color
        if (open_directions.__contains__('DOWN')):
            bottom_line_color = box_color
        if (open_directions.__contains__('LEFT')):
            left_line_color = box_color
        if (open_directions.__contains__('RIGHT')):
            right_line_color = box_color

    canvas.create_rectangle(x * TILE_SIZE, y * TILE_SIZE , x * TILE_SIZE + TILE_SIZE, y * TILE_SIZE + TILE_SIZE, fill=box_color, outline="")

    canvas.create_line(x * TILE_SIZE, y * TILE_SIZE, x * TILE_SIZE + TILE_SIZE, y * TILE_SIZE, fill=top_line_color) # TOP
    canvas.create_line(x * TILE_SIZE, y * TILE_SIZE + TILE_SIZE, x * TILE_SIZE + TILE_SIZE + 1, y * TILE_SIZE + TILE_SIZE, fill=bottom_line_color) # BOTTOM
    canvas.create_line(x * TILE_SIZE, y * TILE_SIZE, x * TILE_SIZE, y * TILE_SIZE + TILE_SIZE, fill=left_line_color) # LEFT
    canvas.create_line(x * TILE_SIZE + TILE_SIZE, y * TILE_SIZE, x * TILE_SIZE + TILE_SIZE, y * TILE_SIZE + TILE_SIZE, fill=right_line_color) # RIGHT


def draw_maze():
    
    root.title("Doolhof")
    root.geometry("1080x700")

    global doolhof_canvas
    doolhof_canvas = Canvas(root, scrollregion=(0, 0, 2000, 2000), bg="silver")

    def on_mousewheel(event):
        shift = (event.state & 0x1) != 0
        scroll = -1 if event.delta > 0 else 1
        if shift:
            doolhof_canvas.xview_scroll(scroll, "units")
        else:
            doolhof_canvas.yview_scroll(scroll, "units")

    y_scroll = ttk.Scrollbar(root, orient="vertical", command=doolhof_canvas.yview)
    x_scroll = ttk.Scrollbar(root, orient="horizontal", command=doolhof_canvas.xview)
    x_scroll.pack(side=BOTTOM, fill=X)
    y_scroll.pack(side=RIGHT, fill=Y)

    doolhof_canvas.pack(side=LEFT, fill=BOTH, expand=1, padx=10, pady=10)

    doolhof_canvas.configure(yscrollcommand=y_scroll.set)
    doolhof_canvas.configure(xscrollcommand=x_scroll.set)

    doolhof_canvas.bind_all("<MouseWheel>", on_mousewheel)

    for x in range(GRID_SIZE):
        for y in range(GRID_SIZE):
            draw_tile(doolhof_canvas, x, y)

    API.reset_maze()

    update_maze_with_current_pos(doolhof_canvas)

    def on_keypress(event):
        key = event.char
        if (key == 'w' or key == 'a' or key == 's' or key == 'd'):
            dir = ""
            has_moved = False
            if (key == 'w'):
                has_moved = API.move('UP')
                dir = 'UP'
            if (key == 'a'):
                has_moved = API.move('LEFT')
                dir = 'LEFT'
            if (key == 's'):
                has_moved = API.move('DOWN')
                dir = 'DOWN'
            if (key == 'd'):
                has_moved = API.move('RIGHT')
                dir = 'RIGHT'
            
            if(has_moved):
                update_maze_with_current_pos(doolhof_canvas)
                recolor_prev_pos(doolhof_canvas, dir)

    doolhof_canvas.bind_all("<Key>", on_keypress)

    doolhof_canvas.bind_all("<space>", tremeux)


def update_maze_with_current_pos(canvas):
    new_tile = API.get_current_pos()
    new_tile.n_visited += 1
    tile_map[new_tile.x][new_tile.y] = new_tile
    draw_tile(canvas, new_tile.x, new_tile.y, new_tile.open_dirs, new_tile.n_visited, True)
    if (new_tile.item):
        print(f"KEY FOUND: {new_tile.item}")
        API.pick_up_key(new_tile.item['keyType'])
        check_doors_and_keys()
    if (len(new_tile.locked_dirs) > 0):
        print(f"LOCKED DOOR(S) FOUND: {new_tile.locked_dirs}")
        draw_locked_doors(new_tile.locked_dirs, new_tile)
        check_doors_and_keys()
    draw_possible_directions(new_tile)
    

def recolor_prev_pos(canvas, moved_in_direction):
    current_pos = API.get_current_pos()
    prev = prev_pos(current_pos, moved_in_direction)
    global prev_dir
    prev_dir = prev_direction(moved_in_direction)
    found_key = False
    if (prev.item):
        found_key = True
    draw_tile(canvas, prev.x, prev.y, prev.open_dirs, prev.n_visited, is_key=found_key)


def auto_move(canvas, direction):
    has_moved = API.move(direction)
    if(has_moved):
        update_maze_with_current_pos(canvas)
        recolor_prev_pos(canvas, direction)


def prev_pos(current_pos, moved_in_direction) -> Tile:
    current_x = current_pos.x
    current_y = current_pos.y

    if (moved_in_direction == "UP"):
        return tile_map[current_x][current_y+1]
    if (moved_in_direction == "DOWN"):
        return tile_map[current_x][current_y-1]
    if (moved_in_direction == "LEFT"):
        return tile_map[current_x+1][current_y]
    if (moved_in_direction == "RIGHT"):
        return tile_map[current_x-1][current_y]


def prev_direction(moved_in_direction) -> Tile:

    if (moved_in_direction == "UP"):
        return "DOWN"
    if (moved_in_direction == "DOWN"):
        return "UP"
    if (moved_in_direction == "LEFT"):
        return "RIGHT"
    if (moved_in_direction == "RIGHT"):
        return "LEFT"


def tremeux(_):
    global prev_dir
    if (API.get_current_pos().x ==0 and API.get_current_pos().y == 0):
        auto_move(doolhof_canvas, "RIGHT")
        prev_dir = "LEFT"
    while(True):
        current_pos = API.get_current_pos()
        available_dirs = current_pos.open_dirs
        if (len(current_pos.open_dirs) == 2):
            available_dirs.remove(prev_dir)
            direction = available_dirs[0]
            auto_move(doolhof_canvas, direction)
            prev_dir = prev_direction(direction)
        elif (len(current_pos.open_dirs) > 2): # Arrived at a junction
            available_dirs.remove(prev_dir)
            direction = random.choice(available_dirs)
            auto_move(doolhof_canvas, direction)
            prev_dir = prev_direction(direction)
        else:
            break
        root.update()


def draw_locked_doors(locked_doors, current_tile):
    for door in locked_doors:
        if (door['direction'] == "UP"):
            draw_tile(doolhof_canvas, current_tile.x, current_tile.y-1, current_tile.open_dirs, current_tile.n_visited, is_locked_door=True)
        if (door['direction'] == "DOWN"):
            draw_tile(doolhof_canvas, current_tile.x, current_tile.y+1, current_tile.open_dirs, current_tile.n_visited, is_locked_door=True)
        if (door['direction'] == "LEFT"):
            draw_tile(doolhof_canvas, current_tile.x-1, current_tile.y, current_tile.open_dirs, current_tile.n_visited, is_locked_door=True)
        if (door['direction'] == "RIGHT"):
            draw_tile(doolhof_canvas, current_tile.x+1, current_tile.y, current_tile.open_dirs, current_tile.n_visited, is_locked_door=True)
        all_locked_doors.append(door)
        

def draw_possible_directions(current_tile):
    for direction in current_tile.open_dirs:
        if (direction == "UP"):
            possible_dir_tile = tile_map[current_tile.x][current_tile.y-1]
            if (not hasattr(possible_dir_tile, 'n_visited')):
                draw_tile(doolhof_canvas, current_tile.x, current_tile.y-1, current_tile.open_dirs, 0, is_navigable=True)
        if (direction == "DOWN"):
            possible_dir_tile = tile_map[current_tile.x][current_tile.y+1]
            if (not hasattr(possible_dir_tile, 'n_visited')):
                draw_tile(doolhof_canvas, current_tile.x, current_tile.y+1, current_tile.open_dirs, 0, is_navigable=True)
        if (direction == "LEFT"):
            possible_dir_tile = tile_map[current_tile.x-1][current_tile.y]
            if (not hasattr(possible_dir_tile, 'n_visited')):
                draw_tile(doolhof_canvas, current_tile.x-1, current_tile.y, current_tile.open_dirs, 0, is_navigable=True)
        if (direction == "RIGHT"):
            possible_dir_tile = tile_map[current_tile.x+1][current_tile.y]
            if (not hasattr(possible_dir_tile, 'n_visited')):
                draw_tile(doolhof_canvas, current_tile.x+1, current_tile.y, current_tile.open_dirs, 0, is_navigable=True)


def check_doors_and_keys():
    keys = API.get_inventory()['keys']
    for key in keys:
        for door in all_locked_doors:
            if (key == door['requiredKey']):
                print(f"JAJA CAN UNLOCK DOOR: {door}")
                response = API.unlock_door(key, door)
                if (response.status_code == 200):
                    print(f"DOOR UNLOCKED: {door}")


def run():
    root.mainloop()