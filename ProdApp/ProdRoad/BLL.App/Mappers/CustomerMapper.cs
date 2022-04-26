using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class CustomerMapper : BaseMapper<Customer,DAL.App.DTO.Customer>
{
    public CustomerMapper(IMapper mapper) : base(mapper)
    {
    }
}