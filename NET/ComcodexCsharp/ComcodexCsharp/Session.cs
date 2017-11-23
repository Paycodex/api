/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 11:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{

	/// <summary>
	/// Sesión.
	/// </summary>
	class Session
	{
		public string sid = null;
		public int maxAge = -1;
		public string expires = null;
		public string now = null;


		public string toString()
		{
			return " Token: " + this.sid + " Time:" + this.maxAge + " Expires:" + this.expires + "  Created:" + this.now;
		}		
	}	
	
}
