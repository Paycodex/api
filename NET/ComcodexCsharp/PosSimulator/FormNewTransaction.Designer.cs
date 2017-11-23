/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 24/09/2014
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PosSimulator
{
	partial class FormNewTransaction
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewTransaction));
            this.groupBoxNewTransaction = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAmount = new System.Windows.Forms.Label();
            this.labelConcept = new System.Windows.Forms.Label();
            this.textBoxConcept = new System.Windows.Forms.TextBox();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBoxNewTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxNewTransaction
            // 
            this.groupBoxNewTransaction.Controls.Add(this.label3);
            this.groupBoxNewTransaction.Controls.Add(this.comboBoxDevice);
            this.groupBoxNewTransaction.Controls.Add(this.label1);
            this.groupBoxNewTransaction.Controls.Add(this.labelAmount);
            this.groupBoxNewTransaction.Controls.Add(this.labelConcept);
            this.groupBoxNewTransaction.Controls.Add(this.textBoxConcept);
            this.groupBoxNewTransaction.Controls.Add(this.textBoxAmount);
            this.groupBoxNewTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxNewTransaction.Location = new System.Drawing.Point(12, 93);
            this.groupBoxNewTransaction.Name = "groupBoxNewTransaction";
            this.groupBoxNewTransaction.Size = new System.Drawing.Size(547, 261);
            this.groupBoxNewTransaction.TabIndex = 8;
            this.groupBoxNewTransaction.TabStop = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "Caja Registradora";
            // 
            // comboBoxDevice
            // 
            this.comboBoxDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxDevice.FormattingEnabled = true;
            this.comboBoxDevice.Items.AddRange(new object[] {
            "Caja-0001-01",
            "Caja-0002-01",
            "Caja-0003-02"});
            this.comboBoxDevice.Location = new System.Drawing.Point(17, 60);
            this.comboBoxDevice.Name = "comboBoxDevice";
            this.comboBoxDevice.Size = new System.Drawing.Size(514, 24);
            this.comboBoxDevice.TabIndex = 14;
            this.comboBoxDevice.Text = "Seleccione la caja .... ";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(159, -25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(356, 29);
            this.label1.TabIndex = 13;
            this.label1.Text = "ABRIENDO NUEVA TRANSACCIÓN";
            // 
            // labelAmount
            // 
            this.labelAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmount.Location = new System.Drawing.Point(17, 108);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(111, 17);
            this.labelAmount.TabIndex = 4;
            this.labelAmount.Text = "Cantidad";
            // 
            // labelConcept
            // 
            this.labelConcept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConcept.Location = new System.Drawing.Point(17, 170);
            this.labelConcept.Name = "labelConcept";
            this.labelConcept.Size = new System.Drawing.Size(138, 19);
            this.labelConcept.TabIndex = 2;
            this.labelConcept.Text = "Concepto";
            // 
            // textBoxConcept
            // 
            this.textBoxConcept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxConcept.Location = new System.Drawing.Point(17, 192);
            this.textBoxConcept.Multiline = true;
            this.textBoxConcept.Name = "textBoxConcept";
            this.textBoxConcept.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxConcept.Size = new System.Drawing.Size(514, 54);
            this.textBoxConcept.TabIndex = 1;
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAmount.Location = new System.Drawing.Point(17, 128);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(514, 22);
            this.textBoxAmount.TabIndex = 3;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNew.Location = new System.Drawing.Point(241, 365);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 41);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "Guardar";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.BtnNewClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(284, 56);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 23);
            this.label2.TabIndex = 12;
            this.label2.Text = "Abriendo Transacción";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorkerProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerRunWorkerCompleted);
            // 
            // FormNewTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 418);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.groupBoxNewTransaction);
            this.Controls.Add(this.pictureBox2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(597, 456);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(597, 456);
            this.Name = "FormNewTransaction";
            this.Text = "frmNewTransaction";
            this.Load += new System.EventHandler(this.FormNewTransaction_Load);
            this.groupBoxNewTransaction.ResumeLayout(false);
            this.groupBoxNewTransaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

		}
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.ComboBox comboBoxDevice;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.TextBox textBoxAmount;
		private System.Windows.Forms.Label labelConcept;
		private System.Windows.Forms.TextBox textBoxConcept;
		private System.Windows.Forms.Label labelAmount;
		private System.Windows.Forms.GroupBox groupBoxNewTransaction;
	}
}
