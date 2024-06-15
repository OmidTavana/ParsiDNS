using ParsiDNS.Core.DTos.dns;
using ParsiDNS.Core.DTos.software;
using ParsiDNS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParsiDNS.Core.Repository
{
    public interface IDnsRepository
    {
        #region Get
        IEnumerable<DnsDTO> GetAllDns();
        IEnumerable<SoftwareDTO> GetAllSoftwares();
        IEnumerable<DnsSoftwareDTO> GetDnsSoftwareBysoftwareId(int softwareId);
        IEnumerable<DnsDTO> SortDnsByLikesCount();
        DNS GetDnsById(int id);
        Software GetSoftware(int softwareId);
        DNS GetBestDnsByLikes();
        #endregion

        #region Add
        void AddDns(DnsDTO dnsDTO, int softwareId);
        void AddSoftware(string software);
        #endregion

        #region Like
        void likeDns(int id);
        void ResetDnsMonltlyLikesCount();
        void ResetDnsWeeklyLikesCount();

        #endregion

        #region Delete
        void DeleteDns(DNS dns);
        void DeleteSoftware(Software software);
        #endregion

    }
}
