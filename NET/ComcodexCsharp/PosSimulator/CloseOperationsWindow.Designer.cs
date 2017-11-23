/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 15/11/2016
 * Time: 11:32 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace PosSimulator
{
	partial class CloseOperationsWindow
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.listBoxTransactionsCloseReport = new System.Windows.Forms.ListBox();
			this.labelTotalPurshase = new System.Windows.Forms.Label();
			this.labelTotalDevolutions = new System.Windows.Forms.Label();
			this.labelTotal = new System.Windows.Forms.Label();
			this.labelTotalPurshaseValue = new System.Windows.Forms.Label();
			this.labelTotalDevolutionsValue = new System.Windows.Forms.Label();
			this.labelTotalValue = new System.Windows.Forms.Label();
			this.backgroundWorkerCloseReport = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(26, 39);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Inicio";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(196, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Fin";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(63, 36);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(114, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "11/05/2016 00:00:00";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(230, 36);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(114, 20);
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "11/10/2016 00:00:00";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(363, 36);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(71, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Filtrar";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// listBoxTransactionsCloseReport
			// 
			this.listBoxTransactionsCloseReport.FormattingEnabled = true;
			this.listBoxTransactionsCloseReport.Location = new System.Drawing.Point(26, 102);
			this.listBoxTransactionsCloseReport.Name = "listBoxTransactionsCloseReport";
			this.listBoxTransactionsCloseReport.Size = new System.Drawing.Size(408, 212);
			this.listBoxTransactionsCloseReport.TabIndex = 5;
			// 
			// labelTotalPurshase
			// 
			this.labelTotalPurshase.Location = new System.Drawing.Point(40, 332);
			this.labelTotalPurshase.Name = "labelTotalPurshase";
			this.labelTotalPurshase.Size = new System.Drawing.Size(100, 19);
			this.labelTotalPurshase.TabIndex = 6;
			this.labelTotalPurshase.Text = "Total Compras Bs. ";
			// 
			// labelTotalDevolutions
			// 
			this.labelTotalDevolutions.Location = new System.Drawing.Point(40, 360);
			this.labelTotalDevolutions.Name = "labelTotalDevolutions";
			this.labelTotalDevolutions.Size = new System.Drawing.Size(123, 19);
			this.labelTotalDevolutions.TabIndex = 7;
			this.labelTotalDevolutions.Text = "Total Devoluciones Bs.";
			// 
			// labelTotal
			// 
			this.labelTotal.Location = new System.Drawing.Point(40, 390);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(100, 19);
			this.labelTotal.TabIndex = 8;
			this.labelTotal.Text = "Total Bs.";
			// 
			// labelTotalPurshaseValue
			// 
			this.labelTotalPurshaseValue.Location = new System.Drawing.Point(162, 332);
			this.labelTotalPurshaseValue.Name = "labelTotalPurshaseValue";
			this.labelTotalPurshaseValue.Size = new System.Drawing.Size(100, 19);
			this.labelTotalPurshaseValue.TabIndex = 9;
			this.labelTotalPurshaseValue.Text = "0";
			// 
			// labelTotalDevolutionsValue
			// 
			this.labelTotalDevolutionsValue.Location = new System.Drawing.Point(162, 360);
			this.labelTotalDevolutionsValue.Name = "labelTotalDevolutionsValue";
			this.labelTotalDevolutionsValue.Size = new System.Drawing.Size(100, 19);
			this.labelTotalDevolutionsValue.TabIndex = 10;
			this.labelTotalDevolutionsValue.Text = "0";
			// 
			// labelTotalValue
			// 
			this.labelTotalValue.Location = new System.Drawing.Point(162, 389);
			this.labelTotalValue.Name = "labelTotalValue";
			this.labelTotalValue.Size = new System.Drawing.Size(100, 19);
			this.labelTotalValue.TabIndex = 11;
			this.labelTotalValue.Text = "0";
			// 
			// backgroundWorkerCloseReport
			// 
			this.backgroundWorkerCloseReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorkerCloseReportDoWork);
			this.backgroundWorkerCloseReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorkerCloseReportRunWorkerCompleted);
			// 
			// CloseOperationsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(475, 417);
			this.Controls.Add(this.labelTotalValue);
			this.Controls.Add(this.labelTotalDevolutionsValue);
			this.Controls.Add(this.labelTotalPurshaseValue);
			this.Controls.Add(this.labelTotal);
			this.Controls.Add(this.labelTotalDevolutions);
			this.Controls.Add(this.labelTotalPurshase);
			this.Controls.Add(this.listBoxTransactionsCloseReport);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CloseOperationsWindow";
			this.Text = "Cierre de Operaciones";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.ComponentModel.BackgroundWorker backgroundWorkerCloseReport;
		private System.Windows.Forms.Label labelTotalValue;
		private System.Windows.Forms.Label labelTotalDevolutionsValue;
		private System.Windows.Forms.Label labelTotalPurshaseValue;
		private System.Windows.Forms.Label labelTotal;
		private System.Windows.Forms.Label labelTotalDevolutions;
		private System.Windows.Forms.Label labelTotalPurshase;
		private System.Windows.Forms.ListBox listBoxTransactionsCloseReport;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		
	}
}
