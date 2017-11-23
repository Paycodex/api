/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 22/09/2014
 * Time: 9:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Comcodex;
using System.Diagnostics;

namespace PosSimulator
{	
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		Comcodex.Setting setting				= null;
		Comcodex.ServiceClient serviceClient	= null;

		const int PROCESS_NOTHING 				= 0;
		const int PROCESS_LOADING_TRANSACTIONS 	= 2;
		const int PROCESS_SHOW_TRANSACTION 		= 3;
		const int PROCESS_FILTER_DATE 			= 4;
		const int PROCESS_REVERT_TRANSACTION    = 5;
		
		int currentProcess 			= 0;
		
		string bankProfileId		= null;
		string transactionToken		= null;
		string pathImageQR			= null;
		Nullable<DateTime> beginDate = null;
		Nullable<DateTime> endDate	 = null;
		
		LoadingForm loading				= null;
		BankProfile[] bankProfiles 		= new BankProfile[0];
		Transaction[] listTransactions 	= new Transaction[0]; 
		Transaction currentTransaction 	= null;
		
		/// <summary>
		/// 
		/// 
		/// </summary>
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			clearTransactionDetails();
			this.setting = new Setting();

			/*
            this.setting.serviceUri = "http://192.168.0.110";
            this.setting.servicePort = "8082";
            this.setting.secretPhrase = "SECRET-39B6E2-69E352-6ACC59-A77A56";
            this.setting.clientKey = "CCODEX-57DAA2-AE6FC9-A67575-00000E";
            this.setting.pathImage = "C:/Users/koiosoft/Documents/Temp";
            this.setting.device       = "PC_API_TEST";
            */
            
           
            setting.serviceUri   = "https://qa-api.paycodex.com";
        	setting.servicePort  = "";
        	setting.secretPhrase = "SECRET-276700-A8FBB7-8C789C-A3978F";
        	setting.clientKey    = "CCODEX-5915FB-61EC74-FE5304-00000D";
        	setting.pathImage = "C:/Users/koiosoft/Documents/Temp";
			setting.device		 = "PC_API_TEST";
			
			this.serviceClient = new ServiceClient( this.setting );
			
			try
			{	

				this.serviceClient.connect();
			}
			catch( Comcodex.ServiceClientException e )
			{
				
				Debug.WriteLine(e);
				if(e.getCode() == Comcodex.ServiceClientException.ERROR_STATUS_UNAUTHORIZED){
					MessageBox.Show("Las credenciales ingresadas son incorrectas");
				}else{
					MessageBox.Show(e.Message);
				}
				
			}

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			

			
		}
		
		/// <summary>
		/// 
		/// 
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ButtonOpenClick(object sender, EventArgs e)
		{
			//
			//	Vacia los Detalles de la transaccion.
			//
			clearTransactionDetails();
			
			if(listBoxTransactions.SelectedIndex >= 0 )
			{
				this.transactionToken = (string)this.listBoxTransactions.SelectedValue;
				
				this.currentProcess = PROCESS_SHOW_TRANSACTION;
				this.backgroundWorker.RunWorkerAsync();
				this.showLoading();
				this.showTransaction();
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
		
		
		/// <summary>
		/// 
		/// Abre una transacción
		/// </summary>
		/// <param name="token"></param>
		void showTransaction()
		{
			
			Transaction tr = this.currentTransaction;
			
			labelValueOpenDate.Text = tr.openDate.ToString();
					
			if(tr.amount != null)
			{ 
				Comcodex.Currency amount = tr.amount;
				labelValueAmount.Text = amount.toDouble().ToString();
			}
			
			label1ValueConcept.Text 		= tr.concept.ToString();
			labelValueDevice.Text 			= tr.device.ToString();
			labelValueStatus.Text 			= this.getStatusTransactionLabel(tr.status);
			
			//
			//  los valores null, no tienen representacion
			//
			labelValuePayedDate.Text 		=  tr.payedDate == null?"":tr.payedDate.ToString();
			labelValuePayedCardHolder.Text 	=  tr.payedCardHolder == null?"":tr.payedCardHolder.ToString();
			labelValuePayedCardNumber.Text 	=  tr.payedCardNumber == null?"":tr.payedCardNumber.ToString();
			labelValuePayedIdentidy.Text 	=  tr.payedIdentity == null?"":tr.payedIdentity.ToString();

			if( this.pathImageQR != null )
			{
				FileStream fileStream = new FileStream( this.pathImageQR, FileMode.Open);
				try
				{
					pictureBoxQR.BackgroundImage 		= null;
					pictureBoxQR.BackgroundImage 		= Image.FromStream(fileStream);
					pictureBoxQR.BackgroundImageLayout 	= ImageLayout.Zoom;		
				}
				catch (Exception e) {
					Debug.WriteLine("Exception:");
					Debug.WriteLine(e);
				}
				finally
				{
				  fileStream.Close();
				}						
			}else{
				//MessageBox.Show("Es null el path de imagen....:");
			}
			
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		string getStatusTransactionLabel( int status )
		{
			string message = "";
			
			switch(status)
			{
				case Transaction.STATUS_WAITING: 
					message = "TRANSACCION EN ESPERA.";
					break;
				case Transaction.STATUS_FAIL: 
					message = "TRANSACCION FALLIDA, ERROR DEL SERVICIO.";
					break;	
				case Transaction.STATUS_OK: 
					message = "TRANSACCION EXITOSA.";
					break;
				case Transaction.STATUS_REJECTED:
					message = "TRANSACCION RECHAZADA";					
					break;	
				case Transaction.STATUS_REVERTED:
					message = "TRANSACCION REVERTIDA";					
					break;	
				default:
					message = "ERROR NO DEFINIDO.";
					break;
			}
			
			return message;
		}
		
		
		
		/// <summary>
		/// 
		/// </summary>
		void clearTransactionDetails(){

			labelValueOpenDate.Text = "";
			labelValueAmount.Text = "";
			label1ValueConcept.Text = "";
			labelValueDevice.Text = "";
			labelValueStatus.Text = "";
			labelValuePayedDate.Text = "";
			labelValuePayedCardHolder.Text = "";
			labelValuePayedCardNumber.Text = "";
			labelValuePayedIdentidy.Text = "";
			
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		void fillTransactionList()
		{
			this.listBoxTransactions.DataSource 	= TransactionListItem.buildList(this.listTransactions);
			if( this.listTransactions.Length > 0)
			{
				this.listBoxTransactions.DisplayMember 	= "Label";
				this.listBoxTransactions.ValueMember 	= "Value";				
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
				catch
				{
					
				}
			}
			else if(  numericParts.Length == 1 )
			{
				try
				{
					currency = new Currency(  Convert.ToInt32(numericParts[0]), 0 );
				}
				catch
				{
					
				}
			}
			else if(  numericParts.Length == 2 )
			{			
				try
				{
					currency = new Currency(  Convert.ToInt32( numericParts[0]),  Convert.ToInt32( numericParts[1]) );
				}
				catch
				{
					
				}
			}
		
			return currency;			
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ButtonFilterDateClick(object sender, EventArgs e)
		{
			this.beginDate = null;
			this.endDate = null;

			try
			{
                if (textBoxDateInitial.Text.Trim() != "")
                {
                    if (textBoxDateInitialHour.Text.Trim() != "")
                    {
                        this.beginDate = new Nullable<DateTime>(Convert.ToDateTime(textBoxDateInitial.Text.Trim() + " " + textBoxDateInitialHour.Text.Trim()));
                    }
                    else
                    {
                        this.beginDate = new Nullable<DateTime>(Convert.ToDateTime(textBoxDateInitial.Text.Trim()));
                    }
                }
                if (textBoxDateEnd.Text.Trim() != "")
                {
                    if (textBoxDateEndHour.Text.Trim() != "")
                    {
                        this.endDate = new Nullable<DateTime>(Convert.ToDateTime(textBoxDateEnd.Text.Trim() + " " + textBoxDateEndHour.Text.Trim()));
                    }
                    else 
                    {
                        this.endDate = new Nullable<DateTime>(Convert.ToDateTime(textBoxDateEnd.Text.Trim()));
                    }
                }
				this.currentProcess = PROCESS_FILTER_DATE;
                this.backgroundWorker.RunWorkerAsync();
				this.showLoading();
				this.fillTransactionList();		
				
			}
			catch( FormatException )
			{
				MessageBox.Show("Error en los formatos de fecha");
			}


		}
		


		
		/// <summary>
		/// Presenta la lista de transacciones del bankProfile Seleccionado
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Button1Click(object sender, EventArgs e)
		{	
			FormNewTransaction frmNewTransaction = new FormNewTransaction(this.serviceClient);
			frmNewTransaction.ShowDialog();	
			this.transactionToken = frmNewTransaction.transactionToken;
			this.loadTransaction();
		}
		
		
		
		/// <summary> 
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void LoadBankProfileButtonClick(object sender, System.EventArgs e)
		{
				this.currentProcess = PROCESS_LOADING_TRANSACTIONS;
				this.backgroundWorker.RunWorkerAsync();
				this.showLoading();
				this.fillTransactionList();
			
		}	

		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BackgroundWorkerDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			TransactionQuery query = null;

			
			switch( this.currentProcess )
			{
										
				case PROCESS_LOADING_TRANSACTIONS:

					query  					= new TransactionQuery();
					query.beginDate			= this.beginDate;
					query.endDate			= this.endDate;
					query.bankProfileId 	= this.bankProfileId;
					this.listTransactions 	= serviceClient.listTransactions(query);

					break;
					
				case PROCESS_SHOW_TRANSACTION:
					this.currentTransaction = serviceClient.retrieveTransaction(this.transactionToken);
					if( this.currentTransaction != null )
					{

						try
						{
							this.pathImageQR = serviceClient.getQrImage(this.currentTransaction);
			
						}
						catch (Exception ex) 
						{
							Debug.WriteLine(ex.Message);
						}	
						
					}
					break;
					
				case PROCESS_FILTER_DATE:
					query  					= new TransactionQuery();
					query.bankProfileId 	= this.bankProfileId;
					query.beginDate			= this.beginDate;
					query.endDate			= this.endDate;
					this.listTransactions 	= serviceClient.listTransactions(query);
					break;
                case PROCESS_REVERT_TRANSACTION:
					
					
                    if (currentTransaction != null)
					{
                        
						if(currentTransaction.status == Transaction.STATUS_OK){
                        
                        	Transaction transactionClient = serviceClient.revertTransaction(currentTransaction);
                        	
                        	if(transactionClient.status == Transaction.STATUS_REVERTED){
                            
                        		MessageBox.Show("La transacción fue revertida exitosamente.");

	                            // Actualizar listado
	                            
								query  					= new TransactionQuery();
								query.bankProfileId 	= this.bankProfileId;
								query.beginDate			= this.beginDate;
								query.endDate			= this.endDate;
								this.listTransactions 	= serviceClient.listTransactions(query);
	                            
	                        }else{
	                            
                        		MessageBox.Show("La transacción no pudo ser revertida.");
	                        }
	                        
	                    }else{
	                        
                        	MessageBox.Show("La transacción debe estar pagada.");
	                    }
                    

					}
					
					break;
			}
			
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void updateCurrentTransactionClick(object sender, EventArgs e)
		{
			this.loadTransaction();
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		private void loadTransaction()
		{
			if( this.transactionToken != null )
			{
				this.currentProcess = PROCESS_SHOW_TRANSACTION;
				this.backgroundWorker.RunWorkerAsync();
				this.showLoading();
				this.showTransaction();
			}

		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void BackgroundWorkerRunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			this.loading.Close();
		}

        private void buttonRevert_Click(object sender, EventArgs e)
        {
            if (this.transactionToken != null)
            {
                this.currentProcess = PROCESS_REVERT_TRANSACTION;
                this.backgroundWorker.RunWorkerAsync();
                this.showLoading();
                this.showTransaction();
            }
        }


	}
}
