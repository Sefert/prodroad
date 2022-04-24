using AutoMapper;
using DAL.Base;

namespace DAL.App.EF.Mappers;

public class AddressMapper : BaseMapper<DTO.App.Address,Domain.App.Address>
{
    public AddressMapper(IMapper mapper) : base(mapper)
    {
    }
}