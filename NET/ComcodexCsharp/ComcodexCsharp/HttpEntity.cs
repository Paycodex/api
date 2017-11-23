/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 14:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// Entidad HTTP.
	/// </summary>
	public class HttpEntity
	{
		private string content;
		
		
		/// <summary>
		/// 
		/// </summary>
		public HttpEntity()
		{
		}
		
		/// <summary>
		/// 
		/// </summary>
		public string Content {
			get { return content; }
			set { content = value; }
		}
	}
}
