using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ParsiDNS.Core.DTos.dns;
using ParsiDNS.Core.DTos.software;
using ParsiDNS.DataLayer.Context;
using ParsiDNS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsiDNS.Core.Repository.Services
{
    public class DnsRepository : IDnsRepository
    {
        private ParsiDnsContext _context;

        public DnsRepository(ParsiDnsContext context)
        {
            _context = context;
        }
        public void AddDns(DnsDTO dnsDTO, int softwareId)
        {
            var dns = new DNS
            {
                Dns1 = dnsDTO.Dns1,
                Dns2 = dnsDTO.Dns2,
                Title = dnsDTO.Title
            };


            _context.DNS.Add(dns);
            _context.SaveChanges();

            _context.DnsSoftware.Add(new DnsSoftwares
            {
                DnsId = dns.DnsId,
                SoftwareId = softwareId,
                LastMonthLikeCount = 0,
                LastWeekLikeCount = 0,
                TotalLikeCount = 0,
            });
            _context.SaveChanges();
        }

        public void AddSoftware(string software)
        {
            _context.Software.Add(new Software
            {
                Title = software,
            });

            _context.SaveChanges();
        }

        public void DeleteDns(DNS dns)
        {
            _context.Remove(dns);

            var dnsSoftware = _context.DnsSoftware
                .Single(d => d.DnsId == dns.DnsId);

            _context.Remove(dnsSoftware);
            _context.SaveChanges();
        }

        public void DeleteSoftware(Software software)
        {
            _context.Remove(software);
            _context.SaveChanges();
        }

        public IEnumerable<DnsDTO> GetAllDns()
        {
            return _context.DNS.ToList().Select(d => new DnsDTO
            {
                Id = d.DnsId,
                Dns1 = d.Dns1,
                Dns2 = d.Dns2,
                Title = d.Title,
            });
        }

        public IEnumerable<SoftwareDTO> GetAllSoftwares()
        {
            return _context.Software.ToList().Select(d => new SoftwareDTO
            {
                Title = d.Title
            });
        }

        public DNS GetBestDnsByLikes()
        {
            return _context.DnsSoftware
                .OrderByDescending(d => d.TotalLikeCount).Select(d => d.DNS).FirstOrDefault();
        }

        public DNS GetDnsById(int id)
        {
            return _context.DNS.SingleOrDefault(d => d.DnsId == id);
        }

        public IEnumerable<DnsSoftwareDTO> GetDnsSoftwareBysoftwareId(int softwareId)
        {
            return _context.DnsSoftware.Include(d => d.Software)
                 .Where(d => d.SoftwareId == softwareId).Select(d => new DnsSoftwareDTO
                 {
                     DnsSoftware = d.Software.Title,
                     LastMonthLikeCount = d.LastMonthLikeCount,
                     LastWeekLikeCount = d.LastWeekLikeCount,
                     TotalLikeCount = d.TotalLikeCount
                 });
        }

        public Software GetSoftware(int softwareId)
        {
            return _context.Software.Find(softwareId);

        }

        public void likeDns(int id)
        {
            var dns = _context.DnsSoftware.FirstOrDefault(d => d.DnsId == id);

            if (dns != null)
            {
                dns.TotalLikeCount++;
                dns.LastMonthLikeCount++;
                dns.LastWeekLikeCount++;

                _context.SaveChanges();
            }
        }

        public void ResetDnsMonltlyLikesCount()
        {
            _context.Database.ExecuteSqlRaw("UPDATE DnsSoftwares SET LastMonthLikeCount = 0");
        }

        public void ResetDnsWeeklyLikesCount()
        {
            _context.Database.ExecuteSqlRaw("UPDATE DnsSoftwares SET LastWeekLikeCount = 0");
        }

        public IEnumerable<DnsDTO> SortDnsByLikesCount()
        {
            return _context.DnsSoftware.Include(d => d.DNS)
                 .OrderByDescending(d => d.TotalLikeCount)
                 .Select(d => new DnsDTO
                 {
                     Dns1 = d.DNS.Dns1,
                     Dns2 = d.DNS.Dns2,
                     Id = d.DnsId,
                     Title = d.DNS.Title,
                 }).ToList();
        }
    }
}
