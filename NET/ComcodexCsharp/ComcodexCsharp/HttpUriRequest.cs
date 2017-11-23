/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Comcodex
{
	/// <summary>
	/// Solicitud HTTP.
	/// </summary>
	public class HttpUriRequest
	{
		
		private UrlEncodedFormEntity entity;
		private Uri uri;
		private string method;
		private List<NameValuePair> headers;
		private string contentType;
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="uriString"></param>
		public HttpUriRequest(string uriString)
		{
			this.uri = new Uri(uriString);
			this.headers = new List<NameValuePair>();
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public Uri Uri {
			get { return uri; }
			set { uri = value; }
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="headerName"></param>
		/// <param name="headerValue"></param>
		public void addHeader( string headerName, string headerValue )
		{
			this.headers.Add(new NameValuePair(headerName, headerValue));
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public UrlEncodedFormEntity Entity {
			get { return entity; }
			set { entity = value; }
		}
		
		
		
		/// <summary>
		/// 
		/// </summary>	
		public string Method {
			get { return method; }
			set { method = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		public List<NameValuePair> Headers {
			get { return headers; }
			set { headers = value; }
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public string ContentType {
			get { return contentType; }
			set { contentType = value; }
		}
		
	}
}
