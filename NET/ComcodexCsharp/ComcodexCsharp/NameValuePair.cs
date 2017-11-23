/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	

	/// <summary>
	/// Representa un par nombre-valor.
	/// </summary>
	public class NameValuePair: IComparable<NameValuePair>
	{
		private string name;
		private string value;
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
        public int CompareTo(NameValuePair obj)
        {
            return this.Name.CompareTo(obj.Name);
        }
        
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public NameValuePair(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public string Name {
			get { return this.name; }
			set { this.name = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Value {
			get { return this.value; }
			set { this.value = value; }
		}
		
	}
}
