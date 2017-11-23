/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 14/11/2016
 * Time: 08:55 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;


namespace Comcodex
{
	/// <summary>
	/// Cierre de operaciones.
	/// </summary>
	public class ClosingReport
	{
	    public String hourInit;
	    public String hourFinish;
	    public String accountNumber;
	    public String transaction;
	    public String detail;
	    public String totalPurchases;
	    public String totalReverted;
	    public String totalByCard;
	    public String globalTotal;
	    
	    /// <summary>
	    /// Obtiene el listado de transacciones del reporte de cierre.
	    /// </summary>
	    public Transaction[] getTransaction(){
	    
			Transaction[] transactions = null;
	    	object[] resultObjects = null;
	    	
	    	try {
				
	    		resultObjects = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<object[]>(this.transaction);
		    	transactions = new Transaction[resultObjects.Length];
				Transaction transaction = new Transaction();
				int count = 0;
				
		        foreach (object result in resultObjects) {
					
					IEnumerable resultEnumerable = (IEnumerable)result;
					transaction = new Transaction();
					
					foreach (object item in resultEnumerable) {
						
						System.Reflection.PropertyInfo propertyInfo =  item.GetType().GetProperty("key");
						var key = item.GetType().GetProperty("Key").GetValue(item, null);
						var value = key;
						try{
							value = item.GetType().GetProperty("Value").GetValue(item, null);
						}catch
						{
							continue;
						}

						switch(key.ToString())
						{
								
							case "amount":
								
								IEnumerable valueEnumerable = (IEnumerable)value;
								int decimalValue = 0;
								int numberValue = 0;
								
								foreach (object itemAmount in valueEnumerable) {
									
									var keyAmount = itemAmount.GetType().GetProperty("Key").GetValue(itemAmount, null);
									var valueAmount = itemAmount.GetType().GetProperty("Value").GetValue(itemAmount, null);
									
									if(keyAmount.ToString() == "decimal" ){
										decimalValue = Convert.ToInt16(valueAmount.ToString());
									}
									
									if(keyAmount.ToString() == "number"){
										numberValue = Convert.ToInt32(valueAmount.ToString());
									}
									
								}
								
								transaction.amount = new Comcodex.Currency(numberValue, decimalValue);
								break;
								
							case "id":
								
								transaction.id = value.ToString();
								break;
							
							case "token":
								
								transaction.token = value.ToString();
								break;
								
							case "openDate":
								
								transaction.openDate = DateTime.Parse(value.ToString());
								break;
								
							case "device":
								
								transaction.device = value.ToString();
								break;
								
							case "concept":
								
								transaction.concept = value.ToString();
								break;
								
							case "bankProfileId":
								
								transaction.bankProfileId = value.ToString();
								break;
								
							case "payed":
								
								transaction.payed = Convert.ToBoolean(value.ToString());
								break;
								
							case "payedDate":
								
								transaction.payedDate = DateTime.Parse(value.ToString());
								break;
								
							case "payedCardHolder":
								
								transaction.payedCardHolder = value.ToString();
								break;
								
							case "payedCardNumber":
								
								transaction.payedCardNumber = value.ToString();
								break;
								
							case "payedIdentity":
								
								transaction.payedIdentity = value.ToString();
								break;
								
							case "status":
								
								transaction.status = Convert.ToInt16(value.ToString());
								break;
								
							case "gateWayResponse":
								
								transaction.setGateWayResponse(value.ToString());
								break;								
								
							default:
								
								break;
						}

					}
					transactions[count] = transaction;
					count++;
		        }
			} 
			catch (Exception e) 
			{
				Debug.WriteLine(e.ToString());
				
			}
	        
	        return transactions;
	        
	    
	    }
	}
}
