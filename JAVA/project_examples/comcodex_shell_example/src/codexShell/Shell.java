package codexShell;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;

import comcodex.*;

public class Shell {

	public static void main(String[] args) {
		
		// TODO Auto-generated method stub
		System.out.println("Iniciando la Prueba");
		
		// TODO Auto-generated method stub
		Setting setting = new Setting();
		
		/*
		setting.serviceUri   = "http://192.168.10.106:8110";//"http://api.comcodex.dev";
		setting.servicePort  = "";
		setting.secretPhrase = "SECRET-B7728D-977E71-52CD01-372098";//"SECRET-E5A664-6BA7C4-944FC1-22D92C";
		setting.clientKey	 = "CCODEX-55CE41-7BA34B-253530-000002";//"CCODEX-5579E6-0582A9-7DB44A-000002";
		setting.qrServiceUri = "http://comcodex.local/image/qrImage/:token.png";
		setting.pathImage 	 = "/home/jcarrillo/Documents/Temp/";//"/home/ebrainc/Documents/qr/";
		setting.device		 = "PC_API_TEST";
		*/
		
                /*
                setting.serviceUri   = "http://192.168.0.110";
                setting.servicePort  = "8082";
                setting.secretPhrase = "SECRET-F2A289-E763A9-9CBCE8-CE28FD";
                setting.clientKey	 = "CCODEX-584073-4CE556-C6F202-000017";
                setting.qrServiceUri = "http://192.168.0.110:8080/image/qrImage/:token.png";
                setting.pathImage 	 = "/home/jcarrillo/Documents/qr/";
                setting.device		 = "PC_API_TEST";
                */
                
                setting.serviceUri = "https://qa-api.paycodex.com";
                setting.servicePort  = "";
                setting.secretPhrase = "SECRET-276700-A8FBB7-8C789C-A3978F";
                setting.clientKey = "CCODEX-5915FB-61EC74-FE5304-00000D";
                setting.qrServiceUri = "http://qa.paycodex.com/image/qrImage/:token.png";
                setting.pathImage = "/home/jcarrillo/Documents/qr/";
                setting.device = "PC_API_TEST";
		
		ServiceClient client = new ServiceClient(setting);

		try
		{
			//client.connect();
			client.connect();
			
						
			Transaction newTransaction 	= new Transaction();
			newTransaction.amount 			= Shell.checkAmount( "220.32" );
			newTransaction.bankProfileId 	= "55d3a2809497c76815000018";
			//newTransaction.bankProfileId 	= "55cc955dc5e33d0d1c000002";
			newTransaction.concept 			= " COMPRA FACTURA 000005";
			
            
            
			Transaction transaction = client.openTransaction(newTransaction);

			
			System.out.println(  (new com.google.gson.Gson().toJson( transaction ) ) );
			
			
			/*
			Transaction newTransaction 		= new Transaction();
			newTransaction.token = "cea6f1ad666f8b7f447c446c03a12ced";

			String pathImageQR = client.getQrImage(newTransaction);
			
			if( pathImageQR != null )
			{
				System.out.println("Imagen de la transaccion " + newTransaction.token  + " fue creada exitosamente en : " + pathImageQR );
			}
			else
			{
				System.out.println("Falla trayendo la Imagen de la transaccion ");
			}*/
            
			/*
			DateFormat df = new SimpleDateFormat("dd/MM/yy HH:mm:ss");
			TransactionQuery query  = new TransactionQuery();

			query.bankProfileId = "55784dc98c07eb0513000006";
            
			query.beginDate = df.parse("08/07/2015 01:00:00");//new Date();
            query.endDate   = df.parse("15/08/2015 01:00:00");
            
			
			Transaction[] listTransactions = client.listTransactions(query);
			
			System.out.println( " Inicio lista ");
			
            
            
			for( Transaction transaction : listTransactions )
			{  
                //System.out.println(  (new com.google.gson.Gson().toJson( transaction ) ) );
				//System.out.println(transaction.getGateWayResponse().gateWay);
                System.out.println(transaction.concept);

			}
			*/
			
			//   Token:  5f92ce6d549885d29579de4b3e184b23
			/*
			System.out.println("############################");
			Transaction transaction = client.retrieveTransaction("627372528e1cce2521152cad1f4f952d");
			System.out.println("##############");
			System.out.println("Antes de Revertir: ");
			
			System.out.println("concepto: " + transaction.concept);
			System.out.println("Status: " + transaction.status);
			*/
			
			/*
			System.out.println("##############");
			System.out.println("Despues de Revertir: ");
			Transaction transactionReverted = client.revertTransaction(transaction);
			
			System.out.println("concepto: " + transactionReverted.concept);
			System.out.println("Status: " + transactionReverted.status);
			*/
			//Transaction t = client.retrieveTransaction("88fd2b5388c4681dd7862ae2ab300819");
			
			//System.out.println( "Transaccion traida del server:" );
			//System.out.println(  (new com.google.gson.Gson().toJson( t ) ) );
            
            
            
            /*
            ClosingReport closingReport = client.closingReport(query);
            System.out.println((new com.google.gson.Gson().toJson( closingReport ) ));
            */
            
            
			
			System.out.println("Fin");
		}
		catch( ServiceClientException e )
		{
			System.out.println("ServiceClientException");
			System.out.println(e.getMessage());
			if( e.getFail() != null )
				System.out.println( "Error Shell:  " + e.getFail().toString() );
		} 
		catch (Exception e) 
		{System.out.println(e.getMessage());
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		

		
	}
	

    /**
     * 
     * @param value
     * @return 
     */
    public static Currency checkAmount( String value ) throws ServiceClientException
    {
        
        DecimalFormat format = (DecimalFormat)DecimalFormat.getInstance();
        DecimalFormatSymbols symbols = format.getDecimalFormatSymbols();
        char sep = symbols.getDecimalSeparator();

        String[] numericParts = value.split( String.valueOf("\\" + sep) );
        
        Currency currency = null;

        if( numericParts.length == 0) 
        {
            try
            {
                currency = new Currency( Integer.parseInt( value ), 0 );
            }
            catch( Exception ex)
            {
                throw new ServiceClientException("Error en el formato numerico. Debe ser del tipo 999" + sep + "99" );
            }
        }
        else if(   numericParts.length == 1 )
        {
            try
            {
                currency = new Currency(  Integer.parseInt( numericParts[0]), 0 );
            }
            catch( Exception ex)
            {
                 throw new ServiceClientException("Error en el formato numerico. Debe ser del tipo 999" + sep + "99" );
            }
        }
        else if(   numericParts.length  == 2 )
        {			
            try
            {
                currency = new Currency(  Integer.parseInt(  numericParts[0]),  Integer.parseInt( numericParts[1]) );
            }
            catch( Exception ex)
            {
                 throw new ServiceClientException("Error en el formato numerico. Debe ser del tipo 999" + sep + "99" );
            }
        }

        return currency;			
    }

}
