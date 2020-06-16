using System;
using System.Diagnostics;
using System.Resources;

namespace LevelLearn.Resource
{
    public abstract class ResourceBase
    {
        protected readonly ResourceManager _resourceManager;

        public ResourceBase(Type type)
        {
            _resourceManager = new ResourceManager(type);
        }

        public string IdObrigatorio() => ObterResource(nameof(IdObrigatorio));
        public string NomePesquisaObrigatorio() => ObterResource(nameof(NomePesquisaObrigatorio));

        /// <summary>
        /// Obtém um recurso e faz as substituições nas mensagens
        /// </summary>
        /// <param name="resourceName">Nome do recurso a ser usado</param>
        /// <param name="replacements">Valores a ser usados para substituir na mensagem do recurso</param>
        protected string ObterResource(string resourceName, params object[] replacements)
        {
            try
            {
                return string.Format(_resourceManager.GetString(resourceName), replacements);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return resourceName;
            }
        }

    }

}
