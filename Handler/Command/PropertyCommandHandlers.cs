using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface ICreatePropertyHandler
    {
        Task<PropertyDetailResponse> HandleAsync(CreatePropertyCommand command, CancellationToken cancellationToken = default);
    }

    public class CreatePropertyHandler : ICreatePropertyHandler
    {
        private readonly IPropertyCommandService _service;
        public CreatePropertyHandler(IPropertyCommandService service) => _service = service;
        public Task<PropertyDetailResponse> HandleAsync(CreatePropertyCommand command, CancellationToken cancellationToken = default) =>
            _service.CreatePropertyAsync(command, cancellationToken);
    }

    public interface IUpdatePropertyHandler
    {
        Task<PropertyDetailResponse?> HandleAsync(UpdatePropertyCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdatePropertyHandler : IUpdatePropertyHandler
    {
        private readonly IPropertyCommandService _service;
        public UpdatePropertyHandler(IPropertyCommandService service) => _service = service;
        public Task<PropertyDetailResponse?> HandleAsync(UpdatePropertyCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdatePropertyAsync(command, cancellationToken);
    }

    public interface IDeletePropertyHandler
    {
        Task<bool> HandleAsync(DeletePropertyCommand command, CancellationToken cancellationToken = default);
    }

    public class DeletePropertyHandler : IDeletePropertyHandler
    {
        private readonly IPropertyCommandService _service;
        public DeletePropertyHandler(IPropertyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeletePropertyCommand command, CancellationToken cancellationToken = default) =>
            _service.DeletePropertyAsync(command, cancellationToken);
    }

    public interface ICreateBlockHandler
    {
        Task<PropertyBlockDetailDto?> HandleAsync(CreateBlockCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateBlockHandler : ICreateBlockHandler
    {
        private readonly IPropertyCommandService _service;
        public CreateBlockHandler(IPropertyCommandService service) => _service = service;
        public Task<PropertyBlockDetailDto?> HandleAsync(CreateBlockCommand command, CancellationToken cancellationToken = default) =>
            _service.CreateBlockAsync(command, cancellationToken);
    }

    public interface IUpdateBlockHandler
    {
        Task<PropertyBlockDetailDto?> HandleAsync(UpdateBlockCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateBlockHandler : IUpdateBlockHandler
    {
        private readonly IPropertyCommandService _service;
        public UpdateBlockHandler(IPropertyCommandService service) => _service = service;
        public Task<PropertyBlockDetailDto?> HandleAsync(UpdateBlockCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateBlockAsync(command, cancellationToken);
    }

    public interface IDeleteBlockHandler
    {
        Task<bool> HandleAsync(DeleteBlockCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteBlockHandler : IDeleteBlockHandler
    {
        private readonly IPropertyCommandService _service;
        public DeleteBlockHandler(IPropertyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteBlockCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteBlockAsync(command, cancellationToken);
    }

    public interface ICreateFloorHandler
    {
        Task<FloorDto?> HandleAsync(CreateFloorCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateFloorHandler : ICreateFloorHandler
    {
        private readonly IPropertyCommandService _service;
        public CreateFloorHandler(IPropertyCommandService service) => _service = service;
        public Task<FloorDto?> HandleAsync(CreateFloorCommand command, CancellationToken cancellationToken = default) =>
            _service.CreateFloorAsync(command, cancellationToken);
    }

    public interface IUpdateFloorHandler
    {
        Task<FloorDto?> HandleAsync(UpdateFloorCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateFloorHandler : IUpdateFloorHandler
    {
        private readonly IPropertyCommandService _service;
        public UpdateFloorHandler(IPropertyCommandService service) => _service = service;
        public Task<FloorDto?> HandleAsync(UpdateFloorCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateFloorAsync(command, cancellationToken);
    }

    public interface IDeleteFloorHandler
    {
        Task<bool> HandleAsync(DeleteFloorCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteFloorHandler : IDeleteFloorHandler
    {
        private readonly IPropertyCommandService _service;
        public DeleteFloorHandler(IPropertyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteFloorCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteFloorAsync(command, cancellationToken);
    }

    public interface ICreateFlatHandler
    {
        Task<FlatDto?> HandleAsync(CreateFlatCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateFlatHandler : ICreateFlatHandler
    {
        private readonly IPropertyCommandService _service;
        public CreateFlatHandler(IPropertyCommandService service) => _service = service;
        public Task<FlatDto?> HandleAsync(CreateFlatCommand command, CancellationToken cancellationToken = default) =>
            _service.CreateFlatAsync(command, cancellationToken);
    }

    public interface IUpdateFlatResidentHandler
    {
        Task<FlatDto?> HandleAsync(UpdateFlatResidentCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateFlatResidentHandler : IUpdateFlatResidentHandler
    {
        private readonly IPropertyCommandService _service;
        public UpdateFlatResidentHandler(IPropertyCommandService service) => _service = service;
        public Task<FlatDto?> HandleAsync(UpdateFlatResidentCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateFlatResidentAsync(command, cancellationToken);
    }

    public interface IDeleteFlatHandler
    {
        Task<bool> HandleAsync(DeleteFlatCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteFlatHandler : IDeleteFlatHandler
    {
        private readonly IPropertyCommandService _service;
        public DeleteFlatHandler(IPropertyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteFlatCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteFlatAsync(command, cancellationToken);
    }

    public interface IAddPropertyDocumentHandler
    {
        Task<PropertyDocumentDto?> HandleAsync(AddPropertyDocumentCommand command, CancellationToken cancellationToken = default);
    }

    public class AddPropertyDocumentHandler : IAddPropertyDocumentHandler
    {
        private readonly IPropertyCommandService _service;
        public AddPropertyDocumentHandler(IPropertyCommandService service) => _service = service;
        public Task<PropertyDocumentDto?> HandleAsync(AddPropertyDocumentCommand command, CancellationToken cancellationToken = default) =>
            _service.AddDocumentAsync(command, cancellationToken);
    }

    public interface IDeletePropertyDocumentHandler
    {
        Task<bool> HandleAsync(DeletePropertyDocumentCommand command, CancellationToken cancellationToken = default);
    }

    public class DeletePropertyDocumentHandler : IDeletePropertyDocumentHandler
    {
        private readonly IPropertyCommandService _service;
        public DeletePropertyDocumentHandler(IPropertyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeletePropertyDocumentCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteDocumentAsync(command, cancellationToken);
    }

    public interface ISaveRwaHandler
    {
        Task<RwaDetailResponseDto?> HandleAsync(SaveRwaDetailsCommand command, CancellationToken cancellationToken = default);
    }

    public class SaveRwaHandler : ISaveRwaHandler
    {
        private readonly IPropertyCommandService _service;
        public SaveRwaHandler(IPropertyCommandService service) => _service = service;
        public Task<RwaDetailResponseDto?> HandleAsync(SaveRwaDetailsCommand command, CancellationToken cancellationToken = default) =>
            _service.SaveRwaAsync(command, cancellationToken);
    }

    public interface IUpdateRwaStatusHandler
    {
        Task<bool> HandleAsync(UpdateRwaStatusCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateRwaStatusHandler : IUpdateRwaStatusHandler
    {
        private readonly IPropertyCommandService _service;
        public UpdateRwaStatusHandler(IPropertyCommandService service) => _service = service;
        public Task<bool> HandleAsync(UpdateRwaStatusCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateRwaStatusAsync(command, cancellationToken);
    }

    public interface IAddRwaCommitteeMemberHandler
    {
        Task<RwaCommitteeMemberDto?> HandleAsync(AddRwaCommitteeMemberCommand command, CancellationToken cancellationToken = default);
    }

    public class AddRwaCommitteeMemberHandler : IAddRwaCommitteeMemberHandler
    {
        private readonly IPropertyCommandService _service;
        public AddRwaCommitteeMemberHandler(IPropertyCommandService service) => _service = service;
        public Task<RwaCommitteeMemberDto?> HandleAsync(AddRwaCommitteeMemberCommand command, CancellationToken cancellationToken = default) =>
            _service.AddRwaCommitteeMemberAsync(command, cancellationToken);
    }

    public interface IDeleteRwaCommitteeMemberHandler
    {
        Task<bool> HandleAsync(DeleteRwaCommitteeMemberCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteRwaCommitteeMemberHandler : IDeleteRwaCommitteeMemberHandler
    {
        private readonly IPropertyCommandService _service;
        public DeleteRwaCommitteeMemberHandler(IPropertyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteRwaCommitteeMemberCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteRwaCommitteeMemberAsync(command, cancellationToken);
    }
}
