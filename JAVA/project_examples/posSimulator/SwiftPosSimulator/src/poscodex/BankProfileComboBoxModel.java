/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package poscodex;

import comcodex.*;
import javax.swing.AbstractListModel;
import javax.swing.ComboBoxModel;

/**
 *
 * @author rogerzavala
 */
public class BankProfileComboBoxModel extends AbstractListModel implements ComboBoxModel 
{
    
    BankProfileDisplayItem[] displayItemList;
    BankProfileDisplayItem itemSelected = null;

    /**
     * 
     * @param index
     * @return 
     */
    public Object getElementAt(int index) 
    {
        return  this.displayItemList[index];
    }

    /**
     * 
     * @return 
     */
    public int getSize() 
    {
        return this.displayItemList.length;
    }

    /**
     * 
     * @param item 
     */
    public void setSelectedItem( Object item ) 
    {
        this.itemSelected =  (BankProfileDisplayItem) item;
    } 

    /**
     * 
     * @return Object
     */
    public BankProfileDisplayItem getSelectedItem() 
    {
        return this.itemSelected;

    }


    /**
     * 
     * @param list BankProfile[] 
     */
    public void fill ( BankProfile[] list)
    {
        this.displayItemList = new BankProfileDisplayItem[list.length];    
        for( int i = 0; i < list.length; i++)
        {
            this.displayItemList[i] = new BankProfileDisplayItem( list[i]);
        }
        
    }

  
  
  
}
