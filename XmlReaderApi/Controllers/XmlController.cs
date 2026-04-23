using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using XmlReader.Entities;
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

        [HttpPost("Criar")]
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

        [HttpPost("upload")]
        public IActionResult UploadXml(IFormFile archive)
        {
            if (archive == null || archive.Length == 0)
                return BadRequest(new { mensagem = "Arquivo inválido ou vazio." });

            try
            {
                string content;

                using (var reader = new StreamReader(archive.OpenReadStream()))
                {
                    content = reader.ReadToEnd();
                }

                if (string.IsNullOrWhiteSpace(content))
                    return BadRequest(new { mensagem = "Conteúdo do arquivo está vazio." });

                var xml = XmlProcessor.Process(content);

                var xmlBase64 = Base64Transform.ConvertToBase64(content);

                _xmlservice.CreateXmlUsingBase64(xmlBase64);

                return StatusCode(201, new { mensagem = "Sucesso" });
            }
            catch (IOException ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao ler o arquivo.",
                    detalhe = ex.Message
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new
                {
                    mensagem = "Acesso negado ao arquivo.",
                    detalhe = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro interno ao processar o XML.",
                    detalhe = ex.Message
                });
            }

        }
    }
}