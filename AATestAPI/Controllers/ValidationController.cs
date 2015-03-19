using Intertech.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AATestAPI.Controllers
{
    [RoutePrefix("api/Validation")]
    [EnableCors(origins: "http://localhost:52536", headers: "*", methods: "*")]
    public class ValidationController : ApiController
    {
        [Route("GetValidations")]
        public IHttpActionResult GetValidations(string dtoObjectName, string jsonObjectName)
        {
            var valHelper = new ValidationHelper();
            object jsonObject = valHelper.GetValidations(dtoObjectName, jsonObjectName,
                "AATestAPI.Models", false, "AATestAPI");

            return Ok(jsonObject);
        }
    }
}
