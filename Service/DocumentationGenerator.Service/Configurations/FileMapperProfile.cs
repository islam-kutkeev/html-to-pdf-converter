using AutoMapper;
using DocumentationGenerator.Service.Dtos;
using DocumentationGenerator.Service.Entities;

namespace DocumentationGenerator.Service.Configurations;

public class FileMapperProfile : Profile
{
    public FileMapperProfile()
    {
        CreateMap<FileEntity, FileDto>()
            .ReverseMap();
    }
}
