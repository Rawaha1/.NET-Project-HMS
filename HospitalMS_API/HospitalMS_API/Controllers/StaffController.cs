﻿using BLL.DTOs;
using BLL.Services;
using HospitalMS_API.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HospitalMS_API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class StaffController : ApiController
    {
        [HttpGet]
        [Route("api/staff/all")]
        public HttpResponseMessage Get([FromUri] PagingModel pagingModel)
        {
            try
            {
                var source = StaffService.Get();
                if (pagingModel == null)
                {
                    pagingModel = new PagingModel();
                    var data = source.Skip((pagingModel.PageNumber - 1) * pagingModel.PageSize).Take(pagingModel.PageSize).ToList();
                    var page = new PaginationFilter(source.Count, pagingModel.PageSize, pagingModel.PageNumber);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Data = data, Page = page });
                }
                else
                {
                    var data = source.Skip((pagingModel.PageNumber - 1) * pagingModel.PageSize).Take(pagingModel.PageSize).ToList();
                    var page = new PaginationFilter(source.Count, pagingModel.PageSize, pagingModel.PageNumber);
                    return Request.CreateResponse(HttpStatusCode.OK, new { Data = data, Page = page });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/staff/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, StaffService.Get(id));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/staff/add")]
        public HttpResponseMessage Add(StaffDTO staff)
        {
            try
            {
                var res = StaffService.Create(staff);
                if (res)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Inserted", Data = staff });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Not Inserted", Data = staff });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = ex.Message, Data = staff });
            }
        }
        
        [HttpPost]
        [Route("api/staff/update")]
        public HttpResponseMessage Update(StaffDTO staff)
        {
            try
            {
                var res = StaffService.Update(staff);
                if (res)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Updated", Data = staff });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Not Updated", Data = staff });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = ex.Message, Data = staff });
            }
        }
        [HttpPost]
        [Route("api/staff/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var res = StaffService.Delete(id);
                if (res)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Delete Success" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Delete failed" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = ex.Message });
            }
        }
    }
}
