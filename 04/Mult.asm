// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Mult.asm

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)

// Put your code here.

	@i
	M=1

	@R0
	D=M
	@addition
	M=D
	@additive
	M=D
	
	@R2
	M=0

	@R1
	D=M
	@maxIterations
	M=D
	
	@R1
	D=M
	@END
	D;JEQ

	@R0
	D=M
	@END
	D;JEQ

(LOOP)
	@maxIterations
	D=M	
	@i
	D=M-D
	@END
	D;JEQ
	
	@additive
	D=M
	@addition
	M=M+D
	
	@i
	D=M
	M=D+1
	
	@addition
	D=M
	@R2
	M=D
	
	@LOOP
	0;JMP

(END)
	@END
	0;JMP