set mydate=%date:~10,4%%date:~7,2%%date:~4,2%
set zipname="CHOP-fMRU_Assistant_Release.zip"

del %zipname%
cd CHOP-fMRU_Assistant\bin\x64\Release
for /d %%a in (.) do ("C:\Program Files\7-zip\7z.exe" a %zipname% "%%a\*")
move %zipname% "..\..\..\.."
cd "..\..\..\.."