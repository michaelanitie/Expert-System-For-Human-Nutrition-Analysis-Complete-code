Option Strict On
Option Explicit On
Imports System.Data.SqlServerCe
Imports System.Data

Public Class frmMain
    Dim dr As SqlCeDataReader
    Dim cmd As New SqlCeCommand
    Dim conn As SqlCeConnection
    Dim sqlce As New SqlCeCommandBuilder

    'Declare CONSTANTS
    Const rangebound1 As Decimal = 18.5D
    Const rangebound2 As Decimal = 25D
    Const impconstant As Decimal = 703D
    Const man_Normal As Decimal = 66.47D
    Const man_Weight As Decimal = 13.75D
    Const man_Height As Decimal = 5.003D
    Const man_Age As Decimal = 6.755D


    Const woman_Normal As Decimal = 655.0955D
    Const woman_Weight As Decimal = 9.5634D
    Const woman_Height As Decimal = 1.8496D
    Const woman_Age As Decimal = 6.755D


    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'default window

        ImperialRBtn1.AutoCheck = True
        MetricRBtn.AutoCheck = True
        AgeTxt.Enabled = False
        WeightTxt.Enabled = False
        HeightTxt.Enabled = False
        BMITextBox.Enabled = False
        ResultTxt.Enabled = False
        CalculateBtn.Enabled = False
        ClearBtn.Enabled = False
    End Sub


    Private Sub ExitBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitBtn.Click
        Me.Close()
    End Sub

    Private Sub MetricRBtn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MetricRBtn.CheckedChanged
        WeightUnitLbl.Text = "Kilograms"
        HeightUnitLbl.Text = "Meters"
        AgeTxt.Enabled = True
        WeightTxt.Enabled = True
        HeightTxt.Enabled = True
        CalculateBtn.Enabled = True
        ClearBtn.Enabled = True
    End Sub

    Private Sub CloseRBtn_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseRBtn.CheckedChanged
        AgeTxt.Enabled = False
        WeightTxt.Enabled = False
        HeightTxt.Enabled = False
        CalculateBtn.Enabled = False
        ClearBtn.Enabled = False
        WeightUnitLbl.Text = "..."
        HeightUnitLbl.Text = "..."
    End Sub

    Private Sub CalculateBtn_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculateBtn.Click
        Dim WeightDecimal, HeightDecimal, BMIDecimal As Decimal

        If (MetricRBtn.Checked = True Or ImperialRBtn1.Checked = True) Then                'check whether radiobutton are checked 

            If (MetricRBtn.Checked = True) Then                                      'check metric radio button is checked



                If (WeightTxt.Text <> "" Or HeightTxt.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                    Try

                        WeightDecimal = Decimal.Parse(WeightTxt.Text)                   'assign variable to the textbox
                        HeightDecimal = Decimal.Parse(HeightTxt.Text)                   'assign variable to the textbox
                        If (WeightDecimal > 0) Then                                         'check for positive weight value
                            If (HeightDecimal > 0) Then                                     'check for positive weight value

                                BMIDecimal = (WeightDecimal) / (HeightDecimal * HeightDecimal)                  'logic for metric calculations
                                BMITextBox.Text = BMIDecimal.ToString("f")
                                If (BMIDecimal < rangebound1) Then                                              'logic for BMI scale
                                    ResultTxt.Text = "Underweight"
                                    frmMetricUWResult.Show()
                                    frmMetricUWResult.DisplayResultLbl.Text = BMIDecimal.ToString("f")
                                ElseIf (BMIDecimal > rangebound1 And BMIDecimal < rangebound2) Then
                                    ResultTxt.Text = "Normal"
                                    frmMetricNWResult.Show()
                                    frmMetricNWResult.DisplayResultLbl.Text = BMIDecimal.ToString("f")
                                Else
                                    ResultTxt.Text = "Overweight"
                                    frmMetricOWResult.Show()
                                    frmMetricOWResult.DisplayResultLbl.Text = BMIDecimal.ToString("f")
                                End If

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightTxt
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightTxt
                                .SelectAll()
                                .Focus()
                            End With
                        End If

                    Catch QuantityFormatException As FormatException
                        MessageBox.Show("Quantity MUST be a numeric value", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Catch QuantityOverflowException As OverflowException
                        MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    Catch AnException As Exception
                        MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                    End Try
                Else
                    MessageBox.Show("Please input a positive values of weight and height. CANNOT leave it blank",
                                   "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    WeightTxt.Focus()
                End If


            Else
                MessageBox.Show("Please input a positive values of weight and height. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightTxt.Focus()


            End If
        End If

    End Sub

    Private Sub ClearBtn_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearBtn.Click
        HeightTxt1.Text = ""
        WeightUnitLbl1.Text = ""
        WeightTxt1.Text = ""
        BMITextBox1.Text = ""
        ResultTxt1.Text = ""
        ImperialRBtn1.Checked = False
        AgeTxt1.Enabled = False
        WeightTxt1.Enabled = False
        HeightTxt1.Enabled = False
    End Sub

    Private Sub CloseRBtn1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseRBtn1.CheckedChanged
        AgeTxt1.Enabled = False
        WeightTxt1.Enabled = False
        HeightTxt1.Enabled = False
        CalculateBtn1.Enabled = False
        ClearBtn1.Enabled = False
        WeightUnitLbl1.Text = "..."
        HeightUnitLbl1.Text = "..."
    End Sub

    Private Sub ImperialRBtn1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImperialRBtn1.CheckedChanged
        WeightUnitLbl1.Text = "Pounds"
        HeightUnitLbl1.Text = "Inches"
        AgeTxt1.Enabled = True
        WeightTxt1.Enabled = True
        HeightTxt1.Enabled = True
        CalculateBtn1.Enabled = True
        ClearBtn1.Enabled = True
    End Sub

    Private Sub ClearBtn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearBtn1.Click
        HeightTxt1.Text = ""
        WeightUnitLbl1.Text = ""
        WeightTxt1.Text = ""
        BMITextBox1.Text = ""
        ResultTxt1.Text = ""
        ImperialRBtn1.Checked = False
        AgeTxt1.Enabled = False
        WeightTxt1.Enabled = False
        HeightTxt1.Enabled = False
    End Sub

    Private Sub CalculateBtn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalculateBtn1.Click
        Dim WeightDecimal, HeightDecimal, BMIDecimal As Decimal
        If (ImperialRBtn1.Checked = True) Then                                'check whether imperial radiobutton is checked


            If (WeightTxt1.Text <> "" Or HeightTxt1.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightTxt1.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightTxt1.Text)                   'assign variable to the textbox
                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value

                            BMIDecimal = (WeightDecimal * impconstant) / (HeightDecimal * HeightDecimal)            'logic for Imperial calculations
                            BMITextBox1.Text = BMIDecimal.ToString("f")
                            If (BMIDecimal < rangebound1) Then                                      'logic for BMI Scale calculations
                                ResultTxt1.Text = "Underweight"
                                frmImperialUWResult.Show()
                                frmImperialUWResult.DisplayResultLbl.Text = BMIDecimal.ToString("f")
                            ElseIf (BMIDecimal > rangebound1 And BMIDecimal < rangebound2) Then
                                ResultTxt1.Text = "Normal"
                                frmImperialNWResult.Show()
                                frmImperialNWResult.DisplayResultLbl.Text = BMIDecimal.ToString("f")
                            Else
                                ResultTxt1.Text = "Overweight"
                                frmImperialOWResult.Show()
                                frmImperialOWResult.DisplayResultLbl.Text = BMIDecimal.ToString("f")
                            End If

                        Else

                            MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With WeightTxt1
                                .SelectAll()
                                .Focus()
                            End With
                        End If

                    Else
                        MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With HeightTxt1
                            .SelectAll()
                            .Focus()
                        End With
                    End If

                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Quantity MUST be a numeric value", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            End If

        Else
            MessageBox.Show("Please input a positive values of weight and height. CANNOT leave it blank",
                           "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            WeightTxt1.Focus()


        End If

    End Sub

  

    Private Sub btnCalculateBMR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateBMR.Click
        Dim WeightDecimal, HeightDecimal, AgeDecimal, BMRDecimal, calorie_requirement As Decimal
        Dim rb1 = 1.2D
        Dim rb2 = 1.357D
        Dim rb3 = 1.55D
        Dim rb4 = 1.725D
        Dim rb5 = 1.9D


        If (RadioBtn1.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt.Text <> "" Or HeightBmrTxt.Text <> "" Or AgeBmrTxt.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = man_Normal + (man_Weight * WeightDecimal) + (man_Height * HeightDecimal) - (man_Age * AgeDecimal)
                                BMRTextBox.Text = BMRDecimal.ToString("f")

                                calorie_requirement = BMRDecimal * rb1
                                CR.Text = calorie_requirement.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am sedentary! Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If


        End If
       






        ''''''''''''''''''''''''''''''''''''''''''''''''''

        If (RadioBtn2.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt.Text <> "" Or HeightBmrTxt.Text <> "" Or AgeBmrTxt.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = man_Normal + (man_Weight * WeightDecimal) + (man_Height * HeightDecimal) - (man_Age * AgeDecimal)
                                BMRTextBox.Text = BMRDecimal.ToString("f")

                                calorie_requirement = BMRDecimal * rb2
                                CR.Text = calorie_requirement.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am lightly active!  Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''''''

        If (RadioBtn3.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt.Text <> "" Or HeightBmrTxt.Text <> "" Or AgeBmrTxt.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = man_Normal + (man_Weight * WeightDecimal) + (man_Height * HeightDecimal) - (man_Age * AgeDecimal)
                                BMRTextBox.Text = BMRDecimal.ToString("f")

                                calorie_requirement = BMRDecimal * rb3
                                CR.Text = calorie_requirement.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am moderately active!  Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''''''

        If (RadioBtn4.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt.Text <> "" Or HeightBmrTxt.Text <> "" Or AgeBmrTxt.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = man_Normal + (man_Weight * WeightDecimal) + (man_Height * HeightDecimal) - (man_Age * AgeDecimal)
                                BMRTextBox.Text = BMRDecimal.ToString("f")

                                calorie_requirement = BMRDecimal * rb4
                                CR.Text = calorie_requirement.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am very active! Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''

        If (RadioBtn5.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt.Text <> "" Or HeightBmrTxt.Text <> "" Or AgeBmrTxt.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = man_Normal + (man_Weight * WeightDecimal) + (man_Height * HeightDecimal) - (man_Age * AgeDecimal)
                                BMRTextBox.Text = BMRDecimal.ToString("f")

                                calorie_requirement = BMRDecimal * rb5
                                CR.Text = calorie_requirement.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am super active! Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If
        End If

    End Sub

    
    Private Sub btnCalculateBMRClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateBMRClear.Click
        WeightBmrTxt.Text = ""
        HeightBmrTxt.Text = ""
        AgeBmrTxt.Text = ""
    End Sub

    Private Sub btnCalculateBMR2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateBMR2.Click
        Dim WeightDecimal, HeightDecimal, AgeDecimal, BMRDecimal, calorie_requirement2 As Decimal
        Dim rb1 = 1.2D
        Dim rb2 = 1.357D
        Dim rb3 = 1.55D
        Dim rb4 = 1.725D
        Dim rb5 = 1.9D


        If (RadioBtn12.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt2.Text <> "" Or HeightBmrTxt2.Text <> "" Or AgeBmrTxt2.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt2.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt2.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt2.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = woman_Normal + (woman_Weight * WeightDecimal) + (woman_Height * HeightDecimal) - (woman_Age * AgeDecimal)
                                BMRTextBox2.Text = BMRDecimal.ToString("f")

                                calorie_requirement2 = BMRDecimal * rb1
                                CR2.Text = calorie_requirement2.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am sedentary! Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If


        End If







        ''''''''''''''''''''''''''''''''''''''''''''''''''

        If (RadioBtn22.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt2.Text <> "" Or HeightBmrTxt2.Text <> "" Or AgeBmrTxt2.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt2.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt2.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt2.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = man_Normal + (woman_Weight * WeightDecimal) + (woman_Height * HeightDecimal) - (woman_Age * AgeDecimal)
                                BMRTextBox2.Text = BMRDecimal.ToString("f")

                                calorie_requirement2 = BMRDecimal * rb2
                                CR2.Text = calorie_requirement2.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt2
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt2
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt2
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am lightly active!  Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt2.Focus()
            End If
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''''''

        If (RadioBtn32.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt2.Text <> "" Or HeightBmrTxt2.Text <> "" Or AgeBmrTxt2.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt2.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt2.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt2.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = woman_Normal + (woman_Weight * WeightDecimal) + (woman_Height * HeightDecimal) - (woman_Age * AgeDecimal)
                                BMRTextBox2.Text = BMRDecimal.ToString("f")

                                calorie_requirement2 = BMRDecimal * rb3
                                CR2.Text = calorie_requirement2.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt2
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt2
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt2
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am moderately active!  Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''''''

        If (RadioBtn42.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt2.Text <> "" Or HeightBmrTxt2.Text <> "" Or AgeBmrTxt2.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt2.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt2.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt2.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = man_Normal + (woman_Weight * WeightDecimal) + (woman_Height * HeightDecimal) - (woman_Age * AgeDecimal)
                                BMRTextBox2.Text = BMRDecimal.ToString("f")

                                calorie_requirement2 = BMRDecimal * rb4
                                CR2.Text = calorie_requirement2.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt2
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt2
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt2
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am very active! Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''

        If (RadioBtn52.Checked = True) Then                'check whether radiobutton are checked 

            'check metric radio button is checked



            If (WeightBmrTxt2.Text <> "" Or HeightBmrTxt2.Text <> "" Or AgeBmrTxt2.Text <> "") Then              'chech whether HeightTextBox and weightTextBox are filled
                Try

                    WeightDecimal = Decimal.Parse(WeightBmrTxt2.Text)                   'assign variable to the textbox
                    HeightDecimal = Decimal.Parse(HeightBmrTxt2.Text)                   'assign variable to the textbox
                    AgeDecimal = Decimal.Parse(AgeBmrTxt2.Text)

                    If (WeightDecimal > 0) Then                                         'check for positive weight value
                        If (HeightDecimal > 0) Then                                     'check for positive weight value
                            If (AgeDecimal > 0) Then
                                BMRDecimal = woman_Normal + (woman_Weight * WeightDecimal) + (woman_Height * HeightDecimal) - (woman_Age * AgeDecimal)
                                BMRTextBox2.Text = BMRDecimal.ToString("f")

                                calorie_requirement2 = BMRDecimal * rb5
                                CR2.Text = calorie_requirement2.ToString("f")

                            Else

                                MessageBox.Show("Height value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                With WeightBmrTxt
                                    .SelectAll()
                                    .Focus()
                                End With
                            End If

                        Else
                            MessageBox.Show("Weight value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            With HeightBmrTxt
                                .SelectAll()
                                .Focus()
                            End With
                        End If
                    Else
                        MessageBox.Show("Age value MUST be a positive number greater than 0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        With AgeBmrTxt
                            .SelectAll()
                            .Focus()
                        End With
                    End If
                Catch QuantityFormatException As FormatException
                    MessageBox.Show("Weight, Height and Age MUST be a numeric value, and no field can be leaft blank", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch QuantityOverflowException As OverflowException
                    MessageBox.Show("Quantity value is out of limits", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Catch AnException As Exception
                    MessageBox.Show("Error:" & AnException.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)



                End Try
            Else
                MessageBox.Show("You selected I am super active! Please input a positive values of weight, height and age. CANNOT leave it blank",
                               "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WeightBmrTxt.Focus()
            End If
        End If
    End Sub

    Private Sub brmAnalysisBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BMRAnalysisBtn.Click
        BMI.Visible = False
        BMR.Visible = True


    End Sub

    Private Sub BMIAnalysisBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BMIAnalysisBtn.Click
        BMI.Visible = True
        BMR.Visible = False
    End Sub

    Private Sub btnCalculateBMRClear2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculateBMRClear2.Click
        WeightBmrTxt2.Text = ""
        HeightBmrTxt2.Text = ""
        AgeBmrTxt2.Text = ""
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        frmAbout.Show()

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        frmAFAQ.Show()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmHelp.Show()

    End Sub
End Class