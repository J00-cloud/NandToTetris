(Sys.init)
// Stop initialisation
// C_PUSH constant 4000
@4000
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP pointer 0
@THIS
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
// C_PUSH constant 5000
@5000
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP pointer 1
@THAT
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
// C_GOTO Sys.main 
@Sys.main
0;JMP
(Return_Address1)
// C_POP temp 1
@5
D=A
@1
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
// C_LABEL LOOP 
(LOOP)
// C_GOTO LOOP 
@LOOP
0;JMP
(Sys.main)
// C_PUSH constant 0
@0
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
// C_PUSH constant 0
@0
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
// C_PUSH constant 0
@0
D=A
@SP
A=M
M=D
@SP
M=M+1
// Stop initialisation
// C_PUSH constant 4001
@4001
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP pointer 0
@THIS
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
// C_PUSH constant 5001
@5001
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP pointer 1
@THAT
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
// C_PUSH constant 200
@200
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP local 1
@LCL
D=M
@1
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
// C_PUSH constant 40
@40
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP local 2
@LCL
D=M
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
// C_PUSH constant 6
@6
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP local 3
@LCL
D=M
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
// C_PUSH constant 123
@123
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
// C_GOTO Sys.add12 
@Sys.add12
0;JMP
(Return_Address2)
// C_POP temp 0
@5
D=A
@0
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
// C_PUSH local 0
@LCL
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
// C_PUSH local 1
@LCL
D=M
@1
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH local 2
@LCL
D=M
@2
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH local 3
@LCL
D=M
@3
D=D+A
A=D
D=M
@SP
A=M
M=D
@SP
M=M+1
// C_PUSH local 4
@LCL
D=M
@4
D=D+A
A=D
D=M
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
(Sys.add12)
// Stop initialisation
// C_PUSH constant 4002
@4002
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP pointer 0
@THIS
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
// C_PUSH constant 5002
@5002
D=A
@SP
A=M
M=D
@SP
M=M+1
// C_POP pointer 1
@THAT
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
// C_PUSH constant 12
@12
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
