using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;
        private readonly IClient _httpclient;
        private readonly ILocalStorageService _localStorageService;

        public LeaveTypeService(IMapper mapper, IClient httpclient, 
            ILocalStorageService localStorageService):base(httpclient, localStorageService)
        {
            _mapper = mapper;
            _httpclient = httpclient;
            _localStorageService = localStorageService;
        }
        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveTypeDto createLeaveTypeD = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                var apiResponse = await _client.LeaveTypesPOSTAsync(createLeaveTypeD);
                if(apiResponse.Success)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            var leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypes = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                LeaveTypeDto leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);
                await _client.LeaveTypesPUTAsync(id.ToString(), leaveTypeDto);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
