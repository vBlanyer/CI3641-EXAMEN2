Imports System.IO
Imports HandlerExpression
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class TestHandlerExpression

    ' Evalúa expresión en orden prefijo
    <TestMethod>
    Public Sub Test1()
        Dim handler As New Handler()
        Dim salida As New StringWriter()
        Console.SetOut(salida)

        handler.OperarPreorden("+ * + 3 4 5 7")

        Dim resultado = salida.ToString()
        Assert.IsTrue(resultado.Contains("42"))
    End Sub

    ' Evalúa expresión en orden postfijo
    <TestMethod>
    Public Sub Test2()
        Dim handler As New Handler()
        Dim salida As New StringWriter()
        Console.SetOut(salida)

        handler.OperarPostorden("8 3 - 8 4 4 + * +")

        Dim resultado = salida.ToString()
        Assert.IsTrue(resultado.Contains("69"))
    End Sub

    ' Muestra expresión infija desde prefijo
    <TestMethod>
    Public Sub Test3()
        Dim handler As New Handler()
        Dim salida As New StringWriter()
        Console.SetOut(salida)

        handler.MostrarPreorden("+ * + 3 4 5 7")

        Dim resultado = salida.ToString().Trim()
        Assert.IsTrue(resultado.Contains("(3 + 4) * 5 + 7"))
    End Sub

    ' Muestra expresión infija desde postfijo
    <TestMethod>
    Public Sub Test4()
        Dim handler As New Handler()
        Dim salida As New StringWriter()
        Console.SetOut(salida)

        handler.MostrarPostorden("8 3 - 8 4 4 + * +")

        Dim resultado = salida.ToString().Trim()
        Assert.IsTrue(resultado.Contains("8 - 3 + 8 * (4 + 4)"))
    End Sub

    ' Expresión inválida (lanza excepción)
    <TestMethod>
    Public Sub Test5()
        Dim handler As New Handler()
        Try
            handler.OperarPreorden("+ 3")
            Assert.Fail("Debería lanzar excepción por expresión inválida.")
        Catch ex As InvalidOperationException
            Assert.IsTrue(ex.Message.Contains("incompleta") OrElse ex.Message.Contains("inválida"))
        End Try
    End Sub

    ' Expresión con un solo número
    <TestMethod>
    Public Sub Test6()
        Dim handler As New Handler()
        Dim salida As New StringWriter()
        Console.SetOut(salida)

        handler.OperarPreorden("7")

        Dim resultado = salida.ToString().Trim()
        Assert.IsTrue(resultado.EndsWith("7"))
    End Sub

    ' Expresión vacía
    <TestMethod>
    Public Sub Test7()
        Dim handler As New Handler()
        Try
            handler.OperarPostorden("")
            Assert.Fail("Debería lanzar excepción por expresión vacía.")
        Catch ex As InvalidOperationException
            Assert.IsTrue(ex.Message.Contains("inválida") OrElse ex.Message.Contains("vacía"))
        End Try
    End Sub

    ' Operador desconocido
    <TestMethod>
    Public Sub Test8()
        Dim handler As New Handler()
        Try
            handler.OperarPreorden("& 3 4")
            Assert.Fail("Debería lanzar excepción por operador no reconocido.")
        Catch ex As InvalidOperationException
            Assert.IsTrue(ex.Message.Contains("no reconocido"))
        End Try
    End Sub

    ' Token no válido
    <TestMethod>
    Public Sub Test9()
        Dim handler As New Handler()
        Try
            handler.OperarPostorden("3 a +")
            Assert.Fail("Debería lanzar excepción por expresion incompleta o pila vacia")
        Catch ex As InvalidOperationException
            Assert.IsTrue(ex.Message.Contains("Expresión incompleta o pila vacía."))
        End Try
    End Sub

    ' Paréntesis por precedencia
    <TestMethod>
    Public Sub Test10()
        Dim handler As New Handler()
        Dim salida As New StringWriter()
        Console.SetOut(salida)

        handler.MostrarPreorden("* + 2 3 4")

        Dim resultado = salida.ToString().Trim()
        Assert.IsTrue(resultado.Contains("(2 + 3) * 4"))
    End Sub

    ' Simetría entre preorden y postorden
    <TestMethod>
    Public Sub Test11()
        Dim handler As New Handler()

        Dim salidaPre As New StringWriter()
        Console.SetOut(salidaPre)
        handler.OperarPreorden("+ * 2 3 4")

        Dim resultadoPre = salidaPre.ToString().Trim()

        Dim salidaPost As New StringWriter()
        Console.SetOut(salidaPost)
        handler.OperarPostorden("2 3 * 4 +")

        Dim resultadoPost = salidaPost.ToString().Trim()

        Assert.AreEqual(resultadoPre, resultadoPost)
    End Sub

    ' Expresión larga y profunda
    <TestMethod>
    Public Sub Test12()
        Dim handler As New Handler()
        Dim salida As New StringWriter()
        Console.SetOut(salida)

        handler.OperarPreorden("+ + + + 1 2 3 4 5")

        Dim resultado = salida.ToString().Trim()
        Assert.IsTrue(resultado.EndsWith("15"))
    End Sub

    <TestMethod>
    Public Sub test13()
        Dim handler As New Handler()
        Try
            handler.OperarPostorden("3 a 4 +")
            Assert.Fail("Debería lanzar excepción por token no válido.")
        Catch ex As InvalidOperationException
            Assert.IsTrue(ex.Message.Contains("Token no válido"))
        End Try
    End Sub

End Class
