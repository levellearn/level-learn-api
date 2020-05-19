using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace LevelLearn.WebApi.Resources
{
    public abstract class ResourceBase
    {
        protected readonly ResourceManager _resourceManager;

        public ResourceBase(Type type)
        {
            _resourceManager = new ResourceManager(type);
        }

        /// <summary>
        /// Builds up a string looking up a resource and doing the replacements.
        /// </summary>
        /// <param name="resourceStringName">Name of resource to use</param>
        /// <param name="replacements">Strings to use for replacing in the resource string</param>
        private string BuildStringFromResource(string resourceStringName, params object[] replacements)
        {
            return string.Format(_resourceManager.GetString(resourceStringName), replacements);
        }
    }
}
