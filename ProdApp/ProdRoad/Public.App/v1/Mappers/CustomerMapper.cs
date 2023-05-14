using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.v1.Mappers;

public class CustomerMapper : BaseMapper<Customer,BLL.App.DTO.Customer>
{
    public CustomerMapper(IMapper mapper) : base(mapper)
    {
    }
}