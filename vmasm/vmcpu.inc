; Base Framebuffer Address
#define FB #hB000

; I/O Pins
#define P1 @h9000 ;9000h Type 1:Out 0:In | 9001H data Byte
#define P2 @h9002
#define P3 @h9004
#define P4 @h9006
#define P5 @h9008
#define P6 @h900B
#define P7 @h900D
#define P8 @h900F

#define P11 @h9010 
#define P12 @h9012
#define P13 @h9014
#define P14 @h9016
#define P15 @h9018
#define P16 @h901B
#define P17 @h901D
#define P18 @h901F

#define CPU0 #hC001
#define CPU1 #hC002
#define CPU2 #hC003
#define CPU3 #hC004

; ORG 0x4 Interrupt
ORG 0x4 
RET

ORG 0x100
InitFramebuffer:
PUSH #6  ; MODE
PUSH #hFF000000; Color 
FBI ; Create FrameBuffer and Write Info To Ram
RET
DestroyFramebuffer:
RET
; Basic IP for other Cores
ORG 0x7
JMP $

; Start Address is 0x10
ORG 0x10