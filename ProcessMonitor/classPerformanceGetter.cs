using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace processMonitor
{
    class classPerformances
    {
        static void settings()
        {
            // プロセッサ数分のPerformanceCounterを格納する配列
            using (test2.ArrayPerformanceCounter pcs = new test2.ArrayPerformanceCounter())
            {
                for (var index = 0; index < pcs.Length; index++)
                {
                    // プロセッサ毎の使用率を計測するPerformanceCounterを作成
                    pcs.setPC(index, new PerformanceCounter("Processor", "% Processor Time", index.ToString()));
                }

                TimerCallback tDelegate = new TimerCallback(GetCpuUsage);
                using (Timer oTimer = new Timer(tDelegate, pcs, 0, 1000))
                {
                    Thread.Sleep(30000);
                }
            }
        }
        static void GetCpuUsage(object state)
        {

            test2.ArrayPerformanceCounter pcs = state as test2.ArrayPerformanceCounter;
            Console.Write(String.Format("{0} : CPU使用率  ", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff")));
            foreach (PerformanceCounter pc in pcs)
                if (pc != null)
                {
                    Console.Write(String.Format(" {0} : {1:f}% \t ", pc.InstanceName, pc.NextValue()));
                }
            Console.WriteLine("");
        }
    }
}
