set mydate=%date:~10,4%%date:~7,2%%date:~4,2%
for /d %%a in (CHOP-fMRU_Assistant\bin\x64\Release) do ("C:\Program Files\7-zip\7z.exe" a "CHOP-fMRU_Assistant_Release.zip" "%%a\*")