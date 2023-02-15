using System.Collections;



public class PackageController : BaseController
{
    public PackageController()
    {
        request = Request.Inventory;
    }

    public Mainpack InitInventory(Server server, Client client, Mainpack pack)
    {
     
        return null;
    }
    public Mainpack RefeshInventory(Server server, Client client, Mainpack pack)
    {
        return null;    
    }
    public Mainpack GetInventory(Server server, Client client, Mainpack pack)
    {
        return pack;
    }
}
