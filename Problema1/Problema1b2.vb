Imports System

Module Problema1b2

    'Decisiones de Implementación:
    ' 1. Decidi usar la forma recursiva del Merge Sort porque es facil de entender.
    ' 2. Los tiempos de complejidad temporal y espacial son O(n log n) y O(n) respectivamente, igual que la version iterativa.
    ' 3. Solo difiere de la implementacion iterativa en el stack de llamadas.
    ' 4. VB pasa los arreglos por referencia, por lo que no es necesario usar punteros.
    ' 5. Como no se puede dividir un arreglo de tal manera que mantenga la referencia al arreglo original,
    '    se crean nuevos arreglos para las mitades izquierda y derecha. Y luego se usan esos arreglos para ordenar arr en la funcion Merge.

    ' Función para fusionar dos subarreglos ordenados
    Sub Merge(arr1 As Integer(), arr2 As Integer(), arr As Integer())
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0

        While (i < arr1.Length AndAlso j < arr2.Length)
            If (arr1(i) < arr2(j)) Then
                arr(k) = arr1(i)
                i += 1
            Else
                arr(k) = arr2(j)
                j += 1
            End If
            k += 1
        End While

        While (i < arr1.Length)
            arr(k) = arr1(i)
            i += 1 : k += 1
        End While

        While (j < arr2.Length)
            arr(k) = arr2(j)
            j += 1 : k += 1
        End While
    End Sub

    ' Función recursiva de Merge Sort
    Sub MergeSort(arr As Integer())
        If (arr.Length > 1) Then
            Dim mid As Integer = arr.Length \ 2
            Dim leftArr As Integer() = New Integer(mid - 1) {}
            Dim rightArr As Integer() = New Integer(arr.Length - mid - 1) {}
            Array.Copy(arr, 0, leftArr, 0, mid)
            Array.Copy(arr, mid, rightArr, 0, arr.Length - mid)
            MergeSort(leftArr)
            MergeSort(rightArr)
            Merge(leftArr, rightArr, arr)
        End If
    End Sub

    Sub Main(args As String())
        Dim arr As Integer() = {38, 27, 43, 3, 9, 82, 10}
        Console.WriteLine("Array original: " & String.Join(", ", arr))
        MergeSort(arr)
        Console.WriteLine("Array ordenado: " & String.Join(", ", arr))
    End Sub
End Module
