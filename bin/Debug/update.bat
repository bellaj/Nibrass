cd "/d %~dp0"
taskkill /F /IM Nibrass.exe /T
del Nibrass.exe /F
rename Nibrass_new.exe Nibrass.exe 
del WPFTaskbarNotifier.dll /F
rename Nibrass_new.dll WPFTaskbarNotifier.dll 
