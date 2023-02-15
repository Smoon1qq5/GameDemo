using System.Collections;

   public abstract class BaseController 
    {
    protected Request request = Request.None;

    public Request GetRequest
    {
        get
        {
            return request;
        }
    }
}
