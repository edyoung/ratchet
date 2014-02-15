using System;
using System.Diagnostics;
using System.IO;
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
            File.WriteAllText(".ratchet",KnownWarning);
            string pathToExecutable = Path.Combine(System.Environment.CurrentDirectory, @"..\..\..\bin\debug\ratchet.exe");
            ProcessStartInfo pInfo = new ProcessStartInfo(pathToExecutable);
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
