import pygame
import os

#constants
CELLSIZE = 32
ROW = 15
COL = 15
WIDTH = CELLSIZE * ROW
HEIGHT = CELLSIZE * COL
FPS = 60
FILLCOLOR = (40, 40, 40)
NUM_MINES = 1
TITLE = "Minesweeper"

#image imports
cell_numbers = []
for i in range(1, 9):
    cell_numbers.append(pygame.transform.scale(pygame.image.load(os.path.join("images", f"{i}.png")), (CELLSIZE, CELLSIZE)))
empty_cell = pygame.transform.scale(pygame.image.load(os.path.join("images", f"0.png")), (CELLSIZE, CELLSIZE))
bomb = pygame.transform.scale(pygame.image.load(os.path.join("images", f"9.png")), (CELLSIZE, CELLSIZE))
flag = pygame.transform.scale(pygame.image.load(os.path.join("images", f"11.png")), (CELLSIZE, CELLSIZE))
unknown_cell = pygame.transform.scale(pygame.image.load(os.path.join("images", f"10.png")), (CELLSIZE, CELLSIZE))