/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 14:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// Constructor de instancias CloseableHttpClient.
	/// </summary>
	public class HttpClient
	{
		
		/// <summary>
		/// 
		/// </summary>
		public HttpClient()
		{
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static HttpClientBuilder Custom()
		{
			
			return HttpClientBuilder.Create();
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public CloseableHttpClient  build()
		{
			return new CloseableHttpClient();
		}
		
	}
}
