﻿Imports LibOptimization2.Optimization

Namespace BenchmarkFunction
    ''' <summary>
    ''' Benchmark function
    ''' De Jong’s function 1 (Sphere Function)
    ''' </summary>
    ''' <remarks>
    ''' Minimum:
    '''  F(0, 0, 0) = 0
    ''' Range
    '''  -5.12 ~ 5.12 
    ''' Refference:
    '''  De Jong, K. A., "Analysis of the Behavior of a Class of Genetic Adaptive Systems", PhD dissertation, The University of Michigan, Computer and Communication Sciences Department (1975)
    ''' </remarks>
    Public Class clsBenchDeJongFunction1 : Inherits absObjectiveFunction
        ''' <summary>
        ''' Default constructor
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Target Function
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function F(ByVal x As List(Of Double)) As Double
            If x Is Nothing Then
                Return 0
            End If

            Dim ret As Double = 0
            For i As Integer = 0 To 2
                If (x(i) >= -5.12) AndAlso (x(i) <= 5.12) Then
                    ret += x(i) ^ 2
                Else
                    'out of range
                    ret += Math.Abs(x(i)) * 2 'penarty
                End If
            Next
            Return ret
        End Function

        Public Overrides Function Gradient(ByVal ai_var As List(Of Double)) As List(Of Double)
            Dim ret As New List(Of Double)
            For i As Integer = 0 To 2
                ret.Add(2.0 * ai_var(i))
            Next
            Return ret
        End Function

        Public Overrides Function Hessian(ByVal ai_var As List(Of Double)) As List(Of List(Of Double))
            Dim ret As New List(Of List(Of Double))
            For i As Integer = 0 To 2
                ret.Add(New List(Of Double))
                For j As Integer = 0 To 2
                    If i = j Then
                        ret(i).Add(2.0)
                    Else
                        ret(i).Add(0)
                    End If
                Next
            Next
            Return ret
        End Function

        Public Overrides ReadOnly Property NumberOfVariable As Integer
            Get
                Return 3
            End Get
        End Property
    End Class

End Namespace
