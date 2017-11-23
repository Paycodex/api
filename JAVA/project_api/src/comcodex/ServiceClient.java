package comcodex;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.ArrayList;

import org.apache.http.HttpResponse;
import org.apache.http.ParseException;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.impl.client.HttpClientBuilder;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;
import org.apache.http.NameValuePair;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.impl.client.CloseableHttpClient;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonSyntaxException;


/**
 * Servicios del cliente
 * 
 * @author koiosoft
 *
 */
public class ServiceClient {
	
	public Setting setting;
	public Integer status = 0;
	
	private String sessionToken = "";

	private Date sessionExpires;
	private Auth auth;
	
	/**
	 * Obtiene la dirección URL del servicio
	 * @return String
	 */
	public String getUri()
	{
		return this.setting.serviceUri + (this.setting.servicePort.trim() == ""?"":":"+this.setting.servicePort);
	}
	

	@SuppressWarnings("unused")
	private ServiceClient()
	{
		
	}
	
	/**
	 * Constructor
	 * @param setting Setting
	 */
	public ServiceClient( Setting setting )
	{
		this.setting = setting;
		this.auth = new Auth(setting);
	}
	
	/**
	 * Obtiene el Token de la Sesion
	 * @return String
	 */
	public String getSessionToken()
	{
		return this.sessionToken;
	}
	
	/**
	 * Obtiene el objeto para la Firma Digital en Auth 2
	 * @return Auth
	 */
	private Auth getAuth()
	{
		if( this.auth == null )
		{
			this.auth = new Auth(this.setting);
		}
		return this.auth;
	}
	
	
	/**
         * 
         * Realiza la conexión con el servicio
         * 
	 * @throws ServiceClientException 
	 * @throws Exception 
	 * 
	 */
	public void connect() throws ServiceClientException 
	{			
		try
		{	
                    Session session 		= this.parseHttpResponse( this.executeGetRequest( HttpUtils.URI_SESSION_OPEN), Session.class, HttpUtils.HTTP_OK );
                    this.sessionToken 		= session.sid;
                    this.sessionExpires 	= new SimpleDateFormat(HttpUtils.DEFAULT_FORMAT_DATE).parse(session.expires);
		}
		catch( Exception e )
		{
                    throw new ServiceClientException( e.getMessage() );
		}
	}
	
	/**
	 *  Obtiene lista de perfiles bancarios
	 * @return BankProfile[]
	 * @throws ServiceClientException 
	 */
	private BankProfile[] getBankProfileList() throws ServiceClientException
	{		
		try {
			
			return this.parseHttpResponse( this.executeGetRequest( HttpUtils.URI_BANKPROFILE_LIST ), BankProfile[].class, HttpUtils.HTTP_OK );
		} 
		catch( ServiceClientException e )
		{
			throw e;
		}
		catch (Exception e) 
		{
			throw new ServiceClientException( e.getMessage() );
		}		

	}
        
        
	/**
	 *  Obtiene perfiles bancario Punto
	 * @return BankProfile[]
	 * @throws ServiceClientException 
	 */
	public BankProfile getBankProfileWallet() throws ServiceClientException
	{		
		try {
			BankProfile[] bankProfiles = this.parseHttpResponse( this.executeGetRequest( HttpUtils.URI_BANKPROFILE_WALLET ), BankProfile[].class, HttpUtils.HTTP_OK );
			return bankProfiles[0];
		} 
		catch( ServiceClientException e )
		{
			throw e;
		}
		catch (Exception e) 
		{
			throw new ServiceClientException( e.getMessage() );
		}		

	}
	
	
	/**
	 * Crea una nueva transacción
         * 
         * @param newTransaction Transaction
	 * @return Transaction
	 * @throws UnsupportedEncodingException 
	 * @throws ServiceClientException 
	 */
	public Transaction openTransaction(Transaction newTransaction ) throws ServiceClientException
	{
		try {
			BankProfile bankProfile = this.getBankProfileWallet();
			List<NameValuePair> parameters = new ArrayList<NameValuePair>();

			parameters.add( new BasicNameValuePair("device", this.setting.device.trim()  ));	
			
			parameters.add( new BasicNameValuePair("bankProfileId", bankProfile.id  ));
			
			parameters.add( new BasicNameValuePair("concept", newTransaction.concept.trim()  ));		
			
			parameters.add( new BasicNameValuePair("number", String.valueOf( newTransaction.amount.number ) ));
			
			parameters.add( new BasicNameValuePair("decimal", String.valueOf( newTransaction.amount.decimal ) ));			
		
			return this.parseHttpResponse( this.executePostRequest(HttpUtils.URI_TRANSACTION_OPEN, parameters), Transaction.class, HttpUtils.HTTP_CREATED );
			
		} 
		catch( ServiceClientException e )
		{
			throw e;
		}
		catch (Exception e) 
		{
			throw new ServiceClientException( e.getMessage() );
		}	
	}
	
	
	
	/**
	 * Obtiene el QR de una transacción
         * 
	 * @param transaction Transaction
	 * @return String
	 * @throws ServiceClientException 
	 */
	public String getQrImage(Transaction transaction) throws ServiceClientException
	{
            try {

                    String  uriQR = this.retrieveQrUri( HttpUtils.URI_TRANSACTION_QR + HttpUtils.URI_SEPARATOR  + transaction.token );

                    if( uriQR != null )
                    {		
                        CloseableHttpResponse response  = this.executeGetImageRequest(uriQR);

                        if( response.getStatusLine().getStatusCode() == HttpUtils.HTTP_OK )
                        {		
                            FileOutputStream fileOutputStream = null;

                            try 			        
                            {	
                                String pathFile = this.setting.pathImage  + transaction.token + HttpUtils.DEFAULT_IMAGE_EXTENSION;
                                File file = new File( pathFile );
                                fileOutputStream = new FileOutputStream(file);
                                fileOutputStream.write(EntityUtils.toByteArray(response.getEntity()));

                                return pathFile;
                            }
                            catch (Exception e) 
                            {
                                throw new ServiceClientException( e.getMessage() );	
                            } 
                            finally 
                            {
                                if( fileOutputStream != null ) fileOutputStream.close();
                                response.close();		            
                            }
                        }	
                    }	

                    return null;
            } 
            catch( ServiceClientException e )
            {
                    throw e;
            }
            catch (Exception e) 
            {
                    throw new ServiceClientException( e.getMessage() );
            }	
	}
	
	
	
	/**
	 * Obtiene la Lista de Transacciones en base a un Objeto de Consulta 
	 * 
	 * @param query TransactionQuery Objeto de Consulta de Lista de Transacciones
	 * @return Transaction[]
	 * @throws ServiceClientException
	 */
	public Transaction[] listTransactions(TransactionQuery query ) throws ServiceClientException
	{

		try {
			BankProfile bankProfile = this.getBankProfileWallet();
                        query.bankProfileId = bankProfile.id;
                        
			List<NameValuePair> parameters = new ArrayList<NameValuePair>();
			
			if( query.device != "" && query.device != null )
				parameters.add( new BasicNameValuePair ( TransactionQuery.FIELD_DEVICE, query.device  ));	
			
			if( query.bankProfileId != "" && query.bankProfileId != null )
				parameters.add( new BasicNameValuePair(TransactionQuery.FIELD_BANKPROFILE_ID, query.bankProfileId  ));
			
			if( query.beginDate != null )
				parameters.add( new BasicNameValuePair (TransactionQuery.FIELD_BEGIN_DATE, String.valueOf( query.beginDate.getTime() ) ));	
	
			if( query.endDate != null )
				parameters.add( new BasicNameValuePair(TransactionQuery.FIELD_END_DATE, String.valueOf( query.endDate.getTime() ) ));
			
			return this.parseHttpResponse( this.executePostRequest( HttpUtils.URI_TRANSACTION_LIST , parameters), Transaction[].class, HttpUtils.HTTP_OK );
			
		} 
		catch( ServiceClientException e )
		{
                    System.out.println(e.getMessage());
			throw e;
		}
		catch (Exception e) 
		{
                    System.out.println(e.getMessage());
			throw new ServiceClientException( e.getMessage() );
		}	
	}
	
	
	
	/**
	 * Obtiene una transacción registrada.
	 * @param token String
	 * @return Transaction
	 * @throws ServiceClientException
	 */
	public Transaction retrieveTransaction( String token ) throws ServiceClientException
	{
            try 
            {
                    return this.parseHttpResponse( this.executeGetRequest( HttpUtils.URI_TRANSACTION_GET + HttpUtils.URI_SEPARATOR + token ), Transaction.class, HttpUtils.HTTP_OK );
            } 
            catch( ServiceClientException e )
            {
                    throw e;
            }
            catch (Exception e) 
            {
                    throw new ServiceClientException( e.getMessage() );
            }	
	}
	
    /**
     * Revierte una transacción pagada.
     * @param transaction Transaction
     * @return Transaction
     * @throws ServiceClientException
     */
    public Transaction revertTransaction(Transaction transaction) throws ServiceClientException
    {
        
        try
        {
            
            if (transaction.status == Transaction.STATUS_OK)
            {
                return this.parseHttpResponse(this.executeGetRequest(HttpUtils.URI_TRANSACTION_REVERT + HttpUtils.URI_SEPARATOR + transaction.token), Transaction.class, HttpUtils.HTTP_OK);
            }
            else
            {
                return null;
            }
             
        }catch(Exception e){
            throw new ServiceClientException( e.getMessage() );
        }
        
    }
    
    /**
     * Realiza el cierre de operaciones.
     * @param query TransactionQuery
     * @return ClosingReport
     * @throws ServiceClientException
     */
    public ClosingReport closingReport(TransactionQuery query ) throws ServiceClientException{
    
        try{
            
            BankProfile bankProfile = this.getBankProfileWallet();
            query.bankProfileId = bankProfile.id;
            
            List<NameValuePair> parameters = new ArrayList<NameValuePair>();

            if (query.device != "" && query.device != null) {
                parameters.add(new BasicNameValuePair(TransactionQuery.FIELD_DEVICE, query.device));
            }

            if (query.bankProfileId != "" && query.bankProfileId != null) {
                parameters.add(new BasicNameValuePair(TransactionQuery.FIELD_BANKPROFILE_ID, query.bankProfileId));
            }

            if (query.beginDate != null) {
                parameters.add(new BasicNameValuePair(TransactionQuery.FIELD_BEGIN_DATE, String.valueOf(query.beginDate.getTime())));
            }

            if (query.endDate != null) {
                parameters.add(new BasicNameValuePair(TransactionQuery.FIELD_END_DATE, String.valueOf(query.endDate.getTime())));
            }

            return this.parseHttpResponse(this.executePostRequest(HttpUtils.URI_TRANSACTION_CLOSED_REPORT, parameters), ClosingReport.class, HttpUtils.HTTP_OK);
            
        }catch(Exception ex){
            throw new ServiceClientException(ex.getMessage());
        }
    
    }
	
	/**
	 * Ejecuta una petición HttpUriRequest
	 * @param request HttpUriRequest
	 * @return HttpResponse
	 * @throws ClientProtocolException
	 * @throws IOException
	 */
	private HttpResponse executeRequest(HttpUriRequest request) throws ClientProtocolException, IOException
	{
		return ( HttpClientBuilder.create().build() ).execute(request);
	}
	
	
	/**
	 * Ejecuta una petición HttpGet
	 * @param actionRequest String
	 * @return HttpResponse
	 * @throws IOException 
	 * @throws ClientProtocolException 
	 */
	private HttpResponse executeGetRequest( String actionRequest ) throws ClientProtocolException, IOException
	{
		HttpGet request = new HttpGet ( this.getUri() + actionRequest  );
		this.getAuth().buildHeader(request, this.getSessionToken() );	
		
		return this.executeRequest(request);
	}
	
	/**
	 * Ejecuta una petición HttpPost
	 * @param actionRequest String
	 * @return HttpResponse
	 * @throws IOException 
	 * @throws ClientProtocolException 
	 */
	private HttpResponse executePostRequest( String actionRequest, List<NameValuePair> parameters ) throws ClientProtocolException, IOException
	{
		HttpPost request = new HttpPost ( this.getUri() + actionRequest  );
		request.setEntity(new UrlEncodedFormEntity(parameters));
		this.getAuth().buildHeader( request, this.getSessionToken(), parameters );		
		
		return this.executeRequest(request);
	}
	
	/**
	 * Ejecuta una petición CloseableHttpClient para solicitar una imagen
	 * @param imageUri String
	 * @return CloseableHttpResponse
	 * @throws IOException 
	 * @throws ClientProtocolException 
	 */
	private CloseableHttpResponse executeGetImageRequest( String imageUri ) throws ClientProtocolException, IOException
	{
		CloseableHttpClient httpclient = HttpClients.custom().build();
		HttpGet request = new HttpGet(imageUri);
		request.setHeader("Content-Type", "image/png");
	
		return httpclient.execute(request);		
	}	
	
	/**
	 * Obtiene el QR de una transacción
	 * @param uri String
	 * @return String
	 * @throws IOException 
	 * @throws ClientProtocolException 
	 * @throws ParseException 
	 * @throws JsonSyntaxException 
	 */
	private String retrieveQrUri( String uri ) throws JsonSyntaxException, ParseException, ClientProtocolException, IOException
	{
            QrImage qrImage = (this.createGson()).fromJson( EntityUtils.toString( this.executeGetRequest( uri ).getEntity(), HttpUtils.DEFAULT_CHARSET), QrImage.class);

            if( qrImage != null && qrImage.qrUrl != null && qrImage.qrUrl != "" )
            {
                return qrImage.qrUrl;
            }

            return null;
	}
	
	
	/**
	 * Realiza el parseo de una respuesta HttpResponse
	 * @param response HttpResponse
         * @param classOfT Class<T>
         * @param statusOK int
	 * @return <T>  T
	 * @throws JsonSyntaxException
	 * @throws ParseException
	 * @throws IOException
	 * @throws ServiceClientException
	 */
	private <T>  T parseHttpResponse( HttpResponse response, Class<T> classOfT, int statusOK ) throws JsonSyntaxException, ParseException, IOException, ServiceClientException
	{
		if( response.getStatusLine().getStatusCode() == statusOK )
		{
			return (this.createGson()).fromJson( (EntityUtils.toString( response.getEntity(), HttpUtils.DEFAULT_CHARSET)), classOfT);	
		}
                else if( response.getStatusLine().getStatusCode() == ServiceClientException.ERROR_STATUS_UNAUTHORIZED )
                {
                    throw new ServiceClientException( ServiceClientException.ERROR_STATUS_UNAUTHORIZED_MESSAGE );	
                }
		else
		{
			HttpUtils.Fail fail = HttpUtils.processFailResponse( response );

			if( fail != null )
				throw new ServiceClientException( fail ); 
			else
				throw new ServiceClientException( ServiceClientException.ERROR_DONT_CAUGHT );			
		}
	}
	
	/**
	 * Create GSon Serializator
	 * @return Gson
	 */
	private Gson createGson()
	{
		return new GsonBuilder().setDateFormat(HttpUtils.DEFAULT_FORMAT_DATE).create();
	}


	/**
     * Obtiene la fecha de expiración de la sesion
	 * @return Date
	 */
	public Date getSessionExpires() 
	{
		return sessionExpires;
	}

}


