Imports System.Math
Public Class Form1



    Public conex As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=estudiantes.accdb")
    Public com As New OleDb.OleDbCommand

    Private Sub ConectarDB()
        Try
            conex.Open()
            com.Connection = conex
            com.CommandType = CommandType.Text
            Console.WriteLine("Conexion con la base de datos correcta.")
        Catch ex As Exception

            If Err.Number = 5 Then
                MsgBox("Error al conectar la base de datos." & Err.Description)
            End If

        End Try
    End Sub


    Private Sub refrescarDatos()

        Dim sqlActualizado As String = "SELECT * FROM Notas"
        Dim adapt As New OleDb.OleDbDataAdapter(sqlActualizado, conex)
        Dim obtenerDatos As New DataSet
        adapt.Fill(obtenerDatos, "Id")

        dataGridNotas.DataSource = obtenerDatos
        dataGridNotas.DataMember = "Id"


    End Sub


    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim sql, ID, Nombre, Apellido, nota1, nota2, promedio As String


        ID = txtID.Text
        Nombre = txtNombre.Text
        Apellido = txtApellido.Text
        nota1 = txtparcial1.Text
        nota2 = txtparcial2.Text
        promedio = txtPromedio.Text

        sql = String.Format("INSERT INTO Notas(Id,nombre,apellido,Iparcial,IIparcial,promedio) VALUES({0},'{1}','{2}',{3},{4},{5})", ID, Nombre, Apellido, nota1, nota2, promedio)


        com.CommandText = sql

        Try
            com.ExecuteNonQuery()
            refrescarDatos()
            MsgBox("Registro ID: " & ID & " guardado correctamente.")

        Catch ex As Exception
            MsgBox(Err.Description)
        End Try

        Console.WriteLine(sql)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConectarDB()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim idABuscar As String = txtID.Text
        If idABuscar = "" Then
            MsgBox("¡Se necesita un ID!")
            Return
        End If
        Dim sqlActualizado As String = "SELECT * FROM Notas WHERE Id = " & txtID.Text & ""
        Dim adapt As New OleDb.OleDbDataAdapter(sqlActualizado, conex)
        Dim ds As New DataSet
        adapt.Fill(ds)

        If ds.Tables(0).Rows.Count = 0 Then
            MsgBox("No se encuentra ningun registro en la base de datos")
            Return

        End If

        Dim dato As DataRow = ds.Tables(0).Rows(0)


        txtID.Text = dato("Id").ToString()
        txtNombre.Text = dato("nombre").ToString()
        txtApellido.Text = dato("apellido").ToString()
        txtparcial1.Text = dato("Iparcial").ToString()
        txtparcial2.Text = dato("IIparcial").ToString()
        txtPromedio.Text = dato("promedio").ToString()












    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        conex.Close()
        Application.Exit()
    End Sub

    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
        Dim prom, nota1, nota2 As Integer
        nota1 = Val(txtparcial1.Text)
        nota2 = Val(txtparcial2.Text)

        prom = Round((nota1 + nota2)) / 2

        txtPromedio.Text = prom.ToString()

    End Sub


    'Oscar Fabian Lopez Raudales
End Class
