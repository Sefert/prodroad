using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class CustomerMapper : BaseMapper<DTO.App.Customer,Domain.App.Customer>
{
    public CustomerMapper(IMapper mapper) : base(mapper)
    {
    }
}