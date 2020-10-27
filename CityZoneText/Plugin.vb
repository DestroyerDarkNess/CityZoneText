Imports System.Drawing
Imports CityZoneText.SAMP_API.API


Namespace Udrakoloader

    Public Class Plugin

        Private Shared processGame As Process = Process.GetCurrentProcess()
        Private Shared processName As String = processGame.ProcessName & ".exe"

#Region " Structure "

        Public Structure GameSize
            Shared X As Integer = 5
            Shared Y As Integer = 5
            Shared InfoNumber As Integer = 0
        End Structure

        Public Structure LabelZoneText
            Shared Text As String = ""
            Shared X As Integer = 50
            Shared Y As Integer = 50
            Shared FontColor As Color = Color.White
            Shared TextSize As Integer = 10
            Shared TextStyle As String = "Arial"
            Shared InfoNumber As Integer = 0
        End Structure

#End Region

        Public Shared Function EntryPoint(ByVal pwzArgument As String) As Integer
            Dim tskThread As New Task(ScriptAsyc, TaskCreationOptions.LongRunning)
            tskThread.Start()
            Return 0
        End Function

        Private Shared CzoneText As String = String.Empty

        Private Shared ScriptAsyc As New Action(
        Sub()

            ' Do While True ' Normal Bucle
            '  Hack
            ' Loop

            For x As Integer = 0 To 2
                Dim Cped As Boolean = CBool(GetPlayerCPed())
                If Cped = True Then

                    Exit For
                End If
                x -= 1
            Next

            SetParam("process", processName)
            DestroyAllVisual()

            Dim CityName As New System.Text.StringBuilder
            Dim ZoneName As New System.Text.StringBuilder
            Dim MaxLen As Integer = 100

            For FixDraw As Integer = 0 To 10
                LabelZoneText.InfoNumber = TextCreate(LabelZoneText.TextStyle, LabelZoneText.TextSize, False, False, LabelZoneText.X, LabelZoneText.Y, colorFormat(LabelZoneText.FontColor), LabelZoneText.Text, False, True)
                DestroyAllVisual()
            Next

            LabelZoneText.InfoNumber = TextCreate(LabelZoneText.TextStyle, LabelZoneText.TextSize, False, False, LabelZoneText.X, LabelZoneText.Y, colorFormat(LabelZoneText.FontColor), LabelZoneText.Text, False, True)

            For i As Integer = 0 To 2 ' My Bucle XD
                Try
                    GetCityName(CityName, MaxLen)
                    GetZoneName(ZoneName, MaxLen)
                    CzoneText = CityName.ToString & " - " & ZoneName.ToString

                    Dim GameResolutionScreen As Point = GetScreenSpecsOverlay()

                    GameSize.X = GameResolutionScreen.X
                    GameSize.Y = GameResolutionScreen.Y

                    LabelZoneText.X = Val((GameSize.X / 2) - (80))
                    LabelZoneText.Y = Val((GameSize.Y - 50))



                    If Not CzoneText = LabelZoneText.Text Then
                        TextSetString(LabelZoneText.InfoNumber, CzoneText)
                        TextSetPos(LabelZoneText.InfoNumber, LabelZoneText.X, LabelZoneText.Y)
                    End If
                Catch ex As Exception

                End Try
                i -= 1
            Next
        End Sub)

    End Class

End Namespace



