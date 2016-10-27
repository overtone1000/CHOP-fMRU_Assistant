set mydate=%date:~10,4%%date:~7,2%%date:~4,2%
set zipname="CHOP-fMRU_Assistant_Release.zip"

del %zipname%

cd CHOP-fMRU_Assistant\bin\x64\

mkdir "temp\CHOP-fMRU_Assistant"
mkdir "temp\CHOP-fMRU_Assistant\Program Files"
xcopy Release\*.* "temp\CHOP-fMRU_Assistant\Program Files" /E /Y /C /B

cd temp

"C:\Program Files\7-zip\7z.exe" a %zipname% *.* -r

move %zipname% "..\..\..\.."

cd ..
REM del temp\*.* /F /Q
rmdir temp /S /Q

cd "..\..\.."