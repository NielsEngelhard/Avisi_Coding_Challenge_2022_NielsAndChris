from cProfile import label
from models import Tree, BigBottle, SmallBottle, GardenHose, FireHose
import matplotlib.pyplot as plt

N_TREES = 50
N_BIG_BOTTLES = 8
N_SMALL_BOTTLES = 16

big_bottles = [BigBottle() for _ in range(N_BIG_BOTTLES)]
small_bottles = [SmallBottle() for _ in range(N_SMALL_BOTTLES)]
garden_hose = [GardenHose()]
fire_hose = [FireHose()]

def water_trees(order):
    trees = [Tree() for _ in range(N_TREES)]
    delta_time = 0
    time_intervals = []
    time_intervals.append(delta_time)

    water_method = order[0][0]
    water_method_type = type(water_method)

    delta_time += water_method.time_to_get
    for tree in trees:
        delta_time += water_method.time_to_change_tree
        while (tree.amt_to_water > 0):

            if (water_method_type == BigBottle or water_method_type == SmallBottle):
                tree.amt_to_water -= water_method.liter_per_second
                delta_time += water_method.liter_per_second
                water_method.amt_water_left -= water_method.liter_per_second

                if (water_method.amt_water_left == 0):
                    order[0].pop(0)
                    if (len(order[0]) == 0):
                        order.pop(0)
                    if(len(order) == 0):
                        break
                    water_method = order[0][0]
                    water_method_type = type(water_method)
                    delta_time += water_method.time_to_get
            else:
                tree.amt_to_water -= 1
                delta_time += 1 / water_method.liter_per_second

        time_intervals.append(delta_time)

        if(len(order) == 0):
            break

    reset()
    return time_intervals


def reset():
    global big_bottles
    big_bottles = [BigBottle() for _ in range(N_BIG_BOTTLES)]
    global small_bottles
    small_bottles = [SmallBottle() for _ in range(N_SMALL_BOTTLES)]
    global garden_hose
    garden_hose = [GardenHose()]
    global fire_hose
    fire_hose = [FireHose()]


def run():
    y_axis_fh = water_trees([fire_hose])
    y_axis_gh = water_trees([garden_hose])
    y_axis_sb = water_trees([small_bottles])
    y_axis_bb = water_trees([big_bottles])

    fig, axis = plt.subplots(2)

    axis[0].plot(range(len(y_axis_fh)), y_axis_fh, label="Fire Hose")
    axis[0].plot(range(len(y_axis_gh)), y_axis_gh, label="Garden Hose")
    axis[0].plot(range(len(y_axis_sb)), y_axis_sb, label="Small Bottles")
    axis[0].plot(range(len(y_axis_bb)), y_axis_bb, label="Big Bottles")

    order_1 = water_trees([small_bottles , big_bottles , fire_hose])
    order_2 = water_trees([big_bottles, small_bottles, fire_hose])

    axis[1].plot(range(len(order_1)), order_1, label="SB -> BB -> FH")
    axis[1].plot(range(len(order_2)), order_2, label="BB -> SB -> GH")

    print("flag{" + str(int(order_1[-1])) + "}")

    axis[0].set_title("All water methods efficiency")
    axis[1].set_title("Test order(s)")
    axis[0].legend()
    axis[1].legend()
    plt.show()
