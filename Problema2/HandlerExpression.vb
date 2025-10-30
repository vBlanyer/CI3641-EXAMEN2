Imports System
Imports System.Collections.Generic
Public Class Handler
    Public Function ObtenerPrecedencia(op As Char) As Integer
        Select Case op
            Case "+"c, "-"c
                Return 1
            Case "*"c, "/"c
                Return 2
            Case Else
                Return Integer.MaxValue ' Para evitar parentesis en n�meros, por ejemplo (4) + (2)
        End Select
    End Function

    Public Function EvaluarOperacion(op As Char, izquierda As Integer, derecha As Integer) As Integer
        Select Case op
            Case "+"c : Return izquierda + derecha
            Case "-"c : Return izquierda - derecha
            Case "*"c : Return izquierda * derecha
            Case "/"c : Return izquierda \ derecha
            Case Else : Throw New InvalidOperationException("Operador no reconocido: " & op)
        End Select
    End Function

    Public Function FormatearExpresion(izquierda As Tuple(Of String, Integer, Char), derecha As Tuple(Of String, Integer, Char), op As Char) As Tuple(Of String, Integer, Char)
        Dim currPrec As Integer = ObtenerPrecedencia(op)

        Dim izquierdaExpre As String = izquierda.Item1
        If izquierda.Item2 < currPrec Then
            izquierdaExpre = "(" & izquierdaExpre & ")"
        End If

        Dim derechaExpre As String = derecha.Item1
        If derecha.Item2 < currPrec OrElse (derecha.Item2 = currPrec AndAlso (op = "-"c OrElse op = "/"c)) Then
            derechaExpre = "(" & derechaExpre & ")"
        End If

        Dim expr As String = izquierdaExpre & " " & op & " " & derechaExpre
        Return Tuple.Create(expr, currPrec, op)
    End Function

    Public Sub OperarPreorden(arr As String)

        Dim pila As New Stack(Of Integer)()
        Dim elementos() As String = arr.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries) ' Se omiten espacios vacios y se toma como separador el espacio

        For i As Integer = elementos.Length - 1 To 0 Step -1 ' Se recorre de atras hacia adelante
            Dim elemento As String = elementos(i)
            Dim value As Integer

            If Integer.TryParse(elemento, value) Then
                pila.Push(value)
            ElseIf elemento.Length = 1 Then
                Dim op As Char = elemento(0)
                If pila.Count < 2 Then
                    Throw New InvalidOperationException("Expresi�n incompleta o pila vac�a.") ' Porque si hay menos de 2 elementos no se puede operar
                End If

                ' En preorden, al encontrar un operador se extraen primero el operando izquierdo y luego el derecho
                Dim izquierda As Integer = pila.Pop()
                Dim derecha As Integer = pila.Pop()
                Dim result As Integer = EvaluarOperacion(op, izquierda, derecha)

                pila.Push(result)
            Else
                Throw New InvalidOperationException("Elemento no v�lido: " & elemento)
            End If
        Next

        If pila.Count = 1 Then
            Console.WriteLine(" Resultado: " & pila.Pop())
        Else
            Throw New InvalidOperationException("Expresi�n inv�lida. Elementos restantes en la pila.")
        End If
    End Sub

    Public Sub OperarPostorden(arr As String)

        Dim pila As New Stack(Of Integer)()
        Dim elementos() As String = arr.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries) ' Se omiten espacios vacios y se toma como separador el espacio

        For i As Integer = 0 To elementos.Length - 1 ' Se recorre de adelante hacia atras
            Dim elemento As String = elementos(i)
            Dim value As Integer

            If Integer.TryParse(elemento, value) Then
                pila.Push(value)
            ElseIf elemento.Length = 1 Then
                Dim op As Char = elemento(0)
                If pila.Count < 2 Then
                    Throw New InvalidOperationException("Expresi�n incompleta o pila vac�a.")
                End If

                ' En postorden, al encontrar un operador se extraen primero el operando derecho y luego el izquierdo
                Dim derecha As Integer = pila.Pop()
                Dim izquierda As Integer = pila.Pop()
                Dim result As Integer = EvaluarOperacion(op, izquierda, derecha)

                pila.Push(result)
            Else
                Throw New InvalidOperationException("Item no v�lido: " & elemento)
            End If
        Next

        If pila.Count = 1 Then
            Console.WriteLine(" Resultado: " & pila.Pop())
        Else
            Throw New InvalidOperationException("Expresi�n inv�lida. Elementos restantes en la pila.")
        End If
    End Sub

    Public Sub MostrarPreorden(arr As String)
        Dim pila As New Stack(Of Tuple(Of String, Integer, Char))()
        Dim elementos() As String = arr.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        For i As Integer = elementos.Length - 1 To 0 Step -1 ' Se recorre de atras hacia adelante
            Dim elemento As String = elementos(i)
            Dim value As Integer

            If Integer.TryParse(elemento, value) Then
                pila.Push(Tuple.Create(elemento, Integer.MaxValue, " "c))
            ElseIf elemento.Length = 1 Then
                Dim op As Char = elemento(0)
                If pila.Count < 2 Then
                    Throw New InvalidOperationException("Expresi�n incompleta o pila vac�a.")
                End If

                Dim izquierda = pila.Pop()
                Dim derecha = pila.Pop()
                pila.Push(FormatearExpresion(izquierda, derecha, op))
            Else
                Throw New InvalidOperationException("Item no v�lido: " & elemento)
            End If
        Next

        If pila.Count = 1 Then
            Console.WriteLine(pila.Pop().Item1)
        Else
            Throw New InvalidOperationException("Expresi�n inv�lida. Elementos restantes en la pila.")
        End If
    End Sub

    Public Sub MostrarPostorden(arr As String)
        Dim pila As New Stack(Of Tuple(Of String, Integer, Char))()
        Dim elementos() As String = arr.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        For i As Integer = 0 To elementos.Length - 1 ' Se recorre de adelante hacia atras
            Dim elemento As String = elementos(i)
            Dim value As Integer

            If Integer.TryParse(elemento, value) Then
                pila.Push(Tuple.Create(elemento, Integer.MaxValue, " "c))
            ElseIf elemento.Length = 1 Then
                Dim op As Char = elemento(0)
                If pila.Count < 2 Then
                    Throw New InvalidOperationException("Expresi�n incompleta o pila vac�a.")
                End If

                Dim derecha = pila.Pop()
                Dim izquierda = pila.Pop()
                pila.Push(FormatearExpresion(izquierda, derecha, op))
            Else
                Throw New InvalidOperationException("Item no v�lido: " & elemento)
            End If
        Next

        If pila.Count = 1 Then
            Console.WriteLine(pila.Pop().Item1)
        Else
            Throw New InvalidOperationException("Expresi�n inv�lida. Elementos restantes en la pila.")
        End If
    End Sub
End Class

Module HandleExpression
    Dim handler As New Handler()
    Sub Interfaz()
        While True
            Console.Write("> ")
            Dim comando = Console.ReadLine()
            Dim p = comando.Split(" "c)
            Dim accion = p(0).ToUpper()

            Select Case accion

                Case "EVAL"
                    If p.Length >= 3 Then
                        Dim orden = p(1).ToUpper()
                        Dim expr As String = String.Join(" ", p, 2, p.Length - 2)
                        Select Case orden
                            Case "PRE"
                                handler.OperarPreorden(expr)
                            Case "POST"
                                handler.OperarPostorden(expr)
                            Case Else
                                Console.WriteLine("Orden no reconocida.")
                        End Select
                    Else
                        Console.WriteLine("Uso incorrecto del comando EVAL.")
                    End If

                Case "MOSTRAR"
                    If p.Length >= 3 Then
                        Dim orden = p(1).ToUpper()
                        Dim expr As String = String.Join(" ", p, 2, p.Length - 2)
                        Select Case orden
                            Case "PRE"
                                handler.MostrarPreorden(expr)
                            Case "POST"
                                handler.MostrarPostorden(expr)
                            Case Else
                                Console.WriteLine("Orden no reconocida.")
                        End Select
                    Else
                        Console.WriteLine("Uso incorrecto del comando MOSTRAR.")
                    End If

                Case "SALIR"
                    Exit While
                Case Else
                    Console.WriteLine("Comando no reconocido.")
            End Select
        End While
    End Sub

    Sub Main()
        Console.WriteLine("Simulador de expresiones PRE y POST orden")
        Console.WriteLine("Comandos:")
        Console.WriteLine("  EVAL <ORDEN> <EXPR>")
        Console.WriteLine("  MOSTRAR <ORDEN> <EXPR>")
        Console.WriteLine("  SALIR")

        Interfaz()
    End Sub

End Module



