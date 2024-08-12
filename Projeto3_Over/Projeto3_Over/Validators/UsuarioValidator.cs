using FluentValidation;
using Projeto3_Over.Models;

namespace Projeto3_Over.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioModel>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O campo <strong>Nome</strong> é obrigatório !")
                .MinimumLength(3).WithMessage("O campo <strong>Nome</strong> deve ter no mínimo 3 caracteres !")
                .MaximumLength(100).WithMessage("O campo <strong>Nome</strong> deve ter no máximo 100 caracteres !");

            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("O campo <strong>Nome de Usuário</strong> é obrigatório !")
                .MinimumLength(3).WithMessage("O campo <strong>Nome de Usuário</strong> deve ter no mínimo 3 caracteres !")
                .MaximumLength(100).WithMessage("O campo <strong>Nome de Usuário</strong> deve ter no máximo 100 caracteres !");

            RuleFor(u => u.CPF)
                 .NotEmpty().WithMessage("O campo <strong>CPF</strong> é obrigatório !")
                 .IsValidCPF().WithMessage("<strong>CPF</strong> inserido inválido !"); 

            RuleFor(u => u.Telefone)
                .NotEmpty().WithMessage("O campo <strong>Telefone</strong> é obrigatório !")
                .MinimumLength(11).WithMessage("O campo <strong>Telefone</strong> deve ter no mínimo 11 caracteres !")
                .MaximumLength(15).WithMessage("O campo <strong>Telefone</strong> deve ter no máximo 15 caracteres !");

            RuleFor(u => u.Status)
                .NotNull().WithMessage("O campo <strong>Status</strong> é obrigatório !");

        }
    }
}
