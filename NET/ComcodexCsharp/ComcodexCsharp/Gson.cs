/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Newtonsoft.Json;

namespace Comcodex
{
	/// <summary>
	/// Convertidor de objetos de Java y Json.
	/// </summary>
	public class Gson
	{
		public Gson()
		{
		}
		
		
		public T FromJson<T>( string json )
		{
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 3 });
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public string ToJson( object obj )
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
