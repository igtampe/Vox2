Public Class VBTyper
    ''' <summary>
    ''' Types a string one letter at a time
    ''' </summary>
    ''' <param name="Doot">The string you want to type</param>
    ''' <param name="delay">How much to wait between letters</param>
    ''' <param name="sound">Play a sound yes or no</param>
    ''' <param name="Soundfile">Which soundfile to play dummy</param>
    Public Shared Sub Type(Doot As String, Optional delay As Integer = 5, Optional sound As Boolean = False, Optional Soundfile As String = "boop.wav")

        For X = 0 To Doot.Length - 1
            Console.Write(Doot(X))
            Try
                If sound = True And Not Doot(X) = " " And Not Doot(X) = vbNewLine Then My.Computer.Audio.Play(Soundfile, AudioPlayMode.Background)
            Catch
            End Try

            If Not delay = 0 Then Threading.Thread.Sleep(delay)
        Next



    End Sub
End Class
