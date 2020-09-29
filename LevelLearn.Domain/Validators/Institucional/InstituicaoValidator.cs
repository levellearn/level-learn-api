using FluentValidation;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Resource.Institucional;

namespace LevelLearn.Domain.Validators.Institucional
{
    public class InstituicaoValidator : AbstractValidator<Instituicao>
    {
        private readonly InstituicaoResource _resource;

        public InstituicaoValidator()
        {
            _resource = new InstituicaoResource();

            ValidarInstituicaoId();
            ValidarNome();
            ValidarSigla();
            ValidarDescricao();
            ValidarCNPJ();
            ValidarMunicipio();
            ValidarCEP();
            ValidarUF();
            ValidarNomePesquisa();
        }

        private void ValidarInstituicaoId()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                    .WithMessage(_resource.IdObrigatorio());
        }

        private void ValidarNome()
        {
            var tamanhoMin = RegraInsituicao.NOME_TAMANHO_MIN;
            var tamanhoMax = RegraInsituicao.NOME_TAMANHO_MAX;

            RuleFor(p => p.Nome)
                .NotEmpty()
                    .WithMessage(_resource.InstituicaoNomeObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.InstituicaoNomeTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarSigla()
        {
            var tamanhoMin = RegraInsituicao.SIGLA_TAMANHO_MIN;
            var tamanhoMax = RegraInsituicao.SIGLA_TAMANHO_MAX;

            RuleFor(p => p.Sigla)
                .NotEmpty()
                    .WithMessage(_resource.InstituicaoSiglaObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.InstituicaoSiglaTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarDescricao()
        {
            var tamanhoMax = RegraInsituicao.DESCRICAO_TAMANHO_MAX;

            RuleFor(p => p.Descricao)
                //.NotEmpty().WithMessage(_resource.InstituicaoDescricaoObrigatorio)
                .MaximumLength(tamanhoMax)
                    .WithMessage(_resource.InstituicaoDescricaoTamanho(tamanhoMax));
        }

        private void ValidarCNPJ()
        {
            RuleFor(c => c.Cnpj)
                .Must(c => ValidarNumeroCNPJ(c))
                    .WithMessage(_resource.InstituicaoCNPJInvalido)
                 .When(c => !string.IsNullOrWhiteSpace(c.Cnpj));
        }

        private bool ValidarNumeroCNPJ(string numero)
        {
            if (string.IsNullOrEmpty(numero)) return false;

            if (numero.Equals("00000000000000") ||
                    numero.Equals("11111111111111") ||
                    numero.Equals("22222222222222") ||
                    numero.Equals("33333333333333") ||
                    numero.Equals("44444444444444") ||
                    numero.Equals("55555555555555") ||
                    numero.Equals("66666666666666") ||
                    numero.Equals("77777777777777") ||
                    numero.Equals("88888888888888") ||
                    numero.Equals("99999999999999"))
                return false;

            string cnpj = numero;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();
            return cnpj.EndsWith(digito);
        }

        private void ValidarCEP()
        {
            var tamanho = RegraInsituicao.CEP_TAMANHO;

            RuleFor(p => p.Cep)
                .NotEmpty()
                    .WithMessage(_resource.InstituicaoCEPObrigatorio)
                .Length(tamanho)
                    .WithMessage(_resource.InstituicaoCEPTamanhoMaximo(tamanho));
        }

        private void ValidarMunicipio()
        {
            var tamanhoMin = RegraInsituicao.MUNICIPIO_TAMANHO_MIN;
            var tamanhoMax = RegraInsituicao.MUNICIPIO_TAMANHO_MAX;

            RuleFor(p => p.Municipio)
                .NotEmpty()
                    .WithMessage(_resource.InstituicaoMunicipioObrigatorio)
                .Length(tamanhoMin, tamanhoMax)
                    .WithMessage(_resource.InstituicaoMunicipioTamanho(tamanhoMin, tamanhoMax));
        }

        private void ValidarUF()
        {
            var tamanho = RegraInsituicao.UF_TAMANHO;

            RuleFor(p => p.UF)
                .NotEmpty()
                    .WithMessage(_resource.InstituicaoUFObrigatorio)
                .Length(tamanho)
                    .WithMessage(_resource.InstituicaoUFTamanhoMaximo(tamanho));
        }


        private void ValidarNomePesquisa()
        {
            RuleFor(p => p.NomePesquisa)
                .NotEmpty()
                    .WithMessage(_resource.NomePesquisaObrigatorio());
        }

    }
}
