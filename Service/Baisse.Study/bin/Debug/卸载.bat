taskkill /f /im Trasen.Bas.PublicService.exe

net stop start Bas-PublicService

sc delete start Bas-PublicService

@echo off
echo hello!  service run total =29 ,err=
pause
