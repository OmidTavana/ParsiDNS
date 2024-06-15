using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ParsiDNS.Core;
using ParsiDNS.Core.DTos.dns;
using ParsiDNS.Core.DTos.software;
using ParsiDNS.Core.Repository;
using ParsiDNS.Core.Security;
using ParsiDNS.DataLayer.Entities;
using System.Net;

namespace ParsiDNS.WebApi.Controllers
{
    [Route("api/dns")]
    [ApiController]
    [ApiKeyAuth]
    public class DnsController : ControllerBase
    {

        #region Injection

        private IDnsRepository _dnsRepository;

        public DnsController(IDnsRepository dnsRepository)
        {
            _dnsRepository = dnsRepository;
        }
        #endregion

        // url => domain/api/dns/dnsId

        [HttpGet("{dnsId}")]
        public ActionResult<DnsDTO> GetDns(int dnsId)
        {
            var dns = _dnsRepository.GetDnsById(dnsId);

            if (dns == null)
            {
                return NotFound();
            }

            DnsDTO dnsDTO = new DnsDTO()
            {
                Id = dns.DnsId,
                Dns1 = dns.Dns1,
                Dns2 = dns.Dns2,
                Title = dns.Title
            };

            return Ok(dnsDTO);
        }

        // url => 

        [HttpGet("GetBestDns")]
        public ActionResult<DnsDTO> GetBestDns()
        {
            var BestDns = _dnsRepository.GetBestDnsByLikes();

            if (BestDns == null)
            {
                return NotFound();
            }

            DnsDTO dnsDTO = new DnsDTO()
            {
                Id = BestDns.DnsId,
                Dns1 = BestDns.Dns1,
                Dns2 = BestDns.Dns2,
                Title = BestDns.Title
            };

            return Ok(dnsDTO);
        }

        // url => 

        [HttpPut("LikeDns/{dnsId}")]
        public ActionResult LikeDns(int dnsId)
        {
            if (_dnsRepository.GetDnsById(dnsId) == null)
            {
                return NotFound();
            }

            _dnsRepository.likeDns(dnsId);

            return Ok("Dns Liked Successfully!");
        }


        // url => 

        [HttpGet("GetAll", Name = "GetAllDns")]
        public ActionResult<IEnumerable<DnsDTO>> GetAll()
        {
            return Ok(_dnsRepository.GetAllDns()); ;
        }

        // url => 

        [HttpPost("InsertSoftware/{title}")]
        public ActionResult<IEnumerable<SoftwareDTO>> InsertSoftware(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("title is required ! please try it again and insert value for title.");
            }

            _dnsRepository.AddSoftware(title);

            return CreatedAtAction(nameof(GetSoftwares),
                new { }, _dnsRepository.GetAllSoftwares());
        }

        // url => 

        [HttpGet("GetSoftwares", Name = "AllSoftwares")]
        public ActionResult<IEnumerable<SoftwareDTO>> GetSoftwares()
        {
            return Ok(_dnsRepository.GetAllSoftwares());
        }

        [HttpGet("GetDnsSoftwares/{softwareId}")]
        public ActionResult<IEnumerable<DnsSoftwareDTO>> GetDnsSoftware(int softwareId)
        {
            var dnsSoftwares = _dnsRepository.GetDnsSoftwareBysoftwareId(softwareId);

            if (!dnsSoftwares.Any())
            {
                return NotFound();
            }

            return Ok(dnsSoftwares);
        }

        // url => 

        [HttpGet("GetOrdersDnsByLikeCount")]
        public ActionResult<IEnumerable<DnsDTO>> OrderAllDnsByLikeCount()
        {
            return Ok(_dnsRepository.SortDnsByLikesCount());
        }

        // url => domain/api/dns/DeleteDns/dnsId

        [HttpDelete("DeleteDns/{dnsId}")]
        public ActionResult DeleteDnsById(int dnsId)
        {
            var dns = _dnsRepository.GetDnsById(dnsId);

            if (dns == null)
            {
                return BadRequest("This item dont exists!");
            }

            _dnsRepository.DeleteDns(dns);

            return Ok("dns deleted successfuly!");
        }

        [HttpDelete("DeleteSoftware/{softwareId}")]
        public ActionResult DeleteSoftwareById(int softwareId)
        {
            var software = _dnsRepository.GetSoftware(softwareId);

            if (software == null)
            {
                return BadRequest("This item dont exists!");
            }

            _dnsRepository.DeleteSoftware(software);
            return Ok("item deleted successfuly!");
        }

        // url => 

        [HttpPost("InsertDns/{softwareId}")]
        public ActionResult<IEnumerable<DnsDTO>> InsertDns(int softwareId, DnsDTO dnsObject)
        {
            // insert dns
            _dnsRepository.AddDns(dnsObject, softwareId);

            // return list of all Dns's
            return CreatedAtAction(nameof(GetAll),
                    new { }, _dnsRepository.GetAllDns());
        }

    }

}

