/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 14:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// Constructor de instancias Gson.
	/// </summary>
	public class GsonBuilder
	{
		
		/// <summary>
		/// 
		/// </summary>
		public GsonBuilder()
		{
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="format"></param>
		public GsonBuilder SetDateFormat(string format)
		{
			return new GsonBuilder();
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Gson Create()
		{
			return new Gson();
		}
		
	}
}
