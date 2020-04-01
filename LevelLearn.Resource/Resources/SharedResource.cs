using Microsoft.Extensions.Localization;

namespace LevelLearn.Resource
{
    public interface ISharedResource
    {
        string GetValue(string resourceKey);
    }

    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string GetValue(string resourceKey)
        {
            return _localizer[resourceKey].Value;
        }

    }

}
