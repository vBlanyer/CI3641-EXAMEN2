Imports System

Module Problema1b1

    ' Funcion que enunciada en el problema
    Function f(n As Integer) As Integer
        Dim result As Integer = 0
        If (n Mod 2 = 0) Then
            result = n \ 2
        Else
            result = 3 * n + 1
        End If
        Return result
    End Function

    ' Funcion dist que calcula la distancia de n a 1 usando la funcion f
    Function dist(n As Integer) As Integer
        Dim result As Integer = 0
        While (n <> 1)
            n = f(n)
            result += 1
        End While
        Return result
    End Function

    ' Funcion count que llama a dist, EL ENUNCIADO SE NOMBRE COMO COUNT(n) a pesar de ser lo mismo que dist(n)
    Function count(n As Integer) As Integer
        Return dist(n)
    End Function


    Sub Main(args As String())
        ' Prueba de la funcion dist
        Dim n As Integer = 42
        Console.WriteLine("count(" & n & ") = " & count(n))

    End Sub
End Module
