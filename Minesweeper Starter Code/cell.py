import pygame
from control import *
from board import *

#a bit of a hint for bomb and number generation
#using type you can manipulate which kind of cell it is
#types
#B - bomb
#N - number
#E - empty
#U - unknown

class Cell:
    def __init__(self, x, y, image, type, revealed=False, flagged=False):
        self.x, self.y = x * CELLSIZE, y * CELLSIZE
        self.image = image
        self.type = type
        self.revealed = revealed #whether the cell has been revealed
        self.flagged = flagged #if the cell has been flagged

    def __repr__(self):
        return self.type

    def draw(self, board_surface):
        if not self.flagged and self.revealed:
            board_surface.blit(self.image, (self.x, self.y))
        elif self.flagged and not self.revealed:
            board_surface.blit(flag, (self.x, self.y))
        elif not self.revealed:
            board_surface.blit(unknown_cell, (self.x, self.y))

