Function GetParent
 
  Exch $R0
  Push $R1
  Push $R2
  Push $R3
 
  StrCpy $R1 0
  StrLen $R2 $R0
 
  loop:
    IntOp $R1 $R1 + 1
    IntCmp $R1 $R2 get 0 get
    StrCpy $R3 $R0 1 -$R1
    StrCmp $R3 "\" get
  Goto loop
 
  get:
    StrCpy $R0 $R0 -$R1
 
    Pop $R3
    Pop $R2
    Pop $R1
    Exch $R0
 
FunctionEnd

;Function onGUIInit
;
;	GetDlgItem $0 $HWNDPARENT 1037
;	SetCtlColors $0 0xFFFF00 transparent
;	GetDlgItem $0 $HWNDPARENT 1038
;	SetCtlColors $0 0xFFFF00 transparent
;
;FunctionEnd