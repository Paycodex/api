/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package poscodex;

import comcodex.Transaction;

/**
 *
 * @author rogerzavala
 */
public class TransactionDisplayItem {
    
    private Transaction transaction;
    
    
    /**
     * 
     * @param Transaction 
     */
    public TransactionDisplayItem( Transaction transaction )
    {
        this.transaction = transaction;
    }
    
    
    /**
     * 
     * @return string
     */
    public String toString()
    {
        return this.transaction.concept;
    }
    
    
    /**
     * 
     * @return 
     */
    public Transaction getValue()
    {
        return this.transaction;
    }
}
