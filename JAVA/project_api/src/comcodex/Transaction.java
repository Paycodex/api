package comcodex;

import com.google.gson.*;
import java.util.Date;
import java.io.*;
import java.lang.reflect.*;

/**
 * Transacción
 * 
 * @author Koiosoft
 *
 */
public class Transaction{

	public  String id;
	public  Date openDate;
	public  String device;
	public  String bankProfileId;
	public  Currency amount;
	public  String concept;
	public  String token;
	public  Boolean payed;
	public  Date payedDate;
	public  String payedCardHolder;
	public  String payedCardNumber;
	public  String payedIdentity;
    public  String deviceId;
    private String gateWayResponse;
    public int status;

    /**
     * Transacción revertida
    */
    public static final int STATUS_REVERT = 5;
    /**
     * Pago rechazado
    */
    public static final int STATUS_REJECTED = 3;
    /**
     * Pago fallido
    */
    public static final int STATUS_FAIL = 4;
    /**
     * Pago exitoso
    */
    public static final int STATUS_OK = 2;
    /**
     * Transacción en espera
    */
    public static final int STATUS_WAITTING = 0;
    
    /**
     * Asigna la respuesta del Gateway
     * 
     * @param value String
     */
    public void setGateWayResponse(String value){
        this.gateWayResponse = value;
    }
    
    /**
     * Obtiene la respuesta del Gateway
     * 
     * @return response GatewayResponse
     */
    public GatewayResponse getGateWayResponse(){
        
        GatewayResponse response = null;
        if (this.gateWayResponse != null){
        
            response = new Gson().fromJson(this.gateWayResponse, GatewayResponse.class);
            
        }else{
            response = new GatewayResponse();
        }
        
        return response;
    
    }
    
    
}
