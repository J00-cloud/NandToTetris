@256
D=A
@SP
M=D
A=D
// start function declaration
// C_PUSH Return_Address1 -1
@Return_Address1
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH argument -1
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH this -1
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH that -1
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH SP -1
@SP
D=M
@SP
A=M
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@LCL
M=D
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 5
@5
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 0
@0
D=A
@SP
A=M
M=D
@SP
M=M+1
// add
@SP
M=M-1
A=M
D=M
A=A-1
M=D+M
D=A
@SP
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
A=A-1
M=M-D
D=A
@SP
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@ARG
M=D
// C_GOTO Sys.init 
@Sys.init
0;JMP
(Return_Address1)
(Sys.init)
// Stop initialisation
// C_PUSH constant 4
@4
D=A
@SP
A=M
M=D
@SP
M=M+1
// start function declaration
// C_PUSH Return_Address2 -1
@Return_Address2
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH argument -1
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH this -1
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH that -1
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH SP -1
@SP
D=M
@SP
A=M
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@LCL
M=D
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 5
@5
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
// add
@SP
M=M-1
A=M
D=M
A=A-1
M=D+M
D=A
@SP
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
A=A-1
M=M-D
D=A
@SP
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@ARG
M=D
// C_GOTO Main.fibonacci 
@Main.fibonacci
0;JMP
(Return_Address2)
// C_LABEL WHILE 
(WHILE)
// C_GOTO WHILE 
@WHILE
0;JMP
(Main.fibonacci)
// Stop initialisation
// C_PUSH argument 0
@ARG
D=M
@0
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 2
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
// lt
@SP
M=M-1
A=M
D=M
A=A-1
D=M-D
@end.checkLt0
D;JLT
@SP
M=M-1
A=M
M=0
@end.endIf0
0;JEQ
(end.checkLt0)
@SP
M=M-1
A=M
M=-1
@end.endIf0
0;JEQ
(end.endIf0)
@SP
M=M+1
// C_IFGOTO IF_TRUE 
@SP
M=M-1
A=M
D=M
@IF_TRUE
D;JNE
// C_GOTO IF_FALSE 
@IF_FALSE
0;JMP
// C_LABEL IF_TRUE 
(IF_TRUE)
// C_PUSH argument 0
@ARG
D=M
@0
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// Start return
// C_POP temp 4
@5
D=A
@4
A=D+A
D=A
@SP
M=M-1
A=M
D=D+M
M=M-D
M=-M
D=D-M
A=M
M=D
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 4
@4
D=A
@SP
A=M
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
A=A-1
M=M-D
D=A
@SP
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@SP
M=D
// C_POP temp 3
@5
D=A
@3
A=D+A
D=A
@SP
M=M-1
A=M
D=D+M
M=M-D
M=-M
D=D-M
A=M
M=D
// C_PUSH argument -1
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_POP temp 2
@5
D=A
@2
A=D+A
D=A
@SP
M=M-1
A=M
D=D+M
M=M-D
M=-M
D=D-M
A=M
M=D
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@SP
M=D
@SP
M=M-1
A=M
D=M
@THAT
M=D
@SP
M=M-1
A=M
D=M
@THIS
M=D
@SP
M=M-1
A=M
D=M
@ARG
M=D
@SP
M=M-1
A=M
D=M
@LCL
M=D
// C_PUSH temp 2
@5
D=A
@2
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@SP
M=D
// C_PUSH temp 4
@5
D=A
@4
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
@8
A=M
0;JMP
// C_LABEL IF_FALSE 
(IF_FALSE)
// C_PUSH argument 0
@ARG
D=M
@0
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 2
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
A=A-1
M=M-D
D=A
@SP
M=D
@SP
M=M+1
// start function declaration
// C_PUSH Return_Address3 -1
@Return_Address3
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH argument -1
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH this -1
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH that -1
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH SP -1
@SP
D=M
@SP
A=M
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@LCL
M=D
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 5
@5
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
// add
@SP
M=M-1
A=M
D=M
A=A-1
M=D+M
D=A
@SP
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
A=A-1
M=M-D
D=A
@SP
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@ARG
M=D
// C_GOTO Main.fibonacci 
@Main.fibonacci
0;JMP
(Return_Address3)
// C_PUSH argument 0
@ARG
D=M
@0
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
A=A-1
M=M-D
D=A
@SP
M=D
@SP
M=M+1
// start function declaration
// C_PUSH Return_Address4 -1
@Return_Address4
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH argument -1
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH this -1
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH that -1
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH SP -1
@SP
D=M
@SP
A=M
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@LCL
M=D
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 5
@5
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
// add
@SP
M=M-1
A=M
D=M
A=A-1
M=D+M
D=A
@SP
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
A=A-1
M=M-D
D=A
@SP
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@ARG
M=D
// C_GOTO Main.fibonacci 
@Main.fibonacci
0;JMP
(Return_Address4)
// add
@SP
M=M-1
A=M
D=M
A=A-1
M=D+M
D=A
@SP
M=D
@SP
M=M+1
// Start return
// C_POP temp 4
@5
D=A
@4
A=D+A
D=A
@SP
M=M-1
A=M
D=D+M
M=M-D
M=-M
D=D-M
A=M
M=D
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH constant 4
@4
D=A
@SP
A=M
M=D
@SP
M=M+1
// sub
@SP
M=M-1
A=M
D=M
A=A-1
M=M-D
D=A
@SP
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@SP
M=D
// C_POP temp 3
@5
D=A
@3
A=D+A
D=A
@SP
M=M-1
A=M
D=D+M
M=M-D
M=-M
D=D-M
A=M
M=D
// C_PUSH argument -1
@ARG
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_POP temp 2
@5
D=A
@2
A=D+A
D=A
@SP
M=M-1
A=M
D=D+M
M=M-D
M=-M
D=D-M
A=M
M=D
// C_PUSH local -1
@LCL
D=M
@SP
A=M
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@SP
M=D
@SP
M=M-1
A=M
D=M
@THAT
M=D
@SP
M=M-1
A=M
D=M
@THIS
M=D
@SP
M=M-1
A=M
D=M
@ARG
M=D
@SP
M=M-1
A=M
D=M
@LCL
M=D
// C_PUSH temp 2
@5
D=A
@2
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
@SP
M=M-1
A=M
D=M
@SP
M=D
// C_PUSH temp 4
@5
D=A
@4
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
@8
A=M
0;JMP
