﻿using System;
using System.Resources;

namespace LevelLearn.Resource.Resources
{
    public abstract class ResourceBase
    {
        protected readonly ResourceManager _resourceManager;

        public ResourceBase(Type type)
        {
            _resourceManager = new ResourceManager(type);
        }

        /// <summary>
        /// Obtém um recurso e faz as substituições nas mensagens
        /// </summary>
        /// <param name="resourceName">Nome do recurso a ser usado</param>
        /// <param name="replacements">Valores a ser usados para substituir na mensagem do recurso</param>
        protected string GetResource(string resourceName, params object[] replacements)
        {
            return string.Format(_resourceManager.GetString(resourceName), replacements);
        }
    }

}
