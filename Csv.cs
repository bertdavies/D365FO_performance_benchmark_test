using System.IO;

namespace DynamicsStressTest
{
    class Csv
    {
        string path = @"C:\Temp\Perfromance.csv";
        public void InitCsv()
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("dateTime,responseTimeAvg,errorRateTotalAvg,taskExecAvg,fails");
                }
            }
        }


        public void AppendCsv(string dateTime, double responseTimeAvg, double errorRateTotalAvg, long taskExecAvg, int fails)
        {
            //Append to CSV
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(dateTime + "," + responseTimeAvg + "," + errorRateTotalAvg + "," + taskExecAvg + "," + fails + ",");
            }
        }
    }
}
