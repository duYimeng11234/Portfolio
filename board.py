import pygame
from control import *
from cell import *

class Board:
    def __init__(self):
        self.board_surface = pygame.Surface((HEIGHT, WIDTH))
        self.board_list = [[Cell(col, row, empty_cell, "E") for row in range(ROW)] for col in range(COL)]
        self.place_mines()
        self.dug = []

    # uh oh, you're gonna need to write this one by yourself...
    def place_clues(self):
        pass


    #change this to manipulate bomb generation
    def place_mines(self):
        x = 0
        y = 0
        self.board_list[x][y].image = bomb
        self.board_list[x][y].type = "B"

    def draw(self, screen):
        for r in self.board_list:
            for c in r:
                c.draw(self.board_surface)
        screen.blit(self.board_surface, (0, 0))

    def dig(self, x, y):
        self.dug.append((x, y))
        if self.board_list[x][y].type == "B":
            self.board_list[x][y].revealed = True
            return False
        elif self.board_list[x][y].type == "N":
            self.board_list[x][y].revealed = True
            return True
        self.board_list[x][y].revealed = True

        #you know how when you click an empty space that isn't a number it'll reveal an area?
        #I recommend that you do it recursively if you choose to implement this function
        #I've already written out the True and False so the rest is up to you!

    def display_board(self):
        for r in self.board_list:
            print(r)
