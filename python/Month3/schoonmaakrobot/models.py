import math

class CleanedArea:

    def __init__(self, x1, y1, x2, y2):
        self.x1 = x1
        self.y1 = y1
        self.x2 = x2
        self.y2 = y2
        self.time = self.calc_time(x1, x2)

    
    def calc_time(self, x1, x2):
        return int(math.sqrt((x2 - x1 + 1)**2))

    # roep deze aan om makkelijk dit object te stringifyen naar de juiste string voor de uitendelijke flag in format Flag{((x,y,t), (x,y,t), (x,y,t)...etc)}
    # format voor de flag: (x,y,t)
    # Maar in ons geval is x,y niet de linkeronderhoek maar de linkerbovenhoek. Ik hebt het getest met het voorbeeld en het werkt gewoon ookal is de y as mirrored.
    # x2 en y2 samen is dan ofc de rechteronderhoek maar die is niet relevant voor de flag, wel voor de visualization :)
    def __str__(self):
        return f"({self.x1},{self.y1},{self.time})"


    def __eq__(self, compare_area: object) -> bool:
        if (self.x1 == compare_area.x1 and self.y1 == compare_area.y1):
            return True
        return False