/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 03/10/2014
 * Time: 15:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Comcodex;
using System.Collections;
using System.Collections.Specialized;

namespace PosSimulator
{
	/// <summary>
	/// Description of BankProfileListItem.
	/// </summary>
	public class BankProfileListItem
	{
				
		private BankProfile bankProfile = null;
		
		
		/// <summary>
		/// 
		/// </summary>
		public string Label
		{
			get
			{
				return this.bankProfile.alias;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public string Value
		{
			get
			{
				return this.bankProfile.id;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="transaction"></param>
		public BankProfileListItem(BankProfile bankProfile)
		{
			this.bankProfile = bankProfile;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="transactions"></param>
		/// <returns></returns>
		public static ArrayList buildList( BankProfile[] bankProfiles )
		{
			ArrayList arrayList =  new ArrayList();
			
			foreach( BankProfile bankProfile in bankProfiles )
			{
				arrayList.Add( new BankProfileListItem(bankProfile) );
			}
			
			return arrayList;
		}

		
	}
}
