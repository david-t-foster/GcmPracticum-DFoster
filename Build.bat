
@echo off

nuget restore
IF ERRORLEVEL 1 GOTO error
msbuild GcmPracticum.sln /property:Configuration=Release
IF ERRORLEVEL 1 GOTO error
msbuild GcmPracticum.Tests/TestRunner.csproj
IF ERRORLEVEL 1 GOTO error

echo running integration test

for /f "delims=" %%i in ('GcmPracticum\bin\release\GcmPracticum.exe "morning, 1, 2"') do @set RESULT=%%i
IF NOT "%RESULT%" == "eggs, toast" GOTO failedIntegration

echo Integration test suceeded

IF NOT EXIST "dist" mkdir dist
copy GcmPracticum\bin\release\GcmPracticum.exe dist

echo.
echo.
echo The application has been copied to the dist folder
echo.
echo.
echo Run dist\GcmPracticum.exe [meal_request] to run the application

GOTO end
:error
echo "An error occurred during the build, aborting..."
GOTO end

:failedIntegration
echo "The integration test has failed!!!!"

:end
