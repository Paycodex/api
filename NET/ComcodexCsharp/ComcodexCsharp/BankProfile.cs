/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
	/// <summary>
	/// Perfil Bancario.
	/// </summary>
	public class BankProfile
	{
	    public string id;
	    public string alias;
	    public string account;
	    public string bank;
	    
	    public string toString()
	    {
	    		return "Id: " + this.id + ", Alias: " + this.alias + ", Account: " + this.account + ", Bank: " +  this.bank;
	    }
	}
}
