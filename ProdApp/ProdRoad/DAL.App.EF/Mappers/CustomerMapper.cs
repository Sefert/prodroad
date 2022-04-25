using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class CustomerMapper : BaseMapper<DAL.App.DTO.Customer,Domain.App.Customer>
{
    public CustomerMapper(IMapper mapper) : base(mapper)
    {
    }
}