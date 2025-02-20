﻿using DoAnMonHocBE.PayLoad.Request.Role;
using DoAnMonHocBE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoAnMonHocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IService_Role service_Role;

        public RoleController(IService_Role service_Role)
        {
            this.service_Role = service_Role;
        }

        [HttpPost("CreateRole")]
        public IActionResult CreateRole(Request_CreateRole request)
        {
            return Ok(service_Role.CreateRole(request));
        }
        [HttpPut("UpdateRole")]
        public IActionResult UpdateRole(Request_UpdateRole request)
        {
            return Ok(service_Role.UpdateRole(request));
        }
        [HttpGet("GetListRole")]
        public IActionResult GetListRole(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_Role.GetListRole(pageSize, pageNumber));
        }
        [HttpGet("GetRoleById")]
        public IActionResult GetRoleById(int roleId)
        {
            return Ok(service_Role.GetRoleById(roleId));
        }
        [HttpDelete("DeleteRole")]
        public IActionResult DeleteRole(int roleId)
        {
            return Ok(service_Role.DeleteRole(roleId));
        }
    }
}
