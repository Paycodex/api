/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 25/09/2014
 * Time: 16:18
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
	/// Description of TransactionListItem.
	/// </summary>
	public class TransactionListItem
	{
		
		private Transaction transaction = null;
		
		
		/// <summary>
		/// 
		/// </summary>
		public string Label
		{
			get
			{
				return this.transaction.openDate.ToString() + " - " +  this.transaction.device  + " - " + "#" + this.transaction.concept ;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public string Value
		{
			get
			{
				return this.transaction.token;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="transaction"></param>
		public TransactionListItem(Transaction transaction)
		{
			this.transaction = transaction;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="transactions"></param>
		/// <returns></returns>
		public static ArrayList buildList( Transaction[] transactions )
		{
			ArrayList arrayList =  new ArrayList();
			
			foreach( Transaction transaction in transactions )
			{
				arrayList.Add( new TransactionListItem(transaction) );
			}
			
			return arrayList;
		}
	}
	
	

	
}
