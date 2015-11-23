@echo off
rem building documentation for AuEmcaNet project

if exist Documentation (rd /Q /S Documentation)
if not exist Documentation (md Documentation)
doxygen -b Doxyfile
hhc .\Documentation\html\index.hhp
if exist "..\..\..\Documentation\SoftFX FIX protocol specification.chm" (del "..\..\..\Documentation\SoftFX FIX protocol specification.chm")
copy .\Documentation\html\index.chm "..\..\..\Documentation\SoftFX FIX protocol specification.chm"
if exist Documentation (rd /Q /S Documentation)

:EXIT
if not "%1" == "/silent" pause