using Microsoft.Extensions.Localization;

namespace LevelLearn.Resource
{
    public interface ISharedResource
    {
        public string GetValue(string resourceKey);
        public string GetValue(string resourceKey, params object[] arguments);


        #region Geral
        public string InternalServerError { get; }
        public string RegisteredSuccessfully { get; }
        public string UpdatedSuccessfully { get; }
        public string DeletedSuccessfully { get; }
        public string BadRequest { get; }
        public string NotFound { get; }
        #endregion

        #region Institution
        public string InstitutionNameRequired { get; }
        public string InstitutionDescriptionRequired { get; }
        public string InstitutionNameLength(params object[] arguments);
        public string InstitutionDescriptionLength(params object[] arguments);
        public string InstitutionFailedSave { get; }
        public string InstitutionNotFound { get; }
        public string InstitutionNotAllowed { get; }
        public string InstitutionAlreadyExists { get; } 
        #endregion

    }

    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        #region Geral
        public string InternalServerError => GetValue(nameof(InternalServerError));
        public string RegisteredSuccessfully => GetValue(nameof(RegisteredSuccessfully));
        public string UpdatedSuccessfully => GetValue(nameof(UpdatedSuccessfully));
        public string DeletedSuccessfully => GetValue(nameof(DeletedSuccessfully));
        public string BadRequest => GetValue(nameof(BadRequest));
        public string NotFound => GetValue(nameof(NotFound));
        #endregion

        #region Institution
        public string InstitutionFailedSave => GetValue(nameof(InstitutionFailedSave));
        public string InstitutionNotFound => GetValue(nameof(InstitutionNotFound));
        public string InstitutionNotAllowed => GetValue(nameof(InstitutionNotAllowed));
        public string InstitutionAlreadyExists => GetValue(nameof(InstitutionAlreadyExists));
        public string InstitutionNameRequired => GetValue(nameof(InstitutionNameRequired));
        public string InstitutionNameLength(params object[] arguments)
        {
            return GetValue(nameof(InstitutionNameLength), arguments);
        }
        public string InstitutionDescriptionRequired => GetValue(nameof(InstitutionDescriptionRequired));
        public string InstitutionDescriptionLength(params object[] arguments)
        {
            return GetValue(nameof(InstitutionDescriptionLength), arguments);
        }
        #endregion

        public string GetValue(string resourceKey)
        {
            return _localizer[resourceKey].Value;
        }

        public string GetValue(string resourceKey, params object[] arguments)
        {
            return _localizer[resourceKey, arguments].Value;
        }
        
    }

}
