#include 'vmcpu.inc'

CALL .InitFramebuffer; Init Framebuffer define in vmcpu.inc
MOV AX,#h34 ; Move 0x34 to AX
ADD #d148   ; Add 2d to AX = 200d

FBSET AX,AX,#h00FF00 ; SetPixel to (200)x(200) in green

HLT
