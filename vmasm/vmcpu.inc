; Base Framebuffer Address
#define FB #B000h

; I/O Pins
#define P1 #A000h ;#A000h Type 1:Out 0:In | #A001H Status (0: NoSignal; 1:Signal)
#define P2 #A002h
#define P3 #A004h
#define P4 #A006h
#define P5 #A008h
#define P7 #A00Bh
#define P8 #A00Dh

#define P11 #A010h 
#define P12 #A012h
#define P13 #A014h
#define P14 #A016h
#define P15 #A018h
#define P17 #A01Bh
#define P18 #A01Dh

; ORG 0x4 Interrupt
ORG 0x4 
RET
; Start Address is 0x10
ORG 0x10