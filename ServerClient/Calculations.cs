namespace ServerClient;

public class Calculations
{
    public Calculations(double requestIntensity, double processIntensity, int poolSize)
    {
        this.requestIntensity = requestIntensity;
        this.processIntensity = processIntensity;
        this.poolSize = poolSize;
    }
    
    private double requestIntensity;
    public double RequestIntensity => requestIntensity;
    
    private double processIntensity;
    public double ProcessIntensity => processIntensity;
    
    private int poolSize;

    public double RequestsPerProcessing()
    {
        return requestIntensity / processIntensity;
    }

    private int Factorial(int num)
    {
        if (num < 0)
        {
            throw new Exception("Cannot get factorial of negative integer");
        }
        int result = 1;
        for (int i = 1; i <= num; i++)
        {
            result *= i;
        }
        return result;
    }

    public double IdlingProbability()
    {
        double sum = 0;
        for (int i = 0; i < poolSize; i++)
        {
            sum += Math.Pow(RequestsPerProcessing(), i) / Factorial(i);
        }
        return 1 / sum;
    }

    public double RejectionProbability()
    {
        return Math.Pow(RequestsPerProcessing(), poolSize) / Factorial(poolSize) * IdlingProbability();
    }

    public double RelativeThroughput()
    {
        return 1 - RejectionProbability();
    }

    public double AbsoluteThroughput()
    {
        return requestIntensity * RelativeThroughput();
    }

    public double AverageBusyThreads()
    {
        return AbsoluteThroughput() / processIntensity;
    }
}