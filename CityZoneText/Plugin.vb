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

#Region " TextEfecct "

        Private Shared UseEffect As Boolean = True

        Private Shared _Showing As String = ""
        Private Shared _avrchar As Integer = 0
        Private Shared _AwaitTIme As Integer = 0
        Private Shared _MaxAwaitTIme As Integer = 5
        Private Shared ContinueAwait As Boolean = True
        Private Shared LocalTextCounter As String = String.Empty
        Private Shared _Interval As Integer = 2

#End Region

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
                    LabelZoneText.Y = Val((GameSize.Y - 30))

                    If UseEffect = True Then

                        If Not CzoneText = LabelZoneText.Text Then
                            _avrchar = 0
                            _AwaitTIme = 0
                            _MaxAwaitTIme = 100
                            ContinueAwait = True
                            LocalTextCounter = String.Empty

                            For Effect As Integer = 0 To 2

                                If _AwaitTIme = _MaxAwaitTIme Then
                                    ContinueAwait = False
                                    If _Showing.Count < CzoneText.Count And _Showing.Count > 0 Then
                                        _Showing = CzoneText.Substring(0, _Showing.Length + 1)
                                    ElseIf _Showing.Count < CzoneText.Count And _Showing.Count = 0 Then
                                        _Showing = CzoneText.Substring(0, 1)
                                    ElseIf _Showing.Count < _avrchar Then
                                        _Showing = " " + _Showing
                                    Else
                                        _Showing = ""
                                    End If
                                    LocalTextCounter = _Showing
                                    TextSetString(LabelZoneText.InfoNumber, _Showing)
                                    TextSetPos(LabelZoneText.InfoNumber, LabelZoneText.X, LabelZoneText.Y)
                                    System.Threading.Thread.Sleep(100)
                                    If LocalTextCounter = CzoneText Then
                                        LabelZoneText.Text = CzoneText
                                        Exit For
                                    End If
                                End If

                                If ContinueAwait = True Then
                                    _AwaitTIme += 1
                                End If

                                Effect -= 1

                            Next

                        End If


                    Else

                        If Not CzoneText = LabelZoneText.Text Then
                            TextSetString(LabelZoneText.InfoNumber, CzoneText)
                            TextSetPos(LabelZoneText.InfoNumber, LabelZoneText.X, LabelZoneText.Y)
                            LabelZoneText.Text = CzoneText
                        End If

                    End If



                Catch ex As Exception

                End Try
                i -= 1
            Next
        End Sub)

    End Class

End Namespace



