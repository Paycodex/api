/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 22/09/2014
 * Time: 9:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PosSimulator
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.listBoxTransactions = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonRevert = new System.Windows.Forms.Button();
			this.pictureBoxQR = new System.Windows.Forms.PictureBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label1ValueConcept = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.labelValuePayedIdentidy = new System.Windows.Forms.Label();
			this.labelValuePayedCardNumber = new System.Windows.Forms.Label();
			this.labelValuePayedCardHolder = new System.Windows.Forms.Label();
			this.labelValuePayedDate = new System.Windows.Forms.Label();
			this.labelValueStatus = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelValueAmount = new System.Windows.Forms.Label();
			this.labelValueDevice = new System.Windows.Forms.Label();
			this.labelValueOpenDate = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textBoxDateEndHour = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxDateInitialHour = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.textBoxDateInitial = new System.Windows.Forms.TextBox();
			this.textBoxDateEnd = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.btnConsult = new System.Windows.Forms.Button();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.buttonLoadBankProfiles = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// listBoxTransactions
			// 
			this.listBoxTransactions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBoxTransactions.FormattingEnabled = true;
			this.listBoxTransactions.ItemHeight = 16;
			this.listBoxTransactions.Location = new System.Drawing.Point(20, 179);
			this.listBoxTransactions.Name = "listBoxTransactions";
			this.listBoxTransactions.Size = new System.Drawing.Size(365, 260);
			this.listBoxTransactions.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox2.Controls.Add(this.buttonRevert);
			this.groupBox2.Controls.Add(this.pictureBoxQR);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(516, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(455, 912);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "FILTRAR";
			// 
			// buttonRevert
			// 
			this.buttonRevert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonRevert.Location = new System.Drawing.Point(330, 309);
			this.buttonRevert.Name = "buttonRevert";
			this.buttonRevert.Size = new System.Drawing.Size(112, 28);
			this.buttonRevert.TabIndex = 12;
			this.buttonRevert.Text = "REVERTIR";
			this.buttonRevert.UseVisualStyleBackColor = true;
			this.buttonRevert.Click += new System.EventHandler(this.buttonRevert_Click);
			// 
			// pictureBoxQR
			// 
			this.pictureBoxQR.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.pictureBoxQR.Location = new System.Drawing.Point(90, 22);
			this.pictureBoxQR.Name = "pictureBoxQR";
			this.pictureBoxQR.Size = new System.Drawing.Size(269, 269);
			this.pictureBoxQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxQR.TabIndex = 11;
			this.pictureBoxQR.TabStop = false;
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.Location = new System.Drawing.Point(181, 309);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(112, 28);
			this.button2.TabIndex = 10;
			this.button2.Text = "ACTUALIZAR";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.updateCurrentTransactionClick);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(32, 309);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 28);
			this.button1.TabIndex = 9;
			this.button1.Text = "CREAR";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label1ValueConcept);
			this.panel1.Controls.Add(this.label16);
			this.panel1.Controls.Add(this.labelValuePayedIdentidy);
			this.panel1.Controls.Add(this.labelValuePayedCardNumber);
			this.panel1.Controls.Add(this.labelValuePayedCardHolder);
			this.panel1.Controls.Add(this.labelValuePayedDate);
			this.panel1.Controls.Add(this.labelValueStatus);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.labelValueAmount);
			this.panel1.Controls.Add(this.labelValueDevice);
			this.panel1.Controls.Add(this.labelValueOpenDate);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Location = new System.Drawing.Point(26, 359);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(421, 397);
			this.panel1.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(136, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 29;
			this.label2.Text = "OPERACION";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(136, 214);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 28;
			this.label1.Text = "DATOS PAGO";
			// 
			// label1ValueConcept
			// 
			this.label1ValueConcept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1ValueConcept.Location = new System.Drawing.Point(136, 167);
			this.label1ValueConcept.Name = "label1ValueConcept";
			this.label1ValueConcept.Size = new System.Drawing.Size(244, 19);
			this.label1ValueConcept.TabIndex = 27;
			this.label1ValueConcept.Text = "Concept";
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(4, 164);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(111, 18);
			this.label16.TabIndex = 26;
			this.label16.Text = "CONCEPTO:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelValuePayedIdentidy
			// 
			this.labelValuePayedIdentidy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelValuePayedIdentidy.Location = new System.Drawing.Point(136, 336);
			this.labelValuePayedIdentidy.Name = "labelValuePayedIdentidy";
			this.labelValuePayedIdentidy.Size = new System.Drawing.Size(205, 23);
			this.labelValuePayedIdentidy.TabIndex = 25;
			this.labelValuePayedIdentidy.Text = "labelValuePayedIdentidy";
			// 
			// labelValuePayedCardNumber
			// 
			this.labelValuePayedCardNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelValuePayedCardNumber.Location = new System.Drawing.Point(136, 304);
			this.labelValuePayedCardNumber.Name = "labelValuePayedCardNumber";
			this.labelValuePayedCardNumber.Size = new System.Drawing.Size(203, 23);
			this.labelValuePayedCardNumber.TabIndex = 24;
			this.labelValuePayedCardNumber.Text = "labelValuePayedCardNumber";
			// 
			// labelValuePayedCardHolder
			// 
			this.labelValuePayedCardHolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelValuePayedCardHolder.Location = new System.Drawing.Point(136, 274);
			this.labelValuePayedCardHolder.Name = "labelValuePayedCardHolder";
			this.labelValuePayedCardHolder.Size = new System.Drawing.Size(203, 23);
			this.labelValuePayedCardHolder.TabIndex = 23;
			this.labelValuePayedCardHolder.Text = "labelValuePayedCardHolder";
			// 
			// labelValuePayedDate
			// 
			this.labelValuePayedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelValuePayedDate.Location = new System.Drawing.Point(136, 246);
			this.labelValuePayedDate.Name = "labelValuePayedDate";
			this.labelValuePayedDate.Size = new System.Drawing.Size(203, 23);
			this.labelValuePayedDate.TabIndex = 22;
			this.labelValuePayedDate.Text = "labelValuePayedDate";
			// 
			// labelValueStatus
			// 
			this.labelValueStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelValueStatus.Location = new System.Drawing.Point(136, 50);
			this.labelValueStatus.Name = "labelValueStatus";
			this.labelValueStatus.Size = new System.Drawing.Size(200, 18);
			this.labelValueStatus.TabIndex = 20;
			this.labelValueStatus.Text = "labelValueStatus";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(4, 333);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(109, 18);
			this.label13.TabIndex = 18;
			this.label13.Text = "CI PAGADOR:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label12.Location = new System.Drawing.Point(4, 304);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(109, 18);
			this.label12.TabIndex = 17;
			this.label12.Text = "Nº TARJETA:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label11.Location = new System.Drawing.Point(4, 274);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(109, 18);
			this.label11.TabIndex = 16;
			this.label11.Text = "PAGADOR:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label10.Location = new System.Drawing.Point(4, 246);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(109, 18);
			this.label10.TabIndex = 15;
			this.label10.Text = "FECHA:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(49, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90, 18);
			this.label4.TabIndex = 13;
			this.label4.Text = "ESTADO:";
			// 
			// labelValueAmount
			// 
			this.labelValueAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelValueAmount.Location = new System.Drawing.Point(136, 133);
			this.labelValueAmount.Name = "labelValueAmount";
			this.labelValueAmount.Size = new System.Drawing.Size(191, 23);
			this.labelValueAmount.TabIndex = 11;
			this.labelValueAmount.Text = "labelValueAmount";
			// 
			// labelValueDevice
			// 
			this.labelValueDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelValueDevice.Location = new System.Drawing.Point(136, 108);
			this.labelValueDevice.Name = "labelValueDevice";
			this.labelValueDevice.Size = new System.Drawing.Size(117, 23);
			this.labelValueDevice.TabIndex = 9;
			this.labelValueDevice.Text = "labelValueDevice";
			// 
			// labelValueOpenDate
			// 
			this.labelValueOpenDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelValueOpenDate.Location = new System.Drawing.Point(136, 81);
			this.labelValueOpenDate.Name = "labelValueOpenDate";
			this.labelValueOpenDate.Size = new System.Drawing.Size(117, 18);
			this.labelValueOpenDate.TabIndex = 8;
			this.labelValueOpenDate.Text = "labelValueOpenDate";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(4, 133);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111, 18);
			this.label7.TabIndex = 5;
			this.label7.Text = "CANTIDAD:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(4, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(109, 18);
			this.label3.TabIndex = 1;
			this.label3.Text = "FECHA:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(4, 108);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(111, 18);
			this.label5.TabIndex = 3;
			this.label5.Text = "DISPOSITIVO:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonOpen
			// 
			this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonOpen.Location = new System.Drawing.Point(447, 454);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(47, 45);
			this.buttonOpen.TabIndex = 7;
			this.buttonOpen.Text = ">>";
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.ButtonOpenClick);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.groupBox5);
			this.groupBox6.Controls.Add(this.listBoxTransactions);
			this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox6.Location = new System.Drawing.Point(29, 234);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(401, 690);
			this.groupBox6.TabIndex = 9;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Transacciones";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.label9);
			this.groupBox5.Controls.Add(this.textBoxDateEndHour);
			this.groupBox5.Controls.Add(this.label8);
			this.groupBox5.Controls.Add(this.textBoxDateInitialHour);
			this.groupBox5.Controls.Add(this.label14);
			this.groupBox5.Controls.Add(this.textBoxDateInitial);
			this.groupBox5.Controls.Add(this.textBoxDateEnd);
			this.groupBox5.Controls.Add(this.label15);
			this.groupBox5.Controls.Add(this.btnConsult);
			this.groupBox5.Location = new System.Drawing.Point(17, 37);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(365, 136);
			this.groupBox5.TabIndex = 9;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Filtros de Fecha";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(184, 72);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(38, 22);
			this.label9.TabIndex = 10;
			this.label9.Text = "Hora";
			// 
			// textBoxDateEndHour
			// 
			this.textBoxDateEndHour.Location = new System.Drawing.Point(221, 72);
			this.textBoxDateEndHour.Name = "textBoxDateEndHour";
			this.textBoxDateEndHour.Size = new System.Drawing.Size(84, 22);
			this.textBoxDateEndHour.TabIndex = 9;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(184, 28);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(38, 22);
			this.label8.TabIndex = 8;
			this.label8.Text = "Hora";
			// 
			// textBoxDateInitialHour
			// 
			this.textBoxDateInitialHour.Location = new System.Drawing.Point(221, 28);
			this.textBoxDateInitialHour.Name = "textBoxDateInitialHour";
			this.textBoxDateInitialHour.Size = new System.Drawing.Size(84, 22);
			this.textBoxDateInitialHour.TabIndex = 7;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(50, 28);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(38, 22);
			this.label14.TabIndex = 5;
			this.label14.Text = "Inicio";
			// 
			// textBoxDateInitial
			// 
			this.textBoxDateInitial.Location = new System.Drawing.Point(94, 28);
			this.textBoxDateInitial.Name = "textBoxDateInitial";
			this.textBoxDateInitial.Size = new System.Drawing.Size(84, 22);
			this.textBoxDateInitial.TabIndex = 2;
			// 
			// textBoxDateEnd
			// 
			this.textBoxDateEnd.Location = new System.Drawing.Point(94, 72);
			this.textBoxDateEnd.Name = "textBoxDateEnd";
			this.textBoxDateEnd.Size = new System.Drawing.Size(84, 22);
			this.textBoxDateEnd.TabIndex = 3;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(62, 72);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(31, 22);
			this.label15.TabIndex = 6;
			this.label15.Text = "Fin";
			// 
			// btnConsult
			// 
			this.btnConsult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnConsult.Location = new System.Drawing.Point(130, 108);
			this.btnConsult.Name = "btnConsult";
			this.btnConsult.Size = new System.Drawing.Size(92, 22);
			this.btnConsult.TabIndex = 4;
			this.btnConsult.Text = "FILTRAR";
			this.btnConsult.UseVisualStyleBackColor = true;
			this.btnConsult.Click += new System.EventHandler(this.ButtonFilterDateClick);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(31, 12);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(308, 58);
			this.pictureBox2.TabIndex = 10;
			this.pictureBox2.TabStop = false;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerDoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerRunWorkerCompleted);
			// 
			// buttonLoadBankProfiles
			// 
			this.buttonLoadBankProfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonLoadBankProfiles.Location = new System.Drawing.Point(40, 125);
			this.buttonLoadBankProfiles.Name = "buttonLoadBankProfiles";
			this.buttonLoadBankProfiles.Size = new System.Drawing.Size(184, 40);
			this.buttonLoadBankProfiles.TabIndex = 17;
			this.buttonLoadBankProfiles.Text = "CARGAR TRANSACCIONES";
			this.buttonLoadBankProfiles.UseVisualStyleBackColor = true;
			this.buttonLoadBankProfiles.Click += new System.EventHandler(this.LoadBankProfileButtonClick);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button3.Location = new System.Drawing.Point(262, 125);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(168, 40);
			this.button3.TabIndex = 18;
			this.button3.Text = "CIERRE OPERACIONES";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(984, 750);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.buttonLoadBankProfiles);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.buttonOpen);
			this.Controls.Add(this.pictureBox2);
			this.MaximumSize = new System.Drawing.Size(1000, 974);
			this.MinimumSize = new System.Drawing.Size(1000, 726);
			this.Name = "MainForm";
			this.Text = "PosSimulator";
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxQR)).EndInit();
			this.panel1.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button3;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.Button buttonLoadBankProfiles;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label1ValueConcept;
		private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label labelValueStatus;
		private System.Windows.Forms.Label labelValuePayedDate;
		private System.Windows.Forms.Label labelValuePayedCardHolder;
		private System.Windows.Forms.Label labelValuePayedCardNumber;
		private System.Windows.Forms.Label labelValuePayedIdentidy;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelValueDevice;
		private System.Windows.Forms.Label labelValueAmount;
		private System.Windows.Forms.Label labelValueOpenDate;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.PictureBox pictureBoxQR;
		private System.Windows.Forms.ListBox listBoxTransactions;
		
		void Button2Click(object sender, System.EventArgs e)
		{
			showTransaction();			
		}
		
		void Button3Click(object sender, System.EventArgs e)
		{
			CloseOperationsWindow closeOperationsWindow = new CloseOperationsWindow(this.serviceClient, this.bankProfileId);
			closeOperationsWindow.ShowDialog();	
        }

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxDateEndHour;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxDateInitialHour;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxDateInitial;
        private System.Windows.Forms.TextBox textBoxDateEnd;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnConsult;
        private System.Windows.Forms.Button buttonRevert;
		

	}
}
