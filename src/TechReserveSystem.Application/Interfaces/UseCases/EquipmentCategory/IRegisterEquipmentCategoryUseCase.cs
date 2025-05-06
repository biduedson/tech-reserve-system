using TechReserveSystem.Shared.Communication.Request.EquipmentCategory;
using TechReserveSystem.Shared.Communication.Response.EquipmentCategory;

namespace TechReserveSystem.Application.Interfaces.UseCases.EquipmentCategory
{
    public interface IRegisterEquipmentCategoryUseCase
    {
        public Task<RegisterEquipmentCategoryResponse> Execute(EquipmentCategoryRegisterRequest request);
    }
}