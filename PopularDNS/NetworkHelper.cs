﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PopularDNS
{
	public static class NetworkHelper
	{
		public static NetworkInterface GetActiveEthernetOrWifiNetworkInterface()
		{
			var Nic = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
				a => a.OperationalStatus == OperationalStatus.Up &&
				(a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
				a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));

			return Nic;
		}
		public static void SetDNS(string DnsString, string DnsString2)
		{
			string[] Dns = { DnsString, DnsString2 };
			var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
			if (CurrentInterface == null) return;

			ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection objMOC = objMC.GetInstances();
			foreach (ManagementObject objMO in objMOC)
			{
				if ((bool)objMO["IPEnabled"])
				{
					if (objMO["Description"].ToString().Equals(CurrentInterface.Description))
					{
						ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
						if (objdns != null)
						{
							objdns["DNSServerSearchOrder"] = Dns;
							objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
						}
					}
				}
			}
		}
		public static void UnsetDNS()
		{
			var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
			if (CurrentInterface == null) return;

			ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection objMOC = objMC.GetInstances();
			foreach (ManagementObject objMO in objMOC)
			{
				if ((bool)objMO["IPEnabled"])
				{
					if (objMO["Description"].ToString().Equals(CurrentInterface.Description))
					{
						ManagementBaseObject objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
						if (objdns != null)
						{
							objdns["DNSServerSearchOrder"] = null;
							objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
						}
					}
				}
			}
		}
	}

}
