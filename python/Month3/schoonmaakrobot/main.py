import os
os.system('cls')

MAP = [(1,6),(3,6),(3,3),(5,3),(5,2),(4,2),(4,1),(1,1),(1,3),(2,3),(2,5),(1,5),(1,6)]

for corner in MAP:
    x = corner[0]
    y = corner[1]
    print(f"{x}, {y}")