/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace Comcodex
{
	/// <summary>
	/// Respuesta HTTP.
	/// </summary>
	public class HttpResponse
	{
		private StatusLine statusLine;
		private HttpEntity entity;
		private MemoryStream contextStream;
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="contextStream"></param>
		/// <param name="statusCode"></param>
		public HttpResponse(Stream contextStream, int statusCode)
		{
			this.statusLine 				= new StatusLine();
			this.entity					= new HttpEntity();
			this.statusLine.StatusCode 	= statusCode;
			
			this.contextStream 			= this.CopyStreamToMemory(contextStream);
			this.entity.Content 			= (new StreamReader(this.contextStream, true)).ReadToEnd();
			this.contextStream.Position	= 0;
			
		}
		
		
		/// <summary>
		/// Estado de la respuesta HTTP.
		/// </summary>
		public StatusLine StatusLine {
			get { return statusLine;}
			set { statusLine = value;}
		}
		
		/// <summary>
		/// Entidad HTTP.
		/// </summary>
		public HttpEntity Entity {
			get { return this.entity;}
			set { this.entity = value; }
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public MemoryStream ContextStream {
			get { return contextStream; }
			set { contextStream = value; }
		}
		
		private MemoryStream CopyStreamToMemory(Stream inputStream)
		{
		    MemoryStream ret = new MemoryStream();
		    const int BUFFER_SIZE = 1024;
		    byte[] buf = new byte[BUFFER_SIZE];
		
		    int bytesread = 0;
		    while ((bytesread = inputStream.Read(buf, 0, BUFFER_SIZE)) > 0)
		        ret.Write(buf, 0, bytesread);
		
		    ret.Position = 0;
		    return ret;
		}
		
	}
}
