using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RatchetTests
{
    /// <summary>
    /// Higher level acceptance tests covering overall functionality
    /// </summary>
    [TestClass]
    public class AcceptanceTests
    {
        private const string KnownWarning = "This is a known warning";
        private const string UnknownWarning = "This is an unknown warning";

        [TestMethod]
        public void ReadWarningsFromInputStream_FilterOutKnownWarningsAndErrorOnUnknown()
        {
            ProcessStartInfo pInfo = new ProcessStartInfo("ratchet.exe");
            pInfo.WorkingDirectory = System.Environment.CurrentDirectory;
            pInfo.UseShellExecute = false;
            pInfo.RedirectStandardInput = true;
            pInfo.RedirectStandardError = true;
            
            string inputWarnings = String.Join("\n", KnownWarning, UnknownWarning);

            Process p = Process.Start(pInfo);
            p.StandardInput.Write(inputWarnings);
            p.StandardInput.Close();
            string errors = p.StandardError.ReadToEnd();
            
            Assert.AreEqual(UnknownWarning, errors);
        }
    }
}
