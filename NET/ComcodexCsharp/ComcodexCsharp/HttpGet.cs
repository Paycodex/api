/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// HTTP GET method.
	/// </summary>
	public class HttpGet: HttpUriRequest
	{
		
		public HttpGet(string url):base(url)
		{
			this.Method = HttpUtils.METHOD_GET;
		}
	}
}
