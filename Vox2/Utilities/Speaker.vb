Imports SpeechLib

Public Class Speaker

    Private SAPI As SpVoice
    Public Color As ConsoleColor
    Public Name As String
    Public pitch As Integer

    Public Sub New(VoiceName As String, VoiceColor As ConsoleColor)
        SAPI = New SpVoice
        Name = VoiceName
        Color = VoiceColor
        pitch = 0
    End Sub

    Public Sub New(VoiceName As String, VoiceColor As ConsoleColor, VoicePitch As Integer)
        SAPI = New SpVoice
        Name = VoiceName
        Color = VoiceColor
        pitch = VoicePitch
    End Sub

    Public Sub New(VoiceName As String, VoiceColor As ConsoleColor, Voice As SpObjectToken)
        SAPI = New SpVoice
        Name = VoiceName
        Color = VoiceColor
        SAPI.Voice = Voice
        pitch = 0

    End Sub

    Public Function getPitch() As Integer
        Return pitch
    End Function

    Public Sub SetPitch(NewPitch As Integer)
        pitch = NewPitch
    End Sub

    Public Function ListVoices() As ISpeechObjectTokens
        Return SAPI.GetVoices
    End Function

    Public Function GetVoice() As SpObjectToken
        Return SAPI.Voice
    End Function

    Public Sub SetVoice(Index As Integer)
        SAPI.Voice = SAPI.GetVoices.Item(Index)
    End Sub

    Public Sub SetVoice(NewVoice As SpObjectToken)
        SAPI.Voice = NewVoice
    End Sub

    Public Function GetName() As String
        Dim NameTemp() As String = SAPI.Voice.GetDescription.ToString.Split(" ")
        Return NameTemp(1)
    End Function

    Public Sub say(TEXT As String)
        SAPI.Speak("<pitch middle = '" & pitch & "'/> " & TEXT)
    End Sub

    Public Function GetVolume()
        Return SAPI.Volume
    End Function

    Public Sub SetVolume(Vol As Long)
        SAPI.Volume = Vol
    End Sub

    Public Sub SetRate(Rate As Long)
        SAPI.Rate = Rate
    End Sub

End Class
