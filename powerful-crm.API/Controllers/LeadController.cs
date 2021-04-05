﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using powerful_crm.API.Models.InputModels;
using powerful_crm.API.Models.OutputModels;
using powerful_crm.Business;
using powerful_crm.Core;
using powerful_crm.Core.Models;
using System;
using System.Collections.Generic;

namespace powerful_crm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private ILeadService _leadService;
        private IMapper _mapper;

        public LeadController(IMapper mapper, ILeadService leadService)
        {
            _leadService = leadService;
            _mapper = mapper;
        }
        /// <summary>lead add</summary>
        /// <param name="inputModel">information about add lead</param>
        /// <returns>rReturn information about added lead</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public ActionResult<LeadOutputModel> AddLead([FromBody] LeadInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                //todo: validationexception
                return Conflict();
            }
            var dto = _mapper.Map<LeadDto>(inputModel);
            var addedLeadId = _leadService.AddLead(dto);
            var outputModel = _mapper.Map<LeadOutputModel>(_leadService.GetLeadById(addedLeadId));
            return Ok(outputModel);

        }
        // https://localhost:44307/api/lead/2/change-password
        /// <summary>Changing password of lead</summary>
        /// <param name="leadId">Id of lead for whom we are changing the password</param>
        /// <param name="inputModel">Old and new password of lead</param>
        /// <returns>Status204NoContent response</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{leadId}/change-password")]
        public ActionResult ChangePassword(int leadId, [FromBody]ChangePasswordInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Conflict();
            }
            if (_leadService.GetLeadById(leadId) == null)
            {
                return NotFound($"Lead with id {leadId} is not found");
            }
            _leadService.ChangePassword(leadId, inputModel.OldPassword, inputModel.NewPassword);
            return NoContent();
        }
        /// <summary>Get info of lead</summary>
        /// <param name="leadId">Id of lead</param>
        /// <returns>Info of lead</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{leadId}")]
        public ActionResult<LeadOutputModel> GetLead(int leadId)
        {
            var lead = _leadService.GetLeadById(leadId);
            if (lead == null)
            {
                return NotFound($"Lead with id {leadId} is not found");
            }

            var outputModel = _mapper.Map<LeadOutputModel>(lead);
            return Ok(outputModel);
        }
        /// <summary>Get info of leads by email</summary>
        /// <param name="email"> Email of lead</param>
        /// <returns>Info of leads</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{email}/by-email")]
        public ActionResult<List<LeadOutputModel>> GetLeadsByEmail (string email)
        {
            var lead = _leadService.GetLeadsByEmail(email);
            if (lead == null)
            {
                return NotFound($"Leads with email: {email} is not found");
            }
            var outputModel = _mapper.Map<List<LeadOutputModel>>(lead);
            return Ok(outputModel);
        }
        /// <summary>Get info of leads by email</summary>
        /// <param name="firstName"> firstName of lead</param>
        /// <returns>Info of leads</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{firstName}/by-firstName")]
        public ActionResult<List<LeadOutputModel>> GetLeadsByFirstName(string firstName )
        {
            var lead = _leadService.GetLeadsByFirstName(firstName);
            if (lead == null)
            {
                return NotFound($"Leads with firstName: {firstName} is not found");
            }
            var outputModel = _mapper.Map<List<LeadOutputModel>>(lead);
            return Ok(outputModel);
        }
        /// <summary>Get info of leads by lastName</summary>
        /// <param name="lastName"> lastName of lead</param>
        /// <returns>Info of leads</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{lastName}/by-lastName")]
        public ActionResult<List<LeadOutputModel>> GetLeadsByLastName(string lastName)
        {
            var lead = _leadService.GetLeadsByLastName(lastName);
            if (lead == null)
            {
                return NotFound($"Leads with lastName: {lastName} is not found");
            }
            var outputModel = _mapper.Map<List<LeadOutputModel>>(lead);
            return Ok(outputModel);
        }
        /// <summary>Get info of leads by city</summary>
        /// <param name="city"> city of lead</param>
        /// <returns>Info of leads</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{city}/by-city")]
        public ActionResult<List<LeadOutputModel>> GetLeadsByCity(string city)
        {
            var lead = _leadService.GetLeadsByLastName(city);
            if (lead == null)
            {
                return NotFound($"Leads with city: {city} is not found");
            }
            var outputModel = _mapper.Map<List<LeadOutputModel>>(lead);
            return Ok(outputModel);
        }
        /// <summary>Get info of leads by birthDate</summary>
        /// <param name="birthDate"> birthDate of lead</param>
        /// <returns>Info of leads</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{birthDate}/by-birthDate")]
        public ActionResult<List<LeadOutputModel>> GetLeadsByBirthDate(string birthDate)
        {
            var dto = DateTime.ParseExact(birthDate, Constants.DATE_FORMAT,System.Globalization.CultureInfo.InvariantCulture);
            var lead = _leadService.GetLeadsByBirthDate(dto);
            if (lead == null)
            {
                return NotFound($"Leads with birthDate: {dto} is not found");
            }
            var outputModel = _mapper.Map<List<LeadOutputModel>>(lead);
            return Ok(outputModel);
        }
        /// <summary>Get info of leads by login</summary>
        /// <param name="login"> login of lead</param>
        /// <returns>Info of leads</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{login}/by-login")]
        public ActionResult<List<LeadOutputModel>> GetLeadsByLogin(string login)
        {
            var lead = _leadService.GetLeadsByLogin(login);
            if (lead == null)
            {
                return NotFound($"Leads with login: {login} is not found");
            }
            var outputModel = _mapper.Map<List<LeadOutputModel>>(lead);
            return Ok(outputModel);
        }
        /// <summary>Get info of leads by phone</summary>
        /// <param name="phone"> phone of lead</param>
        /// <returns>Info of leads</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [HttpGet("{phone}/by-phone")]
        public ActionResult<List<LeadOutputModel>> GetLeadsByPhone(string phone)
        {
            var lead = _leadService.GetLeadsByPhone(phone);
            if (lead == null)
            {
                return NotFound($"Leads with phone: {phone} is not found");
            }
            var outputModel = _mapper.Map<List<LeadOutputModel>>(lead);
            return Ok(outputModel);
        }
        /// <summary>Update information about lead</summary>
        /// <param name="leadId">Id of lead</param>
        /// /// <param name="inputModel">Nonupdated info about  lead </param>
        /// <returns>Updated info about lead</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{leadId}")]
        public ActionResult<LeadOutputModel> UpdateLead(int leadId, [FromBody] UpdateLeadInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Conflict();
            }
            var lead = _leadService.GetLeadById(leadId);
            if (lead == null)
            {
                return NotFound($"Lead with id {leadId} is not found");
            }
            var dto = _mapper.Map<LeadDto>(inputModel);
            _leadService.UpdateLead(leadId, dto);
            var outputModel = _mapper.Map<LeadOutputModel>(_leadService.GetLeadById(leadId));
            return Ok(outputModel);

        }

        /// <summary>Change value of parametr "IsDeleted" to 1(Deleted)</summary>
        /// <param name="leadId">Id of lead</param>
        /// <returns>Update lead, which is deleted</returns>
        [ProducesResponseType(typeof(List<LeadOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpDelete("{leadId}")]
        public ActionResult<LeadOutputModel> DeleteUser(int leadId)
        {
            var lead = _leadService.GetLeadById(leadId);
            if (lead == null)
            {
                return NotFound($"Lead with id {leadId} is not found");
            }
            if (lead.IsDeleted == true)
            {
                return BadRequest($"Lead with id {leadId} has already been deleted");
            }
            _leadService.DeleteLead(leadId);
            var dto = _mapper.Map<LeadOutputModel>(_leadService.GetLeadById(leadId));
            return Ok(dto);
        }

        /// <summary>Change value of parametr "IsDeleted" to 0(Not deleted)</summary>
        /// <param name="leadId">Id of lead</param>
        /// <returns>Update lead, which is not deleted</returns>
        [ProducesResponseType(typeof(List<LeadOutputModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [HttpPut("{leadId}/recover")]
        public ActionResult<LeadOutputModel> RecoverUser(int leadId)
        {
            var lead = _leadService.GetLeadById(leadId);
            if (lead == null)
            {
                return NotFound($"Lead with id {leadId} is not found");
            }
            if (lead.IsDeleted == false)
            {
                return BadRequest($"Lead with id {leadId} is not deleted");
            }
            _leadService.RecoverLead(leadId);
            var dto = _mapper.Map<LeadOutputModel>(_leadService.GetLeadById(leadId));
            return Ok(dto);
        }
    }
}
