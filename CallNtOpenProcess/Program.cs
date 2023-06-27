using System;
using System.Diagnostics;
using static CallNtOpenProcess.Native;

namespace CallNtOpenProcess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process process = Process.GetProcessesByName("notepad")[0];
            CLIENT_ID ci = new CLIENT_ID
            {
                UniqueProcess = (IntPtr)process.Id
            };

            OBJECT_ATTRIBUTES oa = new OBJECT_ATTRIBUTES();

            IntPtr hProcess = IntPtr.Zero;

            NtOpenProcess(ref hProcess, (uint)ProcessAccessFlags.All, ref oa, ref ci);
            Console.WriteLine("done");
        }
    }
}
