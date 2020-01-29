using DataTransferObjects;
using FluentValidation;

namespace TaskList.Application.Services.Validators
{
    public class TaskValidator : AbstractValidator<TaskDto>
    {
        public TaskValidator()
        {
            RuleFor(t => t.Titulo)
                .NotNull()
                .WithMessage("O título da tarefa não pode ser nulo")
                .NotEmpty()
                .WithMessage("O título da tarefa não pode ser vazio")
                .MinimumLength(5)
                .WithMessage("O título deve ter no mínimo 5 caracteres")
                .MaximumLength(255)
                .WithMessage("O título deve ter no máximo 255 caracteres");

            RuleFor(t => t.Descricao)
                .MaximumLength(255)
                .WithMessage("A descrição deve ter no máximo 255 caracteres");

            RuleFor(t => t.Status)
                .NotNull()
                .WithMessage("O status não pode ser nulo");

            RuleFor(t => t.DataDeCriacao)
                .NotNull()
                .WithMessage("A data de criação não pode ser nula");
        }
    }
}