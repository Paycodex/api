/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 11:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace Comcodex
{
    
    /// <summary>
    /// Respuesta Gateway
    /// </summary>
    public class GatewayResponse
    {
        public string gateWay = null;
        public string source = null;
        public string orderNumber = null;
        public string reference = null;
        public string statusCode = null;
        public string message = null;
        public bool status = false;
        public string id = null;

        public GatewayResponse()
        {

        }

        override public string ToString()
        {
            return  "Transaction: " + this.id + " - Gateway Name: " +  this.gateWay  + " - Source: " + this.source;
        }
    }
}