!addplugindir "Plugins"

!ifndef VERSION
  !define VERSION "1.0.0"
!endif

;--------------------------------
;Includes
!include "MUI2.nsh"
!include "nsDialogs.nsh"
!include "LogicLib.nsh"
!include "FileFunc.nsh"
!include "TextFunc.nsh"
!include "WordFunc.nsh"
!include "Plugins\ExtendedFunc.nsh"
!include "Plugins\DotNetChecker.nsh"
;!include "Plugins\x64.nsh"
;!include "Plugins\ToReplace.nsh"
;!include "Resources.en.nsi"
;!include "InstallOptions.nsh"

;--------------------------------
;Basic definitions
!define PRODUCT_NAME "FDK"
!define PROD_NAME_VER "${PRODUCT_NAME} ${VERSION}"
!define SETUP_FILENAME_FDK "${PROD_NAME_VER} Setup.exe"
!define PUBLISHER "SoftFX"

!define DOCUMENTATION "..\..\Documentation"
!define FRE "..\FRE"
!define EXAMPLES "..\Examples"
!define TOOL "..\FRE"

!define DotNet46 "dotNet46Setup.exe"

;--------------------------------
;Global Variables
Var Dialog
Var DialogUn
Var Label1
Var Label2
Var Label3
Var ButtonChange
Var ButtonRepair
Var ButtonRemove
Var NextButton
Var Removed
Var UnCheckBox
Var CheckState
Var counter
Var cbxDoc
Var cbxFRE
Var cbxExamples
Var cbxTools
Var cbxDocState
Var cbxFREState
Var cbxExamplesState
Var cbxToolsState
Var flag

;--------------------------------
;General
SetCompressor lzma
Name "${PROD_NAME_VER}"
Caption "Install ${PROD_NAME_VER}"
OutFile "${SETUP_FILENAME_FDK}"
InstallDir "$PROGRAMFILES\${PUBLISHER}\${PROD_NAME_VER}"
ShowInstDetails hide
ShowUninstDetails hide
LicenseForceSelection radiobuttons "I accept the agreement" "I do not accept the agreement"
BrandingText " "

;--------------------------------
;Interface Settings
!define MUI_ABORTWARNING
!define MUI_ICON "Resources\_SoftFX.ico"
!define MUI_UNICON "Resources\win-uninstall.ico"
;!define MUI_HEADERIMAGE
!define MUI_WELCOMEFINISHPAGE_BITMAP "Resources\dlgbmp3.bmp"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "Resources\dlgbmp3.bmp"
;!define MUI_HEADERIMAGE_BITMAP "Resources\bann24.bmp"
;!define MUI_HEADERIMAGE_UNBITMAP "Resources\bann24.bmp"
;!define MUI_HEADERIMAGE_BITMAP_NOSTRETCH
;!define MUI_UI_HEADERIMAGE "Resources\modern_headerbmpf.exe"
;!define MUI_PAGE_HEADER_TEXT ""
;!define MUI_PAGE_HEADER_SUBTEXT ""
;!define MUI_CUSTOMFUNCTION_GUIINIT onGUIInit
;!define MUI_HEADER_TRANSPARENT_TEXT
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_UNFINISHPAGE_NOAUTOCLOSE

;--------------------------------
;Installation types
InstType "Full"

;--------------------------------
;Pages
!insertmacro MUI_PAGE_WELCOME
Page custom StartUserDialog EndUserDialog
Page custom UnPrevStart UnPrevEnd
Page custom ChangeDialogStart ChangeDialogStop
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
;
!insertmacro MUI_UNPAGE_WELCOME
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Plugins

;--------------------------------
;Languages
!insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Service control macro

!macro Documents

	SetOutPath  "$INSTDIR"
	File /r /x *.pdb /x *.map "${DOCUMENTATION}"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Docs" "1"
!macroend

!macro FRE
    
	SetOutPath  "$INSTDIR\FRE\"
	File "${FRE}\SoftFX.Extended.Net.dll"
	File "${FRE}\SoftFX.Extended.Net.xml"
	File "${FRE}\SoftFX.Net.dll"
	File "${FRE}\SoftFX.Net.xml"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "FRE" "1"
!macroend

!macro Examples
    
	SetOutPath  "$INSTDIR"
	File /r /x *.pdb /x *.map /x Debug /x Release "${EXAMPLES}"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Sample" "1"
!macroend

!macro Tools

	SetOutPath  "$INSTDIR\FRE\"
	File "${TOOL}\QuotesDownloader.exe"
	File "${TOOL}\QuotesDownloader.exe.config"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Tools" "1"
!macroend

!macro RegisterIt
	
	SetOutPath  "$INSTDIR"
	File "Resources\_SoftFX.ico"
	
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "DisplayName" "${PROD_NAME_VER}"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "UninstallString" "$INSTDIR\Uninstall ${PROD_NAME_VER}.exe"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "DisplayIcon" "$INSTDIR\_SoftFX.ico"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Publisher" "${PUBLISHER}"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "DisplayVersion" "${VERSION}"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "InstDir" "$INSTDIR"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Install" "1"
	
	ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "Quant"
	;MessageBox MB_OK "$0"
	${If} $0 == ""
		WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "Quant" "1"
		StrCpy $0 "1"
		WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "NinLine" "1"
	${Else}
		IntOp $0 $0 + 1
		WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "Quant" "$0"
		WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "NinLine" "$0"
	${EndIf}
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "$0" "$INSTDIR\Uninstall ${PROD_NAME_VER}.exe"
	;MessageBox MB_OK "$0"
	
	Sleep 1500
	;MessageBox MB_OK "Regit"
!macroend

!macro UnregIt
	
	ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "NinLine"
	DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}"
	ReadRegStr $1 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "Quant"
	DeleteRegValue HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK\" "$0"
	${If} $1 == $0
		IntOp $1 $1 - 1
	${EndIf}
	
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "Quant" "$1"
!macroend

!macro Shortcut
	
	SetShellVarContext current
	
	${If} ${FileExists} "$INSTDIR\Examples\*.*"
		CreateShortCut  "$SMPROGRAMS\${PUBLISHER}\${PROD_NAME_VER}\Examples.lnk" "$INSTDIR\Examples"
	${EndIf}
	
	${If} ${FileExists} "$INSTDIR\Documentation\*.*"
		CreateShortCut  "$SMPROGRAMS\${PUBLISHER}\${PROD_NAME_VER}\Documentation.lnk" "$INSTDIR\Documentation"
	${EndIf}
	
	${If} ${FileExists} "$INSTDIR\FRE\SoftFX.Net.dll"
		CreateShortCut  "$SMPROGRAMS\${PUBLISHER}\${PROD_NAME_VER}\FRE.lnk" "$INSTDIR\FRE"
	${EndIf}
	
	${If} ${FileExists} "$INSTDIR\FRE\QuotesDownloader.exe"
		CreateShortCut  "$SMPROGRAMS\${PUBLISHER}\${PROD_NAME_VER}\Tools\QuotesDownloader.lnk" "$INSTDIR\FRE\QuotesDownloader.exe"
	${EndIf}
	
    CreateShortCut  "$SMPROGRAMS\${PUBLISHER}\${PROD_NAME_VER}\${PROD_NAME_VER} Uninstall.lnk" "$INSTDIR\Uninstall ${PROD_NAME_VER}.exe"
	
	Sleep 1500
!macroend

!macro DeleteShortcut

	SetShellVarContext all
	RMDir /r "$SMPROGRAMS\${PUBLISHER}\${PROD_NAME_VER}"
!macroend

!macro Uninstall
	
	RMDir /r "$INSTDIR"
	!insertmacro UnregIt
	!insertmacro DeleteShortcut
!macroend

Function StartUserDialog
	
	${If} $flag == "3"
		StrCpy $R9 "-1"
		Call RelGotoPage
		StrCpy $flag ""
		Goto StartUserDialogEnd
	${EndIf}
	
	${If} $flag == "2"
		StrCpy $R9 "1"
		Call RelGotoPage
		Goto StartUserDialogEnd
	${EndIf}
	
	;MessageBox MB_OK "StartUserDialog"
	
	!insertmacro MUI_HEADER_TEXT "Change, repair, or remove installation" "Select the operation you wish to perform"
	ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Install"
	GetDlgItem $NextButton $HWNDPARENT 1 ; next=1, cancel=2, back=315
	EnableWindow $NextButton 0
	
	${If} $0 == 1
		nsDialogs::Create 1018
		Pop $Dialog
		${If} $Dialog == error
			Abort
		${EndIf}
    		
		${NSD_CreateButton} 0 5u 20% 13u "Change"
		Pop $ButtonChange
		${NSD_OnClick} $ButtonChange OnChangeClick
		
		${NSD_CreateLabel} 30 18u 100% 10u "Lets you change the way features are installed."
		Pop $Label1

		${NSD_CreateButton} 0 40u 20% 13u "Repair"
		Pop $ButtonRepair
		${NSD_OnClick} $ButtonRepair OnRepairClick
		
		${NSD_CreateLabel} 30 53u 100% 20u "Repairs errors in the most recent installation by fixing missing and corrupt files, shortcuts, and registry entries."
		Pop $Label2
		
		${NSD_CreateButton} 0 85u 20% 13u "Remove"
		Pop $ButtonRemove
		${NSD_OnClick} $ButtonRemove OnRemoveClick
		
		${NSD_CreateLabel} 30 98u 100% 10u "Removes ${PROD_NAME_VER} from your computer."
		Pop $Label3
		
		nsDialogs::Show
	${Else}
		StrCpy $R9 "1"
		Call RelGotoPage
	${EndIf}
	StartUserDialogEnd:
FunctionEnd

Function EndUserDialog
	${If} $Removed == "1"
		Quit
	${Else}
	${EndIf}
FunctionEnd

Function OnChangeClick

    MessageBox MB_YESNO "Do you really want to change ${PROD_NAME_VER} components" IDYES change IDNO nochange
	change:
	    EnableWindow $NextButton 1
		StrCpy $R9 "2"
		Call RelGotoPage
	nochange:
FunctionEnd

Function OnRepairClick

	MessageBox MB_YESNO "Do you really want to repair ${PROD_NAME_VER}" IDYES repair IDNO norepair
	
	repair:
	RMDir /r "$INSTDIR"
	CreateDirectory "$INSTDIR"
	SetOutPath "$INSTDIR"
	
	ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Sample"
	${If} $0 == "1"
		!insertmacro Examples
	${EndIf}
	
	ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Docs"
	${If} $0 == "1"
		!insertmacro Documents
	${EndIf}
	
	ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "FRE"
	${If} $0 == "1"
		!insertmacro FRE
	${EndIf}
	
	ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Tools"
	${If} $0 == "1"
		!insertmacro Tools
	${EndIf}
	
	WriteUninstaller "$INSTDIR\Uninstall ${PROD_NAME_VER}.exe"
	
	MessageBox MB_OK|MB_ICONINFORMATION "${PROD_NAME_VER} has been successfully repaired."
	EnableWindow $NextButton 1
	StrCpy $Removed "1"
	StrCpy $R9 "1"
	Call RelGotoPage
	Quit
	
	norepair:
FunctionEnd

Function OnRemoveClick
	
	MessageBox MB_YESNO "Do you really want to remove ${PROD_NAME_VER}" IDYES remove IDNO noremove
	remove:
		ReadRegStr $1 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "UninstallString"
		ExecWait '"$1" /S' $2
		
		;ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Install"
		
		;${If} $0 != 1
		;	Goto noremove
		;${EndIf}
		
		${If} $2 == 0
			MessageBox MB_OK|MB_ICONINFORMATION "${PROD_NAME_VER} has been successfully uninstalled from your computer."
		${Else}
			MessageBox MB_OK|MB_ICONEXCLAMATION "Error while ${PROD_NAME_VER} uninstalling."
		${EndIf}
		
		EnableWindow $NextButton 1
		StrCpy $Removed "1"
		StrCpy $R9 "1"
		Call RelGotoPage
		Quit
	noremove:
FunctionEnd

Function UnPrevStart
	
	;MessageBox MB_OK "UnPrevStart"
	;========================
	${If} $flag == "6"
		StrCpy $R9 "-1"
		Call RelGotoPage
		Goto UnPrevStartEnd
	${EndIf}
	;=========================
	
	!insertmacro MUI_HEADER_TEXT "Uninstall previously installed version" "Select the operation you wish to perform"
	ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "Quant"
	${If} $0 == "0"
	${OrIf} $0 == ""
		StrCpy $R9 "2"
		Call RelGotoPage
		StrCpy $flag "0"
	${Else}
		StrCpy $flag "3"
		nsDialogs::Create 1018
		Pop $DialogUn
		${If} $DialogUn == error
			Abort
		${EndIf}
		
		${NSD_CreateCheckBox} 0 5u 100% 13u "Uninstall previously installed versions"
		Pop $UnCheckBox
		${NSD_SetState} $UnCheckBox ${BST_CHECKED}
		
		nsDialogs::Show
	${EndIf}
	
	UnPrevStartEnd:
FunctionEnd

Function UnPrevEnd
	
	${NSD_GetState} $UnCheckBox $CheckState
	${If} $CheckState == "1"
		;StrCpy $flag "2"
		ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "Quant"
		${For} $counter 1 $0
			
			ReadRegStr $1 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "$counter"
			Push "$1"
			Call GetParent
			Pop $R0
			
			ExecWait '"$1" /S _?=$R0'
			Delete "$R0\*.*"
		${Next}
		WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\FDK" "Quant" "0"
	${Else}
		StrCpy $flag "2"
	${EndIf}
	
	StrCpy $R9 "2"
	Call RelGotoPage
FunctionEnd

Function ChangeDialogStart

	;MessageBox MB_OK "flag = $flag"

	;==================
	${If} $flag == "0"
	;${OrIf} $flag == "3"
		StrCpy $R9 "-3"
		Call RelGotoPage
		Goto EndChangeDialogStart
	${EndIf}
	
	${If} $flag == "3"
	${OrIf} $flag == "2"
		StrCpy $R9 "-2"
		Call RelGotoPage
		Goto EndChangeDialogStart
	${EndIf}
	
	;${If} $flag == "2"
	;	StrCpy $R9 "-2"
	;	Call RelGotoPage
	;	Goto EndChangeDialogStart
	;${EndIf}
	;==================

	;==================
	StrCpy $flag "6"
	;==================

	!insertmacro MUI_HEADER_TEXT "Change installation" "Select the components"
	
	nsDialogs::Create 1018
		Pop $Dialog
		${If} $Dialog == error
			Abort
		${EndIf}
		
		${NSD_CreateCheckBox} 0 5u 100% 13u "FRE"
		Pop $cbxFRE
		${If} ${FileExists} "$INSTDIR\FRE\SoftFX.Net.dll"
			${NSD_SetState} $cbxFRE ${BST_CHECKED}
			EnableWindow $cbxFRE 0
		${EndIf}
		
		${NSD_CreateCheckBox} 0 18u 100% 13u "Examples"
		Pop $cbxExamples
		${If} ${FileExists} "$INSTDIR\Examples\*.*"
			${NSD_SetState} $cbxExamples ${BST_CHECKED}
		${EndIf}

		${NSD_CreateCheckBox} 0 31u 100% 13u "Documentation"
		Pop $cbxDoc
		${If} ${FileExists} "$INSTDIR\Documentation\*.*"
			${NSD_SetState} $cbxDoc ${BST_CHECKED}
		${EndIf}
		
		${NSD_CreateCheckBox} 0 44u 100% 13u "Tools"
		Pop $cbxTools
		${If} ${FileExists} "$INSTDIR\FRE\QuotesDownloader.exe"
			${NSD_SetState} $cbxTools ${BST_CHECKED}
		${EndIf}
		
	nsDialogs::Show
	
	EndChangeDialogStart:
FunctionEnd

Function ChangeDialogStop
	
	${NSD_GetState} $cbxExamples $cbxExamplesState
	${NSD_GetState} $cbxFRE $cbxFREState
	${NSD_GetState} $cbxDoc $cbxDocState
	${NSD_GetState} $cbxTools $cbxToolsState
	
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "FRE" "0"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Docs" "0"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Sample" "0"
	WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PROD_NAME_VER}" "Tools" "0"
	
	RMDir /r "$INSTDIR\Examples"
	RMDir /r "$INSTDIR\FRE"
	RMDir /r "$INSTDIR\Documentation"
	
	!insertmacro DeleteShortcut
	
	SetOutPath "$INSTDIR\"
	
	${If} $cbxExamplesState == "1"
		!insertmacro Examples
	${EndIf}
	
	${If} $cbxFREState == "1"
		!insertmacro FRE
	${EndIf}
		
	${If} $cbxDocState == "1"
		!insertmacro Documents
	${EndIf}
	
	${If} $cbxToolsState == "1"
		!insertmacro Tools
	${EndIf}
	
	!insertmacro Shortcut
	
	MessageBox MB_OK "Components of ${PROD_NAME_VER} have been successfully changed."
	Quit
FunctionEnd

Function RelGotoPage

  IntCmp $R9 0 0 Move Move
    StrCmp $R9 "X" 0 Move
      StrCpy $R9 "120"
 
  Move:
  SendMessage $HWNDPARENT "0x408" "$R9" ""
FunctionEnd

;Function un.onInit
;	
;	ClearErrors
;	
;	Rename "$INSTDIR" "$INSTDIR666"
;	
;	${If} ${Errors}
;		MessageBox MB_OK "Files you are trying to uninstall are in use."
;		Rename "$INSTDIR666" "$INSTDIR"
;		Quit
;	${Else}
;		Rename "$INSTDIR666" "$INSTDIR"
;	${EndIf}
;FunctionEnd

Function InternetConnection
    Dialer::GetConnectedState

    Pop $R3
    StrCmp $R3 "online" connected
    MessageBox MB_OK|MB_ICONSTOP "Cannot connect to the internet.Please connect to the internet now."
    Quit

    connected:
FunctionEnd

Section "FRE" SecFre
SectionIn RO
	!insertmacro CheckNetFramework 46
	!insertmacro FRE
SectionEnd

Section "Documentation" SecDoc
SectionIn 1

	!insertmacro Documents
SectionEnd

Section "Examples" SecExampl
SectionIn 1

	!insertmacro Examples
SectionEnd


Section "Tools" SecTools
SectionIn 1

	!insertmacro Tools
SectionEnd

Section -Post
SectionIn 1 2

	WriteUninstaller "$INSTDIR\Uninstall ${PROD_NAME_VER}.exe"
	!insertmacro RegisterIt
	!insertmacro Shortcut
	Sleep 1500
SectionEnd

Section Uninstall SecUn
	
	!insertmacro Uninstall
	RMDir "$INSTDIR"
SectionEnd


LangString DESC_Docs ${LANG_ENGLISH} "Install Documentation"
LangString DESC_Samlp ${LANG_ENGLISH} "Install Examples"
LangString DESC_FRE ${LANG_ENGLISH} "Install FRE"
LangString DESC_TOOLS ${LANG_ENGLISH} "Install Tools"

LangString UN_MP ${LANG_ENGLISH} "Delete ${PROD_NAME_VER}"

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${SecDoc} $(DESC_Docs)
	!insertmacro MUI_DESCRIPTION_TEXT ${SecFre} $(DESC_FRE)
	!insertmacro MUI_DESCRIPTION_TEXT ${SecExampl} $(DESC_Samlp)
	!insertmacro MUI_DESCRIPTION_TEXT ${SecTools} $(DESC_TOOLS)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

!insertmacro MUI_UNFUNCTION_DESCRIPTION_BEGIN
    !insertmacro MUI_DESCRIPTION_TEXT ${secUn} $(UN_MP)
!insertmacro MUI_UNFUNCTION_DESCRIPTION_END
