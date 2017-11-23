/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// HTTP POST method.
	/// </summary>
	public class HttpPost: HttpUriRequest
	{
		
		public HttpPost(string url):base(url)
		{
			this.Method = HttpUtils.METHOD_POST;
		}
	}
}
