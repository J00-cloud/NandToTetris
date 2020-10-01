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
