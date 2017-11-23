/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 14:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Comcodex
{
	/// <summary>
	/// Una entidad compuesta de una lista de pares de url codificadas. Esto suele ser útil cuando se envía una solicitud HTTP POST.
	/// </summary>
	public class UrlEncodedFormEntity
	{
		private List<NameValuePair> parameters;
			
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameters"></param>
		public UrlEncodedFormEntity(List<NameValuePair> parameters)
		{
			this.parameters = ( parameters == null? new List<NameValuePair>():parameters);
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public List<NameValuePair> Parameters{
			get{
				return this.parameters;
			}
			set{
				this.parameters = value;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string entityToJson()
		{
			Dictionary<string, string> list = new Dictionary<string, string>();
			foreach( NameValuePair parameter in this.parameters )
			{
				list.Add( parameter.Name, parameter.Value );
			}
			return JsonConvert.SerializeObject(list, Formatting.Indented);
		}
		
		
	}
}
