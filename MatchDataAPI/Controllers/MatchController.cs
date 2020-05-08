using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using MatchDataAPI.Models;
using Models;
using DAL;
using System.Web.Http.Cors;

namespace MatchDataAPI.Controllers
{
    public class MatchController : ApiController
    {
        [HttpPost]
        [Route("api/SubmitMatchData")]
        //[EnableCors(origins: "http://localhost:4200/", headers:"*",methods:"*")]
        public async Task<HttpResponseMessage> SubmitMatchData(MatchData data)
        {
            DataAccess objData = new DataAccess();
            ResponseData res = new ResponseData();
            try
            {
                bool result = false;

                result = objData.SubmitMatchData(data);
                res.status = result;

                if (result)
                {                    
                    res.ResponseMessage = "Match Data Saved Successfully.";
                    return await Task.Run(() => Request.CreateResponse(HttpStatusCode.OK, res));
                }
                else
                {
                    res.error = "Something went wrong.";
                    return await Task.Run(() => Request.CreateResponse(HttpStatusCode.InternalServerError, res));
                }
            }
            catch (Exception ex)
            {
                res.status = false;
                res.error = "Exception occured: "+ex.Message.ToString();
                return await Task.Run(() => Request.CreateResponse(HttpStatusCode.InternalServerError,res));
            }            
        }
    }
}
