/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package poscodex;

import comcodex.ServiceClientException;
import javax.swing.SwingWorker;

/**
 *
 * @author rogerzavala
 */
public class CustomWorker extends SwingWorker<Boolean, Void>
{
    private DialogTransaction dialog = null;
    private MainFrame frame = null;
    private DialogLoading loading = null;
    private int operation = -1;
    private boolean ready = false;
    private String  msgException = null;
    
    public static final int LOAD_BANK_PROFILE_LIST      = 0;
    public static final int SHOW_TRANSACTION_LIST       = 1;
    public static final int SHOW_TRANSACTION            = 2;
    public static final int CREATE_TRANSACTION          = 3;
    public static final int FILTER_TRANSACTION_LIST     = 4;
    public static final int REVERT_TRANSACTION          = 5;
    
    /**
     * 
     * @param operation
     * @param frame
     * @param loading 
     */
    public CustomWorker( int operation, MainFrame frame, DialogLoading loading )
    {
        this.frame = frame;
        this.loading = loading;
        this.operation = operation;
    }       
    
    /**
     * 
     * @param operation
     * @param dialog
     * @param loading 
     */
    public CustomWorker( int operation, DialogTransaction dialog, DialogLoading loading )
    {
        this.dialog = dialog;
        this.loading = loading;
        this.operation = operation;
    }    
    
    
    
    /**
     * 
     */
   @Override
   protected void done() {
 
        if( this.ready )
        {
            if( this.operation ==  CustomWorker.CREATE_TRANSACTION  )
            {
                this.dialog.setVisible(false);
                 
            }
            this.loading.setVisible(false);     
        }  
        else
        {
            this.loading.setVisible(false);
            
            if( this.operation ==  CustomWorker.CREATE_TRANSACTION  )
            {
                this.dialog.showError(this.msgException);
            }
            else
            {
                this.frame.showError(this.msgException);
            }
        }

   }
   

   /**
    * 
    * @return
    * @throws Exception 
    */
   @Override
    protected Boolean doInBackground() throws Exception 
    {
       switch(this.operation)
       {
           case CustomWorker.LOAD_BANK_PROFILE_LIST:
                try 
                {
                    this.frame.loadService();
                    this.ready = true;       
                } 
                catch (Exception ex) 
                {
                    this.msgException = ex.toString();
                }
               break;
           case CustomWorker.SHOW_TRANSACTION_LIST:
                try 
                {
                    this.frame.loadTransactionList();
                    this.ready = true;       
                } 
                catch (Exception ex) 
                {
                    this.msgException = ex.toString();
                }
               break;
           case CustomWorker.SHOW_TRANSACTION:
                try 
                {
                    this.frame.showTransaction();
                    this.ready = true;                    
                } 
                catch (Exception ex) 
                {
                    this.msgException = ex.toString();
                }                
               break;
           case CustomWorker.CREATE_TRANSACTION:
                try 
                {
                    this.dialog.createTransaction();
                    this.ready = true;
                } 
                catch (Exception ex) 
                {
                   this.msgException = ex.toString();
                }
               break;  
           case CustomWorker.FILTER_TRANSACTION_LIST:
                try 
                {
                   this.frame.filterTransactionList();
                   this.ready = true;
                } 
                catch (Exception ex) 
                {
                    this.msgException = ex.toString();                      
                }
               break; 
           case CustomWorker.REVERT_TRANSACTION:
               
               try
               {
                   
                   this.frame.revertTransaction();
                   this.ready = true;
                   
               }catch(Exception ex)
               {
                   this.msgException = ex.toString();
               }
               
               break;
           default:
               break;
       }
       return this.ready;
    }

    
}
