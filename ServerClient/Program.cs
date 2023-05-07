using ServerClient;

const int requestDelay = 80;
const int processingTime = 500;
const int requests = 75;
const int poolSize = 5;

Server server = new Server(poolSize, processingTime);
Client client = new Client(server);
DateTime before = DateTime.Now;
for (int i = 1; i <= requests; i++)
{
    client.FormRequest(i);
    Thread.Sleep(requestDelay);
}
DateTime after = DateTime.Now;
double time = (after - before).TotalMilliseconds;


double actualLambda = server.Processed / time;
double actualMu = (double) server.Requests / server.PoolSize / time;
Calculations actualCalc = new Calculations(actualLambda, actualMu, server.PoolSize);

double theoryLambda = 1.0 / requestDelay;
double theoryMu = 1.0 / processingTime;
Calculations theoryCalc = new Calculations(theoryLambda, theoryMu, server.PoolSize);


string serverInfo = $"Used server with {server.PoolSize} threads and processing time of {processingTime}ms,\n";
serverInfo += $"applying a load of {requests} requests with delay of {requestDelay}ms between each two;\n";
serverInfo += $"Server worked for {Math.Round(time, 0)}ms and received {server.Requests} requests: {server.Processed} processed, {server.Rejected} rejected.\n\n";
    
string theoryReport = ReportFormer.FormReport(theoryCalc, "Theoretical");
string actualReport = ReportFormer.FormReport(actualCalc, "Actual");

Console.WriteLine();
Console.Write(serverInfo + theoryReport + actualReport);
TextSaver.Save(serverInfo + theoryReport + actualReport, "results.txt");