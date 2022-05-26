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
            Script script = new Script()
            {
                Name = dto.Name,
                Endpoints = dto.Endpoints,
                Description = dto.Description
            };

            _context.Scripts.Add(script);
            _context.SaveChanges();
        }

        void ITestService.DeleteTest(int id)
        {
            var script = _context.Scripts.FirstOrDefault(x => x.Id == id);

            if (script == null)
                throw new NotFoundException($"Script with id: {id} not exist");

            _context.Scripts.Remove(script);
            _context.SaveChanges();
        }

        void ITestService.UpdateTest(int id, ScriptDto dto)
        {
            var script = _context.Scripts.FirstOrDefault(x => x.Id == id);

            if (script == null)
                throw new NotFoundException($"Script with id: {id} not exist");


            script.Name = dto.Name;
            script.Endpoints = dto.Endpoints;
            script.Description = dto.Description;

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

            return result;
        }

        List<Script> ITestService.GetAll()
        {
            return _context.Scripts.Include(x => x.Endpoints).ToList();
        }
    }
}
