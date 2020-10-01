	@R0
	D=M
	@additive
	M=D

	@R1
	D=M
	@maxIterations
	M=D

	@addition
	M=0
	
	@R2
	M=0
	
	@i
	M=0

(LOOP)
	@maxIterations
	D=M
	@i
	D=D-M
	@END
	D;JEQ
	
	@additive
	D=M
	@addition
	M=M+D
	D=M
	
	@R2
	M=D
	
	@i
	M=M+1
	
	@LOOP
	0;JMP

(END)
	
	@i
	M=0
	@END
	0;JMP
