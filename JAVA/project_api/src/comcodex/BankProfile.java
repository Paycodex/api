package comcodex;

/**
 * Perfil Bancario
 * 
 * @author Koiosoft
 *
 */
public class BankProfile {
	
    public String id;
    public String alias;
    public String account;
    public String bank;
    
    public String toString()
    {
    	return "Id: " + this.id + ", Alias: " + this.alias + ", Account: " + this.account + ", Bank: " +  this.bank;
    }
}
