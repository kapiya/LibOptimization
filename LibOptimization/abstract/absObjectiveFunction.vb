﻿Namespace Optimization
    ''' <summary>
    ''' Abstarct objective function class
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable>
    Public MustInherit Class absObjectiveFunction
        ''' <summary>
        ''' Get number of variables
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Function NumberOfVariable() As Integer

        ''' <summary>
        ''' Evaluate
        ''' </summary>
        ''' <param name="x"></param>
        ''' <remarks></remarks>
        Public MustOverride Function F(ByVal x As List(Of Double)) As Double

        ''' <summary>
        ''' Gradient vector (for Steepest descent method, newton method)
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' ex)
        ''' f(x1,..,xn) = x1^2 + ... + xn^2
        ''' del f =  [df/dx1 , ... , df/dxn]
        ''' </remarks>
        Public MustOverride Function Gradient(ByVal x As List(Of Double)) As List(Of Double)

        ''' <summary>
        ''' Hessian matrix (for newton method)
        ''' </summary>
        ''' <param name="x"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' ex)
        ''' f(x1,x2) = x1^2 + x2^2
        ''' del f   =  [df/dx1 df/dx2]
        ''' del^2 f = [d^2f/d^2x1     d^2f/dx1dx2]
        '''           [d^2f/d^2dx2dx1 d^2f/d^2x2]
        ''' </remarks>
        Public MustOverride Function Hessian(ByVal x As List(Of Double)) As List(Of List(Of Double))

        ''' <summary>
        ''' Numerical derivertive
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="h">default 10^-8</param>
        ''' <returns></returns>
        Public Function NumericDerivative(ByVal x As List(Of Double), Optional ByVal h As Double = 0.00000001) As List(Of Double)
            Dim df As New List(Of Double)
            For i As Integer = 0 To NumberOfVariable() - 1
                'central differences
                Dim tempX1 As New List(Of Double)(x)
                Dim tempX2 As New List(Of Double)(x)
                tempX1(i) = tempX1(i) + h
                tempX2(i) = tempX2(i) - h

                'diff
                Dim tempDf = (F(tempX1) - F(tempX2))
                tempDf = tempDf / (2.0 * h)
                df.Add(tempDf)
            Next
            Return df
        End Function

        ''' <summary>
        ''' Numerical 2nd derivertive
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="h">default 10^-5</param>
        ''' <returns></returns>
        Public Function Numeric2ndDerivative(ByVal x As List(Of Double), Optional ByVal h As Double = 0.00001) As List(Of Double)
            Dim df As New List(Of Double)
            For i As Integer = 0 To NumberOfVariable() - 1
                'central differences
                Dim tempX1 As New List(Of Double)(x)
                Dim tempX2 As New List(Of Double)(x)
                tempX1(i) = tempX1(i) + h
                tempX2(i) = tempX2(i) - h

                'diff
                'x_+1 - 2*x + x_-1 / h^2
                Dim tempDf = (F(tempX1) - 2.0 * F(x) + F(tempX2))
                tempDf = tempDf / (h * h)
                df.Add(tempDf)
            Next
            Return df
        End Function

        ''' <summary>
        ''' Numeric Hessian(diagonal element)
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="h">default 10^-5</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' ex)
        ''' f(x1,x2) = x1^2 + x2^2
        ''' der^2 f = [d^2f/d^2x1  0         ]
        '''           [0           d^2f/d^2x2]
        ''' </remarks>
        Public Function NumericHessianToDiagonal(ByVal x As List(Of Double), Optional ByVal h As Double = 0.00001) As List(Of List(Of Double))
            '2回微分を対角成分のみ
            Dim secDerivertive = Numeric2ndDerivative(x, h)
            Dim ret As New List(Of List(Of Double))
            For i As Integer = 0 To Me.NumberOfVariable - 1
                ret.Add(New List(Of Double))
                For j As Integer = 0 To Me.NumberOfVariable - 1
                    If i = j Then
                        ret(i).Add(secDerivertive(i))
                    Else
                        ret(i).Add(0.0)
                    End If
                Next
            Next
            Return ret
        End Function
    End Class

End Namespace