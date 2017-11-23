/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 14:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// Constructor de instancias CloseableHttpClient.
	/// </summary>
	public class HttpClientBuilder
	{
		protected HttpClientBuilder()
		{
		}
		
		
		public static HttpClientBuilder Create()
		{
			return new HttpClientBuilder();
		}
		
		public CloseableHttpClient Build()
		{
			return new CloseableHttpClient();
		}
	}
}
