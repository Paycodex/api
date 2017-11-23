/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 11:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{

	/// <summary>
	/// Configuración de conexión.
	/// </summary>
	public class Setting 
	{
	
		public string 	pathImage;
		public int 		proxyPort;
		public string 	proxyHost;
		public string 	proxyUser;
		public string 	proxyPassword;
		public string 	serviceUri;
		public string 	servicePort;
		public string 	clientKey;
		public string 	secretPhrase;
		public string 	key;
        public string   device;
		
		/// <summary>
		/// 
		/// </summary>
		public Setting()
		{
			
		}
		
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="file"></param>
		public Setting( String file )
		{
			this.loadXml( file );
		}
		
		
		/**
		 * 
		 * @param String fileConfig
		 * @throws ConfigurationException 
		 */
		public void loadXml(String file)
		{	

	
		}
		
	}

	
	
}
