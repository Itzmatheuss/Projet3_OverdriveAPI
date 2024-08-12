using FluentValidation;
using Projeto3_Over.Models;

namespace Projeto3_Over.Validators
{
    public class EmpresaValidator : AbstractValidator<EmpresaModel>
    {
        public EmpresaValidator()
        {
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O campo <strong>Nome</strong> é obrigatório !")
                .MinimumLength(3).WithMessage("O campo <strong>Nome</strong> deve ter no mínimo 3 caracteres !")
                .MaximumLength(100).WithMessage("O campo <strong>Nome</strong> deve ter no máximo 100 caracteres !");

            RuleFor(e => e.NomeFantasia)
                .NotEmpty().WithMessage("O campo <strong>Nome Fantasia</strong> é obrigatório !")
                .MinimumLength(3).WithMessage("O campo <strong>Nome Fantasia</strong> deve ter no mínimo 3 caracteres !")
                .MaximumLength(100).WithMessage("O campo <strong>Nome Fantasia</strong> deve ter no máximo 100 caracteres !");

            RuleFor(e => e.DataCadastro)
               .NotEmpty().WithMessage("O campo <strong>Data de Cadastro</strong> é obrigatório !")
               .Must(BeAValidDate).WithMessage("<strong>Data de Cadastro</strong> não pode ser uma data futura !");
               
            RuleFor(e => e.CNPJ)
                .NotEmpty().WithMessage("O campo <strong>CNPJ</strong> é obrigatório !")
                .IsValidCNPJ().WithMessage("<strong>CNPJ</strong> inserido inválido !");

            RuleFor(e => e.Cnae)
                .NotEmpty().WithMessage("O campo <strong>Cnae</strong> é obrigatório !")
                .MinimumLength(6).WithMessage("O campo <strong>Cnae</strong> deve ter no mínimo 6 caracteres !")
                .MaximumLength(6).WithMessage("O campo <strong>Cnae</strong> deve ter no máximo 6 caracteres !");

            RuleFor(e => e.NaturezaJuridica)
                .NotEmpty().WithMessage("O campo <strong>Natureza Juridica</strong> é obrigatório !")
                .MinimumLength(3).WithMessage("O campo <strong>Natureza Juridica</strong> deve ter no mínimo 3 caracteres !")
                .MaximumLength(100).WithMessage("O campo <strong>Natureza Juridica</strong> deve ter no máximo 100 caracteres !");

            RuleFor(e => e.Cep)
                .NotEmpty().WithMessage("O campo <strong>Cep</strong> é obrigatório !")
                .MinimumLength(8).WithMessage("O campo <strong>Cep</strong> deve ter no mínimo 8 !")
                .MaximumLength(8).WithMessage("O campo <strong>Cep</strong> deve ter no máximo 8 !");

            RuleFor(e => e.Cidade)
                .NotEmpty().WithMessage("O campo <strong>Cidade</strong> é obrigatório !")
                .MinimumLength(3).WithMessage("O campo <strong>Cidade</strong> deve ter no mínimo 3 caracteres !")
                .MaximumLength(100).WithMessage("O campo <strong>Cidade</strong> deve ter no máximo 100 caracteres !");

            RuleFor(e => e.Rua)
                .NotEmpty().WithMessage("O campo <strong>Rua</strong> é obrigatório !")
                .MinimumLength(3).WithMessage("O campo <strong>Rua</strong> deve ter no mínimo 3 caracteres !")
                .MaximumLength(100).WithMessage("O campo <strong>Rua</strong> deve ter no máximo 100 caracteres !");

            RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("O campo <strong>Bairro</strong> é obrigatório !")
                .MinimumLength(3).WithMessage("O campo <strong>Bairro</strong> deve ter no mínimo 3 caracteres !")
                .MaximumLength(100).WithMessage("O campo <strong>Bairro</strong> deve ter no máximo 100 caracteres !");

            RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("O campo <strong>Numero</strong> é obrigatório !")
                .MinimumLength(1).WithMessage("O campo <strong>Numero</strong> deve ter no mínimo 1 caracteres !")
                .MaximumLength(10).WithMessage("O campo <strong>Numero</strong> deve ter no máximo 100 caracteres !");

            RuleFor(e => e.Estado)
                .NotEmpty().WithMessage("O campo <strong>Estado</strong> é obrigatório !")
                .MinimumLength(2).WithMessage("O campo <strong>Estado</strong> deve ter no mínimo 2 caracteres !")
                .MaximumLength(2).WithMessage("O campo <strong>Estado</strong> deve ter no máximo 2 caracteres !");

            RuleFor(e => e.Complemento)
                .MaximumLength(100).WithMessage("O campo Complemento deve ter no máximo 200 caracteres !");

            RuleFor(e => e.Telefone)
                .NotEmpty().WithMessage("O campo <strong>Telefone</strong> é obrigatório !")
                .MinimumLength(11).WithMessage("O campo <strong>Telefone</strong> deve ter no mínimo 11 caracteres !")
                .MaximumLength(16).WithMessage("O campo <strong>Telefone</strong> deve ter no máximo 15 caracteres !");

            RuleFor(e => e.Capital)
                .NotEmpty().WithMessage("O campo <strong>Capital</strong> é obrigatório !")
                .InclusiveBetween(0, 999999999.99M).WithMessage("O campo <strong>Capital</strong> deve estar entre 0 e 999999999,99 !");

            RuleFor(e => e.Status)
                .NotNull().WithMessage("O campo <strong>Status</strong> é obrigatório !");

        }

        private bool BeAValidDate(DateTimeOffset? data)
        {
            return data.HasValue && data.Value <= DateTimeOffset.Now && data.Value != default(DateTimeOffset);
        }
    }
}
