using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface IReviewAgencyRegistrationHandler
    {
        Task<AgencyDetailResponse?> HandleAsync(ReviewAgencyRegistrationCommand command, CancellationToken cancellationToken = default);
    }

    public class ReviewAgencyRegistrationHandler : IReviewAgencyRegistrationHandler
    {
        private readonly IAgencyCommandService _service;
        public ReviewAgencyRegistrationHandler(IAgencyCommandService service) => _service = service;
        public Task<AgencyDetailResponse?> HandleAsync(ReviewAgencyRegistrationCommand command, CancellationToken cancellationToken = default) =>
            _service.ReviewRegistrationAsync(command, cancellationToken);
    }

    public interface ICreateAgencyPersonnelHandler
    {
        Task<AgencyPersonnelDto?> HandleAsync(CreateAgencyPersonnelCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateAgencyPersonnelHandler : ICreateAgencyPersonnelHandler
    {
        private readonly IAgencyCommandService _service;
        public CreateAgencyPersonnelHandler(IAgencyCommandService service) => _service = service;
        public Task<AgencyPersonnelDto?> HandleAsync(CreateAgencyPersonnelCommand command, CancellationToken cancellationToken = default) =>
            _service.AddPersonnelAsync(command, cancellationToken);
    }

    public interface IUpdateAgencyPersonnelHandler
    {
        Task<AgencyPersonnelDto?> HandleAsync(UpdateAgencyPersonnelCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateAgencyPersonnelHandler : IUpdateAgencyPersonnelHandler
    {
        private readonly IAgencyCommandService _service;
        public UpdateAgencyPersonnelHandler(IAgencyCommandService service) => _service = service;
        public Task<AgencyPersonnelDto?> HandleAsync(UpdateAgencyPersonnelCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdatePersonnelAsync(command, cancellationToken);
    }

    public interface IDeleteAgencyPersonnelHandler
    {
        Task<bool> HandleAsync(DeleteAgencyPersonnelCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteAgencyPersonnelHandler : IDeleteAgencyPersonnelHandler
    {
        private readonly IAgencyCommandService _service;
        public DeleteAgencyPersonnelHandler(IAgencyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteAgencyPersonnelCommand command, CancellationToken cancellationToken = default) =>
            _service.DeletePersonnelAsync(command, cancellationToken);
    }

    public interface IRequestPropertyAssignmentHandler
    {
        Task<AgencyPropertyAssignmentDto?> HandleAsync(RequestPropertyAssignmentCommand command, CancellationToken cancellationToken = default);
    }

    public class RequestPropertyAssignmentHandler : IRequestPropertyAssignmentHandler
    {
        private readonly IAgencyCommandService _service;
        public RequestPropertyAssignmentHandler(IAgencyCommandService service) => _service = service;
        public Task<AgencyPropertyAssignmentDto?> HandleAsync(RequestPropertyAssignmentCommand command, CancellationToken cancellationToken = default) =>
            _service.RequestPropertyAssignmentAsync(command, cancellationToken);
    }

    public interface IReviewPropertyAssignmentHandler
    {
        Task<AgencyPropertyAssignmentDto?> HandleAsync(ReviewPropertyAssignmentCommand command, CancellationToken cancellationToken = default);
    }

    public class ReviewPropertyAssignmentHandler : IReviewPropertyAssignmentHandler
    {
        private readonly IAgencyCommandService _service;
        public ReviewPropertyAssignmentHandler(IAgencyCommandService service) => _service = service;
        public Task<AgencyPropertyAssignmentDto?> HandleAsync(ReviewPropertyAssignmentCommand command, CancellationToken cancellationToken = default) =>
            _service.ReviewPropertyAssignmentAsync(command, cancellationToken);
    }

    public interface IRevokePropertyAssignmentHandler
    {
        Task<bool> HandleAsync(RevokePropertyAssignmentCommand command, CancellationToken cancellationToken = default);
    }

    public class RevokePropertyAssignmentHandler : IRevokePropertyAssignmentHandler
    {
        private readonly IAgencyCommandService _service;
        public RevokePropertyAssignmentHandler(IAgencyCommandService service) => _service = service;
        public Task<bool> HandleAsync(RevokePropertyAssignmentCommand command, CancellationToken cancellationToken = default) =>
            _service.RevokePropertyAssignmentAsync(command, cancellationToken);
    }

    public interface ICreateAgencyServiceHandler
    {
        Task<AgencyServiceDto?> HandleAsync(CreateAgencyServiceCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateAgencyServiceHandler : ICreateAgencyServiceHandler
    {
        private readonly IAgencyCommandService _service;
        public CreateAgencyServiceHandler(IAgencyCommandService service) => _service = service;
        public Task<AgencyServiceDto?> HandleAsync(CreateAgencyServiceCommand command, CancellationToken cancellationToken = default) =>
            _service.AddServiceAsync(command, cancellationToken);
    }

    public interface IDeleteAgencyServiceHandler
    {
        Task<bool> HandleAsync(DeleteAgencyServiceCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteAgencyServiceHandler : IDeleteAgencyServiceHandler
    {
        private readonly IAgencyCommandService _service;
        public DeleteAgencyServiceHandler(IAgencyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteAgencyServiceCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteServiceAsync(command, cancellationToken);
    }

    public interface IAddAgencyDocumentHandler
    {
        Task<AgencyDocumentDto?> HandleAsync(AddAgencyDocumentCommand command, CancellationToken cancellationToken = default);
    }

    public class AddAgencyDocumentHandler : IAddAgencyDocumentHandler
    {
        private readonly IAgencyCommandService _service;
        public AddAgencyDocumentHandler(IAgencyCommandService service) => _service = service;
        public Task<AgencyDocumentDto?> HandleAsync(AddAgencyDocumentCommand command, CancellationToken cancellationToken = default) =>
            _service.AddDocumentAsync(command, cancellationToken);
    }

    public interface IDeleteAgencyDocumentHandler
    {
        Task<bool> HandleAsync(DeleteAgencyDocumentCommand command, CancellationToken cancellationToken = default);
    }

    public class DeleteAgencyDocumentHandler : IDeleteAgencyDocumentHandler
    {
        private readonly IAgencyCommandService _service;
        public DeleteAgencyDocumentHandler(IAgencyCommandService service) => _service = service;
        public Task<bool> HandleAsync(DeleteAgencyDocumentCommand command, CancellationToken cancellationToken = default) =>
            _service.DeleteDocumentAsync(command, cancellationToken);
    }
}
