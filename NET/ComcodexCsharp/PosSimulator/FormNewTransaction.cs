/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 24/09/2014
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Comcodex;


namespace PosSimulator
{
	/// <summary>
	/// Description of frmNewTransaction.
	/// </summary>
	public partial class FormNewTransaction : Form
	{
		
		private bool ready = false;
		private ServiceClient serviceClient;
		private LoadingForm loading;
		private string device;
		private string amount;
		private string concept;
		public string transactionToken;
	
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="serviceClient"></param>
		/// <param name="bankProfileId"></param>
		public FormNewTransaction(ServiceClient serviceClient)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			this.serviceClient = serviceClient;
			
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BtnNewClick(object sender, EventArgs e)
		{
			Currency currencyAmount = checkAmount( this.textBoxAmount.Text );
			
			if( currencyAmount != null )
			{
				this.device = (string) this.comboBoxDevice.SelectedItem;
				this.amount = this.textBoxAmount.Text;
				this.concept = this.textBoxConcept.Text;
				
				this.loading = null;
				this.backgroundWorker.RunWorkerAsync();
				this.loading = new LoadingForm();
				this.loading.ShowDialog();	
			}
			else
			{
				MessageBox.Show("Debe utilzar un numero válido como 9999" + System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + "999");
			}
			

			
			
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		Currency checkAmount( string value )
		{
			string [] separators = new string[1] { System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator };
			string[] numericParts = value.Split(separators, StringSplitOptions.RemoveEmptyEntries );
			Currency currency = null;
			
			if( numericParts.Length == 0) 
			{
				try
				{
					currency = new Currency( Convert.ToInt32( value ), 0 );
				}
				catch{}
			}
			else if(  numericParts.Length == 1 )
			{
				try
				{
					currency = new Currency(  Convert.ToInt32(numericParts[0]), 0 );
				}
				catch{}
			}
			else if(  numericParts.Length == 2 )
			{			
				try
				{
					currency = new Currency(  Convert.ToInt32( numericParts[0]),  Convert.ToInt32( numericParts[1]) );
				}
				catch{}
			}
		
			return currency;			
		}	


		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BackgroundWorkerDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			Thread.Sleep(1000);
			Transaction transaction 	= new Transaction();
			transaction.amount 			= checkAmount( this.amount );
			transaction.device 			= this.device;
			transaction.concept 		= this.concept;	
			
			transaction 				= this.serviceClient.openTransaction(transaction);
			this.transactionToken 		= transaction.token;
			this.ready = true;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BackgroundWorkerRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			if( this.loading != null ) this.loading.Close();
			if( this.ready ) 
			{
				this.Close();
			}
			else
			{
				MessageBox.Show("Error : " +  e.Error.Message );
			}
		}
		
		
		
		void BackgroundWorkerProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			MessageBox.Show("Cargando");
		}

        private void FormNewTransaction_Load(object sender, EventArgs e)
        {

        }
	}
	
	
	

	
}
