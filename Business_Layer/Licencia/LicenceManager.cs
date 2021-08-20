using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Business_Layer.Licencia
{
    public class LicenseManager
    {
        public LicenseManager(HtmlWeb htmlWeb)
        {
            HtmlWeb = htmlWeb;
        }

        private HtmlWeb HtmlWeb { get; set; }

        public string RequestAddLicense(string licencia, string mac)
        {
            string url = "http://vydasoc.ddns.net/addmac/lic=" + licencia + "/mac=" + mac;
            Uri uri = new Uri(url);
            HtmlDocument doc = HtmlWeb.Load(uri);

            HtmlNode node = doc.DocumentNode.CssSelect("h1").First();
            return node.InnerText;
        }

        public string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //log.Debug(
                //    "Found MAC Address: " + nic.GetPhysicalAddress() +
                //    " Type: " + nic.NetworkInterfaceType);

                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    //log.Debug("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }

        public string VerificarLicencia(string Licencia, string MAC)
        {
            HtmlDocument doc = HtmlWeb.Load(new Uri($"http://vydasoc.ddns.net/viewlic/lic={Licencia}/mac={MAC}"));

            HtmlNode node = doc.DocumentNode.CssSelect("h1").First();
            return node.InnerText;
        }
    }
}
