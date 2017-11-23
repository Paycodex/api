/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 11:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace Comcodex
{
	/// <summary>
	/// Transacción.
	/// </summary>
	public class Transaction 
	{
	
		public string 				id;
		public Nullable<DateTime>	openDate;
		public string 				device;
		public string 				bankProfileId;
		public Currency	 			amount;
		public string 				concept;
		public string  				token;
		public bool 				payed;
		public Nullable<DateTime>	payedDate;
		public string  				payedCardHolder;
		public string 				payedCardNumber;
		public string  				payedIdentity;
		public int					status;

		/// <summary>
		/// Transacción revertida.
		/// </summary>
        public const int STATUS_REVERTED = 5;
        /// <summary>
        /// Pago rechazado.
        /// </summary>
		public const int STATUS_REJECTED = 3;
		/// <summary>
		/// Pago fallido.
		/// </summary>
		public const int STATUS_FAIL = 4;
		/// <summary>
		/// Pago exitoso.
		/// </summary>
		public const int STATUS_OK = 2;
		/// <summary>
		/// Transacción en espera.
		/// </summary>
		public const int STATUS_WAITING = 0;
		
		private string gatewayResponseText = "";
		
	    [JsonExtensionData]
	 	private IDictionary<string, JToken> _additionalData = null;
	 	
		
		/// <summary>
		/// 
		/// </summary>
		public Transaction()
		{
     
		}
			
	    [OnDeserialized]
	    private void OnDeserialized(StreamingContext context)
	    {
	        // SAMAccountName is not deserialized to any property
	        // and so it is added to the extension data dictionary
	        this.gatewayResponseText = (string)_additionalData["gateWayResponse"];

	    }
		
	    /// <summary>
	    /// Asigna la respuesta del gateway
	    /// </summary>
	    public void setGateWayResponse(String value){
	    	this.gatewayResponseText = value;
	    }
		
	    
	    /// <summary>
	    /// Obtiene la respuesta del Gateway
	    /// </summary>
	    public GatewayResponse gatewayResponse
		{
			get{
				if(  this.gatewayResponseText != null )
				{
					GatewayResponse gatewayResponse = new GatewayResponse();
					
					try{
						
						JToken token = JObject.Parse( this.gatewayResponseText );
					
						gatewayResponse.id = (string)token.SelectToken("id");
						gatewayResponse.source = (string)token.SelectToken("source");
						gatewayResponse.reference = (string)token.SelectToken("reference");
						gatewayResponse.orderNumber = (string)token.SelectToken("orderNumber");
						gatewayResponse.message = (string)token.SelectToken("message");
						gatewayResponse.gateWay = (string)token.SelectToken("gateWay");
						gatewayResponse.statusCode = (string)token.SelectToken("statusCode");
						gatewayResponse.status = (bool)token.SelectToken("status");
						
					}catch(Exception ex)
					{
						Debug.WriteLine(ex.Message);
					}
					
					
					return gatewayResponse;
				}
				return null;
			}
		}
		
	}

}
