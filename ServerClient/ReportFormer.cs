namespace ServerClient;

public class ReportFormer
{
    public static string FormReport(Calculations calc, string? prefix = null)
    {
        if (prefix != null)
        {
            prefix += " ";
        }
        else
        {
            prefix = "";
        }

        int roundDigits = 4;
        string report = "";
        report += $"{prefix}request intensity: {Math.Round(calc.RequestIntensity, roundDigits)}\n";
        report += $"{prefix}process intensity: {Math.Round(calc.ProcessIntensity, roundDigits)}\n";
        report += $"{prefix}requests per processing: {Math.Round(calc.RequestsPerProcessing(), roundDigits)}\n";
        report += $"{prefix}idling probability: {Math.Round(calc.IdlingProbability(), roundDigits)}\n";
        report += $"{prefix}rejection probability: {Math.Round(calc.RejectionProbability(), roundDigits)}\n";
        report += $"{prefix}relative throughput: {Math.Round(calc.RelativeThroughput(), roundDigits)}\n";
        report += $"{prefix}absolute throughput: {Math.Round(calc.AbsoluteThroughput(), roundDigits)}\n";
        report += $"{prefix}average busy threads: {Math.Round(calc.AverageBusyThreads(), roundDigits)}\n";
        report += "\n";
        
        return report;
    }
}