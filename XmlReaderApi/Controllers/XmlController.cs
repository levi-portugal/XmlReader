using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using XmlReader.Helpers;
using XmlReader.Interfaces;

namespace XmlTestProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlController : ControllerBase
    {
        private readonly IXmlService _xmlservice;

        public XmlController(IXmlService xmlservice)
        {
            _xmlservice = xmlservice;
        }

        [HttpGet("{id}")]
        public IActionResult GetXmlById(string id)
        {       
            try
            {
                var xml = _xmlservice.GetXmlById(id);
                return Ok(xml);
            }
            catch (ArgumentException ex)
            {

                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Um erro interno ocorreu");
            }
        }

        [HttpPost]
        public IActionResult CreateXmlWithBase64([FromBody] string content)
        {

            if (Base64Transform.IsBase64(content) == false)
            {
                throw new Exception("Não é um Base64 Válido!");
            }

            try
            {
                _xmlservice.CreateXmlUsingBase64(content);
                return StatusCode(201, new { mensagem = "Sucesso" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Um erro interno ocorreu");
            }
        }

        [HttpGet]
        public IActionResult FilterXmlByPropertie([FromQuery] string? issuerDocument, [FromQuery] string? recipientDocument, [FromQuery] string? shipperCnpj, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? serviceTakerCnpj , [FromQuery] string? recipientName)
        {        
            try
            {
                var xmls = _xmlservice.FilterXmlByProperties(issuerDocument, recipientDocument, shipperCnpj, startDate, endDate, serviceTakerCnpj, recipientName);
                return Ok(xmls);
            }
            catch (ArgumentException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Um erro interno ocorreu");
            }
        }

        [HttpPost]
        public IActionResult UploadXml([FromForm] )
        {

        }
    }
}