/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 14:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Comcodex
{
	/// <summary>
	/// Gestion de ejecución de solicitudes HTTP.
	/// </summary>
	public class CloseableHttpClient
	{
		
		/// <summary>
		/// 
		/// </summary>
		public CloseableHttpClient()
		{
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="uriRequest"></param>
		/// <returns></returns>
		public CloseableHttpResponse Execute(HttpUriRequest uriRequest)
		{
								
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriRequest.Uri);
		    request.Method = uriRequest.Method;
		    request.ContentType = uriRequest.ContentType;
		
		    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
		    
		    //
		    //  Se construyen los headers de la solcitud
		    //
		    foreach( NameValuePair param in uriRequest.Headers )
		    {
		   	 	request.Headers.Add( param.Name, param.Value );
		    }
		        
		    //
		    //  Solo para POST y PUT estan previsto el envio de parametros en forma de JSON
		    //
		    if( uriRequest.Method == HttpUtils.METHOD_POST || uriRequest.Method == HttpUtils.METHOD_PUT )
		    {	     		    
			    using (var streamWriter = new System.IO.StreamWriter(request.GetRequestStream()))
				{
				    streamWriter.Write( uriRequest.Entity.entityToJson());
				    streamWriter.Flush();
				    streamWriter.Close();
				}
		    }
				
			HttpWebResponse  response;
				
		    try 
		    {
		    		response = (HttpWebResponse) request.GetResponse();
		    }
		    catch (WebException ex) 
		    {

		    	HttpUtils.Fail fail = new HttpUtils.Fail();
		    			    	
		    	fail.message = ex.Message;
		    	throw new ServiceClientException(fail);
		    }
		    
            return new CloseableHttpResponse(response.GetResponseStream(), (int)response.StatusCode);

		}
		
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public HttpResponse Execute()
		{	
			return new HttpResponse(new System.IO.MemoryStream(), 0 );
		}
		
	}
}
