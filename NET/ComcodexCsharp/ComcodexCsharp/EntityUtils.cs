/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 14:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// Helper estático para tratar con instancias HttpEntity.
	/// </summary>
	public class EntityUtils
	{
		
		/// <summary>
		/// 
		/// </summary>
		public EntityUtils()
		{
		}
		
				
		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <param name="charset"></param>
		public static string ToString( HttpEntity entity, string charset)
		{
			return entity.Content;
		}
	}
}
