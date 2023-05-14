using AutoMapper;
using DAL.Base;
using Public.App.DTO.v1;

namespace Public.App.v1.Mappers;

public class AddressMapper : BaseMapper<Address,BLL.App.DTO.Address>
{
    public AddressMapper(IMapper mapper) : base(mapper)
    {
    }
}