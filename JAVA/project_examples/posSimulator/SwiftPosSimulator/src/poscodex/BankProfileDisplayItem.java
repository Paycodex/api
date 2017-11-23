/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package poscodex;

import comcodex.*;

/**
 *
 * @author koiosoft
 */
public class BankProfileDisplayItem 
{
    private BankProfile bankProfile;
    
    
    /**
     * 
     * @param bankProfile 
     */
    public BankProfileDisplayItem( BankProfile bankProfile )
    {
        this.bankProfile = bankProfile;
    }
    
    
    /**
     * 
     * @return string
     */
    public String toString()
    {
        return this.bankProfile.alias;
    }
    
    
    /**
     * 
     * @return 
     */
    public BankProfile getValue()
    {
        return this.bankProfile;
    }
}
