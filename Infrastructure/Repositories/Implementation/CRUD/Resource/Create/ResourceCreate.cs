namespace Infrastructure.Repositories.Implementation.CRUD.Resource.Create
{
    using Application.Result;
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.Repository.CRUD;
    using Application.Validators.User;
    using Infrastructure.Repositories.Abstract.CRUD.Create;
    using Microsoft.EntityFrameworkCore;
    using FluentValidation.Results;
    using System.Threading.Tasks;
    using Resource = Domain.Entities.Resource;

    public class ResourceCreate : CreateRepository<Resource>, IResourceCreate
    {
        private readonly IResourcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;
        public ResourceCreate(DbContext context, ILogService logService, IUtilEntity<Resource> utilEntity, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, utilEntity, provider, handler)
        {
            _provider = provider;
            _handler = handler;
            _resourceKeys = new List<string>
            {
                "ResourceFailedDataLength",
                "ResourceFailedDuplicateName"
            };
        }

        protected override async Task<Operation<Resource>> CreateEntity(Resource entity)
        {
            // Validate the user entity using the defined rules
            CreateResourceRules validatorAdd = new CreateResourceRules();
            ValidationResult result = validatorAdd.Validate(entity);
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            // Check if validation fails and return an error if so
            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                var createFailedDataSizeCharacter = _handler.GetResource("ResourceFailedDataLength");
                var message = string.Format(createFailedDataSizeCharacter, errorMessage);
                var business = new BusinessStrategy<Resource>();
                return OperationStrategy<Resource>.Fail(message, business);
            }

            var name = entity?.Name ?? string.Empty;
            // Ensure email uniqueness by checking if it's already used by another user
            IQueryable<Resource> resourceByName = await ReadFilter(p => p.Name == name);
            Resource? resourceExistByName = resourceByName?.FirstOrDefault();
            if (resourceExistByName is not null)
            {
                var createFailedAlreadyRegisteredName = _handler.GetResource("ResourceFailedDuplicateName");
                return OperationStrategy<Resource>.Fail(createFailedAlreadyRegisteredName, new BusinessStrategy<Resource>());
            }

            // Create and return the user entity
            Resource entityAdd = GetResource(entity ?? new Resource());
            return Operation<Resource>.Success(entityAdd);
        }

        private static string GetErrorMessage(ValidationResult result)
        {
            IEnumerable<string> errors = result.Errors.Select(x => x.ErrorMessage).Distinct();
            string errorMessage = string.Join(", ", errors);
            return errorMessage;
        }

        private static Resource GetResource(Resource entity)
        {
            return new Resource()
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value,
                Comment = entity?.Comment ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Active = false,
            };
        }
    }
}
