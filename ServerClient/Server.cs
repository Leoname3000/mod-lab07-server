namespace ServerClient;

public class Server
{
    public Server(int poolSize, int processingTime)
    {
        requestCount = 0;
        processedCount = 0;
        rejectedCount = 0;
        pool = new PoolRecord[poolSize];
        threadLock = new object();
        this.processingTime = processingTime;
    }
    
    private int requestCount;
    public int Requests => requestCount;
    
    private int processedCount;
    public int Processed => processedCount;
    
    private int rejectedCount;
    public int Rejected => rejectedCount;

    struct PoolRecord
    {
        public Thread thread;
        public bool in_use;
    }

    private PoolRecord[] pool;
    public int PoolSize => pool.Length;
    private object threadLock;
    
    public void ProcRequest(object sender, ProcEventArgs e)
    {
        lock(threadLock) {
            Console.WriteLine($"\nПоступила заявка с номером {e.id}");
            requestCount++;
            for(int i = 0; i < pool.Length; i++) {
                if(!pool[i].in_use) {
                    pool[i].in_use = true;
                    pool[i].thread = new Thread(Answer);
                    pool[i].thread.Start(e.id);
                    processedCount++;
                    return;
                }
            }
            Console.WriteLine($"Заявка {e.id} отклонена");
            rejectedCount++;
        }
    }

    private int processingTime;

    void Answer(object? id)
    {
        if (id != null)
        {
            Console.WriteLine($"Заявка {(int) id} обрабатывается...");
            Thread.Sleep(processingTime);
        }
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].thread == Thread.CurrentThread)
            {
                pool[i].in_use = false;
            }
        }
    }
}