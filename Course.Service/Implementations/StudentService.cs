using AutoMapper;
using Course.Core.Entities;
using Course.Core.Repositories;
using Course.Service.Dtos.Common;
using Course.Service.Dtos.StudentDtos;
using Course.Service.Interfaces;

namespace Course.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentService(IGroupRepository groupRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public CreatedResultGroupDto Create(StudentCreateDto dto)
        {
            var entity = _mapper.Map<Student>(dto);
            _studentRepository.Add(entity);
            _studentRepository.Commit();
            return new CreatedResultGroupDto { Id = entity.Id };
        }
        public void Edit(int id, StudentEditDto dto)
        {
            var entity = _studentRepository.Get(x => x.Id == id);
            entity.FullName = dto.Name;
            entity.GroupId = dto.GroupId;
            entity.Point = dto.Point;
            _studentRepository.Commit();
        }
        public List<StudentGetAllItemDto> GetAll()
        {
            var entities = _studentRepository.GetQueryable(x => true, "Group").ToList();
            return _mapper.Map<List<StudentGetAllItemDto>>(entities);
        }
        public StudentGetDto GetById(int id)
        {
            var entity = _studentRepository.Get(x => x.Id == id, "Group");
            return _mapper.Map<StudentGetDto>(entity);
        }
        public void Delete(int id)
        {
            var entity = _studentRepository.Get(x => x.Id == id);
            _studentRepository.Remove(entity);
            _studentRepository.Commit();
        }
    }
}
