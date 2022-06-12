using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MST_REST_Web_API.Entities;
using MST_REST_Web_API.Exceptions;
using MST_REST_Web_API.Models.DTO;

namespace MST_REST_Web_API.Services
{
    public interface ITestService
    {
        void AddTest(ScriptDto dto);
        void DeleteTest(int id);
        void UpdateTest(int id, ScriptDto dto);
        void ExecuteTest(int id, TestDto dto);
        List<Script> GetToDo();
        List<Script> GetAll();
    }

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public TestService(ApplicationDbContext context, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        void ITestService.AddTest(ScriptDto dto)
        {
            if (dto.Endpoints == null || dto.Endpoints.Count == 0)
                throw new BadRequestException("List of endpoints can not be empty");

            Script script = new Script()
            {
                Name = dto.Name,
                Endpoints = new List<Entities.Endpoint>(),
                Description = dto.Description,
                Comment = ""
            };

            foreach (var item in dto.Endpoints)
            {
                var endpoint = new Entities.Endpoint();
                endpoint.Parametrs = item.Parametrs;
                endpoint.URL = item.URL;
                endpoint.Heder = item.Heder;
                endpoint.Body = item.Body;
                endpoint.EndpointTypeId = item.EndpointTypeId;
                script.Endpoints.Add(endpoint);
            }

            _context.Scripts.Add(script);
            _context.SaveChanges();
        }

        void ITestService.DeleteTest(int id)
        {
            var script = _context.Scripts.Include(x => x.Endpoints).FirstOrDefault(x => x.Id == id);

            if (script == null)
                throw new NotFoundException($"Script with id: {id} not exist");


            _context.Scripts.Remove(script);
            _context.SaveChanges();
        }

        void ITestService.UpdateTest(int id, ScriptDto dto)
        {
            if (dto.Endpoints == null || dto.Endpoints.Count == 0)
                throw new BadRequestException("List of endpoints can not be empty");

            var script = _context.Scripts.FirstOrDefault(x => x.Id == id);

            if (script == null)
                throw new NotFoundException($"Script with id: {id} not exist");


            script.Name = dto.Name;
            script.Endpoints = new List<Entities.Endpoint>();
            script.Description = dto.Description;

            foreach (var item in dto.Endpoints)
            {
                var endpoint = new Entities.Endpoint();
                endpoint.Parametrs = item.Parametrs;
                endpoint.URL = item.URL;
                endpoint.Heder = item.Heder;
                endpoint.Body = item.Body;
                endpoint.EndpointTypeId = item.EndpointTypeId;
                script.Endpoints.Add(endpoint);
            }

            _context.SaveChanges();
        }

        void ITestService.ExecuteTest(int id, TestDto dto)
        {
            var script = _context.Scripts.FirstOrDefault(x => x.Id == id);

            if (script == null)
                throw new NotFoundException($"Script with id: {id} not exist");

            script.IsDone = true;
            script.Comment = dto.Comment;
            script.Succes = dto.Succes;
            script.TesterId = (int)_userContextService.GetUserId;

            _context.SaveChanges();
        }

        List<Script> ITestService.GetToDo()
        {
            var result = _context.Scripts.Include(x => x.Endpoints).Where(x => x.IsDone == false).ToList();

            foreach (var item in result)
            {
                var ids = item.Endpoints.Select(x => x.Id).ToList();
                item.Endpoints = _context.Endpoints.Include(x => x.EndpointType).Where(x => ids.Contains(x.Id)).ToList();
            }

            return result;
        }

        List<Script> ITestService.GetAll()
        {
            var result = _context.Scripts.Include(x => x.Endpoints).ToList();

            foreach (var item in result)
            {
                var ids = item.Endpoints.Select(x => x.Id).ToList();
                item.Endpoints = _context.Endpoints.Include(x => x.EndpointType).Where(x => ids.Contains(x.Id)).ToList();
            }

            return result;
        }
    }
}
