using System;
using System.Collections.Generic;
using System.Management;

namespace processMonitor
{
    public class classProcesses
    {
        public classProcesses()
        {
        }

        public List<Dictionary<string,string>> GetCurrentValue()
        {
            ManagementClass mc = new ManagementClass("Win32_Process");
            ManagementObjectCollection moc = mc.GetInstances();

            List<Dictionary<string, string>> htList = new List<Dictionary<string, string>>();
            foreach (ManagementObject mo in moc)
            {
                Dictionary<string, string> ht = new Dictionary<string, string>();
                ht["Name"] = mo["Name"].ToString();
                ht["ProcessId"] = mo["ProcessId"].ToString();
                ht["ExecutablePath"] = (string)mo["ExecutablePath"] ?? "";

                foreach(KeyValuePair<string, string> pair in ht)
                {
                    if(pair.Value == null)
                    {
                        ht[pair.Key]= "";
                    }
                }
                htList.Add(ht);
                mo.Dispose();
            }
            return htList;
        }
    }
}
