using Application.Common.Interface;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public abstract class BaseService
{
    protected readonly IApplicationDbContext _context;
    protected readonly IMapper _mapper;

    protected BaseService(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}