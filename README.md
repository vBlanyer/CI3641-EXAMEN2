# Examen 2: Lenguajes de Programación

**Autor:** Blanyer Vielma — Carnet: 16-11238

## a) Lenguaje escogido para el examen: Visual Basic

### I Breve descripción del lenguaje

   1. Estructuras de control:
      Condicionales:
      - If...Then...ElseIf...Else...End If
      - Select Case...Case...Case Else...End Select
   2. Repeticiones:
      - For..To...Next
      - For Each...In...Next
      - Do While...Loop
      - While...End While
   3. Interrupciones:
      - Exit For
      - Exit Do
      - Continue For
      - Continue Do
      - Return
   4. Manejo de excepciones:
      - Try...Catch...Finally...End Try

### II Orden de evaluación de expresiones y funciones

   A.  
   - Tipo de evaluación Aplicativa, Visual Basic evalúa argumentos y subexpresiones antes de invocar funciones.
   - No tiene evaluacion perezosa por defecto, aunque se puede simular pereza con Lazy(Of T).

   B.  
   - El orden de evaluación de argumentos/operandos Izquierda a derecha, Visual Basic evalúa subexpresiones y argumentos de izquierda a derecha, la precedencia y asociatividad de operadores pueden agrupar subexpresiones antes de aplicar ese orden.  


## b) Implementacion en carpeta de problema 1

### 2. Manejador de Expresiones carpeta Problema2

### 3. asdad

### 4. Implementacion en carpeta Problema4

## Analisis Comparativo

<img width="751" height="452" alt="image" src="https://github.com/user-attachments/assets/a7299f6c-8e15-4193-9933-69be5763573d" />

Como se puede observar en la gráfica, la función más eficiente es la iterativa. Esto se debe a que no utiliza una pila de llamadas, lo que evita los costos asociados al manejo de contexto y la sobrecarga que puede provocar errores como StackOverflowException. Sin embargo, la función iterativa, por razones obvias al tipo de dato utilizado, arroja un error por OverflowException a partir del valor 136, al superar el límite definido por MaxValue
