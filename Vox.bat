@echo off
Title Vox
Set RATE=0
Set VOL=100
:CLS
Mode con: Lines=15
Echo Vox [Version 1.2]
Echo (C)2014 Igtampe, No Rights Reserved
Echo.

:VOICE
Set /p MSG=:
Echo.

REM Commands
IF /i "%MSG%"=="/TUT" Goto :Tutorial
IF /i "%MSG%"=="W" Goto :WAIT
IF /i "%MSG%"=="/W" Goto :WAIT
IF /i "%MSG%"=="/WAIT" Goto :WAIT
IF /i "%MSG%"=="/CLS" Goto :CLS
IF /i "%MSG%"=="/EDIT" START NOTEPAD Vox.bat & Goto :VOICE
IF /i "%MSG%"=="/EXIT" Goto :EXIT
IF /i "%MSG%"=="/L" Goto :Laugh
IF /i "%MSG%"=="/LL" Goto :LaughLong

REM Commands with Arguments
Echo %MSG%| Find "/VOLUME" >NUL
If "%ERRORLEVEL%"=="0" Goto :VOL
Echo %MSG%| Find "/RATE" >NUL
If "%ERRORLEVEL%"=="0" Goto :RATE

REM Message Conversion
Set MSG=%MSG:IDK=I Don't Know%
Set MSG=%MSG:OMG=Oh My God%
Set MSG=%MSG:GTG=Got to Go%
Set MSG=%MSG:FYI=For your Information%
Set MSG=%MSG:JK=Just Kidding%
Set MSG=%MSG:LOL=L O L%
Set MSG=%MSG:ROFL=R O F L%
Set MSG=%MSG:R O F Lcopter=Ruffle Copter%
Set MSG=%MSG:meme=meem%


Rem Actual voice File Generation.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Rate = %RATE% >> M.vbs
Echo sapi.Volume = %VOL% >> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
START M.vbs
Goto :VOICE

:VOL
Set VOL=%MSG:/VOLUME =%
Echo Set Volume to (%VOL%)
Echo.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Rate = %RATE% >> M.vbs
Echo sapi.Volume = %VOL% >> M.vbs
Echo sapi.Speak "Volume is now at %VOL%%%">> M.vbs
Start M.vbs
goto :VOICE

:RATE
Set RATE=%MSG:/RATE =%
Echo Set Rate to (%RATE%)
Echo.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Rate = %RATE% >> M.vbs
Echo sapi.Volume = %VOL% >> M.vbs
Echo sapi.Speak "Rate is now %RATE%">> M.vbs
Start M.vbs
goto :VOICE

:WAIT
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "Please Wait while I write a response">> M.vbs
START M.vbs
Goto :VOICE

:LAUGHLONG
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "ha ha ha ha ha ha ha!">> M.vbs
START M.vbs
Goto :VOICE

:LAUGH
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "ha ha!">> M.vbs
START M.vbs
Goto :VOICE

:Tutorial
Cls
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "Welcome to Vox">> M.vbs
Echo Welcome to Vox.
M.vbs
Echo.
Set MSG=I will be your guide.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=I Will show you how you use the system and at the end we'll show some shortcuts.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
echo I Will show you how you use the system, and at the end we'll show some
Echo Shortcuts.
M.vbs
Echo.
Set MSG=First, let's begin by typing Something for me to say. Type something below.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
echo %MSG%
M.vbs
Echo.
Set /p MSG=:
Echo.
Set MSG2=This is the way the system looks normally. I'll shut up so that we can hear what you typed.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG2%">> M.vbs
Echo This is the way the system looks normally. I'll shut up so that we can
Echo hear what you typed.
M.vbs
Echo.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=Good. Now i'll show you some of the commands for the System
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=Type /l in the system. But even if you don't i'll still act like you did.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set /p MSG2=:
Echo.
Set MSG=This is the result of /l.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=Ha Ha!
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=Type /ll in the system. But even if you don't, I won't care.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set /p MSG2=:
Echo.
Set MSG=This is the result of /ll.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=Ha Ha ha ha ha ha ha!
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=Type /W in the system. If you don't, well guess what, It'll still happen!
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set /p MSG2=:
Echo.
Set MSG=This is the result of /w.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=Please Wait while I write a response.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=You could also only write (W) or (/WAIT)
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo You could also only write "W" or "/wait"
M.vbs
Echo.
Set MSG=To Exit, Type /EXIT. It will also erase the VBS File Left behind.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=/ C L S will clear the screen.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo /cls will clear the screen.
M.vbs
Echo.
Set MSG=Use /Volume and /rate to set volume and rate. Make Sure both are capitalized
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo Use /Volume and /rate to set volume and rate.
Echo Make Sure both are capitalized
M.vbs
Echo.
Set MSG=There is a Sort of Queue when writing with Vox
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo.
Set MSG=If I am Speaking and you send more text, first i'll finish saying the first line, and then start with the next one.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo If I am Speaking and you send more text, first 
Echo i'll finish saying the first line and then start
Echo with the next line.
M.vbs
Echo.
Set MSG=We Hope you have enjoyed this Completely Useless tutorial. Enjoy your day.
Echo Set sapi=CreateObject("sapi.spvoice")> M.vbs
Echo sapi.Speak "%MSG%">> M.vbs
Echo %MSG%
M.vbs
Echo Why hello there good sir!> SIGNAL.dll
Goto :CLS


:EXIT
DEL M.vbs
EXIT & EXIT