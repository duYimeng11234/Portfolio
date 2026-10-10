import pygame
import sys
pygame.init()
##Defined variables
SCREEN = pygame.display.set_mode((750, 750))
pygame.display.set_caption("Connections")
words = open("words.txt").read().split("\n")  #reading in the words list, default 16 words is put in there but full freedom is given. Just change the word.txt and read the file in
notClickedRectangles = []
clickedRectangles = []
rx = 20
ry = 20
font = pygame.font.Font('freesansbold.ttf', 25)

for i in range(4):  #Drawing the 16 rectangles, all rectangles should start in notClickedRectangles
    for f in range(4):
        notClickedRectangles.append(pygame.Rect(rx, ry, 150, 150))
        rx += 175
    ry += 175
    rx = 20

def mainScreen():
    while True:
        SCREEN.fill("white")
        MENU_MOUSE_POS = pygame.mouse.get_pos() #getting the position of the mouse
        for event in pygame.event.get():   #checking for events such as a click
            if event.type == pygame.QUIT:  #Checking if user pressed exit (red circle in top left)
                pygame.quit()
                sys.exit()
            if event.type == pygame.MOUSEBUTTONDOWN: #When mouse button is pressed checking for which rectangle is clicked
                for i in range(len(notClickedRectangles)):
                    if notClickedRectangles[i].collidepoint(MENU_MOUSE_POS): #iterating through seeing if one of the rectangles was clicked
                        clickedRectangles.append(notClickedRectangles[i])  #if rectangle is clicked moving it to clickedRectangle list
                        notClickedRectangles.pop(i)  #removing rectangle from notClickedRectangle list
                        break

        #actually drawing the rectangle
        for rectangles in notClickedRectangles:
            pygame.draw.rect(SCREEN, "grey", rectangles) #if not clicked then grey
        if len(clickedRectangles) >= 1:                     #NOTE: if you want it to draw different colors just change the value
            for rectangles in clickedRectangles:
                pygame.draw.rect(SCREEN, "black", rectangles) #if clicked draw black

        #drawing the text from the wordlist
        index = 0
        tx = 95
        ty = 95
        for i in range(4):
            for f in range(4):
                text = font.render(words[index], True, "white")
                textRect = text.get_rect()      #poor attempt to make the words be drawn in the middle, I'm sure there's much better ways to do this lol
                textRect.center = (tx, ty)
                SCREEN.blit(text, textRect)
                tx += 170
                index += 1
            ty += 170
            tx = 95
        pygame.display.update()

mainScreen()