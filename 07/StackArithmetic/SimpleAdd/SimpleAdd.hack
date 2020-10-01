// C_PUSH constant 7
@SP
A=M
M=7
@SP
M=M+1
// C_PUSH constant 8
@SP
A=M
M=8
@SP
M=M+1
// add
@SP
D=M
A=A-1
M=D+M
D=A
@SP
M=D
