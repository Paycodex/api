﻿/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 13:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// Estado de la respuesta HTTP.
	/// </summary>
	public class StatusLine
	{
		private int statusCode = HttpUtils.HTTP_NONE;
		
		public StatusLine()
		{
		}
		
		public int StatusCode {
			get { return statusCode; }
			set { statusCode = value; }
		}
		
		
	}
}
