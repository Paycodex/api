/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace Comcodex
{
	/// <summary>
	/// Respuesta CloseableHttp.
	/// </summary>
	public class CloseableHttpResponse:  HttpResponse
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="contextStream"></param>
		/// <param name="statusCode"></param>
		public CloseableHttpResponse(Stream contextStream, int statusCode ) : base( contextStream, statusCode )
		{
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public void Close()
		{
			
		}
		

		
	}
}
