using Microsoft.Extensions.Localization;

namespace LevelLearn.Resource
{
    public interface ISharedResource
    {
        string GetValue(string resourceKey);

        #region Geral
        public string InternalServerError { get; }
        #endregion

        #region Institution
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
        #endregion

        #region Institution
        public string InstitutionFailedSave => GetValue(nameof(InstitutionFailedSave));
        public string InstitutionNotFound => GetValue(nameof(InstitutionNotFound));
        public string InstitutionNotAllowed => GetValue(nameof(InstitutionNotAllowed));
        public string InstitutionAlreadyExists => GetValue(nameof(InstitutionAlreadyExists)); 
        #endregion

        public string GetValue(string resourceKey)
        {
            return _localizer[resourceKey].Value;
        }

    }

}
