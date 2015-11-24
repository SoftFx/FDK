@echo off


if exist Documentation (rd /Q /S Documentation)
if not exist Documentation (md Documentation)
doxygen -b Doxyfile
hhc .\Documentation\html\index.hhp
if exist ..\..\Documentation\SoftFX.Extended.Net.chm (del ..\..\Documentation\SoftFX.Extended.Net.chm)
if not exist ..\..\Documentation\ (md ..\..\Documentation\)
copy .\Documentation\html\index.chm ..\..\Documentation\SoftFX.Extended.Net.chm
if exist Documentation (rd /Q /S Documentation)

:EXIT
if not "%1" == "/silent" pause