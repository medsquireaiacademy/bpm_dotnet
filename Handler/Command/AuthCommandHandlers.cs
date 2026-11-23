using beeproperties.admin.webapi.models.DTO.Response;
using beeproperties.admin.webapi.models.Models.Commands;
using beeproperties.admin.webapi.services.Commands.Interface;

namespace beeproperties.admin.webapi.Handler.Command
{
    public interface ILoginHandler
    {
        Task<AuthResponseDto?> HandleAsync(LoginCommand command, CancellationToken cancellationToken = default);
    }

    public class LoginHandler : ILoginHandler
    {
        private readonly IAuthCommandService _service;
        public LoginHandler(IAuthCommandService service) => _service = service;
        public Task<AuthResponseDto?> HandleAsync(LoginCommand command, CancellationToken cancellationToken = default) =>
            _service.LoginAsync(command, cancellationToken);
    }

    public interface IUpdateAdminProfileHandler
    {
        Task<AdminUserDto?> HandleAsync(UpdateAdminProfileCommand command, CancellationToken cancellationToken = default);
    }

    public class UpdateAdminProfileHandler : IUpdateAdminProfileHandler
    {
        private readonly IAuthCommandService _service;
        public UpdateAdminProfileHandler(IAuthCommandService service) => _service = service;
        public Task<AdminUserDto?> HandleAsync(UpdateAdminProfileCommand command, CancellationToken cancellationToken = default) =>
            _service.UpdateProfileAsync(command, cancellationToken);
    }

    public interface IChangeAdminPasswordHandler
    {
        Task<bool> HandleAsync(ChangeAdminPasswordCommand command, CancellationToken cancellationToken = default);
    }

    public class ChangeAdminPasswordHandler : IChangeAdminPasswordHandler
    {
        private readonly IAuthCommandService _service;
        public ChangeAdminPasswordHandler(IAuthCommandService service) => _service = service;
        public Task<bool> HandleAsync(ChangeAdminPasswordCommand command, CancellationToken cancellationToken = default) =>
            _service.ChangePasswordAsync(command, cancellationToken);
    }

    public interface ICreateActivityHandler
    {
        Task<ActivityDto> HandleAsync(CreateActivityCommand command, CancellationToken cancellationToken = default);
    }

    public class CreateActivityHandler : ICreateActivityHandler
    {
        private readonly IAuthCommandService _service;
        public CreateActivityHandler(IAuthCommandService service) => _service = service;
        public Task<ActivityDto> HandleAsync(CreateActivityCommand command, CancellationToken cancellationToken = default) =>
            _service.CreateActivityAsync(command, cancellationToken);
    }
}
