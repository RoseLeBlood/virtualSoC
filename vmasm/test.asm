﻿#include 'vmcpu.inc'

ORG 0x10

Main:
PUSH #5
PUSH #4
PUSH #3
POP #1
PEEK AX
PEEK BX
POP #3
POP #2
Test:
ADD #34d
MOV BX,AX
CLR AX
JMP .Test
END