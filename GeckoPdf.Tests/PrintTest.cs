﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeckoPdf.Config;
using System.Threading.Tasks;

namespace GeckoPdf.Tests
{
    [TestClass]
    public class PrintTest
    {
        [TestMethod]
        public void PrintGoogle()
        {
            var pdf = new GeckoPdf(new GeckoPdfConfig());
            var ms = pdf.Convert("https://www.w3.org");
            Assert.AreNotEqual(ms.Length, 0);
            ms.Close();

            GeckoPdf.UnloadGecko();
        }

        [TestMethod]
        public void PrintMultipleSitesAtOnce()
        {
            var sites = new string[]
            {
                "https://google.com",
                "https://www.w3.org",
                "https://www.microsoft.com",
                "https://github.com/",
                "https://www.mozilla.org"
            };
            
            foreach (var site in sites)
            {
                Task.Run(() =>
                {
                    var pdf = new GeckoPdf(new GeckoPdfConfig());
                    var ms = pdf.Convert(site);
                    Assert.AreNotEqual(ms.Length, 0);
                });
            }
        }
    }
}
