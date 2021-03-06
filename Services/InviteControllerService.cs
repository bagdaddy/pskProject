﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;

namespace TP.Services
{
    public class InviteControllerService : IInviteControllerService
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IEmployeesRepository _employeeRepository;
        public InviteControllerService(IInviteRepository inviteRepository, IEmployeesRepository employeeRepository)
        {
            _inviteRepository = inviteRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<Guid> CreateInvite(Guid id, EmailRequestModel model)
        {
            try
            {
                var employee = await _employeeRepository.GetById(id);

                if(employee == null)
                {
                    throw new Exception("Employee is null");
                }

                var invite = new Invite(model.email, id);

                _inviteRepository.Add(invite);
                await _inviteRepository.SaveChanges();

                return invite.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task DeleteInvite(Guid id)
        {
            try
            {
                var invite = await _inviteRepository.GetById(id);

                if (invite == null)
                {
                    throw new Exception("Invite does not exist");
                }

                _inviteRepository.Delete(invite);
                await _inviteRepository.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Invite> GetInvite(Guid id)
        {
            try
            {
                var invite = await _inviteRepository.GetById(id);
                
                return invite;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
