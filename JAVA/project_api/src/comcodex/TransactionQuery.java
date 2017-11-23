package comcodex;

import java.util.Date;

/**
 * Objeto de Consulta de Lista de Transacciones
 * 
 * @author Koiosoft
 *
 */
public class TransactionQuery {
	
    /**
     * Dispositivo.
     */
    public String device;
    /**
     * Fecha de inicio.
     */
    public Date beginDate;
    /**
     * Fecha fin.
     */
    public Date endDate;
    /**
     * Id del Perfil Bancario.
     */
    public String bankProfileId;
    /**
     * Id Dispositivo.
     */
    public String deviceId;
    /**
     * Status.
     */
    public String status;

    /**
     * 
     */
    protected static String FIELD_DEVICE 		= "device";
    /**
     * 
     */
    protected static String FIELD_BEGIN_DATE 		= "beginDate";
    /**
     * 
     */
    protected static String FIELD_END_DATE 		= "endDate";
    /**
     *
     */
    protected static String FIELD_BANKPROFILE_ID         = "bankProfileId";
    /**
     *
     */
    protected static String FIELD_STATUS                = "status";
    
    /**
     * Transacción revertida
    */
    public static Integer STATUS_APPOVED = 2;
    /**
     * Pago exitoso
    */
    public static Integer STATUS_REVERT  = 5;
    
	
}
