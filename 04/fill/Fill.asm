// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input.
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel;
// the screen should remain fully black as long as the key is pressed. 
// When no key is pressed, the program clears the screen, i.e. writes
// "white" in every pixel;
// the screen should remain fully clear as long as no key is pressed.

// Put your code here.//16384

	@16384
	D=A
	@start
	M=D
	

	@24576
	D=A
	@endOfScreen
	M=D

(WAITFORKEY)
	@KBD
	D=M
	@BLACKSCREEN
	D;JGT
	@WAITFORKEY
	0;JMP

(WAITFORNULL)
	@KBD
	D=M
	@WHITESCREEN
	D;JEQ
	@WAITFORNULL
	0;JMP

(BLACKSCREEN)
	
	@start
	D=M
	@i
	M=D

	(FILOOP1)

		@endOfScreen
		D=M
		@i
		D=D-M
		@WAITFORNULL
		D;JEQ
		
		@i
		D=M
		A=D
		M=-1
		@i
		M=M+1

		@FILOOP1
		0;JMP

(WHITESCREEN)

	@start
	D=M
	@i
	M=D

	(FILOOP2)
		@endOfScreen
		D=M
		@i
		D=D-M
		@WAITFORKEY
		D;JEQ
	
		@i
		D=M
		A=D
		M=0
		@i
		M=M+1
		
		@FILOOP2
		0;JMP

	
