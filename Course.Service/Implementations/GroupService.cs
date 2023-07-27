using AutoMapper;
using Course.Core.Entities;
using Course.Core.Repositories;
using Course.Service.Dtos.Common;
using Course.Service.Dtos.GroupDtos;
using Course.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Service.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }
        public CreatedResultGroupDto Create(GroupCreateDto createDto)
        {
            var entity = _mapper.Map<Group>(createDto);
            _groupRepository.Add(entity);
            _groupRepository.Commit();
            return new CreatedResultGroupDto { Id = entity.Id };
        }
        public void Edit(int id, GroupEditDto editDto)
        {
            var entity = _groupRepository.Get(x => x.Id == id);
            entity.Name = editDto.Name;
            _groupRepository.Commit();
        }
        public List<GroupGetAllItemDto> GetAll()
        {
            var entities = _groupRepository.GetQueryable(x => true).ToList();
            return _mapper.Map<List<GroupGetAllItemDto>>(entities);
        }
        public GroupGetDto GetById(int id)
        {
            var entity = _groupRepository.Get(x => x.Id == id, "Students");
            return _mapper.Map<GroupGetDto>(entity);
        }
        public void Remove(int id)
        {
            var entity = _groupRepository.Get(x => x.Id == id, "Students");
            _groupRepository.Remove(entity);
            _groupRepository.Commit();
        }
    }
}
