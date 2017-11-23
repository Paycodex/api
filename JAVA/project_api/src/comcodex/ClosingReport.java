/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package comcodex;

import com.google.gson.*;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.time.format.DateTimeFormatter;
/**
 * Cierre de operaciones
 * @author ebrainc
 */
public class ClosingReport {
    
    public String hourInit;
    public String hourFinish;
    public String accountNumber;
    public String transaction;
    public String detail;
    public String totalPurchases;
    public String totalReverted;
    public String totalByCard;
    public String globalTotal;
    

    /**
     * Obtiene el listado de transacciones del reporte de cierre
     * 
     * @return Transaction[]
     * @throws ServiceClientException 
     */
    public Transaction[] getTransaction() throws ServiceClientException{
 
        JsonParser jsonParse = new JsonParser();
        
        JsonElement ClosingReportTransaction = jsonParse.parse(this.transaction);
        
        Transaction[] transactions = new Transaction[ClosingReportTransaction.getAsJsonArray().size()];
        
        Integer i = 0;
        
        for (JsonElement element : ClosingReportTransaction.getAsJsonArray()) {

            JsonObject closingElementReport = new Gson().fromJson(element.toString(), JsonObject.class);
            
            Currency currency = new Currency(closingElementReport.get("amount").getAsJsonObject().get("number").getAsInt(), closingElementReport.get("amount").getAsJsonObject().get("decimal").getAsInt());
            
            try{

                String openDateStr = closingElementReport.get("openDate").toString();
                openDateStr = openDateStr.replace("\"", "");
               
                System.out.println(7.8);
                transactions[i]                 = new Transaction();
                transactions[i].id              = closingElementReport.get("id").toString();
                Date openDate                   = this.parse(openDateStr);
                transactions[i].openDate        = openDate;
                transactions[i].device          = closingElementReport.get("device").toString();
                transactions[i].amount          = currency;    
                transactions[i].concept         = closingElementReport.get("concept").toString();
                transactions[i].token           = closingElementReport.get("token").toString();
                transactions[i].payed           = closingElementReport.get("payed").getAsBoolean();
                transactions[i].setGateWayResponse(closingElementReport.get("gateWayResponse").toString());
                transactions[i].status          = closingElementReport.get("status").getAsInt();
                
            }catch(Exception ex){
                throw new ServiceClientException(ex.getMessage());
            }
            
            
            i = i + 1;

        }
        
        return transactions;
    
    }
    
    private Date parse(String input) throws java.text.ParseException {

        //NOTE: SimpleDateFormat uses GMT[-+]hh:mm for the TZ which breaks
        //things a bit.  Before we go on we have to repair this.
        SimpleDateFormat df = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSSz");

        //this is zero time so we need to add that TZ indicator for 
        if (input.endsWith("Z")) {
            input = input.substring(0, input.length() - 1) + "GMT-00:00";
        } else {
            int inset = 6;

            String s0 = input.substring(0, input.length() - inset);
            String s1 = input.substring(input.length() - inset, input.length());

            input = s0 + "GMT" + s1;
        }

        return df.parse(input);

    }
    
}
