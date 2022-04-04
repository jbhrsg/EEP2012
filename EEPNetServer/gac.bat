rem Change to C:
rem Change to "C:\Program Files (x86)\Infolight\EEP2015\EEPNetServer"
cd "C:\Program Files (x86)\Infolight\EEP2015\EEPNetServer"

gacutil /u InfoRemoteModule
gacutil /i InfoRemoteModule.dll
gacutil /u Srvtools
gacutil /i Srvtools.dll

rem See the Result
pause