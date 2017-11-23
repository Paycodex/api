/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 11:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace Comcodex
{

	/// <summary>
	/// Objeto de Consulta de Lista de Transacciones
	/// </summary>
	public class TransactionQuery {
		
		/// <summary>
		/// Dispositivo.
		/// </summary>
		public string 				device			= null;

		/// <summary>
		/// Id Dispositivo.
		/// </summary>
		public string 				IdDevice			= null;
		/// <summary>
		/// Fecha de inicio.
		/// </summary>
		public Nullable<DateTime> 	beginDate		= null;
		/// <summary>
		/// Fecha fin.
		/// </summary>
		public Nullable<DateTime>	endDate			= null;
		/// <summary>
		/// Id del Perfil Bancario.
		/// </summary>
		public string 				bankProfileId	= null;
		/// <summary>
		/// Status.
		/// </summary>
        public string               status          = null;
		
		/// <summary>
		/// Campo dispositivo.
		/// </summary>
		public const String FIELD_DEVICE 			= "device";
		/// <summary>
		/// Campo fecha de inicio.
		/// </summary>
		public const String FIELD_BEGIN_DATE 		= "beginDate";
		/// <summary>
		/// Campo fecha fin.
		/// </summary>
		public const String FIELD_END_DATE 			= "endDate";
		/// <summary>
		/// Campo id del Perfil Bancario.
		/// </summary>
		public const String FIELD_BANKPROFILE_ID 	= "bankProfileId";
		/// <summary>
		/// Campo Status.
		/// </summary>
        public const String FIELD_STATUS            = "status";


		/// <summary>
		/// Pago exitoso.
		/// </summary>
        public const int STATUS_APPROVED = 2;
        

		/// <summary>
		/// Transacción revertida.
		/// </summary>
        public const int STATUS_REVERTED = 5;

		/// <summary>
		/// 
		/// </summary>
		public TransactionQuery()
		{
			
		}
		
	}

	
}
