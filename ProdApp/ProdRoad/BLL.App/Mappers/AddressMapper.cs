using AutoMapper;
using BLL.App.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class AddressMapper : BaseMapper<Address,DAL.App.DTO.Address>
{
    public AddressMapper(IMapper mapper) : base(mapper)
    {
    }
}