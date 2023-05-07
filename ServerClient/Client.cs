namespace ServerClient;

public class Client
{
    public Client(Server server)
    {
        this.server = server;
        request += server.ProcRequest;
    }
    private Server server;
    public event EventHandler<ProcEventArgs> request;
    
    protected virtual void OnProc(ProcEventArgs e)
    {
        EventHandler<ProcEventArgs> handler = request;
        if (handler != null)
        {
            handler(this, e);
        }
    }
    
    public void FormRequest(int id)
    {
        ProcEventArgs req = new ProcEventArgs { id = id };
        OnProc(req);
    }
}