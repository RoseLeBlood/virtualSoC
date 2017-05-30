; Base Framebuffer Address
#define FB #hB000

; I/O Pins
#define P1STR @h9000 ;Type 1:Out 0:In 
#define P1CMD @h9010 ;Adress data

#define P2STR @h9001 ;Type 1:Out 0:In 
#define P2CMD @h9011 ;Adress data

#define P3STR @h9002 ;Type 1:Out 0:In 
#define P4CMD @h9012 ;Adress data

#define P5STR @h9003 ;Type 1:Out 0:In 
#define P5CMD @h9013 ;Adress data

#define P6STR @h9004 ;Type 1:Out 0:In 
#define P6CMD @h9014 ;Adress data

#define P7STR @h9005 ;Type 1:Out 0:In 
#define P7CMD @h9015 ;Adress data

#define P8STR @h9006 ;Type 1:Out 0:In 
#define P8CMD @h9016 ;Adress data

#define P9STR @h9007 ;Type 1:Out 0:In 
#define P9CMD @h9017 ;Adress data

#define P10STR @h9006 ;Type 1:Out 0:In 
#define P10CMD @h9016 ;Adress data

#define P11STR @h9007 ;Type 1:Out 0:In 
#define P11CMD @h9017 ;Adress data

#define CPU0 #hC001
#define CPU1 #hC002
#define CPU2 #hC003
#define CPU3 #hC004

; ORG 0x4 Interrupt
ORG h4 
RET

ORG h100
InitFramebuffer:
PUSH #8  ; MODE
PUSH #hFF000000; Color 
FBI ; Create FrameBuffer and Write Info To Ram
RET
DestroyFramebuffer:
RET
; Basic IP for other Cores
ORG h7
HLT
JMP $

; Start Address is 0x10
ORG h10