@ECHO OFF

pushd "%~dp0"

del build_errors.log /Q 2> nul
del build_output.log /Q 2> nul

SET CONFIG=Release

IF "%~1"=="" GOTO NO_PARAM
	SET CONFIG=%~1
:NO_PARAM

ECHO Restoring Packages
..\.nuget\NuGet.exe restore ./Hackaton.sln > build_restore_output.log

SET MSBUILDPATH=""
IF EXIST "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe" (
    SET MSBUILDPATH="C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"
) 

IF EXIST "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe" (
    SET MSBUILDPATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"
) 

IF %MSBUILDPATH%=="" (
	ECHO MSBuild.exe not found. Please verify the Visual Studio Instalation.
	pause
	exit
)

ECHO Starting %CONFIG% building process
%MSBUILDPATH% .\Hackaton.sln /t:Clean,Build /P:Configuration=%CONFIG% /val /m:4 /nologo /consoleloggerparameters:ErrorsOnly /flp1:logfile=build_errors.log;errorsonly /flp3:logfile=build_output.log;Verbosity:normal

SET BUILD_STATUS=%ERRORLEVEL%
if %BUILD_STATUS%==0 ECHO [32mBuild success[0m
if not %BUILD_STATUS%==0 ECHO [31mBuild failed, please check build_errors.log file[0m

popd
