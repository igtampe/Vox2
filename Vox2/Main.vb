Imports SpeechLib
Imports Vox2.BasicRender
Module Main

    Public MyVoices() As Speaker
    Public LastTalked As Integer
    Public SelectedVoice As Integer
    Public DefaultVoice As SpObjectToken

    Sub Main(ByVal args() As String)
        'mode con:cols = 70 lines=25

        Console.Title = "Vox 2.0"


        Color(ConsoleColor.Black, ConsoleColor.White)

        Console.SetWindowSize(100, 40)
        Console.SetBufferSize(100, 40)

        ReDim Preserve MyVoices(1)
        LastTalked = -1
        MyVoices(0) = New Speaker("Default", ConsoleColor.White)
        DefaultVoice = MyVoices(SelectedVoice).GetVoice()

        If args.Count > 0 Then
            StoryTime(args(0))
            Exit Sub
        End If

        Dim WelcomeText As String

        If FileIO.FileSystem.FileExists("Marker.txt") Then
            WelcomeText = "Welcome back!"
        Else
            WelcomeText = "Hello, and Welcome to VOX 2! We hope you enjoy the new user experience. Go ahead and type something on the text box above, and let's get started, or if you're new, type /TUT for a brief tutorial!"
            FileOpen(1, "./Marker.txt", OpenMode.Output)
            WriteLine(1, "Thanks for Using VOX!")
            FileClose(1)
        End If



        Render()
        ToChat(0, WelcomeText)
        'ToChat(0, "Cross your fingers!")

        Dim ExitFlag As Boolean = False
        SelectedVoice = 0

        Dim Result As String

        While ExitFlag = False

            Result = TextBox()

            If Result.ToUpper = "EXIT" Then
                Exit While
            End If

            If Result.StartsWith("/") Then
                Result = Result.Substring(1)
                Dim ResultSplit() As String = Result.Split(" ")
                Select Case ResultSplit(0).ToUpper
                    Case "TUT"
                        Tutorial()
                    Case "RATE"
                        Dim NewRate As Integer
                        Try
                            NewRate = ResultSplit(1)
                        Catch ex As Exception
                            TextNote("Illegal Arguement", ConsoleColor.Red, ConsoleColor.White)
                        End Try
                        MyVoices(SelectedVoice).SetRate(NewRate)
                        TextNote("Rate is now " & NewRate, ConsoleColor.Blue, ConsoleColor.White)
                        MyVoices(SelectedVoice).say("Rate is now " & NewRate)
                    Case "VOL"
                        Dim NewVol As Integer
                        Try
                            NewVol = ResultSplit(1)
                        Catch ex As Exception
                            TextNote("Illegal Arguement", ConsoleColor.Red, ConsoleColor.White)
                        End Try
                        MyVoices(SelectedVoice).SetVolume(NewVol)
                        TextNote("Volume is now " & NewVol, ConsoleColor.Blue, ConsoleColor.White)
                        MyVoices(SelectedVoice).say("Volume is now " & NewVol)
                    Case "VOLUME"
                        Dim NewVol As Integer
                        Try
                            NewVol = ResultSplit(1)
                        Catch ex As Exception
                            TextNote("Illegal Arguement", ConsoleColor.Red, ConsoleColor.White)
                        End Try
                        MyVoices(SelectedVoice).SetVolume(NewVol)
                        TextNote("Volume is now " & NewVol, ConsoleColor.Blue, ConsoleColor.White)
                        MyVoices(SelectedVoice).say("Volume is now " & NewVol)
                    Case "PITCH"
                        Dim NewPitch As Integer
                        Try
                            NewPitch = ResultSplit(1)
                        Catch ex As Exception
                            TextNote("Illegal Arguement", ConsoleColor.Red, ConsoleColor.White)
                        End Try
                        MyVoices(SelectedVoice).SetPitch(NewPitch)
                        TextNote("Pitch is now " & NewPitch, ConsoleColor.Blue, ConsoleColor.White)
                        MyVoices(SelectedVoice).say("Pitch is now " & NewPitch)
                    Case "L"
                        ToChat(SelectedVoice, "ha ha!")
                    Case "LL"
                        ToChat(SelectedVoice, "ha ha ha ha ha ha ha!")
                    Case "W"
                        ToChat(SelectedVoice, "Please Wait while I write a response")
                    Case "CLS"
                        Color(ConsoleColor.Black, ConsoleColor.White)
                        Console.Clear()
                End Select

            Else
                ToChat(SelectedVoice, Result)
            End If

        End While

    End Sub

    Public Sub StoryTime(File As String)
        FileOpen(1, File, OpenMode.Input)
        Dim Title As String = LineInput(1)
        Dim AuthorSubtitle As String = LineInput(1)
        Dim NumberOfActors As Integer
        ToChat(0, Title & ", " & AuthorSubtitle)

        Dim Read As String
        Dim CurrentCharacter As Integer

        While Not (EOF(1))
            Read = LineInput(1)
            If Read = "" Then
            ElseIf Read.StartsWith(":") Then
                NumberOfActors = Read(1).ToString
                ToChat(0, "A Play for " & NumberOfActors & " Voices")
                ReDim Preserve MyVoices(NumberOfActors)
            ElseIf Read.StartsWith("=") Then
                Exit While
            Else
                MyVoices(CurrentCharacter) = New Speaker(Read.Split(",")(0), StringToColor(Read.Split(",")(1)))
                CurrentCharacter = CurrentCharacter + 1
            End If
        End While

        Dim ActorsSequentially As String = ""

        For I = 0 To NumberOfActors - 2
            ActorsSequentially = ActorsSequentially & MyVoices(I).Name & ", "
        Next
        ActorsSequentially = ActorsSequentially & "and " & MyVoices(NumberOfActors - 1).Name & "."

        ToChat(0, "For this story, you'll need " & NumberOfActors & " Actors, these being " & ActorsSequentially)

        For I = 0 To NumberOfActors - 1
            ToChat(0, "Please select a voice and pitch for " & MyVoices(I).Name)
            SelectedVoice = I
            SelectAVoice()
            SelectAPitch()
        Next

        SelectedVoice = 0
        ToChat(0, "Alright! We're ready to start. Press a key when you're ready")
        Pause()

        LastTalked = -1

        Color(ConsoleColor.Black, ConsoleColor.White)
        Console.Clear()
        Render()

        ToChat(0, Title & ", " & AuthorSubtitle)

        While Not EOF(1)
            Read = LineInput(1)
            If Read = "" Then
            ElseIf Read.StartsWith("[") Then
                SelectedVoice = FindCharacter(Read.Substring(1, Read.Length - 2))
            ElseIf Read.ToUpper.StartsWith(":WAIT") Then
                Sleep(Read.Split(" ")(1))
            Else
                ToChat(SelectedVoice, Read)
            End If
        End While

        Sleep(500)
        ToChat(0, "The story is done. Press a key to close vox.")



    End Sub

    Public Function FindCharacter(Name As String)
        For I = 0 To MyVoices.Length - 1
            If IsNothing(MyVoices(I)) Then
            Else
                If MyVoices(I).Name = Name Then
                    Return I
                End If


            End If
        Next
        Return 0
    End Function

    Public Sub TextNote(ErrorText As String, BG As ConsoleColor, FG As ConsoleColor)
        Box(BG, 93, 1, 2, 2)
        SetPos(3, 2)
        Color(BG, FG)
        Echo(ErrorText)
    End Sub

    Public Sub Tutorial()
        MyVoices(0).Name = MyVoices(0).GetName
        MyVoices(0).Color = ConsoleColor.Green
        LastTalked = -1
        ToChat(0, "Welcome to the Vox. My name is " & MyVoices(0).GetName & ", and I will be your guide. First I Will show you how you use the system and at the end I'll show some more shortcuts and features we have available for you. First, let's begin by typing something for me to say. Above this text is a textbox. Use it to write something for me to say!")
        Dim Result = TextBox()
        ToChat(0, "This is the way the system looks normally. I'll shut up so that we can hear what you typed.")

        MyVoices(0).Name = "You"
        LastTalked = -1
        MyVoices(0).Color = ConsoleColor.White

        ToChat(0, Result)

        MyVoices(0).Name = MyVoices(0).GetName
        MyVoices(0).Color = ConsoleColor.Green
        LastTalked = -1
        ToChat(0, "Good. Now i'll show you some of the commands for the System. For brevity's sake, I'll list them here and you can try them later. /L Gives a short laugh, and /LL Gives a longer laugh. /W tells others to wait for you to type a response, and /CLS Clears the screen. You can also modify some of my parameters. For instance:")

        MyVoices(0).SetVolume(25)
        ToChat(0, "You can decrease my volume. By default I'm at 100%. This is 25%")
        MyVoices(0).SetVolume(100)

        MyVoices(0).SetRate(5)
        ToChat(0, "I can talk very fast, or")

        MyVoices(0).SetRate(-5)
        ToChat(0, "Very slow")

        MyVoices(0).SetRate(0)
        ToChat(0, "And new to this version in particular, I can now talk ")

        MyVoices(0).SetPitch(10)
        ToChat(0, "Really high!")

        MyVoices(0).SetPitch(-10)
        ToChat(0, "Or really low")

        MyVoices(0).SetPitch(0)
        ToChat(0, "Try these new features by typing /VOL, /RATE, and /PITCH, in whatever case you desire, followed by whatever you want it to be at. Volume goes from 0 to 100, and pitch and rate go from -10 to 10.")

        ToChat(0, "To show off one last thing before we go, I hand it off to one of my good friends. Hold on while I get them.")

        Dim Voices As ISpeechObjectTokens = MyVoices(SelectedVoice).ListVoices
        Dim OriginalVoice As SpObjectToken = MyVoices(SelectedVoice).GetVoice()
        Dim OriginalName As String = MyVoices(0).GetName
        Dim SelectedVoiceIndex = 0

        MyVoices(0).Name = OriginalName & "'s Friend"
        LastTalked = -1
        MyVoices(0).Color = ConsoleColor.Blue

RetryVoice:
        MyVoices(SelectedVoice).SetVoice(SelectedVoiceIndex)
        If MyVoices(SelectedVoice).GetName = OriginalName Then
            SelectedVoiceIndex = SelectedVoiceIndex + 1

            If SelectedVoiceIndex = Voices.Count Then
                MyVoices(SelectedVoice).SetVoice(OriginalVoice)
                MyVoices(SelectedVoice).SetVoice(OriginalVoice)
                MyVoices(0).Name = MyVoices(0).GetName
                MyVoices(0).Color = ConsoleColor.Green
                LastTalked = 0
                ToChat(0, "Woops, Looks like I'm the only voice here! If you had more voices, you could select witch one to use with TAB. Try it out if you get some more voices some time.")
                GoTo SkipVoiceDemo
            End If
            GoTo RetryVoice
        End If
        Try
            ToChat(0, "Hi there! I'm " & MyVoices(0).GetName & ", One of " & OriginalName & "'s friends. Starting with this version of VOX, you can now switch between voices by hitting TAB! You can find me there, along with maybe a few other voices!")
        Catch ex As Exception
            SelectedVoiceIndex = SelectedVoiceIndex + 1

            If SelectedVoiceIndex = Voices.Count Then
                MyVoices(SelectedVoice).SetVoice(OriginalVoice)

                MyVoices(SelectedVoice).SetVoice(OriginalVoice)
                MyVoices(0).Name = MyVoices(0).GetName
                MyVoices(0).Color = ConsoleColor.Green
                LastTalked = 0

                ToChat(0, "Woops, Looks like you don't have any working voices for some reason. Probably don't hit TAB Then. Might lockup the system. Anyways, lets finish up.")
                GoTo SkipVoiceDemo
            End If
            GoTo RetryVoice
        End Try

SkipVoiceDemo:
        MyVoices(SelectedVoice).SetVoice(OriginalVoice)
        MyVoices(0).Name = MyVoices(0).GetName
        MyVoices(0).Color = ConsoleColor.Green
        LastTalked = -1

        ToChat(0, "That's about it! I Hope you have enjoyed this, no longer as completely useless tutorial. Enjoy your day, and I hope you enjoy using Vox!")

        MyVoices(0).Name = "Default"
        MyVoices(0).Color = ConsoleColor.White
        LastTalked = -1


    End Sub

    Public Function TextBox()
        Dim Text As String = ""
        Dim KeyPressed As ConsoleKeyInfo
        Color(ConsoleColor.Gray, ConsoleColor.Black)
        While True
            Box(ConsoleColor.Gray, 93, 1, 2, 2)
            SetPos(3, 2)
            If Text.Length > 90 Then
                Echo(Text.Substring(Text.Length - 90))
            Else
                Echo(Text)
            End If

            KeyPressed = Console.ReadKey()

            If KeyPressed.Key = ConsoleKey.Enter Then
                Return Text
            ElseIf KeyPressed.Key = ConsoleKey.Backspace Then
                If Not Text = "" Then
                    Text = Text.Substring(0, Text.Length - 1)
                End If
            ElseIf KeyPressed.Key = ConsoleKey.Tab Then
                SelectAVoice()
                Color(ConsoleColor.Gray, ConsoleColor.Black)
            Else
                Text = Text & KeyPressed.KeyChar
            End If
        End While

    End Function

    Public Sub Header(Header As String, HeaderColor As ConsoleColor, Optional HeaderChar As String = "-")
        Color(ConsoleColor.Black, ConsoleColor.White)
        Echo(HeaderChar & "[")
        Color(ConsoleColor.Black, HeaderColor)
        Echo(Header)
        Color(ConsoleColor.Black, ConsoleColor.White)
        Echo("]")
        For I = 0 To 99 - Header.Length - 3
            Echo(HeaderChar)
        Next


    End Sub

    Public Sub SelectAPitch()
        Dim selectedpitch As Integer = 0
        Dim KeyPressed As ConsoleKeyInfo
        Dim PrevDirection As Integer = 1

        Color(ConsoleColor.DarkGreen, ConsoleColor.Yellow)

        While True
            Box(ConsoleColor.DarkGreen, 93, 1, 2, 2)
            SetPos(3, 2)

            Echo("Select a Pitch: " & selectedpitch)
            MyVoices(SelectedVoice).SetPitch(selectedpitch)
            MyVoices(SelectedVoice).say(selectedpitch)

            KeyPressed = Console.ReadKey()

            If KeyPressed.Key = ConsoleKey.Enter Then
                Return
            ElseIf KeyPressed.Key = ConsoleKey.UpArrow Then
                selectedpitch = selectedpitch + 1

            ElseIf KeyPressed.Key = ConsoleKey.DownArrow Then
                selectedpitch = selectedpitch - 1
            ElseIf KeyPressed.Key = ConsoleKey.PageDown Then
                selectedpitch = -10
            ElseIf KeyPressed.Key = ConsoleKey.PageUp Then
                selectedpitch = 10
            End If

        End While
    End Sub

    Public Sub SelectAVoice()
        Dim Voices As ISpeechObjectTokens = MyVoices(SelectedVoice).ListVoices
        Dim OriginalVoice As SpObjectToken = MyVoices(SelectedVoice).GetVoice()
        Dim SelectedVoiceIndex = -1
        Dim KeyPressed As ConsoleKeyInfo
        Dim PrevDirection As Integer = 1

        Color(ConsoleColor.DarkBlue, ConsoleColor.Yellow)

        While True
RetryVoice:
            Box(ConsoleColor.DarkBlue, 93, 1, 2, 2)
            SetPos(3, 2)

            If SelectedVoiceIndex = -1 Then
                Echo("(" & SelectedVoiceIndex + 2 & "/" & Voices.Count + 1 & ") " & DefaultVoice.GetDescription)
                MyVoices(SelectedVoice).SetVoice(DefaultVoice)
            Else
                Echo("(" & SelectedVoiceIndex + 2 & "/" & Voices.Count + 1 & ") " & Voices.Item(SelectedVoiceIndex).GetDescription)
                MyVoices(SelectedVoice).SetVoice(SelectedVoiceIndex)
            End If

            Try
                MyVoices(SelectedVoice).say("Hi, I'm " & MyVoices(SelectedVoice).GetName)
            Catch ex As Exception
                Debug.Print("Could Not have " & MyVoices(0).GetVoice.GetDescription & " talk. Maybe they're shy?")
                SelectedVoiceIndex = SelectedVoiceIndex + (1 * PrevDirection)

                If SelectedVoiceIndex = -2 Then
                    SelectedVoiceIndex = Voices.Count - 1
                End If

                If SelectedVoiceIndex = Voices.Count Then
                    SelectedVoiceIndex = 0
                End If

                GoTo RetryVoice
            End Try

            KeyPressed = Console.ReadKey()

            If KeyPressed.Key = ConsoleKey.Enter Then
                Return

            ElseIf KeyPressed.Key = ConsoleKey.Escape Then
                MyVoices(SelectedVoice).SetVoice(OriginalVoice)
                Return

            ElseIf KeyPressed.Key = ConsoleKey.UpArrow Then
                PrevDirection = -1
                SelectedVoiceIndex = SelectedVoiceIndex - 1
                If SelectedVoiceIndex = -2 Then
                    SelectedVoiceIndex = Voices.Count - 1
                End If

            ElseIf KeyPressed.Key = ConsoleKey.DownArrow Then
                PrevDirection = 1
                SelectedVoiceIndex = SelectedVoiceIndex + 1
                If SelectedVoiceIndex = Voices.Count Then
                    SelectedVoiceIndex = -1
                End If
            End If



        End While
    End Sub

    Public Sub ToChat(VoiceIndex As Integer, Speech As String)
        SetPos(0, 39)
        Dim SentenceArray() As String = Speech.Split(".")
        If Not LastTalked = VoiceIndex Then
            LastTalked = VoiceIndex
            Echo(".")
            Header(MyVoices(VoiceIndex).Name, MyVoices(VoiceIndex).Color, "_")
            Render()
            SetPos(0, 39)
        End If

        Dim CurrentLeft As Integer
        Dim CurrentTop As Integer

        For Each Sentence In SentenceArray

            If Not (Sentence = "") Then
                Color(ConsoleColor.Black, MyVoices(VoiceIndex).Color)
                Type(Sentence & ".")

                CurrentLeft = Console.CursorLeft
                CurrentTop = Console.CursorTop

                Render()

                Console.CursorLeft = CurrentLeft
                Console.CursorTop = CurrentTop

                MyVoices(VoiceIndex).say(Sentence)
            End If

        Next
        Echo("", True)
        Render()



    End Sub

    Public Sub Render()
        Box(ConsoleColor.Black, 100, 4, 0, 0)
        SetPos(0, 0)
        header("VOX", ConsoleColor.Red)
        SetPos(1, 2)
        Echo(":")
        SetPos(0, 4)
        For I = 0 To 99
            Echo("-")
        Next
    End Sub

    Public Sub Type(Doot As String, Optional delay As Integer = 5)

        For X = 0 To Doot.Length - 1

            Console.Write(Doot(X))
            If Not delay = 0 Then Threading.Thread.Sleep(delay)

        Next



    End Sub

End Module
