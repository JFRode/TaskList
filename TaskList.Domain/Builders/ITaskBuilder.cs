using System;

namespace TaskList.Domain.Builders
{
    public interface ITaskBuilder
    {
        void WithId(Guid id);

        void WithTitulo(string titulo);

        void WithStatus(bool status);

        void WithDescricao(string descricao);

        void WithDataDeCriacao(DateTime dataDeCriacao);

        void WithDataDeEdicao(DateTime dataDeEdicao);

        void WithDataDeExclusao(DateTime dataDeExclusao);

        void WithDataDeConclusao(DateTime dataDeConclusao);

        Task Build();
    }
}