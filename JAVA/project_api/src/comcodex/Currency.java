/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package comcodex;



/**
 * Currency
 * 
 * @author Koiosoft
 *
 */
public class Currency {

        public Integer number;
        public Integer decimal;
        public Integer money;

        /**
         * 
         * @param numberValue Integer
         * @param decimalValue Integer
         */
        public Currency( Integer numberValue, Integer decimalValue )
        {
                this.number = numberValue;
                this.decimal = decimalValue;
                this.money	= 0;
        }


        /**
         * 
         * @return Double
         */
        public double toDouble()
        {	
                return Double.parseDouble( this.number.toString()+ "." + this.decimal.toString() );
        }


}	
