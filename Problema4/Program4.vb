Imports System
Imports System.Diagnostics

Module Program4

    ' a) Funcion Recursiva Directa
    Function F_recursiva(n As Integer, alfa As Integer, beta As Integer) As Integer
        If n >= 0 AndAlso n < alfa * beta Then
            Return n
        ElseIf n >= alfa * beta Then
            Dim suma As Integer = 0
            For i As Integer = 1 To alfa
                suma += F_recursiva(n - beta * i, alfa, beta)
            Next
            Return suma
        Else
            Return 0
        End If
    End Function

    ' b) Funcion Recursiva de Cola
    Function F_Cola(n As Integer, alfa As Integer, beta As Integer) As Integer
        If n >= 0 AndAlso n < alfa * beta Then
            Return n
        ElseIf n < 0 Then
            Return 0
        End If

        Return F_Auxiliar(n, alfa, beta, 1, 0)
    End Function

    Private Function F_Auxiliar(n As Integer, alfa As Integer, beta As Integer, i As Integer, acumulado As Integer) As Integer
        If i > alfa Then
            Return acumulado
        End If

        Dim termino As Integer = F_Cola(n - beta * i, alfa, beta)

        Return F_Auxiliar(n, alfa, beta, i + 1, acumulado + termino)
    End Function

    ' c) Funcion Iterativa
    Function F_Iterativa(n As Integer, alfa As Integer, beta As Integer) As Integer
        If n < alfa * beta Then
            Return n
        End If

        Dim limite As Integer = alfa * beta
        Dim tabla(n) As Integer

        For k As Integer = 0 To limite - 1
            tabla(k) = k
        Next

        For k As Integer = limite To n
            Dim suma As Integer = 0
            For i As Integer = 1 To alfa
                Dim y As Integer = k - beta * i
                If y >= 0 Then
                    suma += tabla(y)
                End If
            Next
            tabla(k) = suma
        Next

        Return tabla(n)
    End Function

    Function MedirTiempo(funcion As Func(Of Integer), ByRef milisegundos As Long) As Integer
        Dim sw As New Stopwatch()
        Dim resultado As Integer = 0

        Try
            sw.Start()
            resultado = funcion()
            sw.Stop()
            milisegundos = sw.Elapsed.TotalMilliseconds
        Catch ex As StackOverflowException
            milisegundos = -1
            resultado = -1
            Console.WriteLine("Error: StackOverflowException")
        Catch ex As OverflowException
            milisegundos = -1
            resultado = -1
            Console.WriteLine("Error: Overflow num�rico")
        Catch ex As Exception
            milisegundos = -1
            resultado = -1
            Console.WriteLine("Error: " & ex.Message)
        End Try

        Return resultado
    End Function

    Sub Main()
        Dim X As Integer = 2
        Dim Y As Integer = 3
        Dim Z As Integer = 8

        Dim alfa As Integer = ((X + Y) Mod 5) + 3
        Dim beta As Integer = ((Y + Z) Mod 5) + 3

        Console.WriteLine("Comparaci�n de tiempos (ms)")
        Console.WriteLine("n".PadRight(5) & "F_recursiva".PadRight(15) & "F_Cola".PadRight(15) & "F_Iterativa")

        For i As Integer = 0 To 136
            Dim n As Integer = i
            Dim tiempoRec, tiempoCola, tiempoIt As Long
            MedirTiempo(Function() F_recursiva(n, alfa, beta), tiempoRec)
            MedirTiempo(Function() F_Cola(n, alfa, beta), tiempoCola)
            MedirTiempo(Function() F_Iterativa(n, alfa, beta), tiempoIt)

            Console.WriteLine(n.ToString().PadRight(5) &
                          tiempoRec.ToString("F2").PadRight(15) &
                          tiempoCola.ToString("F2").PadRight(15) &
                          tiempoIt.ToString("F2"))
        Next

        Console.ReadLine()
    End Sub

End Module
