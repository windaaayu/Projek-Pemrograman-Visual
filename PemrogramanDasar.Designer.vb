<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PemrogramanDasar
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.grpJawaban = New System.Windows.Forms.GroupBox()
        Me.rbB = New System.Windows.Forms.RadioButton()
        Me.rbC = New System.Windows.Forms.RadioButton()
        Me.rbD = New System.Windows.Forms.RadioButton()
        Me.rbA = New System.Windows.Forms.RadioButton()
        Me.lblSoal = New System.Windows.Forms.Label()
        Me.lblNomor = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.grpJawaban.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.lblTimer)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.grpJawaban)
        Me.Panel1.Controls.Add(Me.lblSoal)
        Me.Panel1.Controls.Add(Me.lblNomor)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(605, 450)
        Me.Panel1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.ProjekQuiz.My.Resources.Resources.ad2df776_1c6f_4271_be64_c3cc9b71d1c9
        Me.Panel2.Controls.Add(Me.btnNext)
        Me.Panel2.Controls.Add(Me.btnBack)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 338)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(605, 112)
        Me.Panel2.TabIndex = 6
        '
        'btnNext
        '
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(480, 50)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = "NEXT"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(51, 50)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(75, 23)
        Me.btnBack.TabIndex = 0
        Me.btnBack.Text = "BACK"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'grpJawaban
        '
        Me.grpJawaban.BackColor = System.Drawing.Color.Silver
        Me.grpJawaban.Controls.Add(Me.rbB)
        Me.grpJawaban.Controls.Add(Me.rbC)
        Me.grpJawaban.Controls.Add(Me.rbD)
        Me.grpJawaban.Controls.Add(Me.rbA)
        Me.grpJawaban.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpJawaban.Location = New System.Drawing.Point(22, 144)
        Me.grpJawaban.Name = "grpJawaban"
        Me.grpJawaban.Size = New System.Drawing.Size(533, 169)
        Me.grpJawaban.TabIndex = 5
        Me.grpJawaban.TabStop = False
        Me.grpJawaban.Text = "Pilih Jawaban"
        '
        'rbB
        '
        Me.rbB.AutoSize = True
        Me.rbB.Location = New System.Drawing.Point(7, 64)
        Me.rbB.Name = "rbB"
        Me.rbB.Size = New System.Drawing.Size(83, 17)
        Me.rbB.TabIndex = 3
        Me.rbB.TabStop = True
        Me.rbB.Text = "B. Pilihan B"
        Me.rbB.UseVisualStyleBackColor = True
        '
        'rbC
        '
        Me.rbC.AutoSize = True
        Me.rbC.Location = New System.Drawing.Point(7, 96)
        Me.rbC.Name = "rbC"
        Me.rbC.Size = New System.Drawing.Size(83, 17)
        Me.rbC.TabIndex = 2
        Me.rbC.TabStop = True
        Me.rbC.Text = "C. Pilihan C"
        Me.rbC.UseVisualStyleBackColor = True
        '
        'rbD
        '
        Me.rbD.AutoSize = True
        Me.rbD.Location = New System.Drawing.Point(7, 132)
        Me.rbD.Name = "rbD"
        Me.rbD.Size = New System.Drawing.Size(85, 17)
        Me.rbD.TabIndex = 1
        Me.rbD.TabStop = True
        Me.rbD.Text = "D. Pilihan D"
        Me.rbD.UseVisualStyleBackColor = True
        '
        'rbA
        '
        Me.rbA.AutoSize = True
        Me.rbA.Location = New System.Drawing.Point(7, 30)
        Me.rbA.Name = "rbA"
        Me.rbA.Size = New System.Drawing.Size(83, 17)
        Me.rbA.TabIndex = 0
        Me.rbA.TabStop = True
        Me.rbA.Text = "A. Pilihan A"
        Me.rbA.UseVisualStyleBackColor = True
        '
        'lblSoal
        '
        Me.lblSoal.AutoSize = True
        Me.lblSoal.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoal.Location = New System.Drawing.Point(27, 112)
        Me.lblSoal.Name = "lblSoal"
        Me.lblSoal.Size = New System.Drawing.Size(167, 13)
        Me.lblSoal.TabIndex = 4
        Me.lblSoal.Text = "Pertanyaan akan muncul disini."
        '
        'lblNomor
        '
        Me.lblNomor.AutoSize = True
        Me.lblNomor.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNomor.Location = New System.Drawing.Point(27, 80)
        Me.lblNomor.Name = "lblNomor"
        Me.lblNomor.Size = New System.Drawing.Size(55, 13)
        Me.lblNomor.TabIndex = 3
        Me.lblNomor.Text = "Soal 1/10"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(344, 37)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Quiz Pemrograman Dasar"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(384, 80)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(171, 23)
        Me.ProgressBar1.TabIndex = 8
        '
        'lblTimer
        '
        Me.lblTimer.AutoSize = True
        Me.lblTimer.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimer.Location = New System.Drawing.Point(499, 56)
        Me.lblTimer.Name = "lblTimer"
        Me.lblTimer.Size = New System.Drawing.Size(28, 21)
        Me.lblTimer.TabIndex = 9
        Me.lblTimer.Text = "30"
        '
        'PemrogramanDasar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "PemrogramanDasar"
        Me.Text = "PemrogramanDasar"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.grpJawaban.ResumeLayout(False)
        Me.grpJawaban.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnNext As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents grpJawaban As GroupBox
    Friend WithEvents rbB As RadioButton
    Friend WithEvents rbC As RadioButton
    Friend WithEvents rbD As RadioButton
    Friend WithEvents rbA As RadioButton
    Friend WithEvents lblSoal As Label
    Friend WithEvents lblNomor As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents lblTimer As Label
    Friend WithEvents Timer1 As Timer
End Class
