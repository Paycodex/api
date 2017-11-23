/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 15/11/2016
 * Time: 11:32 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Comcodex;
using PosSimulator;
using System.Diagnostics;

namespace PosSimulator
{

	/// <summary>
	/// Description of CloseOperationsWindow.
	/// </summary>
	public partial class CloseOperationsWindow : Form
	{
		private Comcodex.ServiceClient serviceClient;
		private string bankProfileId;
		private LoadingForm loading;
		Transaction[] listTransactions 	= new Transaction[0];
		Comcodex.ClosingReport report = null;
		
		public CloseOperationsWindow(ServiceClient serviceClient, string bankProfileId)
		{
			
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			this.serviceClient = serviceClient;
			this.bankProfileId = bankProfileId;
		}
		
		
		void Button1Click(object sender, EventArgs e)
		{
			
			this.backgroundWorkerCloseReport.RunWorkerAsync();
			this.showLoading();
			
			if(this.report != null){
				this.labelTotalPurshaseValue.Text = this.report.totalPurchases.ToString();
				this.labelTotalDevolutionsValue.Text = this.report.totalReverted.ToString();
				this.labelTotalValue.Text = this.report.globalTotal.ToString();
				this.listTransactions 	= this.report.getTransaction();
				this.fillTransactionList();
			}


		}
		
		
		/// <summary>
		/// 
		/// </summary>
		void fillTransactionList()
		{
			this.listBoxTransactionsCloseReport.DataSource 	= TransactionListItem.buildList(this.listTransactions);
			if( this.listTransactions.Length > 0)
			{
				this.listBoxTransactionsCloseReport.DisplayMember 	= "Label";
				this.listBoxTransactionsCloseReport.ValueMember 	= "Value";				
			}

		}
		
		/// <summary>
		/// 
		/// </summary>
		private void showLoading()
		{
			this.loading = new LoadingForm();
			loading.ShowDialog(this);
		}
		
		void BackgroundWorkerCloseReportDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{

			TransactionQuery query	= new TransactionQuery();
			Boolean dateIsValid = true;
			
			try
			{

				query.beginDate			= new Nullable<DateTime>(Convert.ToDateTime(this.textBox1.Text));
				query.endDate			= new Nullable<DateTime>(Convert.ToDateTime(this.textBox2.Text));				

			}
			catch( FormatException )
			{
				MessageBox.Show("Error en los formatos de fecha");
				dateIsValid = false;
			}
			
			if(dateIsValid){
				
				query.bankProfileId 	= this.bankProfileId;
		
				try
				{
					ClosingReport report =  serviceClient.ClosingReport(query);
					this.report = report;
	
				}
				catch (Exception ex) 
				{
					Debug.WriteLine("Exception:");
					Debug.WriteLine(ex);
					MessageBox.Show(ex.Message);
				}
				}

		
		}
		
		void BackgroundWorkerCloseReportRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			this.loading.Close();
		}
	}
}
