/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package poscodex;

import comcodex.*;
import javax.swing.AbstractListModel;
import javax.swing.ListModel;

/**
 *
 * @author rogerzavala
 */
public class TransactionListBoxModel extends AbstractListModel implements ListModel {
    
    TransactionDisplayItem[] displayItemList;
    TransactionDisplayItem itemSelected = null;

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
        this.itemSelected =  (TransactionDisplayItem) item;
    } 

    /**
     * 
     * @return Object
     */
    public TransactionDisplayItem getSelectedItem() 
    {
        return this.itemSelected;

    }


    /**
     * 
     * @param list Transaction[] 
     */
    public void fill ( Transaction[] list)
    {
        this.displayItemList = new TransactionDisplayItem[list.length];    
        for( int i = 0; i < list.length; i++)
        {
            this.displayItemList[i] = new TransactionDisplayItem( list[i]);
        }
        
    }
}
