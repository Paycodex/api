package comcodex;

/**
 * Sesión
 * 
 * @author Koiosoft
 *
 */
class Session
{
	public String sid;
	public Integer maxAge;
	public String expires;
	public String now;
	
	/**
	 * 
	 */
	public String toString()
	{
		return " Token: " + this.sid + " Time:" + this.maxAge + " Expires:" + this.expires + "  Created:" + this.now;
	}		
}
