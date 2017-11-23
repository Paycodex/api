/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 9:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Comcodex
{
	/// <summary>
	/// Servicios del cliente.
	/// </summary>
	public class ServiceClient
	{

		/// <summary>
		/// Configuración.
		/// </summary>
		public Setting 		setting			= null;
		/// <summary>
		/// Status.
		/// </summary>
		public int 			status 			= 0;
		/// <summary>
		/// Token de sesión.
		/// </summary>
		private string 		sessionToken 	= "";
		/// <summary>
		/// Fecha de expiración.
		/// </summary>
		private DateTime 	sessionExpires;
		/// <summary>
		/// Autenticación.
		/// </summary>
		private Auth 		auth;
		
		/// <summary>
		/// Obtiene la dirección URL del servicio.
		/// </summary>
		/// <returns></returns>
		public string getUri()
		{
			return this.setting.serviceUri + (this.setting.servicePort.Trim() == ""?"":":"+this.setting.servicePort);
		}

		
		/// <summary>
		/// Asigna la configuración.
		/// </summary>
		/// <param name="setting"></param>
		public ServiceClient( Setting setting )
		{
			this.setting = setting;
			this.auth = new Auth(setting);
		}
		
		
		
		/// <summary>
		/// Obtiene el Token de la Sesion.
		/// </summary>
		/// <returns></returns>
		public String getSessionToken()
		{
			return this.sessionToken;
		}
		
		
		/// <summary>
		/// Obtiene el objeto para la Firma Digital en Auth
		/// </summary>
		/// <returns></returns>
		private Auth getAuth()
		{
			if( this.auth == null )
			{
				this.auth = new Auth(this.setting);
			}
			return this.auth;
		}
		
		
		/// <summary>
		/// Realiza la conexión con el servicio.
		/// </summary>
		public void connect()
		{			
			try
			{	

				Session session 			= this.parseHttpResponse<Session>( this.executeGetRequest( HttpUtils.URI_SESSION_OPEN ), HttpUtils.HTTP_OK );
	            this.sessionToken 		= session.sid;
	            this.sessionExpires 		= DateTime.ParseExact(session.expires, HttpUtils.DEFAULT_FORMAT_DATE, CultureInfo.InvariantCulture );
			}
			catch( Exception e )
			{
				throw new ServiceClientException( e.Message );
			}
		}
		
		
		/// <summary>
		/// Obtiene lista de perfiles bancarios.
		/// </summary>
		/// <returns></returns>
		private BankProfile[] getBankProfileList()
		{		
			try {
				
				return this.parseHttpResponse<BankProfile[]>( this.executeGetRequest( HttpUtils.URI_BANKPROFILE_LIST ),  HttpUtils.HTTP_OK );
			} 
			catch (Exception e) 
			{
				throw new ServiceClientException( e.Message );
			}		
	
		}
		
		
		/// <summary>
		/// Obtiene perfil bancario Punto.
		/// </summary>
		/// <returns></returns>
		private BankProfile getBankProfileWallet()
		{		
			try {
				BankProfile[] bankProfiles =  this.parseHttpResponse<BankProfile[]>( this.executeGetRequest( HttpUtils.URI_BANKPROFILE_LIST ),  HttpUtils.HTTP_OK );
				return bankProfiles[0];
			} 
			catch (Exception e) 
			{
				throw new ServiceClientException( e.Message );
			}		
	
		}
		
		
		/// <summary>
		/// Crea una nueva transacción.
		/// </summary>
		/// <param name="newTransaction"></param>
		/// <returns></returns>
		public Transaction openTransaction(Transaction newTransaction ) 
		{
			try {
				
				BankProfile Punto = this.getBankProfileWallet();
				newTransaction.bankProfileId = Punto.id;
				
				List<NameValuePair> parameters = new List<NameValuePair>();
	
				parameters.Add( new NameValuePair("device", this.setting.device.Trim()  ));
				
				parameters.Add( new NameValuePair("bankProfileId", newTransaction.bankProfileId  ));
				
				parameters.Add( new NameValuePair("concept", newTransaction.concept.Trim()  ));		
				
				parameters.Add( new NameValuePair("number", newTransaction.amount.Number.ToString()  ));
				
				parameters.Add( new NameValuePair("decimal", newTransaction.amount.Decimal.ToString() ));			
			
				return this.parseHttpResponse<Transaction>( this.executePostRequest(HttpUtils.URI_TRANSACTION_OPEN, parameters), HttpUtils.HTTP_CREATED );
				
			} 
			catch (Exception e) 
			{
				throw new ServiceClientException( e.Message );
			}
		}
		
		
		
		
		/// <summary>
		/// Obtiene el QR de una transacción.
		/// </summary>
		/// <param name="transaction"></param>
		/// <returns></returns>
		public String getQrImage(Transaction transaction) 
		{
			try {
				
				string  uriQR = this.retrieveQrUri( HttpUtils.URI_TRANSACTION_QR + HttpUtils.URI_SEPARATOR  + transaction.token );
				
				if( uriQR != null )
				{		
					CloseableHttpResponse response  = this.executeGetImageRequest(uriQR);
			        	if( response.StatusLine.StatusCode == HttpUtils.HTTP_OK )
			        	{		
			        		FileStream file = null;
				        try 			        
				        {	
					        	String pathFile = this.setting.pathImage  + transaction.token + HttpUtils.DEFAULT_IMAGE_EXTENSION;
					        	using (BinaryReader streamReader = new BinaryReader(response.ContextStream) )
					        {
					            using (FileStream localFileStream = new FileStream(pathFile, FileMode.OpenOrCreate))
					            {
					                var buffer = new byte[4096];
					                long totalBytesRead = 0;
					                int bytesRead;
					                while ((bytesRead = streamReader.Read(buffer, 0, buffer.Length)) > 0)
					                {
					                    totalBytesRead += bytesRead;
					                    localFileStream.Write(buffer, 0, bytesRead);
					                    localFileStream.Flush();
					                }
					               
					            }
					        }

				        		
				            return pathFile;
						}
				        catch (Exception e) 
				        {
							throw new ServiceClientException( e.Message + e.StackTrace );	
				        } 
		        			finally 
				        {
		        				if( file != null ) file.Close();
					            response.Close();		            
				        }
			        	}	
				}	
				return null;
			} 
			catch (Exception e) 
			{
				throw new ServiceClientException( e.Message );
			}
		}
		
		
		
		/// <summary>
		/// Obtiene la Lista de Transacciones en base a un Objeto de Consulta.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public Transaction[] listTransactions(TransactionQuery query )
		{
			try {
					
				List<NameValuePair> parameters = new List<NameValuePair>();
				
				BankProfile Punto = this.getBankProfileWallet();
				query.bankProfileId = Punto.id;
				
				if( query.device != "" && query.device != null )
					parameters.Add( new NameValuePair ( TransactionQuery.FIELD_DEVICE, query.device  ));	
				
				if( query.bankProfileId != "" && query.bankProfileId != null )
					parameters.Add( new NameValuePair(TransactionQuery.FIELD_BANKPROFILE_ID, query.bankProfileId  ));
				
				if( query.beginDate != null )
                    parameters.Add(new NameValuePair(TransactionQuery.FIELD_BEGIN_DATE, HttpUtils.ConvertToTimestamp(query.beginDate.Value.ToUniversalTime()).ToString()));
		
				if( query.endDate != null )
                    parameters.Add(new NameValuePair(TransactionQuery.FIELD_END_DATE, HttpUtils.ConvertToTimestamp(query.endDate.Value.ToUniversalTime()).ToString()));

                Transaction[] transactions = this.parseHttpResponse<Transaction[]>(this.executePostRequest(HttpUtils.URI_TRANSACTION_LIST, parameters), HttpUtils.HTTP_OK);

                return transactions;
				
			} 
			catch (Exception e) 
			{
				throw new ServiceClientException( e.ToString() );
			}
		}
				

		/// <summary>
		/// Realiza el cierre de operaciones.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		public ClosingReport ClosingReport(TransactionQuery query )
		{
			try {
					
				List<NameValuePair> parameters = new List<NameValuePair>();
				
				BankProfile Punto = this.getBankProfileWallet();
				query.bankProfileId = Punto.id;
					
				if( query.device != "" && query.device != null )
					parameters.Add( new NameValuePair ( TransactionQuery.FIELD_DEVICE, query.device  ));	
				
				if( query.bankProfileId != "" && query.bankProfileId != null )
					parameters.Add( new NameValuePair(TransactionQuery.FIELD_BANKPROFILE_ID, query.bankProfileId  ));
				
				if( query.beginDate != null )
                    parameters.Add(new NameValuePair(TransactionQuery.FIELD_BEGIN_DATE, HttpUtils.ConvertToTimestamp(query.beginDate.Value.ToUniversalTime()).ToString()));
		
				if( query.endDate != null )
                    parameters.Add(new NameValuePair(TransactionQuery.FIELD_END_DATE, HttpUtils.ConvertToTimestamp(query.endDate.Value.ToUniversalTime()).ToString()));

                ClosingReport report = this.parseHttpResponse<ClosingReport>(this.executePostRequest(HttpUtils.URI_TRANSACTION_CLOSED_REPORT, parameters), HttpUtils.HTTP_OK);
                
                report.globalTotal = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<string>(report.globalTotal);
                report.totalReverted = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<string>(report.totalReverted);
				report.totalPurchases = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<string>(report.totalPurchases);
                
				return report;
				
			} 
			catch (Exception e) 
			{
				Debug.WriteLine(e.ToString());
				throw new ServiceClientException( e.ToString() );
			}
		}

		/// <summary>
		/// Obtiene una transacción registrada.
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public Transaction retrieveTransaction( String token )
		{
			try 
			{
				return this.parseHttpResponse<Transaction>( this.executeGetRequest( HttpUtils.URI_TRANSACTION_GET + HttpUtils.URI_SEPARATOR + token ), HttpUtils.HTTP_OK );
			} 
			catch (Exception e) 
			{
				throw new ServiceClientException( e.ToString() );
			}		
		}


        /// <summary>
        /// Revierte una transacción pagada.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Transaction revertTransaction(Transaction transaction)
        {
            try
            {
                if (transaction.status == Transaction.STATUS_OK)
                {
                    return this.parseHttpResponse<Transaction>(this.executeGetRequest(HttpUtils.URI_TRANSACTION_REVERT + HttpUtils.URI_SEPARATOR + transaction.token), HttpUtils.HTTP_OK);
                }
                else 
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ServiceClientException(e.ToString());
            }
        }

		/// <summary>
		/// Ejecuta una petición HttpUriRequest.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		private HttpResponse executeRequest(HttpUriRequest request)
		{
			return ( HttpClientBuilder.Create().Build() ).Execute(request);
		}
		
		
		/// <summary>
		/// Ejecuta una petición HttpGet.
		/// </summary>
		/// <param name="actionRequest"></param>
		/// <returns></returns>
		private HttpResponse executeGetRequest( String actionRequest )
		{
			HttpGet request = new HttpGet ( this.getUri() + actionRequest  );
			this.getAuth().buildHeader(request, this.getSessionToken() );	
			
			return this.executeRequest(request);
		}
		
		
		/// <summary>
		/// Ejecuta una petición HttpPost.
		/// </summary>
		/// <param name="actionRequest"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private HttpResponse executePostRequest( String actionRequest, List<NameValuePair> parameters ) 
		{
			HttpPost request = new HttpPost ( this.getUri() + actionRequest  );
			request.ContentType = @"application/json";
			request.Entity = new UrlEncodedFormEntity(parameters);
			this.getAuth().buildHeader( request, this.getSessionToken(), parameters );		
			
			return this.executeRequest(request);
		}
		
		/// <summary>
		/// Ejecuta una petición CloseableHttpClient para solicitar una imagen.
		/// </summary>
		/// <param name="imageUri"></param>
		/// <returns></returns>
		private CloseableHttpResponse executeGetImageRequest( String imageUri )
		{
			CloseableHttpClient httpclient = HttpClient.Custom().Build();
			HttpGet request = new HttpGet(imageUri);
			request.ContentType = @"image/png";
		
			return httpclient.Execute(request);		
		}	
		
		/// <summary>
		/// Obtiene el QR de una transacción.
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		private string retrieveQrUri( String uri )
		{
			QrImage qrImage = (this.createGson()).FromJson<QrImage>( EntityUtils.ToString( this.executeGetRequest( uri ).Entity, HttpUtils.DEFAULT_CHARSET) );
			
			if( qrImage != null && qrImage.QrUrl != null && qrImage.QrUrl != "" )
			{
				return qrImage.QrUrl;
			}
		
			return null;
		}
		
		/// <summary>
		/// Realiza el parseo de una respuesta HttpResponse.
		/// </summary>
		/// <param name="response"></param>
		/// <param name="statusOK"></param>
		/// <returns></returns>
		private T  parseHttpResponse<T>( HttpResponse response, int statusOK ) 
		{
            
			if( response.StatusLine.StatusCode == statusOK )
            {
                return (this.createGson()).FromJson<T>(EntityUtils.ToString(response.Entity, HttpUtils.DEFAULT_CHARSET));
			}
			else if( response.StatusLine.StatusCode == ServiceClientException.ERROR_STATUS_UNAUTHORIZED )
			{
				throw new ServiceClientException( ServiceClientException.ERROR_STATUS_UNAUTHORIZED_MESSAGE );
			}
			else
            {
				HttpUtils.Fail fail = HttpUtils.processFailResponse( response );
				
				if( fail != null )
				{
					throw new ServiceClientException( fail );
				}
				else
				{
					throw new ServiceClientException( ServiceClientException.ERROR_DONT_CAUGHT );
				}
			}
		}
				
		/// <summary>
		/// Crea el Serializador GSon.
		/// </summary>
		/// <returns></returns>
		public Gson createGson()
		{
			return (new GsonBuilder()).SetDateFormat(HttpUtils.DEFAULT_FORMAT_DATE).Create();
		}
			
		/// <summary>
		/// Obtiene la fecha de expiración de la sesion.
		/// </summary>
		/// <returns></returns>
		public DateTime getSessionExpires() 
		{
			return sessionExpires;
		}

	}
}
