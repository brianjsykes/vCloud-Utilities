using System;
using VCloudLib;

namespace GetVMStatus
{
    class Program
    {
        //TODO: Set values for "VCUser", "VCUserPwd", and "VCUrl" in the config
        
        /// <summary>
        /// Given a VM Name, will return current status of VM
        /// </summary>
        /// <param name="args">[VM Name]</param>
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("\"GetVMStatus [VM Name]\"");
                return;
            }

            var vmName = args[0];
            Console.WriteLine("Getting status for VM: " + vmName);
            Console.WriteLine("Status: " + StatusChecker.GetVmStatus(vmName));
            return;
        }
    }
}
