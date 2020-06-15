﻿using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.ValueObjects;
using System;

namespace LevelLearn.Domain.ValueObjects
{
    public class Celular : ValueObject
    {
        protected Celular() { }

        public Celular(string numero)
        {
            Numero = numero.GetNumbers();
        }

        public string Numero { get; private set; }

        public override bool EstaValido()
        {
            var validator = new CelularValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return ResultadoValidacao.IsValid;
        }

        public override string ToString()
        {
            int.TryParse(Numero, out int numeroConvertido);

            return numeroConvertido.ToString("+##(##)#####-####");
        }

    }
}
