class Tree:
    def __init__(self):
        self.amt_to_water = 3 # 3 Liter


class BigBottle:
    def __init__(self):
        self.amt_water_left = 8 # 8 Liter
        self.liter_per_second = 1
        self.time_to_change_tree = 1
        self.time_to_get = 6


class SmallBottle:
    def __init__(self):
        self.amt_water_left = 5 # 5 Liter
        self.liter_per_second = 1
        self.time_to_change_tree = 1
        self.time_to_get = 2


class GardenHose:
    def __init__(self):
        self.liter_per_second = 0.5
        self.time_to_change_tree = 3
        self.time_to_get = 3


class FireHose:
    def __init__(self):
        self.liter_per_second = 2
        self.time_to_change_tree = 5
        self.time_to_get = 5