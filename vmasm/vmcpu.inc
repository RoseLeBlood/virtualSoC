; Base Framebuffer Address
#define FB #hB000

; I/O Pins
#define P1 @h9000 ;9000h Type 1:Out 0:In | 9001H data Byte
#define P2 @h9002
#define P3 @h9004
#define P4 @h9006
#define P5 @h9008
#define P7 @h900B
#define P8 @h900D

#define P11 @h9010h 
#define P12 @h9012h
#define P13 @h9014h
#define P14 @h9016h
#define P15 @h9018h
#define P17 @h901Bh
#define P18 @h901Dh

; ORG 0x4 Interrupt
ORG 0x4 
RET

ORG 0x100
InitFramebuffer:
PUSH #6  ; MODE
PUSH #hFF555555 ; Color 
FBI ; Create FrameBuffer and Write Info To Ram
RET
DestroyFramebuffer:
RET

; Start Address is 0x10
ORG 0x10