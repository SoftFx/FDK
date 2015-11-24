.DATA


.CODE
	Decimal8Multiply PROC
	mov		rax, rdx
	imul	rcx
	ret
	Decimal8Multiply ENDP
END