/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 11:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;



namespace Comcodex
{
	/// <summary>
	/// Currency
	/// </summary>
	public class Currency {
		
		private int _numberValue;
		private int _decimalValue;
		private int _money;
			
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="numberValue"></param>
		/// <param name="decimalValue"></param>
		public Currency( int numberValue, int decimalValue )
		{
			this._numberValue = numberValue;
			this._decimalValue = decimalValue;
			this._money		= 0;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public double toDouble()
		{	
			return Double.Parse(this._numberValue.ToString() + System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString() + this._decimalValue.ToString());
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public int Decimal {
			get { return _decimalValue; }
			set { _decimalValue = value; }
		}

		
		/// <summary>
		/// 
		/// </summary>
		public int Number {
			get { return _numberValue; }
			set { _numberValue = value; }
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public int Money {
			get { return _money; }
			set { _money = value; }
		}
		
	}	
}
