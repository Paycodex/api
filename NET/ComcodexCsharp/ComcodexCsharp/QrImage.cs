/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 14:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// Imagen QR.
	/// </summary>
	public class QrImage
	{
		
		private string qrUrl;
		
		/// <summary>
		/// 
		/// </summary>
		public QrImage()
		{
		}
		
		/// <summary>
		/// Url del QR
		/// </summary>
		public string QrUrl {
			get { return qrUrl; }
			set { qrUrl = value; }
		}
		
	}
}
