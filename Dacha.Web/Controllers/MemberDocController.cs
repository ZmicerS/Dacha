using Dacha.Bll.Interfaces;
using Dacha.Bll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Dacha.Web.Controllers
{
    [Authorize]
    public class MemberDocController : ApiController
    {
       IMemberDocService _memberDocService;

        public MemberDocController(IMemberDocService memberDocService)
        {
            _memberDocService = memberDocService;
        }


        [HttpPost]
        [Route("api/upload/files")]
        public async Task<IHttpActionResult> UploadSingleFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }
            MemberDocDto memberDocDto = new MemberDocDto();
              var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var content in provider.Contents)
            {
                var key= content.Headers.ContentDisposition.Name.Trim('\"');
                if (key=="file")
                {
                    var filename = content.Headers.ContentDisposition.FileName.Trim('\"');
                    string[] parts = filename.Split(new char[] {'.'});
                    byte[] fileArray = await content.ReadAsByteArrayAsync();
                    memberDocDto.NameDoc = filename;
                    memberDocDto.Document = fileArray;
                    memberDocDto.DocumentMimeType = parts[1];
                }
                else
                {
                    string value = await content.ReadAsStringAsync();
                    switch (key)
                    {
                        case "memberid":
                            memberDocDto.MemberId = Guid.Parse(value);//
                            break;
                        case "description":                          
                            memberDocDto.Description = value;
                            break;
                        default:
                            break;
                    }
                }                                
            }
            _memberDocService.UploadSingleFile(memberDocDto);
            //
            return Ok("file upload");
        }

         

        //       
        [HttpGet]
        [Route("api/MemberDocListId/{id}")]
        public IEnumerable<string> MemberDocListId(string id)
        {
            var list = new List<string>();
            list = _memberDocService.GetListById(id).ToList();
            return list;
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage Get(string id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
            var doc = _memberDocService.GeMemberDoc(id);
            if (doc != null)
            {
                if (doc.Document != null && doc.DocumentMimeType == "jpg")
                {
                    response.Content = new ByteArrayContent(doc.Document);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                }
            }
            }
            return response;
        }
      
        public async Task<HttpResponseMessage> Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                await _memberDocService.DeleteMemberDocAync(id);
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
///
